using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoloFaster
{
    public class MathUtil
    {
        private MathUtil()
        {
        }


        public static GPSPoint LineIntersection(GPSPoint a1, GPSPoint a2, GPSPoint b1, GPSPoint b2)
        {
            double xa1 = a1.lat;
            double ya1 = a1.lon;
            double xa2 = a2.lat;
            double ya2 = a2.lon;

            double xb1 = b1.lat;
            double yb1 = b1.lon;
            double xb2 = b2.lat;
            double yb2 = b2.lon;

            double denom = (xb1 - xb2) * (ya1 - ya2) - (xa1 - xa2) * (yb1 - yb2);
            if (denom == 0) denom += 0.0000000001;
            double s = (xb1 * (ya1 - ya2) + xa1 * (ya2 - yb1) + xa2 * (yb1 - ya1)) / denom;
            double t = (xb2 * (ya1 - yb1) + xa1 * (yb1 - yb2) + xb1 * (yb2 - ya1)) / (-denom);

            //s and t both being between 0 and 1 mean the lines have intersected - crossed start/finish!
            if ((s >= 0) && (s <= 1) && (t >= 0) && (t <= 1))
            {
                //Find actual intersection point and then interpolate all values
                GPSPoint intersect = new GPSPoint();
                intersect.lat = (t * a2.lat) + ((1 - t) * a1.lat);
                intersect.lon = (t * a2.lon) + ((1 - t) * a1.lon);
                intersect.time = new DateTime((long)((t * a2.time.Ticks) + ((1 - t) * a1.time.Ticks)));
                intersect.velocity = (float)((t * a2.velocity) + ((1 - t) * a1.velocity));
                intersect.altitude = (float)((t * a2.altitude) + ((1 - t) * a1.altitude));
                return intersect;
            }
            return null;
        }

        public static bool LineIntersects(GPSPoint a1, GPSPoint a2, GPSPoint b1, GPSPoint b2)
        {
            double xa1 = a1.lat;
            double ya1 = a1.lon;
            double xa2 = a2.lat;
            double ya2 = a2.lon;

            double xb1 = b1.lat;
            double yb1 = b1.lon;
            double xb2 = b2.lat;
            double yb2 = b2.lon;

            double denom = (xb1 - xb2) * (ya1 - ya2) - (xa1 - xa2) * (yb1 - yb2);
            if (denom == 0) denom += 0.0000000001;
            double s = (xb1 * (ya1 - ya2) + xa1 * (ya2 - yb1) + xa2 * (yb1 - ya1)) / denom;
            double t = (xb2 * (ya1 - yb1) + xa1 * (yb1 - yb2) + xb1 * (yb2 - ya1)) / (-denom);

            //s and t both being between 0 and 1 mean the lines have intersected - crossed start/finish!
            return ((s >= 0) && (s <= 1) && (t >= 0) && (t <= 1));
        }


        public static GPSPoint[] CalculateLinePerpendicularToLine(GPSPoint a, GPSPoint b, double radius)
        {
            //This will return a line that passes through point a, perpendicular to the chord {a,b}, of radius {radius}
            //Good radius = 0.0001

            double travelAngle = Math.Atan2(b.lon - a.lon, b.lat - a.lat);
            travelAngle += Math.PI / 2;
            GPSPoint newA = new GPSPoint();
            GPSPoint newB = new GPSPoint();
            newA.lon = a.lon + radius * Math.Sin(travelAngle);
            newA.lat = a.lat + radius * Math.Cos(travelAngle);
            newB.lon = a.lon - radius * Math.Sin(travelAngle);
            newB.lat = a.lat - radius * Math.Cos(travelAngle);
            return new GPSPoint[] { newA, newB };
        }
    }
}
