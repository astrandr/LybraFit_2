//Designed and Implemented by Andrey Batchvarov 2018
using LybraFit.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LybraFit
{
    public partial class ImportDialog : Form
    {
        private bool hasErrColumn = false;
        private int linesCount = 0;
        private int columnCount = 0;

        public Point LeftTopPos;

        public ImportDialog()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string text, string title)
        {
            errorProvider.SetError((Control)num1stColumnIndex, "");
            errorProvider.SetError((Control)num2ndColumnIndex, "");
            errorProvider.SetError((Control)num3rdColumnIndex, "");
            errorProvider.SetError((Control)txtTitle, "");

            this.txtOriginal.Text = text;
            txtTitle.Text = title;

            this.txt1stColumnName.Text = Configuration.ImportSettings.Column1Name;
            this.txt2ndColumnName.Text = Configuration.ImportSettings.Column2Name;
            this.txt3rdColumnName.Text = Configuration.ImportSettings.Column3Name;

            this.num1stColumnIndex.Value = Configuration.ImportSettings.Column1Index + 1;
            this.num2ndColumnIndex.Value = Configuration.ImportSettings.Column2Index + 1;
            this.num3rdColumnIndex.Value = Configuration.ImportSettings.Column3Index + 1;
            this.chkHasErrColumn.Checked = Configuration.ImportSettings.HasErrColumn;
            FindHeaderLines();
            if (Configuration.ImportSettings.UseStandardDelimiters)
            {
                rdoStandardDelimiters.Checked = true;
            }
            else
            {
                rdoAnyNonNumericDelimiter.Checked = true;
            }
            UpdatePreview();
            return base.ShowDialog();
        }

        private void chkHasErrColumn_CheckedChanged(object sender, EventArgs e)
        {
            hasErrColumn = chkHasErrColumn.Checked;
            txt3rdColumnName.Enabled = chkHasErrColumn.Checked;
            num3rdColumnIndex.Enabled = chkHasErrColumn.Checked;
            UpdatePreview();
        }

        private bool CheckForErrors()
        {
            errorProvider.SetError((Control)num1stColumnIndex, "");
            errorProvider.SetError((Control)num2ndColumnIndex, "");
            errorProvider.SetError((Control)num3rdColumnIndex, "");
            errorProvider.SetError((Control)txt1stColumnName, "");
            errorProvider.SetError((Control)txt2ndColumnName, "");
            errorProvider.SetError((Control)txt3rdColumnName, "");
            errorProvider.SetError((Control)txtTitle, "");

            if (numHeaderLinesCount.Value > linesCount)
            {
                errorProvider.SetError((Control)numHeaderLinesCount, "Invalid number of header lines!");
                numHeaderLinesCount.Focus();
                return false;
            }

            if (linesCount > numHeaderLinesCount.Value && columnCount <= (int)num1stColumnIndex.Value - 1)
            {
                errorProvider.SetError((Control)num1stColumnIndex, "Invalid number of 1st column index lines!");
                num1stColumnIndex.Focus();
                return false;
            }

            if (linesCount > numHeaderLinesCount.Value && columnCount <= (int)num2ndColumnIndex.Value - 1)
            {
                errorProvider.SetError((Control)num2ndColumnIndex, "Invalid number of 2nd column index lines!");
                num2ndColumnIndex.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txt1stColumnName.Text))
            {
                errorProvider.SetError((Control)txt1stColumnName, "Missing name for 1st column");
                txt1stColumnName.Focus();
                return false;
            }

            if (txt1stColumnName.Text.Trim() == "Model" || txt1stColumnName.Text.Trim() == "Difference")
            {
                errorProvider.SetError((Control)txt1stColumnName, "Column name cannot be 'Model' or 'Difference'");
                txt1stColumnName.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txt2ndColumnName.Text))
            {
                errorProvider.SetError((Control)txt2ndColumnName, "Missing name for 2nd column");
                txt2ndColumnName.Focus();
                return false;
            }

            if (txt2ndColumnName.Text.Trim() == "Model" || txt2ndColumnName.Text.Trim() == "Difference")
            {
                errorProvider.SetError((Control)txt2ndColumnName, "Column name cannot be 'Model' or 'Difference'");
                txt1stColumnName.Focus();
                return false;
            }

            if (chkHasErrColumn.Checked && String.IsNullOrEmpty(txt3rdColumnName.Text))
            {
                errorProvider.SetError((Control)txt3rdColumnName, "Missing name for 3rd column");
                txt3rdColumnName.Focus();
                return false;
            }

            if (chkHasErrColumn.Checked && linesCount > numHeaderLinesCount.Value && columnCount <= (int)num3rdColumnIndex.Value - 1)
            {
                errorProvider.SetError((Control)num3rdColumnIndex, "Invalid number of 3rd column index lines!");
                num3rdColumnIndex.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtTitle.Text))
            {
                errorProvider.SetError((Control)txtTitle, "Missing title");
                txtTitle.Focus();
                return false;
            }

            return true;

        }

        private void FindHeaderLines()
        {
            string[] lines = txtOriginal.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            numHeaderLinesCount.Maximum = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(Document.StandardDelimiters, StringSplitOptions.RemoveEmptyEntries);
                double r;
                if (tokens.All(s => double.TryParse(s, out r)))
                {
                    numHeaderLinesCount.Value = i;
                    return;
                }
            }
            numHeaderLinesCount.Value = lines.Length;
        }

        /// <summary>
        /// UpdatePreview - parser the original text based on the settings
        /// </summary>
        private void UpdatePreview()
        {
            Program.IsFromUser = false;
            string[] lines = txtOriginal.Text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            numHeaderLinesCount.Maximum = lines.Length;
            linesCount = lines.Length;
            columnCount = 0;
            listParsedData.Items.Clear();
            txt1stColumnName.Text = ""; txt2ndColumnName.Text = ""; txt3rdColumnName.Text = "";
            chkHasErrColumn.Enabled = false;
            num3rdColumnIndex.Enabled = false;

            if (numHeaderLinesCount.Value > 0 && lines.Length > (int)numHeaderLinesCount.Value - 1 && (int)num1stColumnIndex.Value > 0)
            {
                string[] tokens = lines[(int)numHeaderLinesCount.Value - 1].Split(Document.StandardDelimiters, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length > 0 && tokens.Length >= (int)num1stColumnIndex.Value) txt1stColumnName.Text = tokens[(int)num1stColumnIndex.Value - 1];
                if (tokens.Length > 1 && tokens.Length >= (int)num2ndColumnIndex.Value) txt2ndColumnName.Text = tokens[(int)num2ndColumnIndex.Value - 1];
                if (tokens.Length > 2 && tokens.Length >= (int)num3rdColumnIndex.Value) 
                { 
                    txt3rdColumnName.Text = tokens[(int)num3rdColumnIndex.Value - 1]; 
                    chkHasErrColumn.Enabled = true;
                    num3rdColumnIndex.Enabled = true;
                }

                columnCount = tokens.Length;
                num1stColumnIndex.Maximum = columnCount;
                num2ndColumnIndex.Maximum = columnCount;
                num3rdColumnIndex.Maximum = columnCount;
            }
            else
            {
                //Set default names
                txt1stColumnName.Text = "X";
                txt2ndColumnName.Text = "Y";
                txt3rdColumnName.Text = "Err";
            }


            for (int lineIndex = (int)numHeaderLinesCount.Value; lineIndex < (int)numHeaderLinesCount.Value + 20 && lineIndex < lines.Length; lineIndex++)
            {
                string[] tokens;
                if (rdoStandardDelimiters.Checked)
                {
                    tokens = lines[lineIndex].Split(Document.StandardDelimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (columnCount < tokens.Length)
                    {
                        columnCount = tokens.Length;
                    }
                }
                else
                {
                    Regex regex = new Regex(Constants.NUMBER_MATCH_EXPRESSION);
                    MatchCollection matches = regex.Matches(lines[lineIndex]);
                    tokens = new string[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        tokens[i] = matches[i].ToString();
                    }
                    if (columnCount < tokens.Length)
                    {
                        columnCount = tokens.Length;
                    }
                }


                if (tokens.Length > 0 && tokens.Length >= (int)num1stColumnIndex.Value)
                {
                    ListViewItem row = new ListViewItem(tokens[(int)num1stColumnIndex.Value - 1]);
                    if (tokens.Length >= (int)num2ndColumnIndex.Value) row.SubItems.Add(tokens[(int)num2ndColumnIndex.Value - 1]);
                    if (chkHasErrColumn.Checked && tokens.Length >= (int)num3rdColumnIndex.Value) row.SubItems.Add(tokens[(int)num3rdColumnIndex.Value - 1]);
                    listParsedData.Items.Add(row);
                }
            }
            Program.IsFromUser = true;
        }

        private void numHeaderLinesCount_ValueChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void txt1stColumnName_TextChanged(object sender, EventArgs e)
        {
            listParsedData.Columns[0].Text = txt1stColumnName.Text;
        }

        private void txt2ndColumnName_TextChanged(object sender, EventArgs e)
        {
            listParsedData.Columns[1].Text = txt2ndColumnName.Text;
        }

        private void txt3rdColumnName_TextChanged(object sender, EventArgs e)
        {
            listParsedData.Columns[2].Text = txt3rdColumnName.Text;
        }

        private void num1stColumnIndex_ValueChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            UpdatePreview();
        }

        private void rdoAnyNonNumericDelimiter_CheckedChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            UpdatePreview();
        }

        private void ImportDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (CheckForErrors())
                    {
                        Configuration.ImportSettings.HeaderLinesCount = (int)this.numHeaderLinesCount.Value;
                        Configuration.ImportSettings.Column1Name = this.txt1stColumnName.Text;
                        Configuration.ImportSettings.Column2Name = this.txt2ndColumnName.Text;
                        Configuration.ImportSettings.Column3Name = this.txt3rdColumnName.Text;
                        Configuration.ImportSettings.Column1Index = (int)this.num1stColumnIndex.Value - 1;
                        Configuration.ImportSettings.Column2Index = (int)this.num2ndColumnIndex.Value - 1;
                        Configuration.ImportSettings.Column3Index = (int)this.num3rdColumnIndex.Value - 1;
                        Configuration.ImportSettings.HasErrColumn = chkHasErrColumn.Checked;
                        Configuration.ImportSettings.SkipLinesWithError = chkSkipErrors.Checked;
                        Configuration.ImportSettings.UseStandardDelimiters = !rdoAnyNonNumericDelimiter.Checked;
                        Configuration.GraphSettings.Title = txtTitle.Text;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void ImportDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            chkSkipErrors.Checked = false;
        }

        private void ImportDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void btnOk_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
