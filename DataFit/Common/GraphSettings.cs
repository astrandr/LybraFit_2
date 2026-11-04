//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System.Drawing;
using System.Drawing.Drawing2D;
using GraphControlLib;
using System.Xml.Serialization;

namespace LybraFit.Common
{
    public struct GraphSettings
    {
        public bool XLimitAuto;
        public float XMin;
        public float XMax;
        public float XStep;
        public int XPow;
        public int xDigits;

        public bool YLimitAuto;
        public float YMin;
        public float YMax;
        public float YStep;
        public int YPow;
        public int yDigits;

        public Color BackgroundColor;
        public Color PlotAreaColor;
        public Color AxisColor;

        public Color TickLabelsColor;
        public Color AxisLabelsColor;

        [XmlIgnore]
        public Font TickLabelsFont;
        [XmlIgnore]
        public Font AxisLabelsFont;

        public bool VerticalTextYAxis;

        public uint AxisThickness;
        public bool ShowGridLines;
        public uint GridThickness;

        public DashStyle GridStyle;
        public Color GridColor;

        public Color DataColor;
        public bool ConnectDataPoints;
        public DashStyle DataLineStyle;
        public uint DataLineWidth;
        public uint DataPointSize;

        public Color FitLineColor;
        public uint FitLineWidth;
        public DashStyle FitLineStyle;
        public PlotDataPointType DataPointType;

        public bool ShowTitle;
        public string Title;

        [XmlIgnore]
        public Font TitleFont;
        public bool ShowModel;
        public bool ShowRegressionResult;

        public void Initialize()
        {
            XLimitAuto = true;
            YLimitAuto = true;

            BackgroundColor = Color.White;
            PlotAreaColor = Color.White;
            AxisColor = Color.Black;

            TickLabelsColor = Color.Black;
            AxisLabelsColor = Color.Black;

            DataColor = Color.Red;
            FitLineColor = Color.Blue;

            AxisThickness = 2;
            ShowGridLines = true;
            GridThickness = 1;

            GridStyle = DashStyle.Dot;
            GridColor = Color.Black;
            TickLabelsFont = new System.Drawing.Font("Arial", 9);
            AxisLabelsFont = new System.Drawing.Font("Arial", 10);
            TitleFont = new System.Drawing.Font("Bodoni MT", 12, FontStyle.Italic);

            ConnectDataPoints = true;
            DataLineStyle = DashStyle.Solid;
            DataLineWidth = 1;
            DataPointType = PlotDataPointType.Point;

            FitLineWidth = 2;
            FitLineStyle = DashStyle.Solid;
            FitLineColor = Color.Blue;

            DataPointSize = 5;
            ShowGridLines = true;
        }

    }
}
