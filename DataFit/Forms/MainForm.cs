//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using UIBase;
using MathLib;
using LybraFit.Forms;
using LybraFit.Common;
using FuncLib;
using System.Drawing.Drawing2D;
using AutoFormatNumber;
using GraphControlLib;
using System.Drawing.Imaging;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data;

//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
namespace LybraFit
{
    public struct UpdateMessages
    {
        public const int NEW_DATA = 0;
        public const int NEW_FIT = 1;
        public const int DATA_CHANGED = 2;
        public const int GRAPH_SETTINGS_CHANGED = 3;
        public const int COLUMN_SETTINGS_CHANGED = 4;
        public const int DATA_INSERT = 5;
        public const int DATA_DELETE = 6;
        public const int MODELS_CHANGE = 7;
        public const int DATA_FILTERED = 8;
        public const int VIEW_FILTERED = 9;
    }

    public partial class MainForm : Form
    {
        private LybraFitController controller;
        private Document doc;
        private UpdateMessage uiMessage;
        private string projectFileName = "";
        private string projectFilePath = "";
        private Dictionary<string, FitFunction> functions = new Dictionary<string, FitFunction>();
        private LybraFit.Forms.StartForm startForm = null;
        private ModelsDialog modelsDialog = null;
        private TransformDialog transformDialog = null;
        private PlotExportDialog plotExportDialog = null;
        private string xSelectionMarker = String.Empty;
        private Stack<Command> undoCommands = new Stack<Command>();
        private string mathExpression = "";
        DataGridViewCellEventHandler onChangeGridCell;
        DataGridViewRowEventHandler onGridAddedRow;
        DataGridViewRowCancelEventHandler onGridRowDeleting;
        private ExportDialog exportDialog = new ExportDialog();
        private SizeF graphReportSize = Size.Empty;
        private int graphReportResolution = Constants.STANDARD_DPI;
        private int graphReportMargins = 15;
        private int graphReportUnits = 0;

        List<Component> controlsInStates = new List<Component>();
        List<string> stateFlags = new List<string>();

        public static bool StopRegression = false;

        private delegate void NewRegressionResultHandler(double stdError);

        private readonly NewRegressionResultHandler OnNewRegressionResult = null;

        BackgroundWorker worker = null;

        double eps = 0;

        public MainForm()
        {
            Program.IsFromUser = false;

            InitializeComponent();
#if DEMO
            this.Text = Program.APPLICATION_DEMO_NAME;
#else
            this.Text = Program.APPLICATION_NAME;
#endif

            onChangeGridCell = new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView_CellEndEdit);
            onGridAddedRow = new System.Windows.Forms.DataGridViewRowEventHandler(dataGridView_UserAddedRow);
            onGridRowDeleting = new System.Windows.Forms.DataGridViewRowCancelEventHandler(dataGridView_UserDeletingRow);

            controller = new LybraFitController();
            doc = new Document();
            uiMessage = new UpdateMessage();
            uiMessage.Content = doc;
            plotExportDialog = new PlotExportDialog();
            modelsDialog = new ModelsDialog(doc);

            OnNewRegressionResult += new NewRegressionResultHandler(NewRegressionResult);

            Adapter.Attach(controller, this.dataGridView, "LybraFit.DataGridAdapter");
            Adapter.Attach(controller, this.graph, "LybraFit.PlotAdapter");
            Adapter.Attach(controller, this.dataGridModels, "LybraFit.ModelsGridAdapter");

            this.graph.OnSelectionChange += new EventHandler(DataSelectionChangesFromPlot);

            AttachGridHandlers(); //Consider if this is to be moved to the Grid adapter

            if (Program.HasDefaultGraphSettings)
            {
                Configuration.GraphSettings = Program.DefaultGraphSettings;
            }
            else
            {
                Configuration.GraphSettings.Initialize();
            }

            FindControlsForStates(this);
            FindComponentsForStates(this.contextMenuDataGrid);
            Program.IsFromUser = true;
            SetState("K0");
        }

        private void DataSelectionChangesFromPlot(object sender, EventArgs args)
        {
            SelectionEventArgs selArgs = (SelectionEventArgs)args;

            DataFilterInfo.Items[0].Min = selArgs.MinX;
            DataFilterInfo.Items[0].Max = selArgs.MaxX;
            DataFilterInfo.Items[0].Selected = true;

            DataFilterInfo.Items[1].Min = selArgs.MinY;
            DataFilterInfo.Items[1].Max = selArgs.MaxY;
            DataFilterInfo.Items[1].Selected = true;

            graph.ClearSelection();

            controller.UpdateAllUI(this, UpdateMessages.VIEW_FILTERED, new ViewFilterMessage() { Document = doc, FilterItems = DataFilterInfo.Items.ToArray() });

            SetState("K8");
        }

        private void UpdateControlsState()
        {
            foreach (Component component in controlsInStates)
            {
                if (component is Button)
                {
                    Button Button = component as Button;
                    bool enable = false;
                    foreach (string stateFlag in stateFlags)
                    {
                        if (Button.Tag.ToString().Contains(stateFlag))
                        {
                            enable = true;
                            break;
                        }
                    }
                    Button.Enabled = enable;
                }

                if (component is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = component as ToolStripDropDownItem;
                    bool enable = false;
                    foreach (string stateFlag in stateFlags)
                    {
                        if (item.Tag.ToString().Contains(stateFlag))
                        {
                            enable = true;
                            break;
                        }
                    }
                    item.Enabled = enable;
                }

                if (component is ToolStripButton)
                {
                    ToolStripButton item = component as ToolStripButton;
                    bool enable = false;
                    foreach (string stateFlag in stateFlags)
                    {
                        if (item.Tag.ToString().Contains(stateFlag))
                        {
                            enable = true;
                            break;
                        }
                    }
                    item.Enabled = enable;
                }

                if (component is TabPage)
                {
                    TabPage page = component as TabPage;
                    bool enable = false;
                    foreach (string stateFlag in stateFlags)
                    {
                        if (page.Tag.ToString().Contains(stateFlag))
                        {
                            enable = true;
                            break;
                        }
                    }
                    if (!enable && tabMain.TabPages.Contains(page)) tabMain.TabPages.Remove(page);
                    if (enable && !tabMain.TabPages.Contains(page)) tabMain.TabPages.Add(page);
                }

                if (component is ContextMenuStrip)
                {
                    ToolStripMenuItem item = component as ToolStripMenuItem;
                    bool enable = false;
                    foreach (string stateFlag in stateFlags)
                    {
                        if (item.Tag.ToString().Contains(stateFlag))
                        {
                            enable = true;
                            break;
                        }
                    }
                    item.Enabled = enable;
                }
            }
        }

        private void DisableAllStateFullControls()
        {
            foreach (Component component in controlsInStates)
            {
                if (component is Button)
                {
                    Button Button = component as Button;

                    if (Button.Tag.ToString().Contains("K"))
                    {
                        Button.Enabled = false;
                    }
                }

                if (component is ToolStripDropDownItem)
                {
                    ToolStripDropDownItem item = component as ToolStripDropDownItem;
                    if (item.Tag.ToString().Contains("K"))
                    {
                        item.Enabled = false;
                    }
                }

                if (component is ToolStripButton)
                {
                    ToolStripButton item = component as ToolStripButton;
                    if (item.Tag.ToString().Contains("K"))
                    {
                        item.Enabled = false;
                    }
                }

                if (component is TabPage)
                {
                    TabPage page = component as TabPage;
                    if (page.Tag.ToString().Contains("K"))
                    {
                        page.Enabled = false;
                    }
                }
            }
        }

        private void SetState(string stateKey)
        {
            if (!stateFlags.Contains(stateKey))
            {
                stateFlags.Add(stateKey);
                UpdateControlsState();
            }
        }

        private void RemoveState(string stateKey)
        {
            if (stateFlags.Contains(stateKey))
            {
                stateFlags.Remove(stateKey);
                UpdateControlsState();
            }
        }

