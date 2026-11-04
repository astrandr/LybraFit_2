using DataFit.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFit.Forms
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

        public new DialogResult ShowDialog(string modelName, Document doc, Control parent = null)
        {
            this.doc = doc;
            this.modelName = modelName;
            this.modelFitInfo = doc.Models[modelName];
            this.lblModelName.Text = modelFitInfo.ModelFunction.Description;

            PopulateDerivedValues();
            if(!modelFitInfo.CanModifyDerivedValues())
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
               if( UpdateDerivedValueInfo(derivedValue))
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
                float[] pv = new float[derivedValue.CalcFunction.Parameters.Length];
                float[] sigmas = new float[derivedValue.CalcFunction.Parameters.Length];
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
                        if(UpdateDerivedValueInfo(derivedValueInfo))
                        {
                            dataGridCalculatedValues.Rows[e.RowIndex].Cells[2].Value = derivedValueInfo.Value;
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
            if(derivedValuesInfos.Any(dv => dv.CalcFunction == null || string.IsNullOrEmpty(dv.Name)))
            {
                new InfoBox().Show("There are empty entries for names or calculation functions", this);
                this.errorProvider.SetError(dataGridCalculatedValues, "There are empty entries for names or calculation functions");
                dataGridCalculatedValues.Focus();
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
    }
}
