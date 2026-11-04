//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class ExportDialog : Form
    {
        private Point LeftTopPos;
        private Document doc;
        private string delimiter;
        private string[] delimiters = new string[] { "\t", " ", ";", ":", "," };

        public ExportDialog()
        {
            InitializeComponent();
            cboDelimiters.SelectedIndex = 0;
            delimiter = delimiters[cboDelimiters.SelectedIndex];
        }

        private void CenterToParent(Control dialog)
        {
            LeftTopPos.Y = (dialog.Bottom + dialog.Top) / 2 - (this.Height) / 2;
            LeftTopPos.X = (dialog.Right + dialog.Left) / 2 - (this.Width) / 2;
        }

        public DialogResult ShowDialog(Document doc, Control parent)
        {
            this.doc = doc;
            CenterToParent(parent);
            this.chkErrors.Enabled = doc.HasErrorY;
            this.chkModel.Enabled = doc.CurrentModel != null;
            this.chkDifference.Enabled = doc.CurrentModel != null;
            this.RefreshPreview();
            return this.ShowDialog();
        }

        private void ExportDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            chkHeader.Select();
        }

        private void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            txtHeader.Enabled = chkHeader.Checked;
            if (txtHeader.Enabled)
            {
                txtHeader.Select();
            }
        }

        private void RefreshPreview()
        {
            if (doc == null) return;
            int linesCount = doc.Data.Count > 5 ? 5 : doc.Data.Count;
            txtPreview.Text = GetDataText(linesCount);
        }

        private string GetDataText(int linesCount)
        {
            StringBuilder sb = new StringBuilder();
            if (chkColumns.Checked)
            {
                string line = doc.ColumnNames[0];
                line += delimiter;
                line += doc.ColumnNames[1];

                if (chkErrors.Checked)
                {
                    line += delimiter;
                    line += doc.ColumnNames[2];
                }

                if (chkModel.Checked)
                {
                    line += delimiter;
                    line += doc.ColumnNames[3];
                }

                if (chkDifference.Checked)
                {
                    line += delimiter;
                    line += doc.ColumnNames[4];
                }
                sb.AppendLine(line);
            }
#if DEMO
            linesCount = linesCount > 10 ? 10 : linesCount;
#endif

            for (int i = 0; i < linesCount; i++)
            {
                string line = GetStringForNumber(doc.Data[i][0], 0);
                line += delimiter;
                line += GetStringForNumber(doc.Data[i][1], 1);

                if (chkErrors.Checked)
                {
                    line += delimiter;
                    line += GetStringForNumber(doc.Data[i][2], 2);
                }

                if (chkModel.Checked)
                {
                    line += delimiter;
                    line += GetStringForNumber(doc.Data[i][3], 3);
                }

                if (chkDifference.Checked)
                {
                    line += delimiter;
                    line += GetStringForNumber(doc.Data[i][4], 4);
                }
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        private string GetStringForNumber(float number, int columnIndex)
        {
            if (chkFormat.Checked)
            {
                if (doc.GridColumnSettings[columnIndex].Format == "Numeric")
                {
                    return number.ToString("F" + doc.GridColumnSettings[columnIndex].Digits);
                }
                else
                    if (doc.GridColumnSettings[columnIndex].Format == "Scientific")
                    {
                        return number.ToString("E" + doc.GridColumnSettings[columnIndex].Digits);
                    }
            }
            return number.ToString();
        }

        private void chkFormat_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void cboDelimiters_SelectedIndexChanged(object sender, EventArgs e)
        {
            delimiter = delimiters[cboDelimiters.SelectedIndex];
            RefreshPreview();
        }

        private void ExportDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    StreamWriter file;
                    try
                    {
                        file = File.CreateText(txtOutputFile.Text);
                    }
                    catch (Exception ex)
                    {
                        //TODO Log exception if debug
                        InfoBox.ShowMessage("Cannot create file", "Failure", this);
                        txtOutputFile.Select();
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        if (chkHeader.Checked)
                        {
                            file.Write(txtHeader.Text); file.WriteLine();
                        }
#if DEMO
                        InfoBox.ShowMessage("Demo version will save up to first 10 lines.", "Warning", this);
#endif
                        file.Write(GetDataText(doc.Data.Count));
                        file.Flush();

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        file.Close();
                    }
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = this.txtOutputFile.Text;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtOutputFile.Text = saveFileDialog.FileName;
            }
        }

        private void ExportDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter && !txtHeader.Focused)
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