        /// <summary>
        /// FindStatesByControlsName - in order name of the control to indicate state the name must be like <controlname>_S1_S2.
        ///                             For instance "Button1_S1_S4"
        /// </controlname>
        /// </summary>
        private void FindControlsForStates(Control parentControl)
        {
            foreach (Component component in parentControl.Controls)
            {
                if (component is MenuStrip)
                {
                    MenuStrip menu = component as MenuStrip;
                    foreach (ToolStripMenuItem item in menu.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("K") && !controlsInStates.Contains(item))
                        {
                            controlsInStates.Add(item);
                        }

                        foreach (ToolStripItem dropDownItem in item.DropDownItems)
                        {
                            if (dropDownItem.Tag != null && dropDownItem.Tag.ToString().Contains("K") && !controlsInStates.Contains(dropDownItem))
                            {
                                controlsInStates.Add(dropDownItem);
                            }
                        }
                    }
                }

                if (component is ToolStrip)
                {
                    ToolStrip toolStrip = component as ToolStrip;
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("K") && !controlsInStates.Contains(item))
                        {
                            controlsInStates.Add(item);
                        }
                    }
                }

                if (component is ContextMenuStrip)
                {
                    ToolStrip toolStrip = component as ToolStrip;
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("K") && !controlsInStates.Contains(item))
                        {
                            controlsInStates.Add(item);
                        }
                    }
                }

                if (component is Button)
                {
                    Button Button = component as Button;
                    if (Button.Tag != null && Button.Tag.ToString().Contains("K") && !controlsInStates.Contains(Button))
                    {
                        controlsInStates.Add(Button);
                    }
                }

                if (component is Control)
                {
                    FindControlsForStates(component as Control);
                }

                if (component is SplitContainer)
                {
                    FindControlsForStates((component as SplitContainer).Panel1);
                    FindControlsForStates((component as SplitContainer).Panel2);
                }

                if (component is TabPage)
                {
                    TabPage page = component as TabPage;
                    if (page.Tag != null && page.Tag.ToString().Contains("K") && !controlsInStates.Contains(page))
                    {
                        controlsInStates.Add(page);
                    }
                }
            }
        }

        private void FindComponentsForStates(Component parentComponent)
        {
            foreach (Component component in parentComponent.Container.Components)
            {
                if (component is MenuStrip)
                {
                    MenuStrip menu = component as MenuStrip;
                    foreach (ToolStripMenuItem item in menu.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("K") && !controlsInStates.Contains(item))
                        {
                            controlsInStates.Add(item);
                        }

                        foreach (ToolStripItem dropDownItem in item.DropDownItems)
                        {
                            if (dropDownItem.Tag != null && dropDownItem.Tag.ToString().Contains("K") && !controlsInStates.Contains(dropDownItem))
                            {
                                controlsInStates.Add(dropDownItem);
                            }
                        }
                    }
                }

                if (component is ToolStrip)
                {
                    ToolStrip toolStrip = component as ToolStrip;
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("K") && !controlsInStates.Contains(item))
                        {
                            controlsInStates.Add(item);
                        }
                    }
                }

                if (component is ContextMenuStrip)
                {
                    ToolStrip toolStrip = component as ToolStrip;
                    foreach (ToolStripItem item in toolStrip.Items)
                    {
                        if (item.Tag != null && item.Tag.ToString().Contains("K") && !controlsInStates.Contains(item))
                        {
                            controlsInStates.Add(item);
                        }
                    }
                }

                if (component is Button)
                {
                    Button Button = component as Button;
                    if (Button.Tag != null && Button.Tag.ToString().Contains("K") && !controlsInStates.Contains(Button))
                    {
                        controlsInStates.Add(Button);
                    }
                }

                if (component is Control)
                {
                    FindControlsForStates(component as Control);
                }

                if (component is SplitContainer)
                {
                    FindControlsForStates((component as SplitContainer).Panel1);
                    FindControlsForStates((component as SplitContainer).Panel2);
                }

                if (component is TabPage)
                {
                    TabPage page = component as TabPage;
                    if (page.Tag != null && page.Tag.ToString().Contains("K") && !controlsInStates.Contains(page))
                    {
                        controlsInStates.Add(page);
                    }
                }
            }
        }



        //Consider if this is to be moved to the Grid adapter
        private void AttachGridHandlers()
        {
            this.dataGridView.CellValueChanged += onChangeGridCell;
            this.dataGridView.UserAddedRow += onGridAddedRow;
            this.dataGridView.UserDeletingRow += onGridRowDeleting;
        }

        private void DettachGridHandlers()
        {
            this.dataGridView.CellValueChanged -= onChangeGridCell;
            this.dataGridView.UserAddedRow -= onGridAddedRow;
            this.dataGridView.UserDeletingRow -= onGridRowDeleting;
        }

        private void btnCalcBestFit_Click(object sender, EventArgs e)
        {
            if (dataGridModels.CurrentRow == null)
            {
                MessageBox.Show("No model is selected");
                return;
            }
            if (doc.Data.Count == 0)
            {
                MessageBox.Show("No data exists.");
                return;
            }
            Program.IsFromUser = false;
            List<int> flags = new List<int>();
            UpdateDocumentParameters();
            for (int i = 0; i < dataGridParameters.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = ((DataGridViewCheckBoxCell)dataGridParameters.Rows[i].Cells[1]);
                if ((bool)cell.EditedFormattedValue)
                {
                    flags.Add(i);
                }
            }
            double[] X = doc.X;
            double[] Y = doc.Y;
            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(doc.CurrentModel.ModelFunction.CalcProcedure);
            int[] flagsArr = flags.ToArray();

            StopRegression = false;
            if (worker == null)
            {
                worker = new BackgroundWorker();
                worker.DoWork += new DoWorkEventHandler(BackgroundRegression);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RegressionCompleted);
            }

            mainMenu.Enabled = false;
            toolStripMain.Enabled = false;
            btnStop.Enabled = true;
            btnCalcBestFit.Enabled = false;
            tabPageData.Enabled = false;
            btnPlot.Enabled = false;
            btnInitParameters.Enabled = false;
            btnInitParameters.Refresh();
            dataGridModels.Enabled = false;
            dataGridParameters.Enabled = false;

            Cursor = Cursors.WaitCursor;

            worker.RunWorkerAsync(new object[] { X, Y, doc.Parameters, flagsArr, calcFunc });
        }


        private void BackgroundRegression(object sender, DoWorkEventArgs args)
        {
            object[] prm = (object[])args.Argument;
            eps = EnhancedOptimize((double[])prm[0], (double[])prm[1], (double[])prm[2], (int[])prm[3], (LeastSquare.CalculateValuesDalegate)prm[4]);
        }

        private void RegressionCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            UpdateParametersGridFromDocument();
            UpdateDocumentFit();
            doc.CurrentModel.LastFitParameters = doc.Parameters;
            doc.CurrentModel.IsInitialized = true;
            lblStdError.Text = doc.CurrentModel.FitResultStdError.ToString();
            //lblChiSquared.Text = doc.CurrentModel.FitResultChiSquare.ToString();

            mainMenu.Enabled = true;
            toolStripMain.Enabled = true;
            btnStop.Enabled = false;
            btnCalcBestFit.Enabled = true;
            tabPageData.Enabled = true;
            btnPlot.Enabled = true;
            btnInitParameters.Enabled = doc.CurrentModel.ModelFunction.CanInitParameters;
            btnInitParameters.Refresh();
            btnCalcBestFit.Focus();
            dataGridModels.Enabled = true;
            dataGridParameters.Enabled = true;

            //if model is user store the parameters to the library function so next time it is added it is added with these parameters
            if (Program.UserModels.ContainsKey(doc.CurrentModel.ModelName))
            {
                if (!Program.UserModelsInitialParameter.ContainsKey(doc.CurrentModel.ModelName))
                {
                    Program.UserModelsInitialParameter.Add(doc.CurrentModel.ModelName, null);
                }
                Program.UserModelsInitialParameter[doc.CurrentModel.ModelName] = doc.CurrentModel.LastFitParameters;
            }

            controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);

            Cursor = Cursors.Default;

            Program.IsFromUser = true;
        }

        private void NewRegressionResult(double stdError)
        {
            if (this.InvokeRequired)
            {
                NewRegressionResultHandler d = new NewRegressionResultHandler(NewRegressionResult);
                this.Invoke(d, new object[] { stdError });
            }
            else
            {
                lblStdError.Text = stdError.ToString();
            }
        }

        private double EnhancedOptimize(double[] X, double[] Y, double[] P, int[] flagsArr, LeastSquare.CalculateValuesDalegate calcFunc)
        {
            LeastSquare lsAllParms = new LeastSquare(X, Y, doc.ErrY, doc.Parameters, flagsArr, calcFunc, null);

            double[] bestParameters = new double[doc.Parameters.Length];
            doc.Parameters.CopyTo(bestParameters, 0);
            double initialEps = lsAllParms.CalculateStdError();

            double epsAllParameters = lsAllParms.Optimize(0.01f, 100);

            double minEps, prevMinEps;
            if (epsAllParameters < initialEps)
            {
                doc.Parameters.CopyTo(bestParameters, 0);
                minEps = epsAllParameters;
            }
            else
            {
                minEps = initialEps;
            }

            int iterations = 0, maxIterations = 1000;
            bool foundBetterFit;
            do
            {
                prevMinEps = minEps;
                foundBetterFit = false;
                //TODO: Experiment with performance moving this into parallel processing
                var paramCopy = new double[doc.Parameters.Length];
                Array.Copy(doc.Parameters, paramCopy, doc.Parameters.Length);
                for (int i = 0; i < flagsArr.Length; i++)
                {
                    Array.Copy(paramCopy, doc.Parameters, doc.Parameters.Length);
                    var ls = new LeastSquare(X, Y, doc.ErrY, doc.Parameters, new int[] { flagsArr[i] }, calcFunc, null);
                    double eps = ls.Optimize(0.1f, 100);
                    if (eps < minEps)
                    {
                        minEps = eps;
                        doc.Parameters.CopyTo(bestParameters, 0);
                        foundBetterFit = true;
                    }
                }
                iterations++;
                if (foundBetterFit)
                {
                    Array.Copy(bestParameters, paramCopy, doc.Parameters.Length);
                    Array.Copy(paramCopy, doc.Parameters, doc.Parameters.Length);
                    double cEps = lsAllParms.Optimize(0.01f, 100);
                    if (cEps < minEps)
                    {
                        minEps = cEps;
                        doc.Parameters.CopyTo(bestParameters, 0);
                    }
                    OnNewRegressionResult(minEps);
                }
            } while (!StopRegression && minEps < epsAllParameters && foundBetterFit && iterations <= maxIterations);

            var lsq = new LeastSquare(X, Y, doc.ErrY, doc.Parameters, flagsArr, calcFunc, null);

            if (doc.CurrentModel.LastFitParameters == null)
            {
                doc.CurrentModel.LastFitParameters = new double[doc.Parameters.Length];
            }
            doc.Parameters.CopyTo(doc.CurrentModel.LastFitParameters, 0);
            try
            {
                lsq.GetFitError(0.1f, out doc.CurrentModel.SigmaParameters, out doc.CurrentModel.SigmaY);
                doc.CurrentModel.FitResultStdError = lsq.LastStdError;
                doc.CurrentModel.FitResultChiSquare = lsq.LastChiSquare;
            }
            catch
            {
                doc.CurrentModel.SigmaParameters = new double[doc.CurrentModel.LastFitParameters.Length];
                for (int i = 0; i < doc.CurrentModel.SigmaParameters.Length; i++)
                {
                    doc.CurrentModel.SigmaParameters[i] = double.NaN;
                }
            };

            return minEps;
        }

        /// <summary>
        /// DisplayCurrentModel - updates the plot and parameters grid according to current active model of the document
        /// </summary>
        private void SetCurrentModel(string modelName)
        {
            Program.IsFromUser = false;

            if (String.IsNullOrEmpty(modelName) || !doc.Models.ContainsKey(modelName))
            {
                doc.CurrentModel = null;
                btnInitParameters.Enabled = false;
                btnInitParameters.Refresh();
                btnPlot.Enabled = false;
                btnCalcBestFit.Enabled = false;
                UpdateMathExpressionPreview("");
                RefreshParametersGrid();
                RemoveState("K5");
                Program.IsFromUser = true;
                return;
            }

            doc.CurrentModel = doc.Models[modelName];
            if (doc.CurrentModel.LastFitParameters != null)
            {
                doc.Parameters = doc.CurrentModel.LastFitParameters.ToArray();
            }
            btnInitParameters.Enabled = doc.CurrentModel.ModelFunction.CanInitParameters;
            btnInitParameters.Refresh();
            btnPlot.Enabled = true;
            btnCalcBestFit.Enabled = true;
            UpdateMathExpressionPreview(doc.CurrentModel.ModelFunction.Description);
            RefreshParametersGrid();


            double[] X = doc.X;
            double[] Y = doc.Y;
            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(doc.CurrentModel.ModelFunction.CalcProcedure);
            int[] flagsArr = new int[doc.CurrentModel.ModelFunction.Parameters.Length]; for (int i = 0; i < flagsArr.Length; i++) { flagsArr[i] = i; };

            LeastSquare ls = new LeastSquare(X, Y, doc.ErrY, doc.Parameters, flagsArr, calcFunc, null);
            try
            {
                double eps = ls.CalculateStdError();

                //Calculate sigma parameters and fit plot error
                if (doc.CurrentModel.IsInitialized)
                {
                    try
                    {
                        ls.GetFitErrorAllParameters(0.01f, out doc.CurrentModel.SigmaParameters, out doc.CurrentModel.SigmaY);
                    }
                    catch
                    {

                    }
                }

                UpdateDocumentFit();
                doc.CurrentModel.FitResultStdError = eps;
                lblStdError.Text = eps.ToString();
                controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SetState("K5");
            Program.IsFromUser = true;
        }


        private void btnPlot_Click(object sender, EventArgs e)
        {
            if (dataGridModels.SelectedRows == null || dataGridModels.SelectedRows.Count == 0)
            {
                if (Program.IsFromUser)
                {
                    MessageBox.Show("No model is selected");
                }
                return;
            }

            if (doc.Data.Count == 0)
            {
                if (Program.IsFromUser)
                {
                    MessageBox.Show("No data exists.");
                }
                return;
            }

            Program.IsFromUser = false;

            UpdateDocumentParameters();

            UpdateFitGoodness();

            UpdateDocumentFit();

            //if model is user store the parameters to the library function so next time it is added it is added with these parameters
            if (Program.UserModels.ContainsKey(doc.CurrentModel.ModelName))
            {
                if (!Program.UserModelsInitialParameter.ContainsKey(doc.CurrentModel.ModelName))
                {
                    Program.UserModelsInitialParameter.Add(doc.CurrentModel.ModelName, null);
                }
                Program.UserModelsInitialParameter[doc.CurrentModel.ModelName] = doc.CurrentModel.LastFitParameters;
            }

            controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);

            Program.IsFromUser = true;
        }

        private void UpdateFitGoodness()
        {
            double[] X = doc.X;
            double[] Y = doc.Y;
            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(doc.CurrentModel.ModelFunction.CalcProcedure);
            int[] flagsArr = new int[doc.Parameters.Length]; for (int i = 0; i < flagsArr.Length; i++) { flagsArr[i] = i; };

            LeastSquare ls = new LeastSquare(X, Y, doc.Parameters, flagsArr, calcFunc, null);

            ls.CalculateFitGoodness();

            doc.CurrentModel.FitResultStdError = ls.LastStdError;
            doc.CurrentModel.FitResultChiSquare = ls.LastChiSquare;
            lblStdError.Text = ls.LastStdError.ToString();
            //lblChiSquared.Text = ls.LastChiSquare.ToString();

            try
            {
                ls.GetFitErrorAllParameters(0.01f, out doc.CurrentModel.SigmaParameters, out doc.CurrentModel.SigmaY);
            }
            catch
            {

            }
        }

        private void UpdateDocumentParameters()
        {
            if (dataGridModels.SelectedRows == null || dataGridModels.SelectedRows.Count == 0)
            {
                MessageBox.Show("No model is selected");
                return;
            }

            string name = dataGridModels.SelectedRows[0].Cells["ModelName"].Value.ToString();

            doc.Parameters = new double[doc.CurrentModel.ModelFunction.Parameters.Length];
            for (int i = 0; i < doc.Parameters.Length; i++)
            {
                doc.Parameters[i] = 0;
                if (dataGridParameters[2, i].Value != null)
                {
                    double pValue = 0;
                    if (!double.TryParse(dataGridParameters[2, i].Value.ToString(), out pValue))
                    {
                        MessageBox.Show("Invalid parameter value");
                        return;
                    }
                    doc.Parameters[i] = pValue;
                    if (doc.CurrentModel != null && doc.CurrentModel.LastFitParameters != null)
                    {
                        doc.CurrentModel.LastFitParameters[i] = pValue;
                    }
                }
            }
        }

        private void UpdateDocumentFit()
        {
            doc.FitDataX = doc.X;
            doc.FitDataY = new double[doc.FitDataX.Length];
            doc.CurrentModel.ModelFunction.CalcProcedure(doc.FitDataX, doc.Parameters, ref doc.FitDataY);

            //Calculate difference between the fit and original data
            for (int i = 0; i < doc.Data.Count; i++)
            {
                doc.Data[i][3] = doc.FitDataY[i];//TODO: Refactor if all this needs to be here
                doc.Data[i][4] = doc.Data[i][3] - doc.Data[i][1];
            }
        }

        private void UpdateParametersGridFromDocument()
        {
            int sigmaIndex = 0;
            for (int i = 0; i < doc.Parameters.Length; i++)
            {
                var paramValue = doc.CurrentModel != null && doc.CurrentModel.LastFitParameters != null ? doc.CurrentModel.LastFitParameters[i] : doc.Parameters[i];

                dataGridParameters[2, i].Value = NumberFormatter.FloatToString(paramValue, 6, 2);                

                if (doc.CurrentModel.SigmaParameters != null && (bool)(dataGridParameters[1, i].Value) == true)
                {
                    dataGridParameters[3, i].Value = NumberFormatter.FloatToString(doc.CurrentModel.SigmaParameters[sigmaIndex++], 2, 2);
                }
                else
                {
                    dataGridParameters[3, i].Value = "0";
                }
            }

            dataGridParameters.Refresh();
        }

        private void SendDataChangedMessage(object sender = null)
        {
            tabMain.SelectedTab = tabPageData;
            Program.IsFromUser = false;
            controller.UpdateAllUI(sender ?? this, UpdateMessages.DATA_CHANGED, doc);
            btnPlot_Click(this, null);
            Program.IsFromUser = true;
        }

        private void dataGridView_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            double[] newRowValues = new double[doc.ColumnCount];
            doc.Data.Insert(e.Row.Index - 1, newRowValues);
            if (doc.Data.Count > 0)
            {
                SetState("K2");
            }
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index < doc.Data.Count)
            {
                doc.Data.RemoveAt(e.Row.Index);
                SendDataChangedMessage();
                if (doc.Data.Count == 0)
                {
                    RemoveState("K4");
                    RemoveState("K3");
                    RemoveState("K2");
                }
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText())
            {
                InfoBox.ShowMessage("No data is available in the clipboad", this);
                return;
            }

            //TODO This code is to be moved to separate class
            string text = Clipboard.GetText();
            ImportDialog importDialog = new ImportDialog();
            importDialog.LeftTopPos = CenterDialog(importDialog);
            if (importDialog.ShowDialog(text, "") == DialogResult.OK)
            {
                if (doc.LoadFromMultilineText(text, Configuration.ImportSettings))
                {
                    Program.IsFromUser = false;
                    DettachGridHandlers();

                    doc.GridColumnSettings[0].Name = doc.ColumnNames[0];
                    doc.GridColumnSettings[1].Name = doc.ColumnNames[1];
                    InitUIOnNewDocument();

                    AttachGridHandlers();
                    Program.IsFromUser = true;
                }
                else
                {
                    MessageBox.Show(doc.ErrMessage, "Parsing error");
                }
            }
        }

        private bool ImportFile(string path)
        {
            ImportDialog importDialog = new ImportDialog();
            importDialog.LeftTopPos = CenterDialog(importDialog);
            bool bSuccess = false;
            while (!bSuccess)
            {
                string dataText;
                try
                {
                    var file = File.OpenText(path);
                    dataText = file.ReadToEnd();
                    file.Close();
                }
                catch
                {
                    //TODO Log exception in log or system.applicaiton info logs
                    InfoBox.ShowMessage("Cannot read file. Check if file is not corrupted or not accessible", "Failure", this);
                    return false;
                }

                if (importDialog.ShowDialog(dataText, Path.GetFileNameWithoutExtension(path)) == DialogResult.OK)
                {
                    if (doc.LoadFromMultilineText(dataText, Configuration.ImportSettings))
                    {
                        bSuccess = true;
                        Program.IsFromUser = false;
                        DettachGridHandlers();
                        //doc.Models.Clear();
                        //doc.CurrentModel = null; TODO Remove once tested
                        doc.HasErrorY = Configuration.ImportSettings.HasErrColumn;
                        doc.GridColumnSettings[2].Visible = Configuration.ImportSettings.HasErrColumn;
                        doc.GridColumnSettings[3].Visible = false;
                        doc.GridColumnSettings[4].Visible = false;

                        this.Text = Constants.PROGRAM_NAME + " - " + Configuration.GraphSettings.Title; undoCommands.Clear();

                        InitUIOnNewDocument();

                        AttachGridHandlers();
                        if (string.IsNullOrEmpty(projectFileName))
                        {
                            projectFileName = Configuration.GraphSettings.Title;
                        }
                        Configuration.ImportSettings.LastFile = path;
                        Program.IsFromUser = true;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(doc.ErrMessage, "Parsing error");
                    }
                }
                else
                {
                    break;
                }
            }

            return false;
        }

        private void ImportFileClick(object sender, EventArgs args)
        {
            if (Program.RecentFiles.ContainsKey(sender.ToString()))
            {
                if (!ImportFile(Program.RecentFiles[sender.ToString()]))
                {
                    Program.RecentFiles.Remove(sender.ToString());
                    Program.SaveRecentFiles();
                }
            }
        }

        private async void LoadProjectClick(object sender, EventArgs args)
        {
            if (Program.RecentProjects.ContainsKey(sender.ToString()))
            {
                Cursor = Cursors.WaitCursor;

                await LoadProject(Program.RecentProjects[sender.ToString()]);

                Cursor = Cursors.Default;
            }
        }

        private void importASCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO All the code here is to be moved to separate service class
            openFileDialog.RestoreDirectory = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (ImportFile(openFileDialog.FileName))
                {
                    if (!Program.RecentFiles.ContainsKey(Path.GetFileName(openFileDialog.FileName)))
                    {
                        if (Program.RecentFiles.Count > 7)
                        {
                            string key = toolStripMenuRecentFiles.DropDownItems[0].Text;
                            toolStripMenuRecentFiles.DropDownItems.RemoveAt(0);
                            Program.RecentFiles.Remove(key);
                        }
                        Program.RecentFiles.Add(Path.GetFileName(openFileDialog.FileName), openFileDialog.FileName);
                        Program.SaveRecentFiles();
                        var newMenuItem = new ToolStripMenuItem(Path.GetFileName(openFileDialog.FileName));
                        newMenuItem.Name = Path.GetFileName(openFileDialog.FileName);
                        newMenuItem.Click += new EventHandler(ImportFileClick);
                        toolStripMenuRecentFiles.DropDownItems.Add(newMenuItem);
                    }
                }
            }
        }

        private void formatGraphToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphPropertiesDialog graphPropertiesDialog = new GraphPropertiesDialog();

            graphPropertiesDialog.Settings = Configuration.GraphSettings;
            graphPropertiesDialog.LeftTopPos = CenterDialog(graphPropertiesDialog);

            if (graphPropertiesDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Configuration.GraphSettings = graphPropertiesDialog.Settings;
                controller.UpdateAllUI(this, UpdateMessages.GRAPH_SETTINGS_CHANGED, doc);
            }

        }

        private void editColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColumnSettingsDialog columnSettingsDialog = new ColumnSettingsDialog();
            columnSettingsDialog.Settings = doc.GridColumnSettings;
            columnSettingsDialog.InitializeFromSettings(doc);
            columnSettingsDialog.LeftTopPos = CenterDialog(columnSettingsDialog);

            if (columnSettingsDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < doc.GridColumnSettings.Length; i++)
                {
                    doc.ColumnNames[i] = doc.GridColumnSettings[i].Name;
                }

                controller.UpdateAllUI(this, UpdateMessages.COLUMN_SETTINGS_CHANGED, doc);
            }
        }

        private void InitUIOnNewDocument()
        {
            stateFlags.Clear();
            undoCommands.Clear();
            controller.CleareMessages();

            doc.GridColumnSettings[3].ReadOnly = true; doc.GridColumnSettings[3].Visible = false;
            doc.GridColumnSettings[4].ReadOnly = true; doc.GridColumnSettings[4].Visible = false;

            transformDialog = new TransformDialog(new string[] { doc.ColumnNames[0], doc.ColumnNames[1] });

            DataFilterInfo.Items.Clear();
            foreach (string columnName in doc.ColumnNames)
            {
                DataFilterInfo.Items.Add(new DataFilterInfo() { ColumnName = columnName, Max = 0, Min = 0, Selected = false });
            }

            SetState("K0");
            SetState("K1");
            controller.UpdateAllUI(this, UpdateMessages.NEW_DATA, doc);
            if (doc.Data.Count > 0)
            {
                SetState("K2");
                if (doc.CurrentModel != null)
                {
                    SetState("K5");
                    btnPlot_Click(this, null);
                }
            }
        }

        private async Task NewDocument(NewDataSettings settings)
        {
            ColumnSettings[] oldSettings = doc.GridColumnSettings;
            //Preserve models if there are
            var currentModels = doc.Models;
            var currentModel = doc.CurrentModel;
            this.doc = new Document(5,
                                        new string[] { 
                                                        settings.DataXName,
                                                        settings.DataYName,
                                                        settings.ErrDataName,
                                                        "Model",
                                                        "Diff"
                                        });

            doc.Models = currentModels;
            doc.CurrentModel = currentModel;

            //Restore old column settings
            for (int i = 0; i < oldSettings.Length; i++)
            {
                doc.GridColumnSettings[i].Alignment = oldSettings[i].Alignment;
                doc.GridColumnSettings[i].Format = oldSettings[i].Format;
                doc.GridColumnSettings[i].Digits = oldSettings[i].Digits;
            }

            if (settings.DataXAutoIncrement)
            {
                await PopulateDocumentData(settings);
            }

            doc.HasErrorY = settings.HasErrData;
            doc.YCalculated = settings.CalculateYData;
            doc.yCalculationFunction = settings.YFunction;
            doc.XAutoIncrement = settings.DataXAutoIncrement;
            doc.XAutoIncrementStep = settings.DataXIncrement;

            toolStripMenuItemInverseTransform.Enabled = doc.YCalculated && doc.yCalculationFunction != null;

            doc.GridColumnSettings[2].Visible = settings.HasErrData;

            Configuration.GraphSettings.Title = settings.Title;
            Configuration.GraphSettings.TitleFont = graph.TitleFont;
            if (string.IsNullOrEmpty(projectFileName))
            {
                projectFileName = settings.Title;
            }
            this.Text = Constants.PROGRAM_NAME + " - " + Configuration.GraphSettings.Title;

            InitUIOnNewDocument();

        }

        private Task PopulateDocumentData(NewDataSettings settings)
        {
            double[] P = new double[0];
            for (double x = settings.DataXMin; x <= settings.DataXMax; x += settings.DataXIncrement)
            {
                double y = 0;
                double err = 0;
                if (settings.CalculateYData && settings.YFunction != null)
                {
                    y = settings.YFunction.Calculate(x, P);

                    if (settings.ErrDataCalculatedStd)
                    {
                        err = (double)Math.Sqrt(Math.Abs(y));
                    }
                }

                doc.Data.Add(new double[5] { x, y, err, 0, 0 });
            }
            return Task.FromResult(0);
        }

        private void ImportFromASCIIFile(string fileName)
        {
            if (doc.LoadFromFile(fileName))
            {
                Program.IsFromUser = false;
                DettachGridHandlers();
                modelsDialog = new ModelsDialog(doc);
                doc.GridColumnSettings[2].Visible = false; //TODO this must depend if error column is imported
                doc.GridColumnSettings[3].ReadOnly = true; doc.GridColumnSettings[3].Visible = false;
                doc.GridColumnSettings[4].ReadOnly = true; doc.GridColumnSettings[4].Visible = false;
                transformDialog = new TransformDialog(new string[] { doc.ColumnNames[0], doc.ColumnNames[1] });

                DataFilterInfo.Items.Clear();
                foreach (string columnName in doc.ColumnNames)
                {
                    DataFilterInfo.Items.Add(new DataFilterInfo() { ColumnName = columnName, Max = 0, Min = 0, Selected = false });
                }
                doc.CurrentModel = null;
                btnInitParameters.Enabled = false;
                btnInitParameters.Refresh();
                btnPlot.Enabled = false;
                RefreshParametersGrid();
                btnCalcBestFit.Enabled = false;
                UpdateMathExpressionPreview("");
                controller.UpdateAllUI(this, UpdateMessages.NEW_DATA, doc);
                AttachGridHandlers();
                Program.IsFromUser = true;
            }
            else
            {
                MessageBox.Show(doc.ErrMessage, "Parsing error");
            }
        }

        private async void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateDataDialog newDataDialog = new GenerateDataDialog();
            newDataDialog.Settings = Configuration.NewDataSetting;
            newDataDialog.Settings.Title = Configuration.GraphSettings.Title; //TODO Title is to be moved to Project
            newDataDialog.LeftTopPos = CenterDialog(newDataDialog);
            if (newDataDialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                await NewDocument(newDataDialog.Settings);
                Configuration.NewDataSetting = newDataDialog.Settings;
                projectFilePath = "";
                projectFileName = newDataDialog.Settings.Title;
                tabMain.SelectedTab = tabPageData;
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                dataGridView.Focus();
                this.Cursor = Cursors.Default;
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentCell == null)
            {
                MessageBox.Show("Select the insert place in the table.");
                return;
            }
            int index = dataGridView.CurrentCell.RowIndex;
            //dataGridView.Rows.Insert(index, new DataGridViewRow()); TODO Remove when well tested
            doc.Data.Insert(index, new double[] { 0, 0, 0, 0, 0 });
            dataGridView.CurrentCell = dataGridView.Rows[index].Cells[0];


            Program.IsFromUser = false;
            //Push undo insert command
            List<int> insertedRows = new List<int>();
            insertedRows.Add(index);

            Command undoCommand = new UndoInsertCommand(doc, insertedRows.ToArray());
            undoCommands.Push(undoCommand);
            toolStripButtonUndo.ToolTipText = undoCommand.Name;
            SetState("K9");
            DettachGridHandlers();
            if (doc.Data.Count > 0)
            {
                RemoveState("K3");
                SetState("K2");
            }
            AttachGridHandlers();
            SendDataChangedMessage();
            dataGridView.BeginEdit(true);
            Program.IsFromUser = true;
        }

        private void deleteCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = dataGridView.CurrentCell.RowIndex;
            doc.Data.RemoveAt(index);
            dataGridView.Rows.RemoveAt(index);
            SendDataChangedMessage();
        }

        private void toolStripPasteInsert_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText())
            {
                InfoBox.ShowMessage("No data is available in the clipboad", this);
                return;
            }

            if (dataGridView.CurrentCell == null)
            {
                MessageBox.Show("Select the insert place in the table.");
                return;
            }

            //TODO This is to be moved to separate service class
            string text = Clipboard.GetText();
            int index = dataGridView.CurrentCell.RowIndex;

            string errorMessage;
            List<double[]> newData = Utility.RowsFromText(text, out errorMessage);
            if (newData == null)
            {
                MessageBox.Show("Failed to parse data from clipboard:\r\n" + errorMessage);
                return;
            }

            Program.IsFromUser = false;
            //Push undo insert command
            List<int> insertedRows = new List<int>();
            for (int i = 0; i < newData.Count; i++)
            {
                insertedRows.Add(index);
                doc.Data.Insert(index++, newData[i]);
            }
            Command undoCommand = new UndoInsertCommand(doc, insertedRows.ToArray());
            undoCommands.Push(undoCommand);
            toolStripButtonUndo.ToolTipText = undoCommand.Name;
            SetState("K9");
            DettachGridHandlers();
            if (doc.Data.Count > 0)
            {
                RemoveState("K3");
                SetState("K2");
            }
            AttachGridHandlers();
            SendDataChangedMessage();
            Program.IsFromUser = true;
        }

        private void deleteSelectedStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows == null || dataGridView.SelectedRows.Count == 0)
            {
                dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Selected = true;
            }
            //.Net is smart enough to pass here selected rows sorted in reverse order so removing of one row does not change index of the others
            if (dataGridView.Rows[dataGridView.SelectedRows[0].Index].IsNewRow)
            {
                dataGridView.Rows[dataGridView.SelectedRows[0].Index].Selected = false;
            }

            dataGridView.SuspendLayout();

            List<DeletedRow> deletedRows = new List<DeletedRow>();
            while (dataGridView.SelectedRows.Count > 0)
            {
                int index = dataGridView.SelectedRows[0].Index;
                if (!dataGridView.Rows[dataGridView.SelectedRows[0].Index].IsNewRow)
                {
                    dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
                    deletedRows.Insert(0, new DeletedRow() { index = index, data = doc.Data[index] });
                    doc.Data.RemoveAt(index);
                }
            }

            dataGridView.ResumeLayout();

            //Push undo delete command
            Command undoDeleteCommand = new UndoDeleteCommand(doc, deletedRows.ToArray());
            undoCommands.Push(undoDeleteCommand);
            toolStripButtonUndo.ToolTipText = undoDeleteCommand.Name;
            SetState("K9");
            SendDataChangedMessage(dataGridView);
            if (doc.Data.Count == 0)
            {
                RemoveState("K2");
                RemoveState("K3");
            }

        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Input string is not in correct format.\r\nEnter valid number or press Esc");
            return;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.SaveUserFunctionParameters();
        }

        private Point CenterDialog(Form dialog)
        {
            Point p = Point.Empty;
            p.Y = (this.Top + this.Bottom) / 2 - (dialog.Bottom - dialog.Top) / 2;
            p.X = (this.Left + this.Right) / 2 - (dialog.Right - dialog.Left) / 2;

            return p;
        }

        private void modelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modelsDialog.LeftTopPos = CenterDialog(modelsDialog);
            if (modelsDialog.ShowDialog(doc) == DialogResult.OK)
            {
                if (modelsDialog.ModelsChanged)
                {
                    controller.UpdateAllUI(this, UpdateMessages.MODELS_CHANGE, doc);
                }
            }
        }

        private RectangleF SetMathPreview(string expression, Graphics g, Font font, int X = 0, int Y = 0)
        {
            if (String.IsNullOrEmpty(expression) || g == null || font == null) return RectangleF.Empty;

            MathParser parser = new MathParser();
            Node mathTree = parser.BuildTree(expression);
            if (mathTree == null)
            {
                //Error has occured, silantly exit
                return RectangleF.Empty;
            }
            //See if it is needed to increase the font size
            int fontIndex = 0;
            for (; fontIndex < Node.FontSizes.Length && Node.FontSizes[fontIndex] < font.Size; fontIndex++) ;
            if (fontIndex < parser.NestedLevels)
            {
                fontIndex = parser.NestedLevels;
                int fontSize = Node.FontSizes[fontIndex - 1];
                font = new Font(font.FontFamily, fontSize, font.Style);
            }

            RectangleF boundingBox = mathTree.CalculateLayout(g, font);
            mathTree.DrawTree(g, font, new SolidBrush(Color.Black), -boundingBox.X + X - boundingBox.Width / 2, -boundingBox.Y + Y - boundingBox.Height / 2);

            return boundingBox;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void UpdateMathExpressionPreview(string mathExpression)
        {
            this.mathExpression = mathExpression;
            pictureBoxModelPreview.Refresh();
        }

        private void dataGridModels_SelectionChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;

            string modelName = "";
            if (dataGridModels.SelectedRows != null && dataGridModels.SelectedRows.Count > 0 && doc.Models.Keys.Count > 0)
            {
                modelName = dataGridModels.SelectedRows[0].Cells["ModelName"].Value.ToString();
            }

            SetCurrentModel(modelName);

            Program.IsFromUser = true;
        }

        private void RefreshParametersGrid()
        {
            dataGridParameters.Columns[0].ReadOnly = false;
            dataGridParameters.Rows.Clear();
            if (doc.CurrentModel != null)
            {
                int sigmaIndex = 0;
                for (int i = 0; i < doc.CurrentModel.ModelFunction.Parameters.Length; i++)
                {
                    int index = dataGridParameters.Rows.Add();
                    dataGridParameters.Rows[index].Cells[0].Value = doc.CurrentModel.ModelFunction.Parameters[i];
                    dataGridParameters.Rows[index].Cells[1].Value = true;
                    if (doc.CurrentModel.LastFitParameters != null && i < doc.CurrentModel.LastFitParameters.Length)
                    {
                        //dataGridParameters.Rows[index].Cells[2].Value = NumberFormatter.FloatToString(doc.CurrentModel.LastFitParameters[i], 3, 4); This may be affecting the accuracy
                        dataGridParameters.Rows[index].Cells[2].Value = doc.CurrentModel.LastFitParameters[i];
                        if (doc.CurrentModel.SigmaParameters != null && (bool)(dataGridParameters[1, i].Value) == true)
                        {
                            dataGridParameters.Rows[index].Cells[3].Value = NumberFormatter.FloatToString(doc.CurrentModel.SigmaParameters[sigmaIndex++], 3, 4);
                        }
                        else
                        {
                            dataGridParameters.Rows[index].Cells[3].Value = "0";
                        }
                    }
                }
            }
            dataGridParameters.Columns[0].ReadOnly = true;
            dataGridParameters.Columns[3].ReadOnly = true;
            dataGridParameters.Refresh();
        }

        private async void btnInitParameters_Click(object sender, EventArgs e)
        {
            if (doc.Data.Count == 0)
            {
                MessageBox.Show("No data exists.");
                return;
            }
            Program.IsFromUser = false;
            try
            {
                this.Cursor = Cursors.WaitCursor;

                await Task.Run(() => doc.InitializeParameters(Configuration.RegressionDataSmoothPoints));

                this.Cursor = Cursors.Default;

                doc.CurrentModel.IsInitialized = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to initialize parameters for: \r\n" + doc.CurrentModel.ModelFunction.Name + "\r\n" + ex.Message, "Error");
            }

            UpdateFitGoodness();
            UpdateParametersGridFromDocument();
            UpdateDocumentFit();

            controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);

            Program.IsFromUser = true;
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            if (dataGridView.SelectedRows.Count > 0 || dataGridView.CurrentCell != null)
            {
                SetState("K3");
            }
            else
            {
                RemoveState("K3");
            }
            if (!String.IsNullOrEmpty(xSelectionMarker))
            {
                Program.IsFromUser = false;
                for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
                {
                    double x = dataGridView.Rows[i].Cells[0].Value == null ? 0 : double.Parse(dataGridView.Rows[i].Cells[0].Value.ToString());
                    double y = dataGridView.Rows[i].Cells[1].Value != null ? double.Parse(dataGridView.Rows[i].Cells[1].Value.ToString()) : 0;
                    if (dataGridView.Rows[i].Cells[0].Value != null && dataGridView.Rows[i].Cells[0].Value.ToString() == xSelectionMarker)
                    {
                        xSelectionMarker = String.Empty;
                        dataGridView.CurrentCell = dataGridView.Rows[i].Cells[1];
                        dataGridView.BeginEdit(false);
                    }
                }
                Program.IsFromUser = true;
            }

        }

        private void transformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transformDialog.LeftTopPos = CenterDialog(transformDialog);
            if (transformDialog.ShowDialog(new string[] { doc.ColumnNames[0], doc.ColumnNames[1] }) == DialogResult.OK)
            {
                double[] source = transformDialog.ColumnIndex == 0 ? doc.X : doc.Y;
                double[] transformed = new double[source.Length];
                try
                {
                    transformDialog.TransformFunction.CalcProcedure(source, new double[0], ref transformed);
                }
                catch
                {
                    InfoBox.ShowMessage("Error in calculation. Check math for the transform expression.", this);
                    return;
                }

                UndoTransformCommand undoTransformCommand = new UndoTransformCommand(doc);
                undoCommands.Push(undoTransformCommand);
                toolStripButtonUndo.ToolTipText = undoTransformCommand.Name;
                SetState("K9");

                if (transformDialog.ColumnIndex == 0)
                {
                    doc.UpdateX(transformed);
                }
                else
                {
                    doc.UpdateY(transformed);
                }
                tabMain.SelectedTab = tabPageData;
                controller.UpdateAllUI(this, UpdateMessages.DATA_CHANGED, doc);
                RemoveState("K8");
            }
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterDialog filterDialog = new FilterDialog();
            filterDialog.LeftTopPos = CenterDialog(filterDialog);
            DataFilterInfo.Items[0].Visible = true;
            DataFilterInfo.Items[1].Visible = true;
            DataFilterInfo.Items[2].Visible = doc.HasErrorY;
            DataFilterInfo.Items[3].Visible = doc.CurrentModel != null;
            DataFilterInfo.Items[4].Visible = doc.CurrentModel != null;
            if (filterDialog.ShowDialog() == DialogResult.OK)
            {
                UndoTransformCommand undoTransformCommand = new UndoTransformCommand(doc);
                undoCommands.Push(undoTransformCommand);
                toolStripButtonUndo.ToolTipText = "Undo filter";
                doc.FilterData(DataFilterInfo.Items);
                SetState("K9");
                controller.UpdateAllUI(this, UpdateMessages.DATA_FILTERED, doc);
            }
        }

        private void dataGridView_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void copyStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            string line = "";
            for (int ci = 0; ci < dataGridView.Columns.Count; ci++)
            {
                if (dataGridView.Columns[ci].Visible)
                {
                    line += dataGridView.Columns[ci].Name;
                    if (ci < dataGridView.Columns.Count - 1)
                    {
                        line += "\t";
                    }
                }
            }

            builder.AppendLine(line);

            for (int i = 0; i < doc.Data.Count; i++)
            {
                line = "";
                for (int ci = 0; ci < dataGridView.Columns.Count; ci++)
                {
                    if (dataGridView.Columns[ci].Visible)
                    {
                        line += dataGridView.Rows[i].Cells[ci].Value.ToString();
                        if (ci < dataGridView.Columns.Count - 1)
                        {
                            line += "\t";
                        }
                    }
                }

                builder.AppendLine(line);
            }
            Clipboard.SetText(builder.ToString());
        }

        private void dataGridParameters_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!btnPlot.Enabled || e.ColumnIndex != 2 || !Program.IsFromUser) return;
            Program.IsFromUser = false;
            btnPlot_Click(this, null);
            Program.IsFromUser = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridModels_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!Program.IsFromUser) return;
            if (e.RowIndex == -1) return;

            string modelName = dataGridModels.Rows[e.RowIndex].Cells[0].Value.ToString();
            doc.Models.Remove(modelName);
            dataGridModels.Rows.RemoveAt(e.RowIndex);
            if (e.RowIndex > 0)
            {
                doc.CurrentModel = doc.Models[dataGridModels.Rows[e.RowIndex - 1].Cells[0].Value.ToString()];
            }
            else
                if (doc.Models.Count > e.RowIndex)
                {
                    doc.CurrentModel = doc.Models[dataGridModels.Rows[e.RowIndex].Cells[0].Value.ToString()];
                }
                else
                {
                    doc.CurrentModel = null;
                    doc.GridColumnSettings[3].Visible = false;
                    doc.GridColumnSettings[4].Visible = false;
                    doc.FitDataY = null;
                    doc.FitDataY = null;
                    //lblChiSquared.Text = "";
                    lblStdError.Text = "";
                }
            //Program.IsFromUser = true;
            controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopRegression = true;
        }

        private void toolStripMenuRemoveModel_Click(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;
            //.Net is smart enough to pass here selected rows sorted in reverse order so removing of one row does not change index of the others
            if (dataGridModels.SelectedRows.Count > 0)
            {
                for (int i = dataGridModels.SelectedRows.Count - 1; i >= 0; i--)
                {
                    int index = dataGridModels.SelectedRows[i].Index;
                    string modelName = dataGridModels.Rows[index].Cells[0].Value.ToString();
                    dataGridModels.Rows.RemoveAt(index);
                    doc.Models.Remove(modelName);
                }
            }

            Program.IsFromUser = true;

            if (dataGridModels.Rows.Count > 0 && dataGridModels.SelectedRows.Count == 0)
            {
                dataGridModels.Rows[0].Selected = true;
            }
            else
            {
                doc.CurrentModel = null;
                doc.GridColumnSettings[3].Visible = false;
                doc.GridColumnSettings[4].Visible = false;
                doc.FitDataY = null;
                doc.FitDataY = null;
                //lblChiSquared.Text = "";
                lblStdError.Text = "";
            }
            dataGridModels_SelectionChanged(this, null);

            controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);

        }

        private void toolStripMenuCopy_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support copy of regression results", "Warning", this);
