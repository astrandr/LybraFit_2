//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GraphControlLib
{
    public partial class GraphControl : UserControl
    {
        private Graphics graphics;
        private Bitmap bmp = null;
        private Bitmap frontBuffer = null;
        private bool showYAxisLabels = true;
        private bool showXAxisLabels = true;
        private int xAxisLabelsLength = 3;
        private int xAxisLabelsDigits = 0;
        private int xAxisLabelsPow = 0;
        private Font xAxisLabelsFont = new Font("Arial", 7);
        private float xAxisMin = 0;
        private float xAxisMax = 0;
        private float xAxisStep = 0;
        private float xAxisLabelsMin = 0;
        private float xAxisLabelsMax = 0;
        private float xAxisLabelsStep = 0;
        private float yAxisMin = 0;
        private float yAxisMax = 0;
        private float yAxisStep = 0;
        private float yAxisLabelsMin = 0;
        private float yAxisLabelsMax = 0;
        private float yAxisLabelsStep = 0;
        private int yAxisLabelsLength = 3;
        private int yAxisLabelsDigits = 0;
        private int yAxisLabelsPow = 0;
        private Font yAxisLabelsFont = new Font("Arial", 7);
        private Color plotBkColor = Color.White;
        private Rectangle plotArea;
        private bool frame = true;
        private Color frameColor = Color.Black;
        private uint frameThickness = 1;
        private uint gridThickness = 1;
        private int internalMargins = 10;
        private bool showAxisLines = true;
        private int axisLinesLength = 5;
        private Color axisLabelsColor = Color.Black;
        private string xAxisText = "";
        private string yAxisText = "";
        private bool yAxisTextVertical = false;
        private bool gridLines = false;
        private DashStyle gridDashStyle = DashStyle.Dot;
        private Color gridColor = Color.Black;
        private Color axisTitlesColor = Color.Black;
        private Font axisTitlesFont = new Font("Arial", 10);

        private PointF selectionP1 = Point.Empty, selectionP2 = Point.Empty;
        private bool isInSelection = false;
        private Size minSelectionSize = new Size(5, 5);

        private Dictionary<string, PlotData> data = new Dictionary<string, PlotData>();
        private Dictionary<string, double[]> dataError = new Dictionary<string, double[]>();

        //Mouse and selection related members
        private bool isInSelectMode = false;
        private Point pointStart = Point.Empty, pointEnd = Point.Empty;
        private Color selectionColor = Color.Black;
        private int selectionPenWidth = 1;
        private DashStyle selctionLineStyle = DashStyle.Dot;

        private int minWidth = 80;
        private int minHeigth = 80;

        private bool showTitle = false;
        private string title = "";
        private Font titleFont = new Font("Arial", 11);

        private string subTitle = String.Empty;
        private Font subTitleFont = new Font("Arial", 10);

        private float drawScale = 1;

        private void InitSizes()
        {
            int left = (this.showYAxisLabels) ? (int)(this.yAxisLabelsLength * graphics.MeasureString("0", this.yAxisLabelsFont).Width) + axisLinesLength : 0;
            int top = (this.showYAxisLabels) ? (int)(graphics.MeasureString("0", this.yAxisLabelsFont).Height) : 0;
            int bottom = (this.showXAxisLabels) ? (int)(graphics.MeasureString("0", this.xAxisLabelsFont).Height) + (int)(graphics.MeasureString(this.xAxisText, this.xAxisLabelsFont).Height * 3 / 2) + axisLinesLength : 0;
            this.plotArea = new Rectangle(left + internalMargins, this.ClientRectangle.Top + top + internalMargins, this.ClientRectangle.Right - 2 * internalMargins - left, this.ClientRectangle.Bottom - bottom - top - 2 * internalMargins);
        }

        private void InitSizes(Rectangle clientArea, Graphics g)
        {
            SizeF yAxisTextSize = g.MeasureString(yAxisText, this.axisTitlesFont);
            SizeF digitSize = g.MeasureString("0", this.yAxisLabelsFont);
            int leftPadding = (this.showYAxisLabels) ? (int)((yAxisLabelsLength + 1) * digitSize.Width) + (int)(axisLinesLength * drawScale) : 0;
            if (yAxisTextVertical)
            {
                leftPadding += (int)yAxisTextSize.Height;
            }
            else
            {
                if (leftPadding < yAxisTextSize.Width)
                {
                    leftPadding = (int)yAxisTextSize.Width;
                }
            }

            int topPadding = (this.showYAxisLabels) ? (int)(g.MeasureString("0", this.yAxisLabelsFont).Height) : 0;

            int bottomPadding = (this.showXAxisLabels) ? (int)(g.MeasureString("0", this.xAxisLabelsFont).Height * 3 / 2) + (int)(g.MeasureString(this.xAxisText, this.axisTitlesFont).Height) + (int)(axisLinesLength * drawScale) : 0;
            if (showTitle)
            {
                int titleHeight = (int)g.MeasureString(title, titleFont).Height;
                if (!String.IsNullOrEmpty(subTitle))
                {
                    titleHeight += (int)g.MeasureString(subTitle, subTitleFont).Height;
                    titleHeight += (int)(4 * drawScale);
                }
                if (topPadding < titleHeight + (int)(4 * drawScale))
                {
                    topPadding = titleHeight + (int)(4 * drawScale);
                }
            }

            this.plotArea = new Rectangle(leftPadding + (int)(internalMargins * drawScale),
                                          clientArea.Top + topPadding + (int)(internalMargins * drawScale),
                                          clientArea.Right - 2 * (int)(internalMargins * drawScale) - leftPadding,
                                          clientArea.Bottom - bottomPadding - topPadding - 2 * (int)(internalMargins * drawScale));
        }

        private void AdjustLimits()
        {
            xAxisMin = xAxisLabelsMin * (float)Math.Pow(10, xAxisLabelsPow);
            xAxisMax = xAxisLabelsMax * (float)Math.Pow(10, xAxisLabelsPow);
            yAxisMin = yAxisLabelsMin * (float)Math.Pow(10, yAxisLabelsPow);
            yAxisMax = yAxisLabelsMax * (float)Math.Pow(10, yAxisLabelsPow);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawContent();
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Refresh();
        }

        private void DrawData(Graphics g)
        {
            foreach (string plotDataName in data.Keys)
            {
                PlotData plotData = data[plotDataName];
                plotData.Plot(g, xAxisMin, xAxisMax, yAxisMin, yAxisMax, plotArea, drawScale, dataError.ContainsKey(plotDataName) ? dataError[plotDataName] : null);
            }
        }

        private void DrawXAxisLabels(Graphics g)
        {
            if (showXAxisLabels && xAxisLabelsStep != 0)
            {
                int m = (int)((xAxisLabelsMax - xAxisLabelsMin + 0.1 * xAxisLabelsStep) / xAxisLabelsStep);
                float v = xAxisLabelsMin;

                Brush brush = new SolidBrush(axisLabelsColor);
                Pen axPen = new Pen(frameColor, frameThickness * drawScale);

                string format = "{0,-" + xAxisLabelsLength + ":F" + xAxisLabelsDigits + "}";
                StringFormat sFormat = new StringFormat();
                sFormat.Alignment = StringAlignment.Center;

                string label = String.Format(format, v);
                int n = (xAxisLabelsPow != 0) ? m : m + 1; //Leave space for labels power if it will drawn

                int x = (int)((v - xAxisLabelsMin) / (xAxisLabelsMax - xAxisLabelsMin) * plotArea.Width + plotArea.Left);
                int emptyLabelPos = x;
                for (int i = 0; i < n; i++)
                {
                    x = (int)((v - xAxisLabelsMin) / (xAxisLabelsMax - xAxisLabelsMin) * plotArea.Width + plotArea.Left);
                    //Check if it will not overlapp with previous tick label
                    if ((i == 0 || emptyLabelPos < x))
                    {
                        g.DrawString(label, xAxisLabelsFont, brush, x, this.plotArea.Bottom + axisLinesLength * drawScale + (int)(g.MeasureString("10", xAxisLabelsFont).Height / 3), sFormat);
                        emptyLabelPos = x + (int)g.MeasureString(label, xAxisLabelsFont).Width * 4 / 3;
                    }

                    if (showAxisLines)
                    {
                        g.DrawLine(axPen, x, this.plotArea.Bottom, x, this.plotArea.Bottom + (int)(axisLinesLength * drawScale));
                    }

                    v += xAxisLabelsStep;
                    label = String.Format(format, v);

                }

                if (xAxisLabelsPow != 0)
                {
                    x = (int)((v - xAxisLabelsMin) / (xAxisLabelsMax - xAxisLabelsMin) * plotArea.Width + plotArea.Left);
                    x -= (int)g.MeasureString("10", xAxisLabelsFont).Width;
                    g.DrawString("x10", xAxisLabelsFont, brush, x, this.plotArea.Bottom + axisLinesLength * drawScale + (int)(g.MeasureString("10", xAxisLabelsFont).Height / 3), sFormat);
                    label = xAxisLabelsPow.ToString();
                    Font supScrtiptFont = new Font(xAxisLabelsFont.FontFamily, xAxisLabelsFont.Size - 1, xAxisLabelsFont.Style);
                    g.DrawString(label, supScrtiptFont, brush, x + g.MeasureString("x1", xAxisLabelsFont).Width, this.plotArea.Bottom + axisLinesLength * drawScale, sFormat);
                }

                SizeF labelSize = g.MeasureString(xAxisText, xAxisLabelsFont);
                float xText = (this.plotArea.Left + this.plotArea.Right) / 2 - labelSize.Width / 2;
                float yText = this.plotArea.Bottom + axisLinesLength * drawScale + (int)(g.MeasureString("10", xAxisLabelsFont).Height / 3) + labelSize.Height + 2;
                g.DrawString(xAxisText, this.axisTitlesFont, new SolidBrush(this.axisTitlesColor), new PointF(xText, yText));

                if (gridLines)
                {
                    v = xAxisLabelsMin;
                    Pen gridPen = new Pen(gridColor, gridThickness * drawScale);
                    gridPen.DashStyle = gridDashStyle;
                    x = this.plotArea.Left;
                    for (int i = 1; i < m + 1; i++)
                    {
                        x = (int)((v - xAxisLabelsMin) / (xAxisLabelsMax - xAxisLabelsMin) * plotArea.Width + plotArea.Left);
                        g.DrawLine(gridPen, x, this.plotArea.Bottom, x, this.plotArea.Top);
                        v += xAxisLabelsStep;
                    }
                }
            }
        }

        private void DrawYAxisLabels(Graphics g)
        {

            if (showYAxisLabels && yAxisLabelsStep != 0)
            {
                int m = (int)((yAxisLabelsMax - yAxisLabelsMin + 0.1 * yAxisLabelsStep) / yAxisLabelsStep); //Adding 0.1*step to account for rounding errors
                float v = yAxisLabelsMin;

                Brush brush = new SolidBrush(axisLabelsColor);
                Pen axPen = new Pen(frameColor, frameThickness * drawScale);

                string format = "{0," + yAxisLabelsLength + ":F" + yAxisLabelsDigits + "}";

                StringFormat sFormat = new StringFormat();
                sFormat.Alignment = StringAlignment.Far;
                sFormat.LineAlignment = StringAlignment.Center;

                string label = String.Format(format, v);
                SizeF size;
                int n = (yAxisLabelsPow != 0) ? m : m + 1; //Leave space for labels power if it will drawn
                int emptyLabelPos = plotArea.Bottom;
                int y = plotArea.Bottom;
                size = g.MeasureString(label, yAxisLabelsFont);
                for (int i = 0; i < n; i++)
                {

                    y = (int)(-(v - yAxisLabelsMin) / (yAxisLabelsMax - yAxisLabelsMin) * plotArea.Height + plotArea.Bottom);
                    //Check if it will not overlapp with previous tick label
                    if ((i == 0 || emptyLabelPos >= y))
                    {
                        g.DrawString(label, yAxisLabelsFont, brush, this.plotArea.Left - (int)(axisLinesLength * drawScale) - size.Width - 2, y - size.Height / 2);

                        if (showAxisLines)
                        {
                            g.DrawLine(axPen, this.plotArea.Left, y, this.plotArea.Left - (int)(axisLinesLength * drawScale), y);
                        }

                        emptyLabelPos = y - (int)(size.Height * 3 / 2);
                    }

                    v += yAxisLabelsStep;

                    label = String.Format(format, v);
                }

                if (yAxisLabelsPow != 0)
                {
                    y = (int)(-(v - yAxisLabelsMin) / (yAxisLabelsMax - yAxisLabelsMin) * plotArea.Height + plotArea.Bottom);
                    label = yAxisLabelsPow.ToString();
                    size = g.MeasureString("x10" + label, xAxisLabelsFont);
                    g.DrawString("x10", yAxisLabelsFont, brush, this.plotArea.Left - (int)(axisLinesLength * drawScale) - size.Width, y);

                    Font supScrtiptFont = new Font(yAxisLabelsFont.FontFamily, yAxisLabelsFont.Size - 1, yAxisLabelsFont.Style);
                    size = g.MeasureString(label, yAxisLabelsFont);
                    g.DrawString(label, supScrtiptFont, brush, this.plotArea.Left - (int)(axisLinesLength * drawScale) - size.Width + 2, y - size.Height / 3);
                }

                SizeF yTextSize = g.MeasureString(yAxisText, axisTitlesFont);

                if (yAxisTextVertical)
                {
                    float yText = this.plotArea.Height / 2 - yTextSize.Width / 2;

                    g.RotateTransform(-90);
                    g.DrawString(yAxisText, this.axisTitlesFont, new SolidBrush(this.axisTitlesColor), -yText, yTextSize.Height + (int)(internalMargins * drawScale), sFormat);
                    g.ResetTransform();
                }
                else
                {
                    float xText = this.plotArea.Left - (int)(axisLinesLength * drawScale);
                    float yText = this.plotArea.Top - yTextSize.Height;
                    g.DrawString(yAxisText, this.axisTitlesFont, new SolidBrush(this.axisTitlesColor), xText, yText, sFormat);
                }

                //draw grid lines
                if (gridLines)
                {
                    Pen gridPen = new Pen(gridColor, gridThickness * drawScale);
                    gridPen.DashStyle = gridDashStyle;
                    v = yAxisLabelsMin;
                    for (int i = 1; i < m + 1; i++)
                    {
                        y = (int)(-(v - yAxisLabelsMin) / (yAxisLabelsMax - yAxisLabelsMin) * plotArea.Height + plotArea.Bottom);
                        g.DrawLine(gridPen, this.plotArea.Left, y, this.plotArea.Right, y);
                        v += yAxisLabelsStep;
                    }
                }
            }
        }

        private void DrawTitle(Graphics g)
        {
            SizeF textSize = g.MeasureString(title, titleFont);
            if (textSize.Width > plotArea.Width)
            {
                title = title.Substring(0, (int)(plotArea.Width / (textSize.Width) * title.Length));
                textSize = g.MeasureString(title, titleFont);
            }
            int y = (int)((internalMargins + 2) * drawScale);
            int x = plotArea.Width / 2 - (int)(textSize.Width / 2) + plotArea.Left;
            g.DrawString(title, titleFont, new SolidBrush(this.axisTitlesColor), new PointF(x, y));
            if (!String.IsNullOrEmpty(subTitle))
            {
                y += (int)textSize.Height + (int)(2 * drawScale);

                textSize = g.MeasureString(subTitle, subTitleFont);
                if (textSize.Width > plotArea.Width)
                {
                    subTitle = subTitle.Substring(0, (int)(plotArea.Width / (textSize.Width) * subTitle.Length));
                    textSize = g.MeasureString(subTitle, subTitleFont);
                }
                x = plotArea.Width / 2 - (int)(textSize.Width / 2) + plotArea.Left;
                g.DrawString(subTitle, subTitleFont, new SolidBrush(this.axisTitlesColor), new PointF(x, y));
            }
        }

        private void DrawSelection(Graphics g)
        {
            Point P1 = Point.Empty, P2 = Point.Empty;
            P1.X = (int)((selectionP1.X - xAxisMin) / (xAxisMax - xAxisMin) * plotArea.Width) + plotArea.X;
            P1.Y = plotArea.Bottom - (int)((selectionP1.Y - yAxisMin) / (yAxisMax - yAxisMin) * plotArea.Height);
            P2.X = (int)((selectionP2.X - xAxisMin) / (xAxisMax - xAxisMin) * plotArea.Width) + plotArea.X;
            P2.Y = plotArea.Bottom - (int)((selectionP2.Y - yAxisMin) / (yAxisMax - yAxisMin) * plotArea.Height);
            int selWidth = P1.X <= P2.X ? P2.X - P1.X : P1.X - P2.X;
            int selHeight = P1.Y <= P2.Y ? P2.Y - P1.Y : P1.Y - P2.Y;

            if (selHeight >= minSelectionSize.Height && selWidth >= minSelectionSize.Width)
            {
                int selStartX = P1.X <= P2.X ? P1.X : P2.X;
                int selStartY = P1.Y <= P2.Y ? P1.Y : P2.Y;

                g.DrawRectangle(new Pen(selectionColor, selectionPenWidth) { DashStyle = selctionLineStyle }, selStartX, selStartY, selWidth, selHeight);
            }
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            DrawContent();
        }

        public void DrawContent(ref Bitmap bmp, float scale = 1)
        {
            drawScale = scale;

            AdjustLimits();

            Graphics g = Graphics.FromImage(bmp);
            Rectangle clientArea = new Rectangle(0, 0, bmp.Width, bmp.Height);
            InitSizes(clientArea, g);
            g.FillRectangle(new SolidBrush(this.BackColor), clientArea);
            g.FillRectangle(new SolidBrush(this.plotBkColor), this.plotArea);

            DrawXAxisLabels(g);
            DrawYAxisLabels(g);
            DrawSelection(g);
            DrawData(g);
            if (showTitle)
            {
                DrawTitle(g);
            }

            if (frame)
            {
                g.DrawRectangle(new Pen(frameColor, frameThickness * drawScale), this.plotArea);
            }
#if DEMO
            SizeF textSize = g.MeasureString("LybraFit Demo", new Font("Arial", 72));
            g.DrawString("LybraFit Demo", new Font("Arial", 72), new SolidBrush(Color.FromArgb(170, 100, 100, 100)), new PointF(plotArea.Left, plotArea.Top));
            g.DrawString("LybraFit Demo", new Font("Arial", 72), new SolidBrush(Color.FromArgb(170, 100, 100, 100)), new PointF(plotArea.Left + textSize.Width / 2, plotArea.Top + plotArea.Height / 2 - textSize.Height / 2));
            g.DrawString("LybraFit Demo", new Font("Arial", 72), new SolidBrush(Color.FromArgb(170, 100, 100, 100)), new PointF(plotArea.Left + textSize.Width, plotArea.Bottom - textSize.Height));
#endif

            drawScale = 1;
        }

        private void DrawContent()
        {
            SuspendLayout();

            if (!isInSelection)
            {
                AdjustLimits();
                bmp = new Bitmap(this.Width, this.Height);
                DrawContent(ref bmp);
                frontBuffer = new Bitmap(bmp);
            }

            ResumeLayout();

            GC.Collect();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            selectionP1.X = (float)(e.Location.X - plotArea.X) / plotArea.Width * (xAxisMax - xAxisMin) + xAxisMin;
            selectionP1.Y = (float)(plotArea.Bottom - e.Location.Y) / plotArea.Height * (yAxisMax - yAxisMin) + yAxisMin;
            selectionP2 = selectionP1;
            DrawContent();
            isInSelection = true;

            pictureBox.Refresh();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isInSelection)
            {
                selectionP2.X = (float)(e.Location.X - plotArea.X) / plotArea.Width * (xAxisMax - xAxisMin) + xAxisMin;
                selectionP2.Y = (float)(plotArea.Bottom - e.Location.Y) / plotArea.Height * (yAxisMax - yAxisMin) + yAxisMin;
                DrawContent();
                pictureBox.Refresh();
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isInSelection = false;
            DrawContent();
            pictureBox.Refresh();
            if (OnSelectionChange != null)
            {
                float minX = SelectionStart.X < SelectionEnd.X ? SelectionStart.X : SelectionEnd.X;
                float maxX = SelectionEnd.X < SelectionStart.X ? SelectionStart.X : SelectionEnd.X;
                float minY = SelectionStart.Y < SelectionEnd.Y ? SelectionStart.Y : SelectionEnd.Y;
                float maxY = SelectionEnd.Y < SelectionStart.Y ? SelectionStart.Y : SelectionEnd.Y;

                float minWidthReal = (float)MinSelectionSize.Width / plotArea.Width * (xAxisMax - xAxisMin);
                float minHeightReal = (float)MinSelectionSize.Height / plotArea.Height * (yAxisMax - yAxisMin);
                if (maxX - minX < minWidthReal || maxY - minY < minHeightReal)
                {
                    //TODO Add call here to point click only, may be selecting closest data point
                    return;
                }
                OnSelectionChange(this, new SelectionEventArgs() { MinX = minX, MaxX = maxX, MinY = minY, MaxY = maxY });
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            DrawContent();
            base.OnPaint(e);
            if (this.plotArea.Width < minWidth || this.plotArea.Height < minHeigth) return;
            e.Graphics.DrawImage(frontBuffer, 0, 0);
            DrawSelection(e.Graphics);
        }
    }
}
