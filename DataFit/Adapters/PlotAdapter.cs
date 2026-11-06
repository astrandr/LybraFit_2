//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Windows.Forms;

using UIBase;
using GraphControlLib;
using LybraFit.Common;

namespace LybraFit
{
    public class PlotAdapter : Adapter
    {
        PlotData plotData;
        PlotData plotFit;

        public PlotAdapter(Control control, ViewController controller) : base(control, controller) { }

        private void UpdateGraphSettings(Document doc)
        {
            GraphControl graph = (GraphControl)this.control;

            float min = 0, max = 0, minFit = 0, maxFit = 0, step = 0;
            int pow = 0, minPow = 2, count = 10;

            pow = minPow;

            if (Configuration.GraphSettings.XLimitAuto && doc.X.Length > 0)
            {
                Utility.GetArrayLimits(doc.X, ref min, ref max);
                Utility.GetRoundedLimits2(ref min, ref max, ref count, ref step, ref pow);
                Configuration.GraphSettings.XMin = min;
                Configuration.GraphSettings.XMax = max;
                Configuration.GraphSettings.XStep = step;
                Configuration.GraphSettings.XPow = pow;
            }
            graph.XAxisLabelsFont = Configuration.GraphSettings.TickLabelsFont;
            graph.XAxisLabelsMin = Configuration.GraphSettings.XMin;
            graph.XAxisLabelsMax = Configuration.GraphSettings.XMax;
            graph.XAxisLabelsStep = Configuration.GraphSettings.XStep;
            var s = ((decimal)(Configuration.GraphSettings.XStep)).ToString();
            Configuration.GraphSettings.xDigits = s.IndexOf(".") < 0 ? 0 : s.Length - s.IndexOf(".")-1;
            graph.XAxisLabelsDigits = (int)Configuration.GraphSettings.xDigits;
            graph.XAxisLabelsPow = Configuration.GraphSettings.XPow;

            if (Configuration.GraphSettings.YLimitAuto && doc.Y.Length > 0)
            {
                Utility.GetArrayLimits(doc.Y, ref min, ref max);
                if (doc.FitDataY != null)
                {
                    Utility.GetArrayLimits(doc.FitDataY, ref minFit, ref maxFit);
                    min = min < minFit ? min : minFit;
                    max = max > maxFit ? max : maxFit;
                }
                Utility.GetRoundedLimits2(ref min, ref max, ref count, ref step, ref pow);
                Configuration.GraphSettings.YMin = min;
                Configuration.GraphSettings.YMax = max;
                Configuration.GraphSettings.YStep = step;
                Configuration.GraphSettings.YPow = pow;
            }
            graph.YAxisLabelsFont = Configuration.GraphSettings.TickLabelsFont;
            graph.YAxisLabelsMin = Configuration.GraphSettings.YMin;
            graph.YAxisLabelsMax = Configuration.GraphSettings.YMax;
            graph.YAxisLabelsStep = Configuration.GraphSettings.YStep;
            s = ((decimal)(Configuration.GraphSettings.YStep)).ToString();
            Configuration.GraphSettings.yDigits = s.IndexOf(".") < 0 ? 0 : s.Length - s.IndexOf(".") - 1;
            graph.YAxisLabelsDigits = (int)Configuration.GraphSettings.yDigits;
            graph.YAxisLabelsPow = Configuration.GraphSettings.YPow;
            graph.YAxisLabelsLength = Configuration.GraphSettings.YMax.ToString().Length;
            if (graph.YAxisLabelsLength < Configuration.GraphSettings.YMin.ToString().Length)
            {
                graph.YAxisLabelsLength = Configuration.GraphSettings.YMin.ToString().Length;
            }

            graph.BackColor = Configuration.GraphSettings.BackgroundColor;
            graph.PlotBkColor = Configuration.GraphSettings.PlotAreaColor;
            graph.FrameColor = Configuration.GraphSettings.AxisColor;

            graph.FrameThickness = Configuration.GraphSettings.AxisThickness;
            graph.GridLines = Configuration.GraphSettings.ShowGridLines;
            graph.GridThickness = Configuration.GraphSettings.GridThickness;
            graph.GridDashStyle = Configuration.GraphSettings.GridStyle;
            graph.GridColor = Configuration.GraphSettings.GridColor;

            graph.AxisTitlesColor = Configuration.GraphSettings.AxisLabelsColor;
            graph.AxisTitlesFont = Configuration.GraphSettings.AxisLabelsFont;

            graph.AxisLabelsColor = Configuration.GraphSettings.TickLabelsColor;
            graph.XAxisLabelsFont = Configuration.GraphSettings.TickLabelsFont;
            graph.YAxisLabelsFont = Configuration.GraphSettings.TickLabelsFont;

            if (plotData != null)
            {
                plotData.Color = Configuration.GraphSettings.DataColor;
                plotData.LineStyle = Configuration.GraphSettings.DataLineStyle;
                plotData.Width = Configuration.GraphSettings.DataLineWidth;
                plotData.DrawLine = Configuration.GraphSettings.ConnectDataPoints;
                plotData.PointType = Configuration.GraphSettings.DataPointType;
                plotData.PointSize = (int)Configuration.GraphSettings.DataPointSize;
            }

            if (plotFit != null)
            {
                plotFit.Color = Configuration.GraphSettings.FitLineColor;
                plotFit.LineStyle = Configuration.GraphSettings.FitLineStyle;
                plotFit.Width = Configuration.GraphSettings.FitLineWidth;
            }

            graph.YAxisTextVertical = Configuration.GraphSettings.VerticalTextYAxis;

            UpdateTitle(doc);
        }

