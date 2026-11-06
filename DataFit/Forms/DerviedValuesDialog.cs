//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class DerviedValuesDialog : Form
    {
        public Point LeftTopPos;

        private Document doc = null;
        private string modelName = "";
        private ModelFitInfo modelFitInfo;
        private List<DerivedValueInfo> derivedValuesInfos;

        public DerviedValuesDialog()
        {
            Program.IsFromUser = false;
            InitializeComponent();
            Program.IsFromUser = true;
        }

        private Point CenterToParent(Control dialog)
        {
            Point p = Point.Empty;
            p.Y = (dialog.Bottom + dialog.Top) / 2 - (this.Height) / 2;
            p.X = (dialog.Right + dialog.Left) / 2 - (this.Width) / 2;
            return p;
        }

        public DialogResult ShowDialog(string modelName, Document doc, Control parent = null)
        {
            this.doc = doc;
            this.modelName = modelName;
            this.modelFitInfo = doc.Models[modelName];
            this.lblModelName.Text = modelFitInfo.ModelFunction.Description;

            PopulateDerivedValues();
            if (!modelFitInfo.CanModifyDerivedValues())
            {
                new InfoBox().Show("Model log is not emtpy. Cannot modify the dervied values", this);
                this.dataGridCalculatedValues.ReadOnly = true;
                this.dataGridCalculatedValues.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            else
            {
                this.dataGridCalculatedValues.ReadOnly = false;
                this.dataGridCalculatedValues.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            if (parent != null)
            {
                this.LeftTopPos = CenterToParent(parent);
            }

            return this.ShowDialog();
        }

        private void PopulateDerivedValues()
        {
            Program.IsFromUser = false;
            ModelFitInfo modelFitInfo = doc.Models[modelName];
            if (modelFitInfo.DerivedValues == null)
            {
                modelFitInfo.DerivedValues = new List<DerivedValueInfo>();
            }
            derivedValuesInfos = modelFitInfo.DerivedValues;
            dataGridCalculatedValues.Rows.Clear();
            foreach (var derivedValue in derivedValuesInfos)
            {
                if (UpdateDerivedValueInfo(derivedValue))
                {
                    dataGridCalculatedValues.Rows.Add(derivedValue.Name, derivedValue.CalcFunction.Description, derivedValue.Value, derivedValue.Sigma);
                }
            }
            Program.IsFromUser = true;
        }

        private bool UpdateDerivedValueInfo(DerivedValueInfo derivedValue)
        {
            //Calculate the value
            if (derivedValue.CalcFunction != null)
            {
                double[] pv = new double[derivedValue.CalcFunction.Parameters.Length];
                double[] sigmas = new double[derivedValue.CalcFunction.Parameters.Length];
                //Set values for the derived value function from the model best fit parameters
                for (int i = 0; i < derivedValue.CalcFunction.Parameters.Length; i++)
                {
                    //Find the matching parameter in the model function
                    bool found = false;
                    for (int j = 0; j < modelFitInfo.ModelFunction.Parameters.Length; j++)
                    {
                        if (derivedValue.CalcFunction.Parameters[i] == modelFitInfo.ModelFunction.Parameters[j])
                        {
                            found = true;
                            pv[i] = modelFitInfo.LastFitParameters[j];
                            sigmas[i] = modelFitInfo.SigmaParameters[j];
                            break;
                        }
                    }

                    if (!found)
                    {
                        new InfoBox().Show("parameter '" + derivedValue.CalcFunction.Parameters[i] + "' not found", "Error", this);
                        return false;
                    }
                }

                derivedValue.UpdateValue(pv, sigmas);

                return true;
            }

            return false;
        }


        private void dataGridCalculatedValues_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!Program.IsFromUser) return;
            if (e.RowIndex == -1 ||
                dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ||
                string.IsNullOrEmpty(dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()))
            {
                new InfoBox().Show("Value cannot be empty", "Error", this);
                dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                dataGridCalculatedValues.BeginEdit(true);
                return;
            }
            switch (e.ColumnIndex)
            {
                case 0:
                    derivedValuesInfos.ElementAt(e.RowIndex).Name = dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    break;
                case 1:
                    string errors = "";
                    if (!derivedValuesInfos.ElementAt(e.RowIndex).SetCalcFunction(dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out errors))
                    {
                        new InfoBox().Show("Invalid math expression", "Error", this);
                        dataGridCalculatedValues.Rows[e.RowIndex].Cells[0].Selected = true;
                    }
                    else
                    {
                        DerivedValueInfo derivedValueInfo = derivedValuesInfos.ElementAt(e.RowIndex);
                        if (UpdateDerivedValueInfo(derivedValueInfo))
                        {
                            dataGridCalculatedValues.Rows[e.RowIndex].Cells[2].Value = derivedValueInfo.Value;
                            dataGridCalculatedValues.Rows[e.RowIndex].Cells[3].Value = derivedValueInfo.Sigma;
                        }
                        else
                        {
                            derivedValuesInfos.ElementAt(e.RowIndex).CalcFunction = null;
                            dataGridCalculatedValues.Rows[e.RowIndex].Cells[0].Selected = true;
                        }
                    }
                    break;
            }
        }

        private void dataGridCalculatedValues_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (!Program.IsFromUser) return;
            derivedValuesInfos.Add(new DerivedValueInfo());
        }

        private void dataGridCalculatedValues_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (!Program.IsFromUser) return;
            derivedValuesInfos.RemoveAt(e.RowIndex);
        }

        private void DerviedValuesDialog_Load(object sender, EventArgs e)
        {
            this.Left = this.LeftTopPos.X;
            this.Top = this.LeftTopPos.Y;
            this.errorProvider.SetError(dataGridCalculatedValues, "");
        }

        private void DerviedValuesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(derivedValuesInfos.Where(dv => dv.CalcFunction == null || string.IsNullOrEmpty(dv.Name)).Any())
            {
                new InfoBox().Show("Empty name for derived value!", "Error", this);
                e.Cancel = true;
            }
        }

        private void DerviedValuesDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }

            if (e.KeyCode == Keys.F1)
            {
                Help.ShowHelp(this, "LybraFitHelp.chm");
            }
        }

        private void dataGridCalculatedValues_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!Program.IsFromUser) return;

            if (e.RowIndex == dataGridCalculatedValues.NewRowIndex) return;

            if (e.RowIndex == -1 || string.IsNullOrEmpty(e.FormattedValue.ToString()))
            {
                new InfoBox().Show("Value cannot be empty", "Error", this);
                dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                dataGridCalculatedValues.BeginEdit(true);
                e.Cancel = true;
                return;
            }

            //Verify if it is correct name 
            if (e.ColumnIndex == 0)
            {
                Regex reg = new Regex("^[a-zA-Z]+[a-zA-Z0-9]*$");
                if (!reg.IsMatch(e.FormattedValue.ToString()))
                {
                    new InfoBox().Show("Name must be alphanumeric and start with a letter", "Error", this);
                    dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    dataGridCalculatedValues.BeginEdit(true);
                    e.Cancel = true;
                    return;
                }
            }

            //Verify if it is the math expression for the derived value
            if (e.ColumnIndex == 1)
            {
                string errors = "";
                if (!derivedValuesInfos.ElementAt(e.RowIndex).SetCalcFunction(e.FormattedValue.ToString(), out errors))
                {
                    new InfoBox().Show("Invalid math expression", "Error", this);
                    dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    dataGridCalculatedValues.BeginEdit(true);
                    e.Cancel = true;
                    return;
                }

                DerivedValueInfo derivedValueInfo = derivedValuesInfos.ElementAt(e.RowIndex);
                if (UpdateDerivedValueInfo(derivedValueInfo))
                {
                    dataGridCalculatedValues.Rows[e.RowIndex].Cells[2].Value = derivedValueInfo.Value;
                    dataGridCalculatedValues.Rows[e.RowIndex].Cells[3].Value = derivedValueInfo.Sigma;
                }
                else
                {
                    dataGridCalculatedValues.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    dataGridCalculatedValues.BeginEdit(true);
                    e.Cancel = true;
                    return;
                }
            }
        }


        private void toolStripMenuDelete_Click(object sender, EventArgs e)
        {
            if (dataGridCalculatedValues.CurrentCell != null && dataGridCalculatedValues.CurrentRow.Index != dataGridCalculatedValues.NewRowIndex)
            {
                dataGridCalculatedValues.Rows.RemoveAt(dataGridCalculatedValues.CurrentCell.RowIndex);
            }
        }

        private void toolStripMenuCopy_Click(object sender, EventArgs e)
        {
            if (dataGridCalculatedValues.CurrentCell != null && dataGridCalculatedValues.CurrentRow.Index != dataGridCalculatedValues.NewRowIndex)
            {
                Clipboard.SetText(String.Format("{0}\t{1}\t{2}\t{3}",
                    dataGridCalculatedValues.CurrentRow.Cells[0].Value,
                    dataGridCalculatedValues.CurrentRow.Cells[1].Value,
                    dataGridCalculatedValues.CurrentRow.Cells[2].Value,
                    dataGridCalculatedValues.CurrentRow.Cells[3].Value));
            }

        }

        private void contextMenuDerivedValues_Opening(object sender, CancelEventArgs e)
        {
            bool canEnable = dataGridCalculatedValues.CurrentCell != null && dataGridCalculatedValues.CurrentRow.Index != dataGridCalculatedValues.NewRowIndex;
            toolStripMenuCopy.Enabled = canEnable;
            toolStripMenuDelete.Enabled = canEnable && !dataGridCalculatedValues.ReadOnly;
        }
    }
}
