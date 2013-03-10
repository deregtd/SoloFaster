using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SoloFaster
{
    public class ProcessedLap
    {
        public class ProcessedLapComparer : IComparer<ProcessedLap>
        {
            int IComparer<ProcessedLap>.Compare(ProcessedLap x, ProcessedLap y)
            {
                return x.lapPoints[0].time.CompareTo(y.lapPoints[0].time);
            }
        }

        public Color color;

        public List<GPSPoint> lapPoints;
        public float[] distances;
        public OpenedSession session;
        public int lapNum;
        public ListViewItem listViewRow;
        public bool enabled;

        public TimeSpan totalTime;
        public float totalDistance;

        public ProcessedLap()
        {
            lapNum = 0;
            color = Color.White;

            enabled = true;
            lapPoints = new List<GPSPoint>();
        }

        public string GetName()
        {
            string tryName = lapPoints.First().time.ToString("HH:mm:ss");//session.session.gpsPoints.First().time.ToString("HH:mm:ss");
            if (lapNum > 0)
                tryName += " #" + (lapNum + 1);
            if (session.session.driverName != "")
                tryName += " " + session.session.driverName;
            if (session.session.notes != "")
                tryName += " " + session.session.notes;
            return tryName;
        }

        public void CalculateDistances()
        {
            distances = new float[lapPoints.Count];

            float ongoingDistance = 0;
            for (int i = 0; i < lapPoints.Count; i++)
            {
                if (i > 0)
                {
                    double R = 20902231; // feet radius of earth
                    double dLat = (lapPoints[i].lat - lapPoints[i - 1].lat) * Math.PI / 180.0;
                    double dLon = (lapPoints[i].lon - lapPoints[i - 1].lon) * Math.PI / 180.0;
                    double lat1 = lapPoints[i - 1].lat * Math.PI / 180.0;
                    double lat2 = lapPoints[i].lat * Math.PI / 180.0;

                    double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                            Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
                    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                    ongoingDistance += (float)(R * c);
                }

                distances[i] = ongoingDistance;
            }

            totalDistance = ongoingDistance;
            totalTime = (lapPoints.Last().time - lapPoints.First().time);
        }
    }
}
