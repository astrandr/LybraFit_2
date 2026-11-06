//Designed and Implemented by Andrey Batchvarov 2018
using LybraFit.Common;
//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using FuncLib;
using MathLib;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class ModelsDialog : Form
    {

        private Document doc = null;
        private string mathExpression = "";
        private MathModelDialog mathModelDialog;
        public Point LeftTopPos;
        public bool ModelsChanged = false;

        public ModelsDialog(Document doc)
        {
            InitializeComponent();
            this.doc = doc;
            PopulateFitFunctions();
            PopulateUserFunctions();
            mathModelDialog = new MathModelDialog();
        }

        public DialogResult ShowDialog(Document doc)
        {
            this.doc = doc;
            this.ModelsChanged = false;
            return base.ShowDialog();
        }

        private void PopulateFitFunctions()
        {
            dataGridFitFunctions.Rows.Clear();
            foreach (BaseFunction function in Program.LibraryModels.Values)
            {
                bool modelChecked = doc.Models.Keys.Contains(function.Name);
                dataGridFitFunctions.Rows.Add(modelChecked, function.Name, function.Description);
            }
        }

        private void PopulateUserFunctions()
        {
            dataGridCustomFunctions.Rows.Clear();
            foreach (BaseFunction function in Program.UserModels.Values)
            {
                bool modelChecked = doc.Models.Keys.Contains(function.Name);
                dataGridCustomFunctions.Rows.Add(modelChecked, function.Name, function.Description);
            }

            btnEditUserFunction.Enabled = false;
        }

        private void FilterLibraryFunctions(string filter)
        {
            string[] keyWords = filter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < keyWords.Length; i++)
            {
                keyWords[i] = keyWords[i].ToUpper().Trim();
            }

            foreach (DataGridViewRow row in dataGridFitFunctions.Rows)
            {
                bool found = true;
                foreach (string keyWord in keyWords)
                {
                    if (row.Cells["LibName"].Value != null && row.Cells["Description"].Value != null)
                    {
                        if (!row.Cells["LibName"].Value.ToString().ToUpper().Contains(keyWord) &&
                            !row.Cells["Description"].Value.ToString().ToUpper().Contains(keyWord))
                        {
                            found = false;
                            break;
                        }
                    }
                }
                if (!row.IsNewRow)
                {
                    row.Visible = keyWords.Length == 0 || found;
                }
            }
        }

        private void FilterUserFunctions(string filter)
        {
            string[] keyWords = filter.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < keyWords.Length; i++)
            {
                keyWords[i] = keyWords[i].ToUpper().Trim();
            }

            foreach (DataGridViewRow row in dataGridCustomFunctions.Rows)
            {
                bool found = true;
                foreach (string keyWord in keyWords)
                {
                    if (row.Cells["Name"].Value != null && row.Cells["MathExpression"].Value != null)
                    {
                        if (!row.Cells["Name"].Value.ToString().ToUpper().Contains(keyWord) &&
                            !row.Cells["MathExpression"].Value.ToString().ToUpper().Contains(keyWord))
                        {
                            found = false;
                            break;
                        }
                    }
                }
                if (!row.IsNewRow)
                {
                    row.Visible = keyWords.Length == 0 || found;
                }
            }
        }

        private bool SelectUserFunction(string name)
        {
            name = name.ToUpper();
            foreach (DataGridViewRow row in dataGridCustomFunctions.Rows)
            {
                if (row.Cells["Name"].Value != null)
                {
                    if (row.Cells["Name"].Value.ToString().ToUpper().Contains(name))
                    {
                        row.Selected = true;
                        return true;
                    }
                }
            }
            return false;
        }

        private Point CenterDialog(Form dialog)
        {
            Point p = Point.Empty;
            p.Y = (this.Top + this.Bottom) / 2 - (dialog.Bottom - dialog.Top) / 2;
            p.X = (this.Left + this.Right) / 2 - (dialog.Right - dialog.Left) / 2;

            return p;
        }
        private void btnEditUserFunction_Click(object sender, EventArgs e)
        {
            MathModelDialog mathModelDialog = new MathModelDialog();
            mathModelDialog.LeftTopPos = CenterDialog(mathModelDialog);
            UserFunction function = Program.UserModels[dataGridCustomFunctions.SelectedRows[0].Cells["Name"].Value.ToString()];

            if (mathModelDialog.ShowDialog(function) != DialogResult.OK) return;

            Program.IsFromUser = false;

            function = mathModelDialog.UserFunction;
            MessageDialog messageDialog = new MessageDialog();
            messageDialog.LeftTopPos = CenterDialog(messageDialog);


            foreach (DataGridViewRow row in dataGridFitFunctions.Rows)
            {
                if (row.Cells["Description"].Value != null && row.Cells["Description"].Value.ToString() == function.Expression.Trim().Replace(" ", ""))
                {
                    new InfoBox().Show(new string[] { "Model with given math expression", "exists in library: " + row.Cells["Description"].Value.ToString() }, "Warning", this);
                    {
                        Program.IsFromUser = true;
                        dataGridCustomFunctions_SelectionChanged(this, null);
                        return;
                    }
                }
            }

            function.SetUniqueDescription();

            foreach (string functionName in Program.LibraryModels.Keys)
            {
                if (Program.LibraryModels[functionName].UniqueDescription == function.UniqueDescription)
                {
                    new InfoBox().Show(new string[] { "Model with equivalent math expression", "exists in library: " + functionName }, "Warning", this);
                    {
                        Program.IsFromUser = true;
                        dataGridCustomFunctions_SelectionChanged(this, null);
                        return;
                    }
                }
            }

            Program.UserModels[function.Name] = function;
            PopulateUserFunctions();
            Program.IsFromUser = true;

            SelectUserFunction(function.Name);
            dataGridCustomFunctions.Refresh();
            dataGridCustomFunctions_SelectionChanged(this, null);
            ModelsChanged = true;
        }

        private void btnNewUserFunction_Click(object sender, EventArgs e)
        {
#if DEMO
            if (dataGridCustomFunctions.Rows.Count > 0)
            {
                InfoBox.ShowMessage("Demo version does not allow more than one custom models", "Warning", this);
                return;
            }
#endif
            mathModelDialog.LeftTopPos = CenterDialog(mathModelDialog);
            if (mathModelDialog.ShowDialog(null) != DialogResult.OK) return;

            MessageDialog messageDialog = new MessageDialog();
            messageDialog.LeftTopPos = CenterDialog(messageDialog);

            Program.IsFromUser = false;
            UserFunction function = mathModelDialog.UserFunction;

            //Check if the funciton does not exist in library. Check if exists in user functions and replace the existing one if confirmed
            foreach (DataGridViewRow row in dataGridCustomFunctions.Rows)
            {
                if (row.Cells["Name"].Value != null && row.Cells["Name"].Value.ToString().Replace(" ", "") == function.Name.Trim().Replace(" ", ""))
                {
                    if (messageDialog.Show("Warning ",
                        new string[]{"Model with given name already","exists in custom functions",row.Cells["Name"].Value.ToString(),
                        "Do you want to replace existing one?"}, this) == DialogResult.No)
                    {
                        row.Selected = true;
                        dataGridCustomFunctions_SelectionChanged(this, null);
                        Program.IsFromUser = true;
                        return;
                    }
                    else { break; }
                }
            }

            foreach (DataGridViewRow row in dataGridFitFunctions.Rows)
            {
                if (row.Cells["LibName"].Value != null && row.Cells["LibName"].Value.ToString().Replace(" ", "") == function.Name.Trim().Replace(" ", ""))
                {
                    new InfoBox().Show("Model with given name already exists in library.", this);
                    dataGridCustomFunctions_SelectionChanged(this, null);
                    row.Selected = true;
                    Program.IsFromUser = true;
                    return;
                }
            }

            foreach (DataGridViewRow row in dataGridCustomFunctions.Rows)
            {
                if (row.Cells["MathExpression"].Value != null && row.Cells["MathExpression"].Value.ToString().Replace(" ", "") == function.Description.Trim().Replace(" ", ""))
                {
                    if (messageDialog.Show("Warning ",
                        new string[]{"Model with given math expression","exists in user functions:", row.Cells["Name"].Value.ToString(),
                        "Do you want to replace existing one?"}, this) == DialogResult.No)
                    {
                        row.Selected = true;
                        dataGridCustomFunctions_SelectionChanged(this, null);
                        Program.IsFromUser = true;
                        return;
                    }
                    else
                    {
                        Program.UserModels.Remove(row.Cells["Name"].Value.ToString());
                        break;
                    }
                }
            }

            foreach (DataGridViewRow row in dataGridFitFunctions.Rows)
            {
                if (row.Cells["Description"].Value != null && row.Cells["Description"].Value.ToString().Replace(" ", "") == function.Description.Trim().Replace(" ", ""))
                {
                    new InfoBox().Show("Model with given math expression already exists in library:" + row.Cells["LibName"].Value.ToString(), this);
                    row.Selected = true;
                    dataGridCustomFunctions_SelectionChanged(this, null);
                    Program.IsFromUser = true;
                    return;
                }
            }

            function.SetUniqueDescription();

            foreach (string functionName in Program.LibraryModels.Keys)
            {
                if (Program.LibraryModels[functionName].UniqueDescription == function.UniqueDescription)
                {
                    new InfoBox().Show("Model with equivalent math expression already exists in library:" + functionName, this);
                    dataGridCustomFunctions_SelectionChanged(this, null);
                    Program.IsFromUser = true;
                    return;
                }

            }

            foreach (string functionName in Program.UserModels.Keys)
            {
                if (Program.UserModels[functionName].UniqueDescription == function.UniqueDescription)
                {

                    if (messageDialog.Show("Warning ",
                        new string[]{"Model with equivalent math expression","exists in user functions:",functionName,
                        "Do you want to replace it?"}, this) == DialogResult.No)
                    {
                        Program.IsFromUser = true;
                        dataGridCustomFunctions_SelectionChanged(this, null);
                        return;
                    }
                    else
                    {
                        Program.UserModels.Remove(functionName);
                        break;
                    }
                }
            }

            if (Program.UserModels.Keys.Contains(function.Name))
            {
                Program.UserModels.Remove(function.Name);
            }

            Program.UserModels.Add(function.Name, function);
            Program.SaveUserFunctions();

            PopulateUserFunctions();

            Program.IsFromUser = true;

            SelectUserFunction(function.Name);
            dataGridCustomFunctions.Refresh();
            dataGridCustomFunctions_SelectionChanged(this, null);
            //dataGridCustomFunctions.SelectedRows[0].Cells[0].Value = true; TODO Remove when tested

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Program.IsFromUser = false;

            int rowToSelect = 0;
            //Remove the selected user function from the grid and from the user functions in the application
            foreach (DataGridViewRow row in dataGridCustomFunctions.SelectedRows)
            {
                if (row.Index <= dataGridCustomFunctions.Rows.Count - 1)
                {
                    if (row.Index > 0 && rowToSelect == 0)
                    {
                        rowToSelect = row.Index - 1;
                    }
                    Program.UserModelsInitialParameter.Remove(row.Cells["Name"].Value.ToString());
                    Program.UserModels.Remove(row.Cells["Name"].Value.ToString());
                    dataGridCustomFunctions.Rows.Remove(row);
                }
            }

            if (dataGridCustomFunctions.Rows.Count > 0)
            {
                dataGridCustomFunctions.Rows[rowToSelect].Selected = true;
            }

            btnRemoveUserFunction.Enabled = dataGridCustomFunctions.SelectedRows.Count > 0;

            Program.SaveUserFunctions();

            Program.IsFromUser = true;

            dataGridCustomFunctions_SelectionChanged(this, null);
        }

        private void dataGridCustomFunctions_SelectionChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            btnRemoveUserFunction.Enabled = dataGridCustomFunctions.SelectedRows.Count == 1;
            btnEditUserFunction.Enabled = dataGridCustomFunctions.SelectedRows.Count == 1;
            if (dataGridCustomFunctions.SelectedRows.Count == 1)
            {
                mathExpression = dataGridCustomFunctions.SelectedRows[0].Cells[2].Value.ToString();
            }
            else
            {
                mathExpression = "";
            }
            pictFunctionPreview.Refresh();
        }

        private void dataGridFitFunctions_SelectionChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            mathExpression = dataGridFitFunctions.SelectedRows[0].Cells[2].Value.ToString();
            pictFunctionPreview.Refresh();
        }

        private void dataGridCustomFunctions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dataGridFitFunctions_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void UpdateModels()
        {
            Program.SaveUserFunctions();
            double[] X = doc.X;
            double[] Y = doc.Y;
            foreach (DataGridViewRow row in dataGridFitFunctions.Rows)
            {
                if (row.IsNewRow) continue;
                string name = row.Cells["LibName"].Value.ToString();

                //TODO: This mechanism is to be moved to the document or service class
                if ((bool)row.Cells["Checked"].Value == true && !doc.Models.Keys.Contains(name))
                {
                    if (!ModelsChanged)
                    {
                        ModelsChanged = true;
                    }
                    FitFunction function = Program.LibraryModels[name];
                    ModelFitInfo modelFitInfo = new ModelFitInfo() { ModelName = name, ModelFunction = function, FitResultChiSquare = 0, FitResultStdError = 0 };
                    if (modelFitInfo.ModelFunction.CanInitParameters)
                    {
                        try
                        {
                            if (Configuration.RegressionDataSmoothPoints == 0 || Y.Length < Configuration.RegressionDataSmoothPoints * 4)
                            {
                                modelFitInfo.LastFitParameters = modelFitInfo.ModelFunction.InitParameters(X, Y);
                            }
                            else
                            {
                                modelFitInfo.LastFitParameters = modelFitInfo.ModelFunction.InitParameters(X, DataSmooth.MovingAverage(Y, Configuration.RegressionDataSmoothPoints));
                            }

                            LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                            int[] flagsArr = new int[modelFitInfo.LastFitParameters.Length]; for (int i = 0; i < flagsArr.Length; i++) { flagsArr[i] = i; };
                            LeastSquare ls = new LeastSquare(X, Y, modelFitInfo.LastFitParameters, new int[0], calcFunc, null);
                            double eps = ls.CalculateStdError();
                            double[] errY;
                            ls.GetFitErrorAllParameters(0.01f, out modelFitInfo.SigmaParameters, out errY);
                            modelFitInfo.FitResultStdError = eps;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Failed to initialize parameters for: " + modelFitInfo.ModelFunction.Name, "Internal Error");
                        }
                    }
                    doc.Models.Add(name, modelFitInfo);
                }
            }

            foreach (DataGridViewRow row in dataGridCustomFunctions.Rows)
            {
                if (row.IsNewRow) continue;
                string name = row.Cells["Name"].Value.ToString();
                FitFunction function = Program.UserModels[name];
                //If functions in the document models but is changed remove it
                if (doc.Models.Keys.Contains(name) && doc.Models[name].ModelFunction.Description != function.Description)
                {
                    doc.Models.Remove(name);
                    row.Cells["Selected"].Value = true; //Mark the model to be added  - new version
                }
                if ((bool)row.Cells["Selected"].Value == true && !doc.Models.Keys.Contains(name))
                {
                    if (!ModelsChanged)
                    {
                        ModelsChanged = true;
                    }

                    ModelFitInfo modelFitInfo = new ModelFitInfo()
                    {
                        ModelName = name,
                        ModelFunction = function,
                        FitResultChiSquare = 0,
                        FitResultStdError = 0,
                        LastFitParameters = new double[function.Parameters.Length]
                    };
                    if (Program.UserModelsInitialParameter.ContainsKey(name))
                    {
                        //Check against wrong number of parameters
                        if (function.Parameters.Length == Program.UserModelsInitialParameter[name].Length)
                        {
                            modelFitInfo.LastFitParameters = Program.UserModelsInitialParameter[name];
                        }
                    }
                    LeastSquare.CalculateValuesDalegate calcFunc = new LeastSquare.CalculateValuesDalegate(modelFitInfo.ModelFunction.CalcProcedure);
                    int[] flagsArr = new int[modelFitInfo.LastFitParameters.Length]; for (int i = 0; i < flagsArr.Length; i++) { flagsArr[i] = i; };
                    LeastSquare ls = new LeastSquare(X, Y, modelFitInfo.LastFitParameters, new int[0], calcFunc, null);
                    double eps = ls.CalculateStdError();
                    double[] errY;
                    ls.GetFitErrorAllParameters(0.01f, out modelFitInfo.SigmaParameters, out errY);
                    modelFitInfo.FitResultStdError = eps;
                    doc.Models.Add(name, modelFitInfo);

                }
            }
        }

        private void ModelsDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            txtModelFilter.Focus();
        }

        private void ModelsDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                btnOk.Focus();
                btnOk_Click(this, null);
            }

            if (e.KeyCode == Keys.F1)
            {
                Help.ShowHelp(this, "LybraFitHelp.chm");
            }

            if (e.Control && e.KeyCode == Keys.L)
            {
                tabCtrlModels.SelectedTab = tabPageLibraryModels;
                dataGridFitFunctions.Focus();
            }

            if (e.Control && e.KeyCode == Keys.U)
            {
                tabCtrlModels.SelectedTab = tabPageCustomModels;
                dataGridCustomFunctions.Focus();
            }

            if (e.Control && e.KeyCode == Keys.F)
            {
                txtModelFilter.Focus();
            }

            if (e.Control && e.KeyCode == Keys.E)
            {
                btnEditUserFunction_Click(this, null);
            }

            if (e.Control && e.KeyCode == Keys.N)
            {
                btnNewUserFunction_Click(this, null);
            }

            if (e.Control && e.KeyCode == Keys.R)
            {
                btnRemove_Click(this, null);
            }

        }

        private void tabCtrlModels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtrlModels.SelectedTab == tabPageLibraryModels)
            {
                if (dataGridFitFunctions.SelectedRows.Count > 0)
                {
                    mathExpression = dataGridFitFunctions.SelectedRows[0].Cells[2].Value.ToString();
                }
                else
                {
                    mathExpression = "";
                }
            }
            else
            {
                if (dataGridCustomFunctions.SelectedRows.Count > 0)
                {
                    mathExpression = dataGridCustomFunctions.SelectedRows[0].Cells[2].Value.ToString();
                }
                else
                {
                    mathExpression = "";
                }
            }
            pictFunctionPreview.Refresh();
        }

        private void pictFunctionPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (mathExpression == "")
            {
                g.FillRectangle(new SolidBrush(pictFunctionPreview.BackColor), pictFunctionPreview.ClientRectangle);
            }
            new MathControl(pictFunctionPreview.Width, pictFunctionPreview.Height).DrawExpression(g, mathExpression, pictFunctionPreview.ClientRectangle, pictFunctionPreview.BackColor);
        }

        private void dataGridCustomFunctions_Enter(object sender, EventArgs e)
        {
            dataGridCustomFunctions.BackgroundColor = Color.White;
        }

        private void dataGridCustomFunctions_Leave(object sender, EventArgs e)
        {
            dataGridCustomFunctions.BackgroundColor = System.Drawing.SystemColors.Info;
        }

        private void ModelsDialog_Shown(object sender, EventArgs e)
        {
            txtModelFilter.Focus();
        }

        private void dataGridCustomFunctions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && !e.Shift)
            {
                btnEditUserFunction.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift)
            {
                txtModelFilter.Focus();
                e.Handled = true;
            }
        }

        private void txtModelFilter_TextChanged(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            try
            {
                FilterLibraryFunctions(txtModelFilter.Text);
                FilterUserFunctions(txtModelFilter.Text);
            }
            catch
            {
                MessageBox.Show("Error while applying filter.", "Error");
            }
            Program.IsFromUser = true;
        }

        private void dataGridFitFunctions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && !e.Shift)
            {
                btnCancel.Focus();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Tab && e.Shift)
            {
                txtModelFilter.Focus();
                e.Handled = true;
            }
        }

        private void txtModelFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (tabCtrlModels.SelectedTab == tabPageLibraryModels)
                    dataGridFitFunctions.Focus();
                else
                    dataGridCustomFunctions.Focus();

                e.Handled = true;
            }
        }

        private void dataGridCustomFunctions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            Cursor = Cursors.WaitCursor;
            btnOk.Enabled = false;
            btnCancel.Enabled = false;
            dataGridFitFunctions.Enabled = false;
            dataGridCustomFunctions.Enabled = false;
            tabCtrlModels.Enabled = false;
            await Task.Run(() => UpdateModels());
            btnOk.Enabled = true;
            btnCancel.Enabled = true;
            tabCtrlModels.Enabled = true;
            dataGridFitFunctions.Enabled = true;
            dataGridCustomFunctions.Enabled = true;
            Cursor = Cursors.Default;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Program.IsFromUser = true;
            this.Close();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridFitFunctions.SelectedRows.Count > 0)
            {
                Clipboard.SetText(dataGridFitFunctions.SelectedRows[0].Cells[2].Value.ToString());
            }
        }
    }
}
