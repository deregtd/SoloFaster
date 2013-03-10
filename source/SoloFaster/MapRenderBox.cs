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
    public partial class MapRenderBox : UserControl
    {
        public delegate void NeedRenderDelegate(Graphics g, MapRenderBox box);
        public event NeedRenderDelegate OnNeedRender;

        public delegate void LineCallback(GPSPoint start, GPSPoint end);

        private bool validPoints;
        private double minLat, minLong, maxLat, maxLong;
        private double renderMinLat, renderMinLong, renderMaxLat, renderMaxLong, renderScale;

        public MapRenderBox()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint,
                true);

            this.Resize += new EventHandler(MapRenderBox_Resize);
            this.Paint += new PaintEventHandler(MapRenderBox_Paint);

            draggingLine = false;
        }

        // ----------------------- LINE DRAGGING CODE ----------------------

        private bool draggingLine;
        private LineCallback lineCallback;
        private Color lineColor;
        private GPSPoint startPoint, endPoint;

        public void StartDraggingLine(LineCallback callback, Color color)
        {
            this.Cursor = Cursors.Cross;

            draggingLine = true;
            startPoint = null;
            endPoint = null;

            this.lineCallback = callback;
            this.lineColor = color;
        }

        public void StopDraggingLine()
        {
            if (!draggingLine)
                return;

            this.Cursor = Cursors.Default;
            startPoint = null;
            endPoint = null;
            draggingLine = false;

            this.Invalidate();
        }

        private void MapRenderBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!draggingLine)
                return;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                startPoint = BackConvertPoint(e.Location);
            }
        }

        private void MapRenderBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draggingLine)
                return;

            if (startPoint != null && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                endPoint = BackConvertPoint(e.Location);
                this.Invalidate();
            }
        }

        private void MapRenderBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!draggingLine)
                return;

            if (startPoint != null && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                endPoint = BackConvertPoint(e.Location);
                this.lineCallback(startPoint, endPoint);
                this.StopDraggingLine();
            }
        }


        // ---------------------- RENDERING CODE -----------------------

        private void MapRenderBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.Clear(Color.Black);

            if (this.OnNeedRender != null)
                this.OnNeedRender.Invoke(e.Graphics, this);

            if (draggingLine)
            {
                if (startPoint != null && endPoint != null)
                    e.Graphics.DrawLine(new Pen(lineColor, 2), ConvertPoint(startPoint), ConvertPoint(endPoint));
            }
        }

        private void MapRenderBox_Resize(object sender, EventArgs e)
        {
            //Resize scales real fast
            RecalcRenderLats();
        }

        public void Recenter(List<GPSPoint> pointsToShow)
        {
            validPoints = false;
            minLat = double.PositiveInfinity;
            maxLat = double.NegativeInfinity;
            minLong = double.PositiveInfinity;
            maxLong = double.NegativeInfinity;

            if (pointsToShow.Count > 0)
            {
                foreach (GPSPoint point in pointsToShow)
                {
                    if (point.lat > maxLat)
                        maxLat = point.lat;
                    if (point.lat < minLat)
                        minLat = point.lat;
                    if (point.lon > maxLong)
                        maxLong = point.lon;
                    if (point.lon < minLong)
                        minLong = point.lon;
                }

                if (maxLat == minLat) { minLat -= 0.0000001; maxLat += 0.0000001; }
                if (maxLong == minLong) { minLong -= 0.0000001; maxLong += 0.0000001; }

                validPoints = true;
                RecalcRenderLats();
            }
        }

        private void RecalcRenderLats()
        {
            if (!validPoints)
                return;

            double border = 0.02;
            double latTotal = maxLat - minLat;
            renderMinLat = minLat - border * latTotal;
            renderMaxLat = maxLat + border * latTotal;
            double lonTotal = maxLong - minLong;
            renderMinLong = minLong - border * lonTotal;
            renderMaxLong = maxLong + border * lonTotal;

            //Rescale to keep everything consistent
            double latScale = this.Height / (renderMaxLat - renderMinLat);
            double lonScale = this.Width / (renderMaxLong - renderMinLong);
            if (latScale >= lonScale)
            {
                renderScale = lonScale;
                double latAvg = (renderMinLat + renderMaxLat) / 2;
                double latDiff = (this.Height / 2) / renderScale;
                renderMinLat = latAvg - latDiff;
                renderMaxLat = latAvg + latDiff;
            }
            else
            {
                renderScale = latScale;
                double lonAvg = (renderMinLong + renderMaxLong) / 2;
                double lonDiff = (this.Width / 2) / renderScale;
                renderMinLong = lonAvg - lonDiff;
                renderMaxLong = lonAvg + lonDiff;
            }
        }

        public Point ConvertPoint(GPSPoint point)
        {
            int x = (int)Math.Round((point.lon - renderMinLong) * renderScale);
            int y = this.Height - (int)Math.Round((point.lat - renderMinLat) * renderScale);
            return new Point(x, y);
        }

        public GPSPoint BackConvertPoint(Point point)
        {
            GPSPoint ret = new GPSPoint();
            ret.lon = (double)point.X / renderScale + renderMinLong;
            ret.lat = (double)(this.Height - point.Y) / renderScale + renderMinLat;
            return ret;
        }
    }
}
