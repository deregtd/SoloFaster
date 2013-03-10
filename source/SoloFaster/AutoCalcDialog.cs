using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SoloFaster
{
    public partial class AutoCalcDialog : Form
    {
        private List<OpenedSession> sessions;
        public GPSPoint[] bestStart = null, bestFinish = null;

        public AutoCalcDialog(List<OpenedSession> sessions)
        {
            InitializeComponent();
            this.sessions = sessions;
        }

        public bool RunCalculation()
        {
            DialogResult res = this.ShowDialog();
            return (res != System.Windows.Forms.DialogResult.Cancel);
        }

        private Thread runThread;
        private void AutoCalcDialog_Shown(object sender, EventArgs e)
        {
            this.progressBar1.Minimum = 0;
            this.progressBar1.Value = 0;

            int max = 0;
            foreach (OpenedSession session in sessions)
                max += session.session.gpsPoints.Count - 2;

            this.progressBar1.Maximum = max;

            runThread = new Thread(new ThreadStart(Calculate));
            runThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runThread.Abort();
        }

        private void Calculate()
        {
            //Start with start/finish lines at the beginning/end of the first session
            int bestWorking = 0, bestPoints = 0;

            //Go through each session, using it as the base to walk through
            int lastCounter = 0;
            foreach (OpenedSession session in sessions)
            {
                //Only do 1/100th of the points for speed reasons...  Damn n^3 algorithms...
                int incrementer = (session.session.gpsPoints.Count / 100);
                if (incrementer < 1) incrementer = 1;

                //Walk the start and finish through to the other end of the run until the most sessions line up.
                for (int s = 0; s < session.session.gpsPoints.Count - 2; s += incrementer)
                {
                    GPSPoint[] testStart = MathUtil.CalculateLinePerpendicularToLine(session.session.gpsPoints[s + 1], session.session.gpsPoints[s], 0.00005);

                    for (int f = 0; f < session.session.gpsPoints.Count - 2 - s; f += incrementer)
                    {
                        GPSPoint[] testFinish = MathUtil.CalculateLinePerpendicularToLine(session.session.gpsPoints[session.session.gpsPoints.Count - f - 2], session.session.gpsPoints[session.session.gpsPoints.Count - f - 1], 0.00005);

                        int working = 1;

                        //Walk through each session to see if they intersect
                        foreach (OpenedSession testSession in sessions)
                        {
                            if (testSession == session)
                                continue;

                            GPSPoint lastPoint = null;
                            bool foundStart = false, foundFinish = false;
                            for (int h = 0; h < testSession.session.gpsPoints.Count; h++)
                            {
                                GPSPoint newPoint = testSession.session.gpsPoints[h];
                                if (lastPoint != null)
                                {
                                    //check to see if we've crossed a start or finish line
                                    if (!foundStart && MathUtil.LineIntersects(lastPoint, newPoint, testStart[0], testStart[1]))
                                        foundStart = true;
                                    //Can only cross the finish if we've already crossed the start
                                    if (foundStart && MathUtil.LineIntersects(lastPoint, newPoint, testFinish[0], testFinish[1]))
                                        foundFinish = true;

                                    //If we've found both already, skip out of this one
                                    if (foundStart && foundFinish)
                                        break;
                                }
                                lastPoint = newPoint;
                            }

                            if (foundStart && foundFinish)
                                working++;
                        }

                        //See if either start or finish is better than the previous record
                        int testPoints = session.session.gpsPoints.Count - s - f;
                        if (working > bestWorking || (working == bestWorking && testPoints > bestPoints))
                        {
                            bestStart = testStart;
                            bestFinish = testFinish;

                            bestWorking = working;
                            bestPoints = testPoints;
                        }
                    }

                    this.Invoke(new ThreadStart(delegate() { this.progressBar1.Value = lastCounter + s; }));
                }
                lastCounter += session.session.gpsPoints.Count - 2;
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
