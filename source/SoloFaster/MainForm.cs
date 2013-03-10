using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Reflection;

using LumenWorks.Framework.IO.Csv;

namespace SoloFaster
{
    public partial class MainForm : Form
    {
        private Thread gpsFinderThread;
        private List<string> portScanList;

        private GPS gps;

        private bool recordingPoints = false;

        private List<RecordedSession> sessionList;

        private List<OpenedSession> openedSessions;

        private List<ProcessedLap> processedLaps;
        private ListViewItem selectedBasisLap = null;

        private DirectoryInfo saveDir;

        enum FlowMode
        {
            Collect = 1,
            Analyze = 2
        };

        private FlowMode flowMode;

        public MainForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            String baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            saveDir = new DirectoryInfo(baseDir + "\\SavedSessions");
            if (!saveDir.Exists)
                saveDir.Create();

            sessionList = new List<RecordedSession>();
            openedSessions = new List<OpenedSession>();
            processedLaps = new List<ProcessedLap>();

            flowMode = FlowMode.Collect;

            LoadSettings();

            collectMapBox.OnNeedRender += RenderCollectBox;
            analyzeOverviewMapBox.OnNeedRender += RenderOverviewBox;
            analyzeTape.XDataChanged += new AnalysisTape.XDataReturn(analyzeTape_XDataChanged);

            recordingSaved = true;
            clearRecordingButton_Click(null, null);
            ClearAllLineModes();
            UpdateOpenSessionsList();
            UpdateProcessedLapsList();

            portScanList = new List<string>();
            gpsFinderThread = new Thread(new ThreadStart(FindGPSThread));
            gpsFinderThread.Name = "GPS Finder";
            gpsFinderThread.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //kill GPS
            if (gps != null)
                gps.Close();

            //kill GPS finder thread
            if (gpsFinderThread != null && gpsFinderThread.IsAlive)
                gpsFinderThread.Abort();

