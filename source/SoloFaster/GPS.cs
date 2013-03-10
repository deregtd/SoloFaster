using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace SoloFaster
{
    public struct GPSSatellite
    {
        public int num;
        public int elevation, azimuth;
        public int signalStrength;
    }

    class GPS
    {
        public string portName;

        private SerialPort serialPort;
        private Thread runThread;

        public GPS(string portName, int bitRate)
        {
            this.portName = portName;

            //Start up the serial port
            serialPort = new SerialPort(portName);
            serialPort.BaudRate = bitRate;
            serialPort.DataBits = 8;
            serialPort.Parity = 0;
            serialPort.StopBits = StopBits.One;

            //Initialize member vars
            fixActive = false;
            time = "";
            satellites = new List<GPSSatellite>();
            satellitesUsed = new Dictionary<int, bool>();
            velocityMPH = 0;
            bearingRadians = 0;
            dimensionalFix = 0;
            DOP = float.PositiveInfinity;
            latitude = 0;
            longitude = 0;
            altitudeFeet = 0;
        }

        public bool Open()
        {
            try
            {
                serialPort.Open();

                runThread = new Thread(new ThreadStart(RunThread));
                runThread.Start();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();

        public void Close()
        {
            running = false;
            serialPort.Close();
        }

        bool running = false;
        private void RunThread()
        {
            running = true;
            string comBuffer = "";

            while (running)
            {
                byte[] buffer = new byte[1000];
                try
                {
                    int recvd = serialPort.Read(buffer, 0, 1000);

                    if (recvd > 0)
                    {
                        //Add it to the current buffer string
                        comBuffer += asciiEncoding.GetString(buffer, 0, (int)recvd);

                        while (comBuffer.Contains('$') && comBuffer.Contains('*') && comBuffer.Length >= comBuffer.IndexOf('*') + 3)
                        {
                            //Trim off starting garbage
                            comBuffer = comBuffer.Substring(comBuffer.IndexOf("$")+1);

                            //Make sure we still have a message
                            if (!comBuffer.Contains('*') || comBuffer.Length < comBuffer.IndexOf('*') + 3)
                                continue;

                            //Grab message and checksum and trim message off stack.
                            string message = comBuffer.Substring(0, comBuffer.IndexOf('*'));
                            string checkSumHex = comBuffer.Substring(comBuffer.IndexOf('*') + 1, 2);
                            comBuffer = comBuffer.Substring(comBuffer.IndexOf('*') + 3);

                            //Check checksum...
                            try
                            {
                                int checkSumVal = Convert.ToInt32(checkSumHex, 16);
                                byte checkSumCheck = 0;
                                foreach (char chr in message)
                                    checkSumCheck ^= Convert.ToByte(chr);
                                if (checkSumCheck == checkSumVal)
                                    ProcessMessage(message);
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                catch (IOException)
                {
                    //serialPort closed out from under us
                    return;
                }
            }
        }

        public bool fixActive;
        public string time;
        public List<GPSSatellite> satellites;
        public Dictionary<int, bool> satellitesUsed;
        public float velocityMPH;
        public float bearingRadians;
        public int dimensionalFix;
        public float DOP;
        public double latitude, longitude;
        public float altitudeFeet;

        //Internal workings
        private List<GPSSatellite> newSatelliteList;
        public delegate void NoParamHandler();
        public event NoParamHandler positionUpdate;
        public event NoParamHandler satelliteUpdate;

        private void ProcessMessage(string message)
        {
            Console.WriteLine("Message: " + message);

            string[] MessageData = message.Split(new char[] { ',' });
            switch (MessageData[0])
            {
                case "GPRMC":
                    //http://www.codepedia.com/1/The+GPRMC+Sentence

                    time = MessageData[1];
                    fixActive = (MessageData[2] == "A");

                    velocityMPH = (int)(Math.Round(double.Parse(MessageData[7]) * 1.15078));
                    if (MessageData[8] != "")
                        bearingRadians = float.Parse(MessageData[8]) * (float)Math.PI / 180.0f;
                    break;

                case "GPGSV":
                    /*        GSV - Satellites in view
                            GSV,2,1,08,01,40,083,46,02,17,308,41,12,07,344,39,14,22,228,45*75
                               2            Number of sentences for full data
                               1            sentence 1 of 2
                               08           Number of satellites in view
                               01           Satellite PRN number
                               40           Elevation, degrees
                               083          Azimuth, degrees
                               46           Signal strength - higher is better
                               <repeat for up to 4 satellites per sentence>
                                    There my be up to three GSV sentences in a data packet*/

                    int numSent = int.Parse(MessageData[1]);
                    int thisSent = int.Parse(MessageData[2]);
                    if (thisSent == 1)
                        newSatelliteList = new List<GPSSatellite>();

                    if (newSatelliteList == null)   //Must have started in the middle of a sentence
                        break;

                    int numSats = int.Parse(MessageData[3]);

                    int numParse = (int)Math.Ceiling((MessageData.Length - 5) / 4.0f);
                    for (int i = 0; i < numParse; i++)
                    {
                        if (MessageData[4 + 4 * i] == "")
                            continue;

                        //Valid satellite
                        GPSSatellite sat = new GPSSatellite();

                        sat.num = int.Parse(MessageData[4 + 4 * i]);

                        if (MessageData[4 + 4 * i + 1] != "")
                            sat.elevation = int.Parse(MessageData[4 + 4 * i + 1]);
                        if (MessageData[4 + 4 * i + 2] != "")
                            sat.azimuth = int.Parse(MessageData[4 + 4 * i + 2]);

                        if (MessageData.Length <= 4 + 4 * i + 3 || MessageData[4 + 4 * i + 3] == "")
                            sat.signalStrength = 0;
                        else
                        {
                            string ss = MessageData[4 + 4 * i + 3];
                            if (ss.Contains('*'))
                                ss = ss.Substring(0, ss.IndexOf('*'));
                            if (ss == "")
                                ss = "0";

                            sat.signalStrength = 0;
                            int.TryParse(ss, out sat.signalStrength);
                            //sat.signalStrength = int.Parse(ss);
                        }

                        newSatelliteList.Add(sat);
                    }

                    if (thisSent == numSent)
                    {
                        //Got all sentences

                        satellites = newSatelliteList;
                        newSatelliteList = null;

                        if (satelliteUpdate != null)
                            satelliteUpdate.Invoke();
                    }

                    break;

                case "GPGSA":
                    /*        GSA - GPS DOP and active satellites
                            GSA,A,3,04,05,,09,12,,,24,,,,,2.5,1.3,2.1*39
                               A            Auto selection of 2D or 3D fix (M = manual)
                               3            3D fix
                               04,05...     PRNs of satellites used for fix (space for 12)
                               2.5          PDOP (dilution of precision)
                               1.3          Horizontal dilution of precision (HDOP)
                               2.1          Vertical dilution of precision (VDOP)
                                 DOP is an indication of the effect of satellite geometry on
                                 the accuracy of the fix.*/

                    dimensionalFix = int.Parse(MessageData[2]);
                    DOP = float.Parse(MessageData[15]);

                    satellitesUsed.Clear();
                    for (int i = 3; i < 3 + 12; i++)
                    {
                        if (MessageData[i] != "")
                            satellitesUsed.Add(int.Parse(MessageData[i]), true);
                    }
                    break;

                case "GPGGA":
                    /* GGA - Global Positioning System Fix Data
                            GGA,123519,4807.038,N,01131.324,E,1,08,0.9,545.4,M,46.9,M, , *42
                               123519       Fix taken at 12:35:19 UTC
                               4807.038,N   Latitude 48 deg 07.038' N
                               01131.324,E  Longitude 11 deg 31.324' E
                               1            Fix quality: 0 = invalid
                                                         1 = GPS fix
                                                         2 = DGPS fix
                               08           Number of satellites being tracked
                               0.9          Horizontal dilution of position
                               545.4,M      Altitude, Metres, above mean sea level
                               46.9,M       Height of geoid (mean sea level) above WGS84
                                            ellipsoid
                               (empty field) time in seconds since last DGPS update
                               (empty field) DGPS station ID number */

                    time = MessageData[1];

                    latitude = Double.Parse(MessageData[2].Substring(0, 2))
                        + Double.Parse(MessageData[2].Substring(2)) / 60.0;
                    if (MessageData[3] == "S") latitude *= -1;

                    longitude = Double.Parse(MessageData[4].Substring(0, 3))
                        + Double.Parse(MessageData[4].Substring(3)) / 60.0;
                    if (MessageData[5] == "W") longitude *= -1;

                    altitudeFeet = float.Parse(MessageData[9]) * 3.2808399f;

                    if (positionUpdate != null)
                        positionUpdate.Invoke();

                    break;
            };
        }
    }
}
