//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphControlLib
{
    public enum PlotDataPointType
    {
        Point,
        Circle,
        Square,
        FilledSquare,
        Diamond,
        FilledDiamond
    }

    public class PlotData
    {
        private double[] dataX;

        public double[] DataX
        {
            get { return dataX; }
            set { dataX = value; }
        }

        private double[] dataY;

        public double[] DataY
        {
            get { return dataY; }
            set { dataY = value; }
        }

        private Color color = Color.Black;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        private DashStyle lineStyle = DashStyle.Dot;

        public DashStyle LineStyle
        {
            get { return lineStyle; }
            set { lineStyle = value; }
        }

        private uint width = 1;

        public uint Width
        {
            get { return width; }
            set { width = value; }
        }

        private int pointSize = 3;

        public int PointSize
        {
            get { return pointSize; }
            set { pointSize = value; }
        }

        private PlotDataPointType pointType = PlotDataPointType.Point;

        public PlotDataPointType PointType
        {
            get { return pointType; }
            set { pointType = value; }
        }

        private bool drawLine = true;

        public bool DrawLine
        {
            get { return drawLine; }
            set { drawLine = value; }
        }



        public PlotData(double[] dataX, double[] dataY, Color color, DashStyle lineStyle, int pointSize, uint penWidth, PlotDataPointType pointType)
        {
            this.dataX = dataX;
            this.dataY = dataY;
            this.color = color;
            this.width = penWidth;
            this.lineStyle = lineStyle;
            this.pointSize = pointSize;
            this.pointType = pointType;
        }

        public void Plot(Graphics g, float minX, float maxX, float minY, float maxY, Rectangle area, float drawScale = 1, double[] dataError = null)
        {
            if (dataX != null)
            {
                Pen pen = new Pen(color, width * drawScale);
                Brush brush = new SolidBrush(color);
                Pen linePen = new Pen(color, width * drawScale);
                linePen.DashStyle = lineStyle;
                int xo = 0, yo = 0;
                bool hasPrevPoint = false;
                for (int i = 0; i < dataX.Length; i++)
                {
                    if (dataX[i] >= minX && dataX[i] <= maxX && dataY[i] >= minY && dataY[i] <= maxY)
                    {
                        int x = (int)((dataX[i] - minX) / (maxX - minX) * area.Width + area.Left);
                        int y = (int)(-(dataY[i] - minY) / (maxY - minY) * area.Height + area.Bottom);
                        int markerSize = (int)(pointSize * drawScale);
                        switch (pointType)
                        {
                            case PlotDataPointType.Circle: g.DrawEllipse(pen, x - markerSize / 2, y - markerSize / 2, markerSize, markerSize); break;
                            case PlotDataPointType.Point: g.FillEllipse(brush, x - markerSize / 2, y - markerSize / 2, markerSize, markerSize); break;
                            case PlotDataPointType.Square: g.DrawRectangle(pen, x - markerSize / 2, y - markerSize / 2, markerSize, markerSize); break;
                            case PlotDataPointType.FilledSquare: g.FillRectangle(brush, x - markerSize / 2, y - markerSize / 2, markerSize, markerSize); break;
                            case PlotDataPointType.Diamond: g.DrawPolygon(pen, new Point[]
                                                                { 
                                                                    new Point(x - markerSize / 2, y),
                                                                    new Point(x , y - markerSize /2),
                                                                    new Point(x + markerSize / 2, y),
                                                                    new Point(x, y + markerSize / 2)
                                                                
                                                                }); break;
                            case PlotDataPointType.FilledDiamond: g.FillPolygon(brush, new Point[]
                                                                { 
                                                                    new Point(x - markerSize / 2, y),
                                                                    new Point(x , y - markerSize /2),
                                                                    new Point(x + markerSize / 2, y),
                                                                    new Point(x, y + markerSize / 2)
                                                                
                                                                }); break;
                        }

                        if (drawLine && i > 0 && lineStyle != DashStyle.Custom && hasPrevPoint)
                        {
                            g.DrawLine(linePen, xo, yo, x, y);
                        }

                        if (dataError != null && dataError.Length > i)
                        {
                            int errLength = (int)(dataError[i] / (maxY - minY) / 2 * area.Height);
                            int y1 = y + errLength < area.Bottom ? y + errLength : area.Bottom - 1;
                            int y2 = y - errLength > area.Top ? y - errLength : area.Top + 1;
                            g.DrawLine(linePen, x, y1, x, y2);
                        }

                        xo = x;
                        yo = y;
                        hasPrevPoint = true;
                    }
                    else
                    {
                        hasPrevPoint = false;
                        if (double.IsInfinity(dataY[i]))
                        {
                            Pen errPen = new Pen(color, width * drawScale); errPen.DashStyle = DashStyle.Dot;
                            int x = (int)((dataX[i] - minX) / (maxX - minX) * area.Width + area.Left);
                            g.DrawLine(errPen, x, area.Top, x, area.Bottom);
                        }
                    }
                }
            }
        }
    }
}