            if (!recordingSaved)
            {
                if (MessageBox.Show("You have unsaved data recorded.  Would you like to save this before exiting?", "Save recorded data?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    saveRecordingButton_Click(null, null);
            }

            SaveSettings();
        }

        private Dictionary<RecordedSession, string> sessionFilenameLookup;

        private void LoadSettings()
        {
            //COM port list...
            gpsComPortMenuItem.DropDownItems.Clear();
            foreach (string portName in System.IO.Ports.SerialPort.GetPortNames())
            {
                ToolStripItem item = gpsComPortMenuItem.DropDownItems.Add(portName);
                item.Click += new EventHandler(ComPortSelectedClick);
            }

            //Load sessions from directories!
            sessionFilenameLookup = new Dictionary<RecordedSession, string>();
            foreach (DirectoryInfo eventDir in saveDir.GetDirectories())
            {
                foreach (FileInfo fi in eventDir.GetFiles("*.session"))
                {
                    try
                    {
                        RecordedSession session = RecordedSession.DeSerialize(File.ReadAllText(fi.FullName));
                        sessionFilenameLookup[session] = fi.FullName;
                        AddSession(session);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void SaveSettings()
        {
            //I'm sure something will go here eventually...
        }

        // ----------------------- RUN ANALYSIS ---------------------------

        private GPSPoint startLineStart, startLineEnd;
        private GPSPoint finishLineStart, finishLineEnd;

        private void analyzeSetStart_Click(object sender, EventArgs e)
        {
            if (analyzeSetStart.BackColor != Color.Transparent)
            {
                ClearAllLineModes();
                return;
            }

            analyzeOverviewMapBox.StartDraggingLine(StartLineCallback, Color.Green);
            analyzeSetStart.BackColor = Color.Green;

            analyzeSetFinish.Enabled = false;
            analyzeSetStartFinish.Enabled = false;
            analyzeAutoCalculateSF.Enabled = false;
        }

        private void StartLineCallback(GPSPoint start, GPSPoint end)
        {
            startLineStart = start;
            startLineEnd = end;
            analyzeOverviewMapBox.Invalidate();
            ClearAllLineModes();

            ReanalyzeSessionsForRuns();
        }

        private void analyzeSetFinish_Click(object sender, EventArgs e)
        {
            if (analyzeSetFinish.BackColor != Color.Transparent)
            {
                ClearAllLineModes();
                return;
            }

            analyzeOverviewMapBox.StartDraggingLine(FinishLineCallback, Color.Red);
            analyzeSetFinish.BackColor = Color.Red;

            analyzeSetStart.Enabled = false;
            analyzeSetStartFinish.Enabled = false;
            analyzeAutoCalculateSF.Enabled = false;
        }

        private void FinishLineCallback(GPSPoint start, GPSPoint end)
        {
            finishLineStart = start;
            finishLineEnd = end;
            analyzeOverviewMapBox.Invalidate();
            ClearAllLineModes();

            ReanalyzeSessionsForRuns();
        }

        private void analyzeSetStartFinish_Click(object sender, EventArgs e)
        {
            if (analyzeSetStartFinish.BackColor != Color.Transparent)
            {
                ClearAllLineModes();
                return;
            }

            analyzeOverviewMapBox.StartDraggingLine(StartFinishLineCallback, Color.Cyan);

            analyzeSetStartFinish.BackColor = Color.Cyan;
            analyzeSetStart.Enabled = false;
            analyzeSetFinish.Enabled = false;
            analyzeAutoCalculateSF.Enabled = false;
        }

        private void StartFinishLineCallback(GPSPoint start, GPSPoint end)
        {
            //This way it won't analyze twice on the start callback too
            finishLineStart = null;
            finishLineEnd = null;
            StartLineCallback(start, end);
            FinishLineCallback(start, end);
        }

        private void analyzeClearSF_Click(object sender, EventArgs e)
        {
            startLineStart = null;
            startLineEnd = null;
            finishLineStart = null;
            finishLineEnd = null;

            analyzeOverviewMapBox.Invalidate();
            ClearAllLineModes();
            ReanalyzeSessionsForRuns();
        }

        private void ClearAllLineModes()
        {
            analyzeSetStart.Enabled = analyzeSetFinish.Enabled = analyzeSetStartFinish.Enabled =
                analyzeAutoCalculateSF.Enabled = (openedSessions.Count > 0);

            analyzeClearSF.Enabled = (startLineStart != null || finishLineStart != null);

            analyzeSetStart.BackColor = Color.Transparent;
            analyzeSetFinish.BackColor = Color.Transparent;
            analyzeSetStartFinish.BackColor = Color.Transparent;

            analyzeOverviewMapBox.StopDraggingLine();
        }

        private void analyzeAutoCalculateSF_Click(object sender, EventArgs e)
        {
            AutoCalcDialog dialog = new AutoCalcDialog(openedSessions);
            if (dialog.RunCalculation())
            {
                //If we calculated new ones, set them as the new lines!
                if (dialog.bestStart != null)
                {
                    startLineStart = dialog.bestStart[0];
                    startLineEnd = dialog.bestStart[1];
                }
                if (dialog.bestFinish != null)
                {
                    finishLineStart = dialog.bestFinish[0];
                    finishLineEnd = dialog.bestFinish[1];
                }

                analyzeOverviewMapBox.Invalidate();
                ClearAllLineModes();
                ReanalyzeSessionsForRuns();
            }
        }

        private void ReanalyzeSessionsForRuns()
        {
            processedLaps.Clear();

            if (startLineStart != null && finishLineStart != null)
            {
                //Chop up laps by start/finish
                foreach (OpenedSession session in openedSessions)
                {
                    int lapCount = 0;
                    ProcessedLap lap = null;
                    bool inLap = false;
                    GPSPoint lastPoint = null;

                    for (int i = 0; i < session.session.gpsPoints.Count; i++)
                    {
                        GPSPoint newPoint = session.session.gpsPoints[i];
                        if (lastPoint != null)
                        {
                            //check to see if we've crossed a line
                            GPSPoint startIntersect = MathUtil.LineIntersection(lastPoint, newPoint, startLineStart, startLineEnd);
                            GPSPoint finishIntersect = MathUtil.LineIntersection(lastPoint, newPoint, finishLineStart, finishLineEnd);

                            if (finishIntersect != null)
                            {
                                //end lap!
                                if (inLap)
                                {
                                    inLap = false;
                                    lap.lapPoints.Add(finishIntersect);
                                    lap.CalculateDistances();
                                    processedLaps.Add(lap);

                                    lapCount++;
                                }
                            }

                            if (startIntersect != null)
                            {
                                //start lap!
                                if (!inLap)
                                {
                                    inLap = true;
                                    lap = new ProcessedLap();
                                    lap.session = session;
                                    lap.lapNum = lapCount;
                                    lap.lapPoints.Add(startIntersect);

                                    //Find an unused color for the new lap
                                    foreach (Color testColor in brightColors)
                                    {
                                        bool found = false;
                                        foreach (ProcessedLap tLap in processedLaps)
                                        {
                                            if (tLap.color == testColor)
                                                found = true;
                                        }
                                        if (!found)
                                        {
                                            lap.color = testColor;
                                            break;
                                        }
                                    }

                                    if (lap.color == null) //More than 8? Bleh. White.
                                        lap.color = Color.White;
                                }
                            }
                        }
                        lastPoint = newPoint;

                        if (inLap)
                        {
                            lap.lapPoints.Add(newPoint);
                        }
                    }
                }

                //Normalize runs
                float maxDist = 0;
                foreach (ProcessedLap lap in processedLaps)
                {
                    if (maxDist < lap.totalDistance)
                        maxDist = lap.totalDistance;
                }
                foreach (ProcessedLap lap in processedLaps)
                {
                    float normalizationFactor = (maxDist / lap.totalDistance);
                    for (int i = 0; i < lap.lapPoints.Count; i++)
                        lap.distances[i] *= normalizationFactor;
                    lap.totalDistance = maxDist;
                }
            }

            //Sort laps by timestamp
            processedLaps.Sort(new ProcessedLap.ProcessedLapComparer());

            //Populate the lap list
            UpdateProcessedLapsList();
        }

        private void UpdateProcessedLapsList()
        {
            object selectedItem = null;
            if (availableLapsListview.SelectedItems.Count > 0)
                selectedItem = availableLapsListview.SelectedItems[0].Tag;

            availableLapsListview.Items.Clear();

            foreach (ProcessedLap lap in processedLaps)
            {
                ListViewItem item = new ListViewItem(new string[] {
                    lap.GetName(),
                    ((int)lap.totalTime.Minutes) + ":" + lap.totalTime.Seconds + "." + lap.totalTime.Milliseconds.ToString("D3")
                    });
                item.Tag = lap;
                item.BackColor = lap.color;
                item.Checked = lap.enabled;
                item.ToolTipText = lap.GetName();
                lap.listViewRow = item;
                availableLapsListview.Items.Add(item);

                if (lap == selectedItem)
                    item.Selected = true;
            }

            analyzeSplitter.Panel2Collapsed = !(processedLaps.Count > 0);

            if (processedLaps.Count > 0 && availableLapsListview.SelectedItems.Count < 1)
                FindNewBasisLap();

            analyzeTape.SetLaps(processedLaps);
        }

        private void FindNewBasisLap()
        {
            selectedBasisLap = null;
            foreach (ProcessedLap lap in processedLaps)
            {
                if (lap.enabled)
                {
                    lap.listViewRow.Selected = true;
                    selectedBasisLap = lap.listViewRow;
                    return;
                }
            }
        }

        private void analyzeTape_XDataChanged(int newX, float newDistance)
        {
            //Nuked column in table, since we display it on the charts now...
            //for (int i = 0; i < processedLaps.Count; i++)
            //processedLaps[i].listViewRow.SubItems[2].Text = Math.Round(analyzeTape.processedMPH[i][newX], 1).ToString();

            //Force map to update
            analyzeOverviewMapBox.Invalidate();
        }

        private void availableLapsListview_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ProcessedLap lap = (ProcessedLap)e.Item.Tag;
            lap.enabled = e.Item.Checked;

            if (selectedBasisLap == e.Item)
                FindNewBasisLap();

            analyzeTape.ProcessCurves();
            analyzeTape.Invalidate();
            analyzeOverviewMapBox.Invalidate();
        }

        private void availableLapsListview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (availableLapsListview.SelectedItems.Count == 0)
                return;

            if (availableLapsListview.SelectedItems[0].Checked)
            {
                ProcessedLap lap = (ProcessedLap)availableLapsListview.SelectedItems[0].Tag;
                selectedBasisLap = lap.listViewRow;
                analyzeTape.SetBasisLap(lap);

                foreach (ProcessedLap lapp in processedLaps)
                    lapp.listViewRow.ForeColor = Color.Black;

                lap.listViewRow.ForeColor = Color.DarkSalmon;
            }
            else
            {
                availableLapsListview.SelectedItems.Clear();
                selectedBasisLap.Selected = true;
            }
        }


        // ----------------------- SESSION MANAGEMENT ----------------------

        private void AddSession(RecordedSession session)
        {
            //Populate event/driver combos
            if (!driverCombo.Items.Contains(session.driverName))
                driverCombo.Items.Add(session.driverName);
            if (!eventCombo.Items.Contains(session.eventName))
                eventCombo.Items.Add(session.eventName);

            sessionList.Add(session);
        }

        private static Color[] brightColors = { Color.Cyan, Color.Lime, Color.Magenta, Color.Red, Color.LightBlue, Color.LemonChiffon, Color.Coral };
        private void OpenSession(RecordedSession session)
        {
            foreach (OpenedSession tSession in openedSessions)
            {
                if (tSession.session == session)
                {
                    MessageBox.Show("This session is already open!");
                    return;
                }
            }

            OpenedSession aSession = new OpenedSession(session);

            //Find an unused color for the new session
            foreach (Color testColor in brightColors)
            {
                bool found = false;
                foreach (OpenedSession tSession in openedSessions)
                {
                    if (tSession.color == testColor)
                        found = true;
                }
                if (!found)
                {
                    aSession.color = testColor;
                    break;
                }
            }

            if (aSession.color == null) //More than 8? Really? Screw it, gray.
                aSession.color = Color.Gray;

            openedSessions.Add(aSession);

            UpdateOpenSessionsList();
            ReanalyzeSessionsForRuns();
        }

        private void UpdateOpenSessionsList()
        {
            openSessionsListbox.Items.Clear();

            foreach (OpenedSession session in openedSessions)
                openSessionsListbox.Items.Add(session);

            analyzeCloseSession.Enabled = (openSessionsListbox.SelectedItem != null);

            //Recenter overview box!
            List<GPSPoint> pointsToShow = new List<GPSPoint>();
            foreach (OpenedSession session in openedSessions)
                pointsToShow.AddRange(session.session.gpsPoints);
            analyzeOverviewMapBox.Recenter(pointsToShow);

            //Now rerender!
            analyzeOverviewMapBox.Invalidate();

            ClearAllLineModes();
        }


        // ----------------------- UI LAYER CODE -------------------------

        private void modeTab_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == pageAnalyze)
            {
                flowMode = FlowMode.Analyze;
            }
            else if (e.TabPage == pageCollect)
            {
                flowMode = FlowMode.Collect;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            recordingPoints ^= true;
            if (recordingPoints)
            {
                recordButton.Text = "Stop Recording";
            }
            else
            {
                recordButton.Text = "Start Recording";
            }
        }

        private void clearRecordingButton_Click(object sender, EventArgs e)
        {
            if (!recordingSaved)
            {
                if (MessageBox.Show("There are unsaved recorded points.  Are you sure you want to clear all recorded points?", "Clear All Recorded Points?", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                    return;
            }

            recordedPointList = new List<GPSPoint>();
            recordingStatusText.Text = "No Points Recorded";
            recordingSaved = true;
            clearRecordingButton.Enabled = saveSessionAndAnalyzeButton.Enabled = saveSessionButton.Enabled = !recordingSaved;
        }

        private void analyzeOpenSession_Click(object sender, EventArgs e)
        {
            OpenSessionDialog dialog = new OpenSessionDialog(sessionFilenameLookup, sessionList);
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                foreach (RecordedSession session in dialog.toOpen)
                    OpenSession(session);
            }
        }

        private void openSessionsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            analyzeCloseSession.Enabled = (openSessionsListbox.SelectedItem != null);
        }

        private void analyzeCloseSession_Click(object sender, EventArgs e)
        {
            OpenedSession session = (OpenedSession)openSessionsListbox.SelectedItem;
            openedSessions.Remove(session);

            UpdateOpenSessionsList();
            ReanalyzeSessionsForRuns();
        }

        private void saveSessionAndAnalyzeButton_Click(object sender, EventArgs e)
        {
            RecordedSession session = SaveSession();
            if (session == null)
                return;

            modeTab.SelectedTab = pageAnalyze;
            OpenSession(session);
        }

        private void saveRecordingButton_Click(object sender, EventArgs e)
        {
            SaveSession();

            MessageBox.Show("Session saved!");
        }

        private RecordedSession SaveSession()
        {
            if (recordedPointList.Count == 0)
            {
                MessageBox.Show("No points recorded!  Nothing to save!");
                return null;
            }

            RecordedSession session = new RecordedSession();
            session.eventName = eventCombo.Text;
            session.driverName = driverCombo.Text;
            session.notes = notesBox.Text;
            session.gpsPoints = recordedPointList;

            AddSession(session);

            //Serialize session and save to file...
            string serializedSession = session.Serialize();
            DirectoryInfo eventDir = saveDir.CreateSubdirectory(session.GetEventDirName());
            File.WriteAllText(eventDir.FullName + "\\" + session.GetFileName() + ".session", serializedSession);

            //Clear everything to get ready for a new session...
            notesBox.Text = "";
            recordingSaved = true;
            clearRecordingButton_Click(null, null);

            return session;
        }

        private void openSessionsListbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= openSessionsListbox.Items.Count || e.Index < 0)
                return;

            OpenedSession session = (OpenedSession)openSessionsListbox.Items[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(session.color), e.Bounds);

            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(session.session.ToString(), e.Font, textBrush, e.Bounds.Location);
            }
        }