        private void UpdateAxisLables(Document doc)
        {
            GraphControl graph = (GraphControl)this.control;
            graph.XAxisText = doc.GridColumnSettings[0].Name;
            graph.YAxisText = doc.GridColumnSettings[1].Name;
            graph.Refresh();
        }

        private void UpdateTitle(Document doc)
        {
            GraphControl graph = (GraphControl)this.control;
            graph.ShowTitle = Configuration.GraphSettings.ShowTitle;
            if (Configuration.GraphSettings.ShowTitle)
            {
                string title = Configuration.GraphSettings.Title;
                if (Configuration.GraphSettings.ShowModel && doc.CurrentModel != null)
                {
                    title += ", model:" + doc.CurrentModel.ModelName;
                    if (Configuration.GraphSettings.ShowRegressionResult && doc.CurrentModel != null)
                    {
                        title += ", Std.Err:" + doc.CurrentModel.FitResultStdError;
                    }
                }
                graph.Title = title;
                graph.TitleFont = Configuration.GraphSettings.TitleFont;
            }
        }

        /// <summary>
        /// NewDataFile - processes loading new data file in the document. Adjust graph limits to fit the data and removes any previous fit plot
        /// </summary>
        /// <param name="doc"></param>
        private void NewData(Document doc)
        {
            GraphControl graph = (GraphControl)this.control;

            plotData = new PlotData(doc.X, doc.Y, Configuration.GraphSettings.DataColor, Configuration.GraphSettings.DataLineStyle, (int)Configuration.GraphSettings.DataPointSize, (uint)Configuration.GraphSettings.DataLineWidth, PlotDataPointType.Point);

            UpdateGraphSettings(doc);

            graph.RemovePlotData("Fit");
            graph.RemovePlotData("FitLo");
            graph.RemovePlotData("FitHi");

            graph.SetPlotData("Data", plotData);

            if (doc.HasErrorY)
            {
                float[] ErrYData = Array.ConvertAll(doc.ErrY, v => (float)v);
                graph.SetPlotDataError("Data", ErrYData);
            }
            else
            {
                graph.RemovePlotDataError("Data");
            }

            graph.XAxisText = doc.ColumnNames[0];
            graph.YAxisText = doc.ColumnNames[1];

            UpdateTitle(doc);

            graph.Refresh();
        }

        /// <summary>
        /// NewFilter - changes plot on new data limits
        /// </summary>
        /// <param name="doc"></param>
        private void NewFilter(Document doc)
        {
            GraphControl graph = (GraphControl)this.control;
 
            plotData = new PlotData(doc.X, doc.Y, Configuration.GraphSettings.DataColor, Configuration.GraphSettings.DataLineStyle, (int)Configuration.GraphSettings.DataPointSize, (uint)Configuration.GraphSettings.DataLineWidth, PlotDataPointType.Point);
            graph.SetPlotData("Data", plotData);
            if (doc.HasErrorY)
            {
                float[] errYData = Array.ConvertAll(doc.ErrY, v => (float)v);
                graph.SetPlotDataError("Data", errYData);
            }

            UpdateGraphSettings(doc);

            if (doc.CurrentModel != null)
            {
                try
                {
                    graph.RemovePlotData("Fit");
                    plotFit = new PlotData(doc.FitDataX, doc.FitDataY, Configuration.GraphSettings.FitLineColor, Configuration.GraphSettings.FitLineStyle, 0, (uint)Configuration.GraphSettings.FitLineWidth, PlotDataPointType.FilledSquare);
                    graph.SetPlotData("Fit", plotFit);

                }
                catch (Exception)
                {
                    //Add logging here
                    MessageBox.Show("Out of limits error");
                }
            }
            else
            {
                graph.RemovePlotData("Fit");
            }

            graph.Refresh();

        }

