using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace SoloFaster
{
    public class RecordedSession
    {
        public string eventName;
        public string driverName;
        public string notes;

        public List<GPSPoint> gpsPoints;

        public RecordedSession()
        {
            gpsPoints = new List<GPSPoint>();
        }

        public string Serialize()
        {
            XmlSerializer s = new XmlSerializer(this.GetType());
            MemoryStream stream = new MemoryStream();
            s.Serialize(stream, this);
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            return asciiEncoding.GetString(stream.GetBuffer());
        }

        public string GetFullEventName()
        {
            string tryName = gpsPoints.First().time.ToString("yyyy-MM-dd");
            if (eventName != "")
                tryName += " " + eventName;
            return tryName;
        }

        public string GetEventDirName()
        {
            return MakeSafeForFilename(GetFullEventName().Replace(" ", "_"));
        }

        public string GetFileName()
        {
            string tryName = gpsPoints.First().time.ToString("HHmmss");
            if (driverName != "")
                tryName += "_" + MakeSafeForFilename(driverName).Replace(" ", "");
            return tryName;
        }

        public override string ToString()
        {
            string tryName = gpsPoints.First().time.ToString("HH:mm:ss");
            if (driverName != "")
                tryName += " " + driverName;
            if (notes != "")
                tryName += ": " + notes.Replace("\n","");
            return tryName;
        }

        public static RecordedSession DeSerialize(string instr)
        {
            XmlSerializer s = new XmlSerializer(typeof(RecordedSession));
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            MemoryStream stream = new MemoryStream(asciiEncoding.GetBytes(instr));
            return (RecordedSession) s.Deserialize(stream);
        }

        public static string MakeSafeForFilename(string instr)
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                instr = instr.Replace(c, '_');

            return instr;
        }
    }
}
