using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoloFaster
{
    public class GPSPoint
    {
        public double lat, lon;
        public DateTime time;
        public float velocity;
        public float altitude;

        public override bool Equals(object obj)
        {
            if (!(obj is GPSPoint))
                return false;

            GPSPoint objo = (GPSPoint)obj;

            if (objo.lat != lat)
                return false;
            if (objo.lon != lon)
                return false;
            if (objo.time != time)
                return false;
            if (objo.velocity != velocity)
                return false;
            if (objo.altitude != altitude)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            //RxTODO: This, someday, maybe, if it matters?
            return base.GetHashCode();
        }
    }
}
