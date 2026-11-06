//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.ComponentModel;
//Designed and Implemented by Andrey Borissov Batchvarov 2018
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace GraphControlLib
{
    public struct Margins
    {
        public int Left;
        public int Right;
        public int Top;
        public int Bottom;
    }

    public partial class GraphControl : UserControl
    {
        public bool ShowYAxisLabels
        {
            get { return showYAxisLabels; }
            set { showYAxisLabels = value; }
        }

        public bool ShowXAxisLabels
        {
            get { return showXAxisLabels; }
            set { showXAxisLabels = value; }
        }

        public int XAxisLabelsLength
        {
            get { return xAxisLabelsLength; }
            set { xAxisLabelsLength = value; }
        }

        public int XAxisLabelsDigits
        {
            get { return xAxisLabelsDigits; }
            set { xAxisLabelsDigits = value; }
        }

        public int XAxisLabelsPow
        {
            get { return xAxisLabelsPow; }
            set { xAxisLabelsPow = value; }
        }

        public Font XAxisLabelsFont
        {
            get { return xAxisLabelsFont; }
            set { xAxisLabelsFont = value; }
        }

        public float XAxisMin
        {
            get { return xAxisMin; }
        }

        public float XAxisMax
        {
            get { return xAxisMax; }
        }

        public float XAxisLabelsMin
        {
            get { return xAxisLabelsMin; }
            set { xAxisLabelsMin = value; }
        }

        public float XAxisLabelsMax
        {
            get { return xAxisLabelsMax; }
            set { xAxisLabelsMax = value; }
        }

        public float XAxisLabelsStep
        {
            get { return xAxisLabelsStep; }
            set { xAxisLabelsStep = value; }
        }

        public float YAxisLabelsMin
        {
            get { return yAxisLabelsMin; }
            set { yAxisLabelsMin = value; }
        }

        public float YAxisLabelsMax
        {
            get { return yAxisLabelsMax; }
            set { yAxisLabelsMax = value; }
        }

        public float YAxisLabelsStep
        {
            get { return yAxisLabelsStep; }
            set { yAxisLabelsStep = value; }
        }
        public int YAxisLabelsLength
        {
            get { return yAxisLabelsLength; }
            set { yAxisLabelsLength = value; }
        }

        public int YAxisLabelsDigits
        {
            get { return yAxisLabelsDigits; }
            set { yAxisLabelsDigits = value; }
        }

        public int YAxisLabelsPow
        {
            get { return yAxisLabelsPow; }
            set { yAxisLabelsPow = value; }
        }

        public Font YAxisLabelsFont
        {
            get { return yAxisLabelsFont; }
            set { yAxisLabelsFont = value; }
        }

        public float YAxisMin
        {
            get { return yAxisMin; }
        }

        public float YAxisMax
        {
            get { return yAxisMax; }
        }

        public int InternalMargins
        {
            set { internalMargins = value; }
            get { return internalMargins; }
        }

        public Color PlotBkColor
        {
            get { return plotBkColor; }
            set { plotBkColor = value; }
        }

        public bool Frame
        {
            get { return frame; }
            set { frame = value; }
        }

        public Color FrameColor
        {
            get { return frameColor; }
            set { frameColor = value; }
        }

        public uint FrameThickness
        {
            get { return frameThickness; }
            set { frameThickness = value; }
        }

        public Color AxisLabelsColor
        {
            get { return axisLabelsColor; }
            set { axisLabelsColor = value; }
        }

        public string XAxisText
        {
            get { return xAxisText; }
            set { xAxisText = value; }
        }

        public string YAxisText
        {
            get { return yAxisText; }
            set { yAxisText = value; }
        }

        public bool YAxisTextVertical
        {
            get { return yAxisTextVertical; }
            set { yAxisTextVertical = value; }
        }

        public bool GridLines
        {
            get { return gridLines; }
            set { gridLines = value; }
        }

        public uint GridThickness
        {
            get { return gridThickness; }
            set { gridThickness = value; }
        }

        public DashStyle GridDashStyle
        {
            get { return gridDashStyle; }
            set { gridDashStyle = value; }
        }

        public Color GridColor
        {
            get { return gridColor; }
            set { gridColor = value; }
        }

        public Color AxisTitlesColor
        {
            get { return axisTitlesColor; }
            set { axisTitlesColor = value; }
        }

        public Font AxisTitlesFont
        {
            get { return axisTitlesFont; }
            set { axisTitlesFont = value; }
        }

        public Size MinSelectionSize
        {
            get { return minSelectionSize; }
            set { minSelectionSize = value; }
        }

        [Browsable(false)]
        public PointF SelectionEnd
        {
            get { return selectionP2; }
            set { selectionP2 = value; }
        }

        [Browsable(false)]
        public PointF SelectionStart
        {
            get { return selectionP1; }
            set { selectionP1 = value; }
        }

        [Browsable(true)]
        public int MinPlotWidth
        {
            get { return minWidth; }
            set { minWidth = value; }
        }

        [Browsable(true)]
        public int MinPlotHeigth
        {
            get { return minHeigth; }
            set { minHeigth = value; }
        }

        [Browsable(true)]
        public Color SelectionColor
        {
            get { return selectionColor; }
            set { selectionColor = value; }
        }

        [Browsable(true)]
        public int SelectionPenWidth
        {
            get { return selectionPenWidth; }
            set { selectionPenWidth = value; }
        }

        [Browsable(true)]
        public DashStyle SelctionLineStyle
        {
            get { return selctionLineStyle; }
            set { selctionLineStyle = value; }
        }

        [Browsable(true)]
        public bool ShowTitle
        {
            get { return showTitle; }
            set { showTitle = value; }
        }

        [Browsable(true)]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [Browsable(true)]
        public Font TitleFont
        {
            get { return titleFont; }
            set { titleFont = value; }
        }

        [Browsable(true)]
        public string SubTitle
        {
            get { return subTitle; }
            set { subTitle = value; }
        }

        [Browsable(true)]
        public Font SubTitleFont
        {
            get { return subTitleFont; }
            set { subTitleFont = value; }
        }

        [Browsable(true)]
        public EventHandler OnSelectionChange = null;

        public void SetPlotData(string name, PlotData plotData)
        {
            if (this.data.ContainsKey(name))
            {
                this.data[name] = plotData;
            }
            else
            {
                this.data.Add(name, plotData);
            }
        }

        public PlotData GetPlotData(string name)
        {
            if (this.data.ContainsKey(name))
            {
                return this.data[name];
            }
            else
            {
                return null;
            }
        }

        public void RemovePlotData(string name)
        {
            if (this.data.ContainsKey(name))
            {
                this.data.Remove(name);
            }
        }

        public void SetPlotDataError(string name, double[] plotData)
        {
            if (this.dataError.ContainsKey(name))
            {
                this.dataError[name] = plotData;
            }
            else
            {
                this.dataError.Add(name, plotData);
            }
        }

        public float[] GetPlotDataError(string name)
        {
            if (this.dataError.ContainsKey(name))
            {
                return this.dataError[name];
            }
            else
            {
                return null;
            }
        }

        public void RemovePlotDataError(string name)
        {
            if (this.dataError.ContainsKey(name))
            {
                this.dataError.Remove(name);
            }
        }

        public void ClearSelection()
        {
            selectionP1 = Point.Empty;
            selectionP2 = Point.Empty;
        }

        public GraphControl()
        {
            InitializeComponent();
            graphics = this.CreateGraphics();
            InitSizes();
        }

    }
}