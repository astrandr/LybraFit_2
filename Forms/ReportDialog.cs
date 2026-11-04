//Designed and Implemented by Andrey Batchvarov 2018
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

namespace DataFit
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
            Clipboard.SetText(txtReport.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = File.CreateText(saveFileDialog.FileName);
                writer.Write(txtReport.Text);
                writer.Close();
            }
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