#else
            if (dataGridModels.Rows.Count > 0)
            {
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < dataGridModels.Rows.Count; i++)
                {
                    string modelName = dataGridModels.Rows[i].Cells[0].Value.ToString();
                    string modelStdError = dataGridModels.Rows[i].Cells[1].Value.ToString();
                    builder.AppendLine(modelName + "\t" + modelStdError);
                }

                Clipboard.SetText(builder.ToString());
            }
#endif
        }

        private void toolStripMenuCopyParameters_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support copy of value or parameters", "Warning", this);
#else
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < dataGridParameters.Rows.Count; i++)
            {
                string parameterName = dataGridParameters.Rows[i].Cells[0].Value.ToString();
                string parameterValue = dataGridParameters.Rows[i].Cells[2].Value.ToString();
                builder.AppendLine(parameterName + "\t" + parameterValue);
            }

            Clipboard.SetText(builder.ToString());
#endif
        }

        private void toolStripMenuCopyValue_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support copy of value or parameters", "Warning", this);
#else
            if (dataGridParameters.CurrentCell != null)
            {
                Clipboard.SetText(dataGridParameters.Rows[dataGridParameters.CurrentCell.RowIndex].Cells[2].Value.ToString());
            }
#endif
        }

        private void toolStripButtonReport_Click(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Regression report");
            builder.AppendLine("");
            for (int i = 0; i < dataGridModels.Rows.Count; i++)
            {
                string modelName = dataGridModels.Rows[i].Cells[0].Value.ToString();
                ModelFitInfo fitInfo = doc.Models[modelName];
                if (fitInfo.LastFitParameters == null) continue;
                string line = String.Format("{0:20}\t{1:15}\tStd. Error: {2}", fitInfo.ModelName, fitInfo.ModelFunction.Description, fitInfo.FitResultStdError);
                builder.AppendLine(line);
                //Write last best fit parameters and values
                for (int j = 0; j < fitInfo.ModelFunction.Parameters.Length; j++)
                {
                    if (fitInfo.SigmaParameters != null)
                    {
                        line = String.Format("{0:20}\t=\t{1:F7}\t±{2}", fitInfo.ModelFunction.Parameters[j], fitInfo.LastFitParameters[j], fitInfo.SigmaParameters[j]);
                    }
                    else
                    {
                        line = String.Format("{0:20}\t=\t{1:F7}", fitInfo.ModelFunction.Parameters[j], fitInfo.LastFitParameters[j]);
                    }
                    builder.AppendLine(line);
                }

                if (fitInfo.DerivedValues != null && fitInfo.DerivedValues.Count > 0)
                {
                    builder.AppendLine("");
                    builder.AppendLine("Derived values:");
                    //Add derived values if any
                    for (int j = 0; j < fitInfo.DerivedValues.Count; j++)
                    {
                        line = String.Format("{0:20}\t=\t{1:F7}\t±{2:F7}", fitInfo.DerivedValues[j].Name, fitInfo.DerivedValues[j].Value, fitInfo.DerivedValues[j].Sigma);
                        builder.AppendLine(line);
                    }
                    builder.AppendLine("");
                }
                builder.AppendLine("----------------------------------------------------------------------------------------------");
                builder.AppendLine("");
            }
            ReportDialog reportDialog = new ReportDialog();
            reportDialog.LeftTopPos = CenterDialog(reportDialog);
            reportDialog.ShowReport(builder.ToString());
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {
            exportDialog.ShowDialog(doc, this);
        }

        private void toolStripMenuReInit_Click(object sender, EventArgs e)
        {
            doc.ReInitializeParameters();
            controller.UpdateAllUI(this, UpdateMessages.MODELS_CHANGE, doc);
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            DataFilterInfo.Items[0].Selected = false;
            DataFilterInfo.Items[1].Selected = false;

            controller.UpdateAllUI(this, UpdateMessages.VIEW_FILTERED, new ViewFilterMessage() { Document = doc, FilterItems = null });

            RemoveState("K8");
        }

        private void plotMenuCopy_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(graph.Width, graph.Height);
            graph.DrawContent(ref bmp);
            Clipboard.SetImage(bmp);
        }

        private void plotMenuSave_Click(object sender, EventArgs e)
        {
            if (savePlotImageFileDialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(graph.Width, graph.Height);
                graph.DrawContent(ref bmp);
                bmp.Save(savePlotImageFileDialog.FileName);
            }
        }                

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                e.Graphics.FillRectangle(
                        new LinearGradientBrush(
                            new Point(e.CellBounds.Left, e.CellBounds.Top),
                            new Point(e.CellBounds.Left, e.CellBounds.Bottom),
                            Color.FromArgb(255, 170, 170, 170),
                            Color.FromArgb(255, 220, 220, 220)
                            ),
                            e.CellBounds);

                Rectangle cellRect = e.CellBounds;
                cellRect.Inflate(0, -1);
                cellRect.Offset(-1, 0);
                e.Graphics.DrawRectangle(new Pen(Color.DarkGray, 1), cellRect);
                e.PaintContent(e.CellBounds);

                e.Handled = true;
            }
            else
                if (e.ColumnIndex == -1)
                {
                    e.PaintBackground(e.CellBounds, true);

                    Rectangle cellRect = e.CellBounds;

                    e.Graphics.FillRectangle(
                             new LinearGradientBrush(
                                new Point(cellRect.Left, cellRect.Top),
                                 new Point(cellRect.Left, cellRect.Bottom),
                                 Color.FromArgb(255, 200, 200, 200),
                                 Color.FromArgb(255, 230, 230, 230)
                                 ),
                                e.CellBounds);
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(255, 200, 200, 200), 1), cellRect.Left, cellRect.Bottom - 1, cellRect.Right, cellRect.Bottom - 1);
                    if (e.RowIndex >= 0)
                    {
                        e.Graphics.DrawString((e.RowIndex + 1).ToString(), dataGridView.ColumnHeadersDefaultCellStyle.Font, new SolidBrush(Color.Black),
                                                e.CellBounds.Right - e.Graphics.MeasureString((e.RowIndex + 1).ToString(), dataGridView.ColumnHeadersDefaultCellStyle.Font).Width - 4,
                                               (e.CellBounds.Top + e.CellBounds.Bottom) / 2 - e.Graphics.MeasureString(e.ColumnIndex.ToString(), dataGridView.ColumnHeadersDefaultCellStyle.Font).Height / 2);
                    }

                    e.Handled = true;
                }
        }

        private async void toolStripButtonFitAll_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;

            var initializationFailures = await AddLibraryModelsAsync();

            this.Cursor = Cursors.Default;

            if (initializationFailures.Count > 0)
            {
                new InfoBox().Show(initializationFailures.ToArray(), "Failed to initialize parameters for:", this);
            }

            tabMain.SelectedTab = tabPagePlot;
            controller.UpdateAllUI(this, UpdateMessages.MODELS_CHANGE, doc);
        }

        private Task<List<string>> AddLibraryModelsAsync()
        {
            double[] X = doc.X;
            double[] Y = doc.Y;
            List<string> initializationFailures = new List<string>();

            Parallel.ForEach<string>(Program.LibraryModels.Keys, name =>
            {
                if (!doc.Models.Keys.Contains(name))
                {
                    FitFunction function = Program.LibraryModels[name];
                    ModelFitInfo modelFitInfo = new ModelFitInfo() { ModelName = name, ModelFunction = function, FitResultChiSquare = 0, FitResultStdError = 0 };
                    if (modelFitInfo.ModelFunction.CanInitParameters)
                    {
                        try
                        {
                            if (Configuration.RegressionDataSmoothPoints == 0 || Y.Length < Configuration.RegressionDataSmoothPoints * 5)
                            {
                                modelFitInfo.LastFitParameters = modelFitInfo.ModelFunction.InitParameters(X, Y);
                            }
                            else
                            {
                                modelFitInfo.LastFitParameters = modelFitInfo.ModelFunction.InitParameters(X, DataSmooth.MovingAverage(Y, Configuration.RegressionDataSmoothPoints));
                            }
                            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                            int[] flagsArr = new int[modelFitInfo.LastFitParameters.Length]; for (int i = 0; i < flagsArr.Length; i++) { flagsArr[i] = i; };
                            LeastSquare ls = new LeastSquare(X, Y, modelFitInfo.LastFitParameters, flagsArr, calcFunc, null);
                            double eps = ls.CalculateStdError();
                            modelFitInfo.FitResultStdError = eps;
                            modelFitInfo.IsInitialized = true;
                        }
                        catch (InitializationException)
                        {
                            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                            modelFitInfo.LastFitParameters = new double[modelFitInfo.ModelFunction.Parameters.Length];
                            LeastSquare ls = new LeastSquare(X, Y, modelFitInfo.LastFitParameters, new int[0], calcFunc, null);
                            double eps = ls.CalculateStdError();
                            modelFitInfo.FitResultStdError = eps;
                        }
                        catch (Exception)
                        {
                            initializationFailures.Add(modelFitInfo.ModelFunction.Name);
                            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                            modelFitInfo.LastFitParameters = new double[modelFitInfo.ModelFunction.Parameters.Length];
                            LeastSquare ls = new LeastSquare(X, Y, modelFitInfo.LastFitParameters, new int[0], calcFunc, null);
                            double eps = ls.CalculateStdError();
                            modelFitInfo.FitResultStdError = eps;
                        }
                    }
                    lock (doc)
                    {
                        doc.Models.Add(name, modelFitInfo);
                    }
                }
            });

            return Task.FromResult<List<string>>(initializationFailures);
        }

        private void dataGridModels_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 1)
            {
                try
                {
                    double v1, v2;
                    if (double.TryParse(e.CellValue1.ToString(), out v1))
                    {
                        if (double.TryParse(e.CellValue2.ToString(), out v2))
                        {
                            e.SortResult = v2 > v1 ? -1 : 1;
                        }
                    }
                }
                catch (Exception exc)
                {
                    Trace.TraceError(exc.ToString());
                    throw;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveProjectDialog.InitialDirectory = projectFilePath;
            saveProjectDialog.FileName = projectFileName;
            if (saveProjectDialog.ShowDialog() == DialogResult.OK)
            {
                projectFileName = saveProjectDialog.FileName;
                projectFilePath = Path.GetDirectoryName(projectFileName);
                SaveProjectInBinary(projectFileName);
            }
        }

        private async Task LoadProject(string path)
        {
            try
            {
                //TODO Refactor the code in this method, all initialization must happen in separate control specific handlers/methods
                Program.IsFromUser = false;
                bool openedSuccess = await Task.Run(() => OpenProjectInBinary(path));
                if (!openedSuccess)
                {
                    RemoveProjectFromRecentProjects(path);
                    Program.IsFromUser = true;
                    InfoBox.ShowMessage("Cannot open project file", "Error", this);
                    return;
                }

                projectFileName = path;
                projectFilePath = Path.GetDirectoryName(projectFileName);
                Configuration.GraphSettings.Title = Path.GetFileNameWithoutExtension(projectFileName);
                this.Text = Constants.PROGRAM_NAME + " - " + Configuration.GraphSettings.Title;
                stateFlags.Clear();
                undoCommands.Clear();
                controller.CleareMessages();
                RefreshParametersGrid();
                btnCalcBestFit.Enabled = false;
                UpdateMathExpressionPreview("");
                doc.GridColumnSettings[3].ReadOnly = true;
                doc.GridColumnSettings[4].ReadOnly = true;

                transformDialog = new TransformDialog(new string[] { doc.ColumnNames[0], doc.ColumnNames[1] });

                DataFilterInfo.Items.Clear();
                foreach (string columnName in doc.ColumnNames)
                {
                    DataFilterInfo.Items.Add(new DataFilterInfo() { ColumnName = columnName, Max = 0, Min = 0, Selected = false });
                }

                SetState("K0");
                SetState("K1");
                if (doc.Data.Count > 0)
                {
                    SetState("K2");
                }

                AddProjectToRecentProjects(projectFileName);

                Program.IsFromUser = true;

                controller.UpdateAllUI(this, UpdateMessages.NEW_DATA, doc);
                controller.UpdateAllUI(this, UpdateMessages.MODELS_CHANGE, doc);
                tabMain.SelectedTab = tabPageData;
            }
            catch
            {
                Program.IsFromUser = true;
                new InfoBox().Show("Failure opening the project file.", "Internal Error", this);
            }

        }

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openProjectDialog.RestoreDirectory = false;
            if (openProjectDialog.ShowDialog() == DialogResult.OK)
            {
                if (String.Compare(Path.GetExtension(openProjectDialog.FileName), Program.PROJECT_FILE_EXTENSION, true) != 0)
                {
                    InfoBox.ShowMessage("File is not of expected type!", this);
                    return;
                }

                Cursor = Cursors.WaitCursor;
                await LoadProject(openProjectDialog.FileName);
                Cursor = Cursors.Default;
            }
        }

        private void AddProjectToRecentProjects(string fileName)
        {
            if (!Program.RecentProjects.ContainsKey(Path.GetFileName(fileName)))
            {
                if (Program.RecentProjects.Count > 7)
                {
                    string key = toolStripMenuItemRecentProjects.DropDownItems[0].Text;
                    toolStripMenuItemRecentProjects.DropDownItems.RemoveAt(0);
                    Program.RecentProjects.Remove(key);
                }
                Program.RecentProjects.Add(Path.GetFileName(fileName), fileName);
                Program.SaveRecentProjects();
                var newMenuItem = new ToolStripMenuItem(Path.GetFileName(fileName));
                newMenuItem.Click += new EventHandler(LoadProjectClick);
                toolStripMenuItemRecentProjects.DropDownItems.Add(newMenuItem);
            }
        }

        private void RemoveProjectFromRecentProjects(string fileName)
        {
            if (Program.RecentProjects.ContainsKey(Path.GetFileName(fileName)))
            {
                Program.RecentProjects.Remove(Path.GetFileName(fileName));
                Program.SaveRecentProjects();
                toolStripMenuItemRecentProjects.DropDownItems.RemoveByKey(Path.GetFileName(fileName));
            }
        }

        private void SaveProjectInBinary(string fileName)
        {
            Project project = new Project();
            project.Title = Configuration.GraphSettings.Title;
            project.HasErrorY = doc.HasErrorY;
            project.Data = doc.Data;
            project.DataXName = doc.ColumnNames[0];
            project.DataYName = doc.ColumnNames[1];
            project.ErrDataName = doc.ColumnNames[2];
            project.GridColumnSettings = doc.GridColumnSettings;
            project.Models = doc.Models.Values.ToArray<ModelFitInfo>();

            FileStream stream = null;
            try
            {
                stream = File.Create(fileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, project);
                stream.Close();
                new InfoBox().Show("\r\nProject saved", this);
                AddProjectToRecentProjects(fileName);
            }
            catch
            {
                InfoBox.ShowMessage("Failed to save the project file.", "Internal Error", this);
                return;
            }
            finally
            {
                try
                {
                    stream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failure saving the project file.", "Internal Error");
                }
            }
        }

        private bool OpenProjectInBinary(string fileName)
        {
            Project project = null;
            FileStream stream = null;
            try
            {
                stream = File.Open(fileName, FileMode.Open);
                BinaryFormatter serializer = new BinaryFormatter();
                project = (Project)serializer.Deserialize(stream);
                stream.Close();
            }
            catch
            {
                try
                {
                    stream.Close();
                }
                catch
                {
                }

                return false;
            }

            this.doc = new Document(5, new string[] { project.DataXName, project.DataYName, project.ErrDataName, "Model", "Diff" });

            doc.HasErrorY = project.HasErrorY;
            doc.Data = project.Data;
            doc.ColumnNames[0] = project.DataXName;
            doc.ColumnNames[1] = project.DataYName;
            doc.ColumnNames[2] = project.ErrDataName;
            doc.GridColumnSettings = project.GridColumnSettings;
            Configuration.GraphSettings.Title = project.Title;

            foreach (ModelFitInfo modelFitInfo in project.Models)
            {
                string name = modelFitInfo.ModelName;
                FitFunction function = null;
                if (Program.UserModels.ContainsKey(modelFitInfo.ModelName))
                {
                    function = Program.UserModels[name];
                }
                else if (Program.LibraryModels.ContainsKey(modelFitInfo.ModelName))
                {
                    function = Program.LibraryModels[name];
                }

                bool createNewFunction = function == null;

                if (modelFitInfo.ModelFunction == null)
                {
                    //This is library function which was not created during deserialization, so it must be taken from the library
                    modelFitInfo.ModelFunction = function;
                }

                if (function != null && function.Description != modelFitInfo.ModelFunction.Description)
                {
                    name = name + "_1"; //TODO here handling must be better, ask the user to recreate the function or not and if yes what shell be the name
                    createNewFunction = true;
                }

                string compileErrors;

                if (createNewFunction)
                {
                    //If function is with same name but different math description recreate it with different name and add it to user functions
                    //TODO handle the case when there are errors
                    function = UserFunction.Create(name, modelFitInfo.ModelFunction.Description, out compileErrors);
                }

                ModelFitInfo model = new ModelFitInfo()
                {
                    ModelName = name,
                    ModelFunction = function,
                    FitResultChiSquare = 0,
                    LastFitParameters = modelFitInfo.LastFitParameters,
                    SigmaParameters = modelFitInfo.SigmaParameters,
                    SigmaY = modelFitInfo.SigmaY,
                    FitResultStdError = modelFitInfo.FitResultStdError,
                    DerivedValues = modelFitInfo.DerivedValues,
                    Log = modelFitInfo.Log
                };

                if (model.DerivedValues != null)
                {
                    foreach (DerivedValueInfo derivedValueInfo in model.DerivedValues)
                    {
                        derivedValueInfo.SetCalcFunction(derivedValueInfo.CalcFunction.Description, out compileErrors);
                    }
                }

                doc.Models.Add(name, model);
            }
            if (doc.Models.Count > 0)
            {
                doc.CurrentModel = doc.Models.Values.FirstOrDefault<ModelFitInfo>();
            }
            doc.GridColumnSettings = project.GridColumnSettings;

            return true;
        }

        private void SaveProjectInXml(string fileName)
        {
            Project project = new Project();
            project.Title = Configuration.GraphSettings.Title;
            project.HasErrorY = doc.HasErrorY;
            project.Data = doc.Data;
            project.DataXName = doc.ColumnNames[0];
            project.DataYName = doc.ColumnNames[1];
            project.ErrDataName = doc.ColumnNames[2];
            project.GridColumnSettings = doc.GridColumnSettings;

            project.Models = doc.Models.Values.ToArray<ModelFitInfo>();
            List<Type> modelTypes = new List<Type>();
            foreach (ModelFitInfo modelFitInfo in project.Models)
            {
                modelTypes.Add(modelFitInfo.ModelFunction.GetType());
            }
            modelTypes.Add(typeof(UserFunction));
            FileStream stream = null;
            try
            {
                stream = File.Create(fileName);
                XmlSerializer serializer = new XmlSerializer(typeof(Project), modelTypes.ToArray());
                serializer.Serialize(stream, project);
                stream.Close();
                new InfoBox().Show("Project saved", this);
            }
            catch
            {
                MessageBox.Show("Failure saving the project file.", "Internal Error");
                return;
            }
            finally
            {
                try
                {
                    stream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Failure saving the project file.", "Internal Error");
                }
            }
        }

        private void OpenProjectInXml(string fileName)
        {
            Project project;

            //TODO Ptotect this in try catch and close the stream
            FileStream stream = File.Open(fileName, FileMode.Open);
            List<Type> modelTypes = new List<Type>();
            foreach (var fitFunction in Program.LibraryModels.Values)
            {
                modelTypes.Add(fitFunction.GetType());
            }
            modelTypes.Add(typeof(UserFunction));
            modelTypes.Add(typeof(DerivedValueInfo));
            XmlSerializer serializer = new XmlSerializer(typeof(Project), modelTypes.ToArray());
            project = (Project)serializer.Deserialize(stream);
            stream.Close();

            this.doc = new Document(5, new string[] { project.DataXName, project.DataYName, project.ErrDataName, "Model", "Diff" });

            Configuration.GraphSettings.Title = project.Title;
            doc.HasErrorY = project.HasErrorY;
            doc.Data = project.Data;
            doc.ColumnNames[0] = project.DataXName;
            doc.ColumnNames[1] = project.DataYName;
            doc.ColumnNames[2] = project.ErrDataName;
            doc.GridColumnSettings = project.GridColumnSettings;

            foreach (ModelFitInfo modelFitInfo in project.Models) //TODO Project.Models is to be changed to project.ModelFitInfo
            {
                //TODO What if the user function was deleted or has same name but different math expression? We shell recreate it and if it exists we add number to the name
                string name = modelFitInfo.ModelName;
                FitFunction function = null;
                if (Program.UserModels.ContainsKey(modelFitInfo.ModelName))
                {
                    function = Program.UserModels[name];
                }
                else if (Program.LibraryModels.ContainsKey(modelFitInfo.ModelName))
                {
                    function = Program.LibraryModels[name];
                }

                bool createNewFunction = function == null;
                if (function != null && function.Description != modelFitInfo.ModelFunction.Description)
                {
                    name = name + "_1"; //TODO here handling must be better, ask the user to recreate the function or not and if yes what shell be the name
                    createNewFunction = true;
                }

                string compileErrors;

                if (createNewFunction)
                {
                    //If function is with sam ename but different math description recreate it with different name and add it to user functions
                    //TODO handle the case when there are errors
                    function = UserFunction.Create(name, modelFitInfo.ModelFunction.Description, out compileErrors);
                }

                ModelFitInfo model = new ModelFitInfo()
                {
                    ModelName = name,
                    ModelFunction = function,
                    FitResultChiSquare = 0,
                    LastFitParameters = modelFitInfo.LastFitParameters,
                    SigmaParameters = modelFitInfo.SigmaParameters,
                    SigmaY = modelFitInfo.SigmaY,
                    FitResultStdError = modelFitInfo.FitResultStdError,
                    DerivedValues = modelFitInfo.DerivedValues
                };

                foreach (DerivedValueInfo derivedValueInfo in model.DerivedValues)
                {
                    derivedValueInfo.SetCalcFunction(derivedValueInfo.CalcFunction.Description, out compileErrors);
                }

                doc.Models.Add(name, model);
            }
            if (doc.Models.Count > 0)
            {
                doc.CurrentModel = doc.Models.Values.FirstOrDefault<ModelFitInfo>();
            }
            doc.GridColumnSettings = project.GridColumnSettings;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(projectFilePath))
            {
                saveAsToolStripMenuItem_Click(this, e); //TODO Replace this with call to the actual method
            }
            else
            {
                SaveProjectInBinary(projectFileName);
            }
        }

        private void toolStripMenuAbout_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.LeftTopPos = CenterDialog(aboutBox);
            aboutBox.ShowDialog();
        }

        private void toolStripMenuRemoveAllModels_Click(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;
            doc.Models.Clear();
            doc.CurrentModel = null;
            doc.FitDataY = null;
            doc.FitDataY = null;
            //lblChiSquared.Text = "";
            lblStdError.Text = "";
            SetCurrentModel("");
            controller.UpdateAllUI(this, UpdateMessages.MODELS_CHANGE, doc);
            Program.IsFromUser = true;

        }

        private void toolStripMenuUpdateStdErrors_Click(object sender, EventArgs e)
        {

            double[] X = doc.X;
            double[] Y = doc.Y;
            StringBuilder initializationFailures = new StringBuilder();
            initializationFailures.AppendLine("Failed to initialize parameters for:");
            initializationFailures.AppendLine("");
            bool hasInitializationFaiures = false;
            foreach (string name in doc.Models.Keys)
            {

                ModelFitInfo modelFitInfo = doc.Models[name];
                if (modelFitInfo.ModelFunction.CanInitParameters)
                {
                    try
                    {
                        if (modelFitInfo != null)
                        {
                            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                            LeastSquare ls = new LeastSquare(X, Y, modelFitInfo.LastFitParameters, new int[0], calcFunc, null);
                            double eps = ls.CalculateStdError();
                            modelFitInfo.FitResultStdError = eps;
                        }
                    }
                    catch (Exception)
                    {
                        hasInitializationFaiures = true;
                        initializationFailures.AppendLine(modelFitInfo.ModelFunction.Name);
                        LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                        LeastSquare ls = new LeastSquare(X, Y, new double[modelFitInfo.ModelFunction.Parameters.Length], new int[0], calcFunc, null);
                        double eps = ls.CalculateStdError();
                        modelFitInfo.FitResultStdError = eps;
                    }
                }
            }

            if (hasInitializationFaiures)
            {
                MessageBox.Show(initializationFailures.ToString(), "Warning");
            }
            tabMain.SelectedTab = tabPagePlot;
            controller.UpdateAllUI(this, UpdateMessages.MODELS_CHANGE, doc);
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            ((Command)undoCommands.Pop()).Execute();
            if (undoCommands.Count == 0)
            {
                RemoveState("K9");
            }
            else
            {
                toolStripButtonUndo.ToolTipText = ((Command)undoCommands.Peek()).Name;
            }
            Program.IsFromUser = false;
            if (doc.Data.Count == 0)
            {
                RemoveState("K2");
            }
            else
            {
                SetState("K2");
            }
            SendDataChangedMessage();
            Program.IsFromUser = true;
        }

        private void tableViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPageData;
        }

        private void graphViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabMain.SelectedTab = tabPagePlot;
        }

        private void toolStripButtonHelp_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Help.ShowHelp(this, Path.Combine(path, "LybraFitHelp.chm"));
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer.Interval = 2500;
            timer.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