        private void availableLapsListbox_DrawItem(object sender, DrawItemEventArgs e)
        {
            //AnalyzedRun run = (AnalyzedRun)openSessionsListbox.Items[e.Index];
            //e.Graphics.FillRectangle(new SolidBrush(run.color), e.Bounds);

            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString("test", e.Font, textBrush, e.Bounds.Location);
            }
        }


        // --------------------- GPS PORT MANAGEMENT -------------------------

        private void ComPortSelectedClick(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;

            if (gps != null)
            {
                gps.Close();
                gps = null;
            }

            lock (portScanList)
            {
                portScanList.Clear();
                portScanList.Add(item.Text);
            }
        }

        private void StartGPSAutoFind()
        {
            lock (portScanList)
            {
                portScanList.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            }
        }

        private void SetGPS(object portName)
        {
            gpsStatus.Text = "Connecting to " + portName + "...";
            Application.DoEvents();

            if (gps != null)
            {
                gps.Close();
                gps = null;
            }

            gps = new GPS((string)portName, 57600);
            gps.positionUpdate += GPSPositionUpdate;
            gps.satelliteUpdate += SatUpdateCallback;

            if (!gps.Open())
            {
                //failed to connect
                gpsStatus.Text = "No GPS found on " + portName;
            }
            else
            {
                gpsStatus.Text = "Connected to " + portName;
            }
        }

        private void FindGPSThread()
        {
            try
            {
                while (true)
                {
                    if (gps == null)
                    {
                        lock (portScanList)
                        {
                            if (portScanList.Count == 0)
                                portScanList.AddRange(System.IO.Ports.SerialPort.GetPortNames());

                            if (portScanList.Count > 0)
                            {
                                string portName = portScanList.First();
                                portScanList.RemoveAt(0);

                                this.Invoke(new ThreadStart(delegate()
                                {
                                    gpsStatus.Text = "Trying " + portName + "...";
                                }));

                                if (TestGPSPort(portName))
                                {
                                    this.Invoke(new ParameterizedThreadStart(SetGPS), new object[] { portName });
                                    portScanList.Clear();
                                }
                            }
                        }
                    }

                    Thread.Sleep(100);
                }
            }
            catch (ThreadAbortException)
            {
            }
        }

        private bool TestGPSPort(string portName)
        {
            SerialPort port = new SerialPort(portName);
            try
            {
                port.Open();
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

            if (!port.IsOpen)
                return false;

            port.WriteTimeout = 100;
            try
            {
                port.WriteLine("");

                port.NewLine = "\r\n";
                port.ReadTimeout = 3000;

                string testLine = port.ReadLine();
                Console.WriteLine("Read: " + testLine.Length + ": " + testLine);
                if (testLine.Contains('$') || testLine.Contains('*'))
                {
                    port.Close();
                    return true;
                }
            }
            catch (TimeoutException)
            {
            }
            port.Close();
            return false;
        }


        // ------------------- SATELLITE UPDATING CODE ---------------------

        private void SatUpdateCallback()
        {
            this.Invoke(new ThreadStart(SatUpdate));
        }

        private void SatUpdate()
        {
            satPicToolbar.Width = 12 * gps.satellites.Count;

            Bitmap renderMap = new Bitmap(satPicToolbar.Width, statusStrip.Height);
            Graphics g = Graphics.FromImage(renderMap);
            g.Clear(Color.Black);

            if (gps.satellites.Count > 0)
            {
                Font f = new Font(FontFamily.GenericMonospace, 6);
                Brush whiteBrush = new SolidBrush(Color.White);
                Brush redBrush = new SolidBrush(Color.Red);
                Brush blueBrush = new SolidBrush(Color.Blue);

                for (int i = 0; i < gps.satellites.Count; i++)
                {
                    GPSSatellite sat = gps.satellites[i];

                    int barHeight = (int)Math.Round((float)renderMap.Height * sat.signalStrength / 100.0f);

                    Brush brushUse = gps.satellitesUsed.ContainsKey(sat.num) ? blueBrush : redBrush;
                    g.FillRectangle(brushUse, i * 12 + 1, renderMap.Height - barHeight, 10, barHeight);

                    g.DrawString(sat.signalStrength.ToString(), f, whiteBrush, i * 12, -1);
                }
            }

            satPicToolbar.ImageScaling = ToolStripItemImageScaling.None;
            satPicToolbar.Image = renderMap;
            g.Dispose();
        }


        // ----------------------- RIGHT WINDOW RENDERING CODE ---------------------

        private void RenderCollectBox(Graphics g, MapRenderBox box)
        {
            lock (recordedPointList)
            {
                //Recenter box no matter what
                List<GPSPoint> pointsToShow = new List<GPSPoint>();
                if (lastPoint != null)
                    pointsToShow.Add(lastPoint);
                if (recordedPointList != null)
                    pointsToShow.AddRange(recordedPointList);
                box.Recenter(pointsToShow);

                //Render anything useful
                if (pointsToShow.Count > 0)
                {
                    //Draw recorded point list
                    if (recordedPointList != null && recordedPointList.Count > 1)
                    {
                        Pen whitePen = new Pen(Color.White);

                        List<Point> linePoints = new List<Point>();
                        foreach (GPSPoint point in recordedPointList)
                            linePoints.Add(box.ConvertPoint(point));
                        g.DrawLines(whitePen, linePoints.ToArray());
                    }

                    //Draw last point
                    if (lastPoint != null)
                    {
                        Pen greenPen = new Pen(Color.Green);
                        {
                            Point ptCenter = box.ConvertPoint(lastPoint);
                            g.DrawEllipse(greenPen, ptCenter.X - 1, ptCenter.Y - 1, 3, 3);
                        }
                    }
                }
            }
        }

        private void RenderOverviewBox(Graphics g, MapRenderBox box)
        {
            //Render each session, in dark versions if laps are open
            foreach (OpenedSession session in openedSessions)
            {
                Pen drawPen;
                if (processedLaps.Count > 0)
                    drawPen = new Pen(Color.FromArgb(session.color.R / 3, session.color.G / 3, session.color.B / 3));
                else
                    drawPen = new Pen(session.color, 1);

                //Draw linemap
                List<Point> linePoints = new List<Point>();
                foreach (GPSPoint point in session.session.gpsPoints)
                    linePoints.Add(box.ConvertPoint(point));
                g.DrawLines(drawPen, linePoints.ToArray());
            }

            //Draw laps, if any, in full color
            foreach (ProcessedLap lap in processedLaps)
            {
                if (!lap.enabled) continue;

                Pen drawPen = new Pen(lap.color, 1);

                //Draw linemap
                List<Point> linePoints = new List<Point>();
                foreach (GPSPoint point in lap.lapPoints)
                    linePoints.Add(box.ConvertPoint(point));
                g.DrawLines(drawPen, linePoints.ToArray());
            }

            //Start/finish lines
            if (startLineStart != null || finishLineStart != null)
            {
                if (startLineStart != null && finishLineStart != null && startLineEnd != null && finishLineEnd != null &&
                    startLineStart.Equals(finishLineStart) && startLineEnd.Equals(finishLineEnd))
                {
                    g.DrawLine(new Pen(Color.Cyan, 2), box.ConvertPoint(startLineStart), box.ConvertPoint(startLineEnd));
                }
                else
                {
                    if (startLineStart != null)
                        g.DrawLine(new Pen(Color.Green, 2), box.ConvertPoint(startLineStart), box.ConvertPoint(startLineEnd));

                    if (finishLineStart != null)
                        g.DrawLine(new Pen(Color.Red, 2), box.ConvertPoint(finishLineStart), box.ConvertPoint(finishLineEnd));
                }
            }

            //Draw overview positions
            if (analyzeTape.distanceSelected > 0)
            {
                foreach (ProcessedLap lap in processedLaps)
                {
                    if (!lap.enabled) continue;

                    Pen selectedPen = new Pen(lap.color);
                    for (int i = 0; i < lap.lapPoints.Count - 1; i++)
                    {
                        if (lap.distances[i] <= analyzeTape.distanceSelected && lap.distances[i + 1] >= analyzeTape.distanceSelected)
                        {
                            float t = (analyzeTape.distanceSelected - lap.distances[i]) / (lap.distances[i + 1] - lap.distances[i]);

                            GPSPoint nPoint = new GPSPoint();
                            nPoint.lat = t * lap.lapPoints[i + 1].lat + (1 - t) * lap.lapPoints[i].lat;
                            nPoint.lon = t * lap.lapPoints[i + 1].lon + (1 - t) * lap.lapPoints[i].lon;
                            Point renderPoint = box.ConvertPoint(nPoint);

                            g.DrawEllipse(selectedPen, renderPoint.X - 4, renderPoint.Y - 4, 9, 9);
                            g.DrawLine(selectedPen, renderPoint.X - 2.82f, renderPoint.Y - 2.82f, renderPoint.X + 2.82f, renderPoint.Y + 2.82f);
                            g.DrawLine(selectedPen, renderPoint.X + 2.82f, renderPoint.Y - 2.82f, renderPoint.X - 2.82f, renderPoint.Y + 2.82f);
                            break;
                        }
                    }
                }
            }
        }


        // ----------------------- POINT RECORDING CODE ----------------------

        private List<GPSPoint> recordedPointList;
        private GPSPoint lastPoint;
        private bool recordingSaved;

        private double lastGPSTime = -1;
        private void GPSPositionUpdate()
        {
            if (gps.time != null)
            {
                double newTime = double.Parse(gps.time);
                float s = (float)(newTime - lastGPSTime);
                if (s > 0)
                {
                    float hz = 1.0f / s;
                    this.Invoke(new ThreadStart(delegate()
                    {
                        gpsStatus.Text = "Connected to " + gps.portName + ": " + Math.Round(hz, 1) + " hz";
                    }));
                }
                lastGPSTime = newTime;

                if (gps.fixActive)
                {
                    GPSPoint nPoint = new GPSPoint();
                    nPoint.lat = gps.latitude;
                    nPoint.lon = gps.longitude;
                    DateTime checkTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(gps.time.Substring(0, 2)), int.Parse(gps.time.Substring(2, 2)), int.Parse(gps.time.Substring(4, 2)), (int)(1000 * float.Parse(gps.time.Substring(6))), DateTimeKind.Utc).ToLocalTime();
                    nPoint.time = checkTime;
                    nPoint.velocity = gps.velocityMPH;
                    nPoint.altitude = gps.altitudeFeet;

                    if (autoRecordBox.Checked)
                    {
                        //Figure out if we need to turn it on or off...
                        if ((!recordingPoints && nPoint.velocity >= (float)autoRecordMPH.Value) ||
                            (recordingPoints && nPoint.velocity < (float)autoRecordMPH.Value))
                            this.Invoke(new ThreadStart(delegate()
                            {
                                recordButton_Click(null, null);
                            }));
                    }

                    lock (recordedPointList)
                    {
                        if (recordingPoints)
                        {
                            recordedPointList.Add(nPoint);
                            recordingSaved = false;
                        }

                        lastPoint = nPoint;
                    }

                    this.Invoke(new ThreadStart(delegate()
                    {
                        clearRecordingButton.Enabled = saveSessionAndAnalyzeButton.Enabled = saveSessionButton.Enabled = !recordingSaved;

                        lock (recordedPointList)
                        {
                            if (recordedPointList.Count > 0)
                                recordingStatusText.Text = recordedPointList.Count + " Points (" + Math.Round((recordedPointList.Last().time - recordedPointList.First().time).TotalSeconds, 3) + " s)";
                        }

                        gpsInfoDump.Text = nPoint.time + ": " + Math.Round(nPoint.lat, 6) + ", " + Math.Round(nPoint.lon, 6) + " @ " + Math.Round(nPoint.altitude, 2) + " ft, " + nPoint.velocity + " mph";
                    }));
                }
            }

            if (flowMode == FlowMode.Collect)
                collectMapBox.Invalidate();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string fileName in openFileDialog1.FileNames)
                {
                    FileInfo fi = new FileInfo(fileName);
                    if (!fi.Exists)
                        continue;

                    bool loaded = false;
                    if (fi.Extension == ".csv")
                    {
                        string[] lines = File.ReadAllLines(fileName);
                        
                        if (lines.Length < 10)
                            continue;

                        //Traqmate?
                        string[] line0 = lines[0].Split(',');
                        if (line0.Length >= 2 && line0[1] == " Traqmate Trackvision")
                        {
                            //Traqmate!

                            //Make new CSV out of only the data lines..
                            StringBuilder collectedString = new StringBuilder();
                            bool found = false;
                            Dictionary<string, string> lookupList = new Dictionary<string,string>();

                            for (int i=0; i<lines.Length; i++)
                            {
                                if (!found)
                                {
                                    string[] linesplit = lines[i].Split(',');
                                    if (linesplit[0] == "GPS Reading")
                                        found = true;
                                    else if (linesplit.Length > 1)
                                        lookupList[linesplit[0]] = linesplit[1];
                                }

                                if (found)
                                    collectedString.AppendLine(lines[i]);
                            }

                            
                            TextReader tr = new StreamReader(new MemoryStream(new System.Text.UTF8Encoding().GetBytes(collectedString.ToString())));
                            CsvReader reader = new CsvReader(tr, true);

                            RecordedSession newSession = new RecordedSession();
                            newSession.driverName = lookupList["Driver"];
                            newSession.eventName = lookupList["Track"];
                            newSession.notes = "Vehicle: " + lookupList["Vehicle"];

                            DateTime timeBase = DateTime.Parse(lookupList["Starting Date"] + " " + lookupList["Starting Time"]);
                            while (reader.ReadNextRecord())
                            {
                                GPSPoint point = new GPSPoint();
                                float timeAdd = float.Parse(reader["Elapsed Time"]);
                                point.time = timeBase.AddSeconds(timeAdd);
                                point.altitude = float.Parse(reader["Altitude (feet)"]);
                                point.velocity = float.Parse(reader["Velocity (MPH)"]);

                                //there's also "feet" we could use to increase precision, but let's try this for now...
                                point.lat = double.Parse(reader["Lat (Degrees)"]);
                                point.lon = double.Parse(reader["Lon (Degrees)"]);

                                newSession.gpsPoints.Add(point);
                            }

                            //Save and add to store
                            AddSession(newSession);

                            //Serialize session and save to file...
                            string serializedSession = newSession.Serialize();
                            DirectoryInfo eventDir = saveDir.CreateSubdirectory(newSession.GetEventDirName());
                            File.WriteAllText(eventDir.FullName + "\\" + newSession.GetFileName() + ".session", serializedSession);

                            //Open for analysis
                            OpenSession(newSession);

                            loaded = true;
                        }
                    }

                    if (!loaded)
                        MessageBox.Show("Error: File format not known for file:\n" + fi.FullName);
                }

                //Analyze
                modeTab.SelectedTab = pageAnalyze;
            }
        }
    }
}
