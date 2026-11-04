//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class LogNameDialog : Form
    {
        public string Identifier { get { return this.txtIdentifier.Text; } }
        public LogNameDialog()
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

        public DialogResult Show(string identifier, Control parent)
        {
            this.LeftTopPos = CenterToParent(parent);
            this.txtIdentifier.Text = identifier;
            return this.ShowDialog();
        }

        private void LogNameDialog_Load(object sender, EventArgs e)
        {
            this.Top = this.LeftTopPos.Y;
            this.Left = this.LeftTopPos.X;
        }

        private void LogNameDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void LogNameDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(txtIdentifier.Text))
                    {
                        errorProvider.SetError(txtIdentifier, "Enter identifier");
                        txtIdentifier.Focus();
                        e.Cancel = true;
                    }
                }
            }
        }

    }
}
