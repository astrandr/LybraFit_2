using DataFit.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataFit.Forms
{
    public partial class LogDialog : Form
    {

        private ModelFitInfo modelFitInfo;

        public LogDialog()
        {
            InitializeComponent();
        }

        private Point LeftTopPos;

        private Point CenterToParent(Control dialog)
        {
            Point p = Point.Empty;
            p.Y = (dialog.Bottom + dialog.Top) / 2 - (this.Height) / 2;
            p.X = (dialog.Right + dialog.Left) / 2 - (this.Width) / 2;
            return p;
        }

        public DialogResult Show(ModelFitInfo modelFitInfo, Control parent)
        {
            this.modelFitInfo = modelFitInfo;
            RefreshGrid();
            this.lblModelName.Text = modelFitInfo.ModelName +"  " + modelFitInfo.ModelFunction.Description;
            this.LeftTopPos = CenterToParent(parent);
            return this.ShowDialog();
        }

        private void RefreshGrid()
        {
            dataGridLogs.Columns.Clear();
            if (modelFitInfo.Log == null) return;
            dataGridLogs.Columns.Add("Key", "Key");
            dataGridLogs.Columns["Key"].Width = 150;
            int gridWidth = 0;
            int gridHeight = 2*dataGridLogs.ColumnHeadersHeight;
            foreach (var valueName in modelFitInfo.Log.ValueNames)
            {
                dataGridLogs.Columns.Add(valueName, valueName);
            }

            foreach (var record in modelFitInfo.Log.Records)
            {
                var rowIndex = dataGridLogs.Rows.Add();
                dataGridLogs.Rows[rowIndex].Cells[0].Value = record.Key;
                for (int i = 0; i < modelFitInfo.Log.ValueNames.Length; i++)
                {
                    dataGridLogs.Rows[rowIndex].Cells[modelFitInfo.Log.ValueNames[i]].Value = record.Values[i];                    
                }
                gridHeight += dataGridLogs.Rows[rowIndex].Height;
            }

            foreach(DataGridViewColumn column in dataGridLogs.Columns)
            {
                gridWidth += column.Width;
            }

            this.Width = gridWidth + 10;
            if (Width < this.MinimumSize.Width) Width = MinimumSize.Width;
            if (Width > this.MaximumSize.Width) Width = MaximumSize.Width;

            int deltaHeight = gridHeight - this.dataGridLogs.Height;
            this.Height += deltaHeight;
            if (this.Height < this.MinimumSize.Height) this.Height = this.MinimumSize.Height;
            if (this.Height > this.MaximumSize.Height) this.Height = this.MaximumSize.Height;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (new MessageDialog().Show("Warning",
                new string[] { "You are about to delete the regressions log", "Confirm delete?" }, this) == DialogResult.Yes)
            {
                this.modelFitInfo.Log = null;
                this.RefreshGrid();
            }
        }

        private void LogDialog_Load(object sender, EventArgs e)
        {
            this.Top = this.LeftTopPos.Y;
            this.Left = this.LeftTopPos.X;
        }

        private void LogDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(ConvertToString());
        }

        private string ConvertToString()
        {
            if (this.modelFitInfo == null || this.modelFitInfo.Log == null || this.modelFitInfo.Log.ValueNames == null) return "No log data";
            StringBuilder sb = new StringBuilder();
            string line = "Log record identifier";
            for (int i = 0; i < this.modelFitInfo.Log.ValueNames.Length; i++)
            {
                line += "\t" + this.modelFitInfo.Log.ValueNames[i];
            }
            sb.AppendLine(line);
            for (int i = 0; i < this.modelFitInfo.Log.Records.Count; i++)
            {
                line = this.modelFitInfo.Log.Records[i].Key;
                for (int j = 0; j < this.modelFitInfo.Log.Records[i].Values.Length; j++)
                {
                    line += "\t" + this.modelFitInfo.Log.Records[i].Values[j].ToString();
                }
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var file = File.CreateText(saveFileDialog.FileName);
                    file.Write(ConvertToString());
                    file.Close();
                }
                catch
                {
                    new InfoBox().Show("Error occured while trying to save to the file.", this);
                }
            }
        }
    }
}