        private void NewViewFilter(ViewFilterMessage filterMessage)
        {
            GraphControl graph = (GraphControl)this.control;
            if (filterMessage.FilterItems == null || filterMessage.FilterItems.Length == 0)
            {
                //Restore the graph settings
                UpdateGraphSettings(filterMessage.Document);
                graph.Refresh();
                return;
            }

            float min = (float)filterMessage.FilterItems[0].Min;
            float max = (float)filterMessage.FilterItems[0].Max;

            float step = 0;
            int pow = 0, minPow = 2, count = 10;

            pow = minPow;

            Utility.GetRoundedLimits2(ref min, ref max, ref count, ref step, ref pow);
            graph.XAxisLabelsMin = min;
            graph.XAxisLabelsMax = max;
            graph.XAxisLabelsStep = step;
            graph.XAxisLabelsPow = pow;
            string s = step.ToString();
            graph.XAxisLabelsDigits = s.IndexOf(".") < 0 ? 0 : s.Length - s.IndexOf(".") - 1;

            if (filterMessage.FilterItems.Length > 1)
            {
                min = (float)filterMessage.FilterItems[1].Min;
                max = (float)filterMessage.FilterItems[1].Max;
                Utility.GetRoundedLimits2(ref min, ref max, ref count, ref step, ref pow);

                graph.YAxisLabelsMin = (float)min;
                graph.YAxisLabelsMax = (float)max;
                graph.YAxisLabelsStep = (float)step;
                graph.YAxisLabelsPow = pow;
                s = step.ToString();
                graph.YAxisLabelsDigits = s.IndexOf(".") < 0 ? 0 : s.Length - s.IndexOf(".") - 1;
            }

            graph.Refresh();

        }

        /// <summary>
        /// NewFit processes new fit curve. Adjusts the graph limits and sets new fit plot. 
        /// </summary>
        /// <param name="doc"></param>
        private void NewFit(Document doc)
        {
            GraphControl graph = (GraphControl)this.control;

            if (doc.CurrentModel != null)
            {
                try
                {
                    graph.RemovePlotData("Fit");
                    plotFit = new PlotData(doc.FitDataX, doc.FitDataY, Configuration.GraphSettings.FitLineColor, Configuration.GraphSettings.FitLineStyle, 0, (uint)Configuration.GraphSettings.FitLineWidth, PlotDataPointType.FilledSquare);
                    graph.SetPlotData("Fit", plotFit);
                }
                catch (Exception)
                {
                    //Add logging here
                    MessageBox.Show("Out of limits error");
                }
            }
            else
            {
                graph.RemovePlotData("Fit");
            }            

            UpdateTitle(doc);
            UpdateGraphSettings(doc);

            graph.Refresh();
        }

        /// <summary>
        /// Calle dby UIBase framewark where other components sents and update message via controller
        /// </summary>
        /// <param name="update"></param>
        protected override void SynchronizeWithDocument(UpdateMessage update)
        {
            switch (update.Type)
            {
                case UpdateMessages.NEW_DATA: NewData((Document)update.Content); break;
                case UpdateMessages.DATA_CHANGED:
                case UpdateMessages.DATA_FILTERED: NewFilter((Document)update.Content); break;
                case UpdateMessages.NEW_FIT:
                case UpdateMessages.MODELS_CHANGE: NewFit((Document)update.Content); break;
                case UpdateMessages.GRAPH_SETTINGS_CHANGED: UpdateGraphSettings((Document)update.Content); ((GraphControl)control).Refresh(); break;
                case UpdateMessages.COLUMN_SETTINGS_CHANGED: UpdateAxisLables((Document)update.Content); break;
                case UpdateMessages.VIEW_FILTERED: NewViewFilter((ViewFilterMessage)update.Content); break;
            }
        }
    }
}
