using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoloFaster
{
    public partial class AnalysisTape : UserControl
    {
        private List<ProcessedLap> laps;

        public List<float[]> processedMPH, processedTime;
        private float highestMph, lowestMph;
        private float highestOffset, lowestOffset;

        public int xClicked;
        public float distanceSelected;

        private int basisLap;

        public delegate void XDataReturn(int newX, float newDistance);
        public event XDataReturn XDataChanged;

        public AnalysisTape()
        {
            InitializeComponent();

            distanceSelected = 0;
            xClicked = 0;
            basisLap = 0;

            this.Resize += new EventHandler(AnalysisTape_Resize);

            this.velocityTape.Paint += new PaintEventHandler(VelocityPaint);
            this.velocityTape.MouseDown += new MouseEventHandler(velocityTape_MouseDown);
            this.deltaTape.Paint += new PaintEventHandler(DeltaPaint);
            this.deltaTape.MouseDown += new MouseEventHandler(velocityTape_MouseDown);
        }

        private void velocityTape_MouseDown(object sender, MouseEventArgs e)
        {
            xClicked = e.X;

            UpdateXOutput();
        }

        private void UpdateXOutput()
        {
            if (laps != null && laps.Count > 0)
            {
                float distanceIncrement = laps.First().totalDistance / (float)this.Width;
                distanceSelected = distanceIncrement * xClicked;
            }

            this.deltaTape.Invalidate();
            this.velocityTape.Invalidate();
            if (XDataChanged != null)
                XDataChanged.Invoke(xClicked, distanceSelected);
        }

        private void VelocityPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.Clear(Color.Black);

            if (laps == null || laps.Count == 0)
                return;

            const float mphBorder = 2.0f;
            float vertInc = (float)this.velocityTape.Height / (highestMph - lowestMph + 2 * mphBorder);

            //show grid lines
            Pen gridPen = new Pen(Color.Gray, 1);
            Font gridFont = DefaultFont;
            Brush gridBrush = new SolidBrush(Color.Gray);

            float mph = lowestMph;
            while (mph <= highestMph)
            {
                int yPos = (int)Math.Round(this.velocityTape.Height - (mph - lowestMph + mphBorder) * vertInc);
                e.Graphics.DrawLine(gridPen, 0, yPos, this.Width, yPos);
                e.Graphics.DrawString(Math.Round(mph, 1).ToString(), gridFont, gridBrush, 0, yPos - gridFont.Height);

                if (mph == lowestMph)
                {
                    mph = (float)Math.Ceiling(lowestMph / 10.0f) * 10.0f;
                    if (mph == lowestMph)
                        mph += 10.0f;
                }
                else if (mph < highestMph)
                {
                    mph += 10.0f;
                    if (mph > highestMph)
                        mph = highestMph;
                }
                else if (mph == highestMph)
                    break;
            }

            //Draw lines
            for (int i = 0; i < laps.Count; i++)
            {
                ProcessedLap lap = laps[i];
                if (!lap.enabled) continue;

                Point[] points = new Point[this.Width];
                for (int x = 0; x < this.Width; x++)
                    points[x] = new Point(x, (int)Math.Round(this.velocityTape.Height - (processedMPH[i][x] - lowestMph + mphBorder) * vertInc));

                e.Graphics.DrawLines(new Pen(lap.color, 1), points);
            }

            //show X line
            e.Graphics.DrawLine(new Pen(Color.White, 1), xClicked, 0, xClicked, velocityTape.Height);

            //Draw points on the X line
            for (int i = 0; i < laps.Count; i++)
            {
                ProcessedLap lap = laps[i];
                if (!lap.enabled) continue;

                int yPos = (int)Math.Round(this.velocityTape.Height - (processedMPH[i][xClicked] - lowestMph + mphBorder) * vertInc);
                e.Graphics.DrawString(Math.Round(processedMPH[i][xClicked], 1).ToString(), gridFont, new SolidBrush(lap.color), xClicked, yPos - gridFont.Height);
            }
        }

        private void DeltaPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.Clear(Color.Black);

            if (laps == null || laps.Count < 2)
                return;

            float vertInc = (float)this.deltaTape.Height / (highestOffset - lowestOffset);
            float offsetBase = deltaTape.Height - (-lowestOffset * vertInc);

            for (int i = 0; i < laps.Count; i++)
            {
                ProcessedLap lap = laps[i];
                if (!lap.enabled) continue;

                Point[] points = new Point[this.Width];
                for (int x = 0; x < this.Width; x++)
                    points[x] = new Point(x, (int)Math.Round(offsetBase - (processedTime[i][x] - processedTime[basisLap][x]) * vertInc));

                e.Graphics.DrawLines(new Pen(laps[i].color, 1), points);
            }

            //draw zero line
            e.Graphics.DrawLine(new Pen(Color.Gray, 1), 0, offsetBase, this.Width, offsetBase);
            e.Graphics.DrawString("0", DefaultFont, new SolidBrush(Color.Gray), 0, offsetBase);
            e.Graphics.DrawString(Math.Round(highestOffset, 3).ToString(), DefaultFont, new SolidBrush(Color.Gray), 0, 0);
            e.Graphics.DrawString(Math.Round(lowestOffset, 3).ToString(), DefaultFont, new SolidBrush(Color.Gray), 0, deltaTape.Height - DefaultFont.Height);

            //show X line
            e.Graphics.DrawLine(new Pen(Color.White, 1), xClicked, 0, xClicked, deltaTape.Height);

            //Draw points on the X line
            for (int i = 0; i < laps.Count; i++)
            {
                ProcessedLap lap = laps[i];
                if (!lap.enabled) continue;

                int yPos = (int)Math.Round(offsetBase - (processedTime[i][xClicked] - processedTime[basisLap][xClicked]) * vertInc);
                e.Graphics.DrawString(Math.Round(processedTime[i][xClicked] - processedTime[basisLap][xClicked], 3).ToString(), DefaultFont, new SolidBrush(lap.color), xClicked, yPos - DefaultFont.Height);
            }
        }

        private void AnalysisTape_Resize(object sender, EventArgs e)
        {
            ProcessCurves();
            this.Invalidate();
            UpdateXOutput();
        }

        public void ProcessCurves()
        {
            if (laps == null || laps.Count < 1)
                return;

            if (laps.Count > 0 && basisLap >= laps.Count)
                basisLap = laps.Count - 1;

            float distanceIncrement = laps.First().totalDistance / (float)this.Width;
            lowestMph = 200;
            highestMph = 1;

            //calculate MPH curves
            int enabledLaps = 0;
            processedMPH = new List<float[]>();
            processedTime = new List<float[]>();
            foreach (ProcessedLap lap in laps)
            {
                if (lap.enabled)
                    enabledLaps++;

                float[] mphS = new float[this.Width];
                float[] timeS = new float[this.Width];

                float distance = 0;
                int lastIndex = 0;
                DateTime baseTime = lap.lapPoints[basisLap].time;

                for (int i = 0; i < this.Width; i++)
                {
                    //Find the distance point at which we cross this pixel distance
                    while (lastIndex < lap.lapPoints.Count && lap.distances[lastIndex] < distance)
                        lastIndex++;
                    //Then go one back to get the one before it
                    lastIndex--;
                    if (lastIndex < 0) lastIndex = 0;

                    //Find the distance point past the current pixel distance
                    int nextIndex = lastIndex;
                    while (nextIndex < lap.lapPoints.Count && lap.distances[nextIndex] < distance)
                        nextIndex++;

                    float mph = 0, time = 0;
                    if (nextIndex == lastIndex || lastIndex >= lap.lapPoints.Count || nextIndex >= lap.lapPoints.Count)
                    {
                        if (lastIndex < lap.lapPoints.Count)
                            mph = lap.lapPoints[lastIndex].velocity;
                    }
                    else
                    {
                        //Interpolate mph based on distance
                        float t = (distance - lap.distances[lastIndex]) / (lap.distances[nextIndex] - lap.distances[lastIndex]);
                        mph = t * lap.lapPoints[nextIndex].velocity + (1 - t) * lap.lapPoints[lastIndex].velocity;
                        time = (float)(t * (lap.lapPoints[nextIndex].time - baseTime).TotalSeconds + (1 - t) * (lap.lapPoints[lastIndex].time - baseTime).TotalSeconds);
                    }

                    mphS[i] = mph;
                    if (lap.enabled)
                    {
                        if (mph < lowestMph)
                            lowestMph = mph;
                        if (mph > highestMph)
                            highestMph = mph;
                    }

                    timeS[i] = time;

                    distance += distanceIncrement;
                }

                processedMPH.Add(mphS);
                processedTime.Add(timeS);
            }

            //Now calculate delta curve high/low waters
            lowestOffset = 0;
            highestOffset = 0;
            for (int i = 0; i < laps.Count; i++)
            {
                if (!laps[i].enabled)
                    continue;

                for (int x = 0; x < this.Width; x++)
                {
                    float delta = processedTime[i][x] - processedTime[basisLap][x];
                    if (delta > highestOffset)
                        highestOffset = delta;
                    if (delta < lowestOffset)
                        lowestOffset = delta;
                }
            }

            //Hide the delta tape if we have nothing to compare
            this.splitContainer1.Panel2Collapsed = (enabledLaps <= 1);
        }

        public void SetLaps(List<ProcessedLap> laps)
        {
            this.laps = laps;

            ProcessCurves();
            if (xClicked == 0)
            {
                xClicked = this.Width / 2;
                UpdateXOutput();
            }
            this.Invalidate();
        }

        public void SetBasisLap(ProcessedLap lap)
        {
            for (int i = 0; i < laps.Count; i++)
            {
                if (lap == laps[i])
                {
                    basisLap = i;
                    ProcessCurves();
                    this.Invalidate();
                    UpdateXOutput();
                    return;
                }
            }
        }
    }
}