#if !DEMO
            if (!CheckRegistered()) this.Close();
#endif
            startForm = new Forms.StartForm();
            startForm.Show();

            //Load recent files
            foreach (var fileName in Program.RecentFiles.Values)
            {
                var newMenuItem = new ToolStripMenuItem(Path.GetFileName(fileName));
                newMenuItem.Click += new EventHandler(ImportFileClick);
                toolStripMenuRecentFiles.DropDownItems.Add(newMenuItem);
            }

            //Load recent projects
            foreach (var fileName in Program.RecentProjects.Values)
            {
                var newMenuItem = new ToolStripMenuItem(Path.GetFileName(fileName));
                newMenuItem.Click += new EventHandler(LoadProjectClick);
                toolStripMenuItemRecentProjects.DropDownItems.Add(newMenuItem);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            startForm.Close();
            timer.Stop();
        }

        private void toolStripMenuItemInverseTransform_Click(object sender, EventArgs e)
        {
            double[] X = doc.X;
            double[] Y = doc.Y;
            double minY = double.MaxValue;
            double maxY = double.MinValue;
            for (int i = 0; i < Y.Length; i++)
            {
                if (Y[i] > maxY) maxY = Y[i];
                if (Y[i] < minY) minY = Y[i];
            }

            InverseTransformSettings settings = new InverseTransformSettings()
            {
                MaxY = maxY,
                MaxYV = maxY,
                MinY = minY,
                MinYV = minY
            };

            InverseTransformDialog inverseTransformDialog = new InverseTransformDialog();
            inverseTransformDialog.LeftTopPos = CenterDialog(inverseTransformDialog);

            if (inverseTransformDialog.ShowDialog(doc.ColumnNames, ref settings) == System.Windows.Forms.DialogResult.OK)
            {
                UndoInverseTransformCommand undoInverseTransformCommand = new UndoInverseTransformCommand(doc);
                undoCommands.Push(undoInverseTransformCommand);
                toolStripButtonUndo.ToolTipText = undoInverseTransformCommand.Name;
                SetState("K9");

                double[] Err = new double[X.Length];
                InverseTransform.InverseData(ref X, ref Y, ref Err, doc.yCalculationFunction, settings.MinY, settings.MaxY, settings.StepY, settings.Eps);

                doc.Data.Clear();
                string firstColumnName = doc.ColumnNames[0];
                doc.ColumnNames[0] = doc.ColumnNames[1];
                doc.ColumnNames[1] = firstColumnName;

                doc.GridColumnSettings[0].Name = doc.GridColumnSettings[1].Name;
                doc.GridColumnSettings[1].Name = firstColumnName;

                for (int i = 0; i < X.Length; i++)
                {
                    doc.Data.Add(new double[] { X[i], Y[i], Err[i], 0, 0 });
                }

                doc.HasErrorY = true;
                doc.GridColumnSettings[2].Visible = true;
                tabMain.SelectedTab = tabPageData;
                Program.IsFromUser = false;
                controller.UpdateAllUI(this, UpdateMessages.NEW_DATA, doc);
                Program.IsFromUser = true;
            }
        }

        private void pictureBoxModelPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (mathExpression == "")
            {
                g.FillRectangle(new SolidBrush(pictureBoxModelPreview.BackColor), pictureBoxModelPreview.ClientRectangle);
            }
            else
            {
                new MathControl(pictureBoxModelPreview.Width, pictureBoxModelPreview.Height).DrawExpression(g, mathExpression, pictureBoxModelPreview.ClientRectangle, pictureBoxModelPreview.BackColor);
            }
        }

        private void dataGridModels_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && !e.Shift)
            {
                dataGridParameters.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift)
            {
                btnPlot.Focus();
                e.Handled = true;
            }
        }

        private void dataGridParameters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && !e.Shift)
            {
                btnPlot.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift)
            {
                dataGridModels.Focus();
                e.Handled = true;
            }
        }

        private void dataGridParameters_Leave(object sender, EventArgs e)
        {
            dataGridParameters.ClearSelection();
            dataGridParameters.Refresh();
        }

        private void dataGridModels_Leave(object sender, EventArgs e)
        {
            dataGridModels.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Info;
            dataGridModels.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void dataGridModels_Enter(object sender, EventArgs e)
        {
            dataGridModels.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridModels.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void derivedValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridModels.SelectedRows.Count == 1)
            {
                string selectedModelName = dataGridModels.SelectedRows[0].Cells[0].Value.ToString();
                new DerviedValuesDialog().ShowDialog(selectedModelName, doc, this);
            }
        }

        private void toolStripMenuAddToLog_Click(object sender, EventArgs e)
        {
            if (doc.CurrentModel != null)
            {
                LogNameDialog logNameDialog = new LogNameDialog();
                if (logNameDialog.Show(Configuration.GraphSettings.Title, this) == System.Windows.Forms.DialogResult.OK)
                {
                    doc.CurrentModel.LogFitResults(logNameDialog.Identifier);
                    LogDialog logDialog = new LogDialog();
                    logDialog.Show(doc.CurrentModel, this);
                }
            }
        }

        private void toolStripMenuFitLogsReport_Click(object sender, EventArgs e)
        {
            if (doc.CurrentModel != null)
            {
                LogDialog logDialog = new LogDialog();
                logDialog.Show(doc.CurrentModel, this);
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView.SelectedCells != null && dataGridView.SelectedCells.Count != 0)
                {
                    dataGridView.Rows[dataGridView.SelectedCells[0].RowIndex].Selected = true;
                    deleteSelectedStripMenuItem1_Click(this, null);
                }
            }
        }


        private static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        private bool CheckRegistered()
        {
            var regFilePath = Path.Combine(Program.GetApplicationDataFolder(), "..\\lfr.bin");
            if (!File.Exists(regFilePath))
            {
                string questionKey = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
                var registrationForm = new RegistrationForm();
                string activationKey = registrationForm.ShowDialog(questionKey);
                if (activationKey == null) return false;
                activationKey = activationKey.ToUpper();
                var sb = new StringBuilder();

                string s = registrationForm.FirstName + registrationForm.LastName + questionKey;
                for (int i = 0; i < 3; i++)
                {
                    int j = i;
                    while (j < s.Length)
                    {
                        sb.Append((char)((int)s[j] + 2)); j = j + 3;
                    }
                }
                string sk = GenerateSHA256String("LybraFit18112018" + sb.ToString()).Substring(0, 6);
                if (sk != activationKey)
                {
                    MessageBox.Show("Wrong activation key. Registration failed.");
                    return false;
                }
                var regFile = File.CreateText(regFilePath);
                s = GenerateSHA256String("LybraFit11112018" + System.Environment.MachineName).Substring(0, 12);
                regFile.Write(s);
                regFile.Close();
                return true;
            }
            else
            {
                var regFile = File.OpenText(regFilePath);
                string s1 = GenerateSHA256String("LybraFit11112018" + System.Environment.MachineName).Substring(0, 12);
                string s2 = regFile.ReadToEnd();
                regFile.Close();
                if (s1 != s2)
                {
                    MessageBox.Show("You are trying to start unregistered copy. Please go to vendor's registration page!");
                    return false;
                }
            }

            return true;
        }

        private void toolStripMenuItemLeaveSelected_Click(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;
            //.Net is smart enough to pass here selected rows sorted in reverse order so removing of one row does not change index of the others
            for (int i = dataGridModels.Rows.Count - 1; i >= 0; i--)
            {
                if (!dataGridModels.Rows[i].Selected)
                {
                    string modelName = dataGridModels.Rows[i].Cells[0].Value.ToString();
                    dataGridModels.Rows.RemoveAt(i);
                    doc.Models.Remove(modelName);
                }
            }

            Program.IsFromUser = true;

            if (dataGridModels.Rows.Count > 0 && dataGridModels.SelectedRows.Count == 0)
            {
                dataGridModels.Rows[0].Selected = true;
            }
            else
            {
                doc.CurrentModel = null;
                doc.GridColumnSettings[3].Visible = false;
                doc.GridColumnSettings[4].Visible = false;
            }
            dataGridModels_SelectionChanged(this, null);

            controller.UpdateAllUI(this, UpdateMessages.NEW_FIT, doc);
        }

        private void toolStripMenuPasteParameters_Click(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            string text = Clipboard.GetText();
            if (!string.IsNullOrEmpty(text))
            {
                string[] lines = Clipboard.GetText().Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    string[] tokens = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    if (tokens.Length == 2)
                    {
                        var name = tokens[0].Trim();
                        double value;
                        if (double.TryParse(tokens[1].Trim(), out value))
                        {
                            foreach (DataGridViewRow row in dataGridParameters.Rows)
                            {
                                if (name == row.Cells[0].Value.ToString())
                                {
                                    row.Cells[2].Value = value;
                                }
                            }
                        }
                    }
                }
                btnPlot_Click(this, null);
            }
            Program.IsFromUser = true;
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!Program.IsFromUser) return;
            double newValue = 0.0;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

            if (!double.TryParse(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out newValue))
            {
                MessageBox.Show("Invalid value");
                dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = doc.Data[e.RowIndex][e.ColumnIndex];
                return;
            }

            Program.IsFromUser = false;
            doc.Data[e.RowIndex][e.ColumnIndex] = newValue;

            if (e.ColumnIndex > 0 && (dataGridView.Rows[e.RowIndex].Cells[0].Value == null || string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())))
            {
                dataGridView.Rows[e.RowIndex].Cells[0].Value = 0;
                doc.Data[e.RowIndex][0] = 0;
            }

            //Calculate standard deviation as Sqrt of Y value
            if (e.ColumnIndex == 1)
            {
                if (Configuration.NewDataSetting.ErrDataCalculatedStd)
                {
                    dataGridView.Rows[e.RowIndex].Cells[2].Value = (double)Math.Sqrt(Math.Abs(newValue));
                    doc.Data[e.RowIndex][2] = (double)Math.Sqrt(Math.Abs(newValue));
                }
            }
            else if (dataGridView.Rows[e.RowIndex].Cells[2].Value == null || string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString()))
            {
                dataGridView.Rows[e.RowIndex].Cells[2].Value = 0;
                doc.Data[e.RowIndex][2] = 0;
            }

            //Check if data is entered out of order. Also perform auto calculation if it is set
            if (e.ColumnIndex == 0)
            {
                if (doc.YCalculated)
                {
                    double v1 = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ? 0 : double.Parse(dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    double v2 = doc.yCalculationFunction.Calculate(v1, new double[0]);
                    doc.Data[e.RowIndex][1] = v2;
                    dataGridView.Rows[e.RowIndex].Cells[1].Value = v2;
                }
                else if (dataGridView.Rows[e.RowIndex].Cells[1].Value == null || string.IsNullOrEmpty(dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString()))
                {
                    doc.Data[e.RowIndex][1] = 0;
                    dataGridView.Rows[e.RowIndex].Cells[1].Value = 0;
                }

                if (Configuration.NewDataSetting.ErrDataCalculatedStd)
                {
                    dataGridView.Rows[e.RowIndex].Cells[2].Value = (double)Math.Sqrt(Math.Abs(doc.Data[e.RowIndex][1]));
                    doc.Data[e.RowIndex][2] = (double)Math.Sqrt(Math.Abs(doc.Data[e.RowIndex][1]));
                }

                //Preserve sorting of the data with respect to first column
                if (Configuration.AutoSort)
                {
                    bool inversedOrder = false;
                    if (e.RowIndex > 0)
                    {
                        var index = e.RowIndex;
                        double rowFirstColumnValue = 0;
                        do
                        {
                            index--;
                            rowFirstColumnValue = dataGridView.Rows[index].Cells[e.ColumnIndex].Value == null || string.IsNullOrEmpty(dataGridView.Rows[index].Cells[e.ColumnIndex].Value.ToString()) ? 0 : double.Parse(dataGridView.Rows[index].Cells[e.ColumnIndex].Value.ToString());
                            inversedOrder |= rowFirstColumnValue > newValue;
                        } while (index > 0 && rowFirstColumnValue > newValue);

                        if (inversedOrder)
                        {
                            int destinationRowIndex = index == 0 && rowFirstColumnValue > newValue ? 0 : index + 1;
                            doc.MoveRow(e.RowIndex, destinationRowIndex);
                            var table = dataGridView.DataSource as DataTable;
                            dataGridView.BindingContext[dataGridView.DataSource].EndCurrentEdit();
                            System.Data.DataRow addedRow = table.Rows[e.RowIndex];
                            System.Data.DataRow newRow = table.NewRow();
                            newRow.ItemArray = addedRow.ItemArray;
                            table.Rows.RemoveAt(e.RowIndex);
                            table.Rows.InsertAt(newRow, destinationRowIndex);
                            dataGridView.Rows[destinationRowIndex].Selected = true;
                        }
                    }

                    if (!inversedOrder && e.RowIndex < dataGridView.RowCount - 2)
                    {
                        var index = e.RowIndex;
                        double rowFirstColumnValue = 0;
                        inversedOrder = false;
                        do
                        {
                            index++;
                            rowFirstColumnValue = dataGridView.Rows[index].Cells[e.ColumnIndex].Value == null || string.IsNullOrEmpty(dataGridView.Rows[index].Cells[e.ColumnIndex].Value.ToString()) ? 0 : double.Parse(dataGridView.Rows[index].Cells[e.ColumnIndex].Value.ToString());
                            inversedOrder |= rowFirstColumnValue < newValue;
                        } while (index < dataGridView.RowCount - 2 && rowFirstColumnValue < newValue);

                        if (inversedOrder)
                        {
                            int destinationRowIndex = index == dataGridView.RowCount - 2 && rowFirstColumnValue < newValue ? dataGridView.RowCount - 2 : index - 1;
                            if (destinationRowIndex == doc.Data.Count)
                            {
                                double[] newRowValues = new double[doc.ColumnCount];
                                doc.Data.Insert(destinationRowIndex, newRowValues);
                            }
                            doc.MoveRow(e.RowIndex, destinationRowIndex);
                            var table = dataGridView.DataSource as DataTable;
                            dataGridView.BindingContext[dataGridView.DataSource].EndCurrentEdit();
                            System.Data.DataRow addedRow = table.Rows[e.RowIndex];
                            System.Data.DataRow newRow = table.NewRow();
                            newRow.ItemArray = addedRow.ItemArray;
                            table.Rows.RemoveAt(e.RowIndex);
                            table.Rows.InsertAt(newRow, destinationRowIndex);
                            dataGridView.Rows[destinationRowIndex].Selected = true;
                        }
                    }
                }
            }

            SendDataChangedMessage(dataGridView);

            Program.IsFromUser = true;
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void toolStripMenuSort_Click(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            doc.Sort();
            SendDataChangedMessage(null);
            Program.IsFromUser = true;
        }

        private void toolStripButtonGraphicalReport_Click(object sender, EventArgs e)
        {
            string title = String.IsNullOrEmpty(graph.Title) ? Configuration.GraphSettings.Title : graph.Title;
            string subTitle = String.Empty;

            plotExportDialog.LeftTopPos = CenterDialog(plotExportDialog);
            bool landScape = false;
            if (graphReportSize.IsEmpty)
            {
                graphReportSize = new Size(graph.Width, graph.Height);
            }

            Size size = plotExportDialog.ShowDialog(ref graphReportUnits, ref graphReportSize, ref title, ref subTitle, doc, ref graphReportResolution, ref landScape, ref graphReportMargins);
            if (size != Size.Empty)
            {
                float resolutionScaling = (float)graphReportResolution / Constants.STANDARD_DPI; //96 dpi is the default resolution when the bitmap initialized
                Bitmap bmp = new Bitmap((int)(size.Width), (int)(size.Height));
                bmp.SetResolution(graphReportResolution, graphReportResolution);

                string oldTitle = graph.Title;
                graph.ShowTitle = true;
                graph.Title = title;
                graph.SubTitle = subTitle;
                graph.SubTitleFont = new Font(graph.TitleFont.FontFamily, graph.TitleFont.Size - 2, graph.TitleFont.Style);
                int graphMargins = graph.InternalMargins;
                graph.InternalMargins = (int)((float)graphReportMargins / resolutionScaling);
                graph.DrawContent(ref bmp, resolutionScaling); //reslution scaling is need for all drawing. Setting bitmap resoultion takes care about the fonts
                if (landScape)
                {
                    bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                }
                GraphicalReport graphicalReport = new GraphicalReport();
                graphicalReport.LeftTopPos = CenterDialog(graphicalReport);
                graphicalReport.ShowDialog(bmp);
                graph.Title = oldTitle;
                graph.InternalMargins = graphMargins;
                graph.ShowTitle = false;
                graph.SubTitle = String.Empty;
            }
        }

        private void dataGridView_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            if (doc.XAutoIncrement)
            {
                if (e.Row.Index == 0)
                {
                    e.Row.Cells[0].Value = doc.XAutoIncrementStep;
                }
                else
                {
                    var prevValue = (double)dataGridView.Rows[e.Row.Index - 1].Cells[0].Value;
                    e.Row.Cells[0].Value = prevValue + doc.XAutoIncrementStep;
                }
            }
        }
    }
}
