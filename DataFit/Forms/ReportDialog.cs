using LybraFit.Forms;
//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace LybraFit
{
    public partial class ReportDialog : Form
    {
        public Point LeftTopPos;

        public ReportDialog()
        {
            InitializeComponent();
        }

        public void ShowReport(string text)
        {
            this.txtReport.Text = text;
            this.ShowDialog();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support copy of text report", "Warning", this);
#else
            Clipboard.SetText(txtReport.Text);
#endif
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
#if DEMO
            InfoBox.ShowMessage("Demo version does not support saving of text report", "Warning", this);
#else
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(saveFileDialog.FileName);
                writer.Write(txtReport.Text);
                writer.Close();
            }
#endif
        }

        private void ReportDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
        }

        private void ReportDialog_KeyDown(object sender, KeyEventArgs e)
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
