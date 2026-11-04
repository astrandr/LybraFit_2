//Designed and Implemented by Andrey Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.Windows.Forms;
using FuncLib;

namespace LybraFit.Forms
{
    public partial class TransformDialog : Form
    {
        private UserFunction transformFunction = null;

        public Point LeftTopPos;

        public int ColumnIndex
        {
            get { return this.cboColumns.SelectedIndex; }
        }
        public UserFunction TransformFunction
        {
            get { return this.transformFunction; }
        }

        public TransformDialog(string[] columnNames)
        {
            InitializeComponent();

            for (int i = 0; i < columnNames.Length; i++)
            {
                cboColumns.Items.Add(columnNames[i]);
            }

            cboColumns.SelectedIndex = 0;
        }

        public DialogResult ShowDialog(string[] columnNames)
        {
            cboColumns.Items.Clear();

            for (int i = 0; i < columnNames.Length; i++)
            {
                cboColumns.Items.Add(columnNames[i]);
            }

            cboColumns.SelectedIndex = 0;
            errorProvider.SetError(txtTransformExpression, "");
            return base.ShowDialog();
        }

        private void TransformDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
        }

        private void TransformDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void TransformDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    string compileErrors;
                    transformFunction = UserFunction.Create("Transform", txtTransformExpression.Text, out compileErrors);
                    if (transformFunction == null || !string.IsNullOrEmpty(compileErrors))
                    {
                        errorProvider.SetError(txtTransformExpression, "Invalid math expression for the transform.");
                        txtTransformExpression.Select();
                        e.Cancel = true;
                        return;
                    }

                    if (transformFunction.Parameters.Length > 0)
                    {
                        errorProvider.SetError((Control)txtTransformExpression, "No parameters are accepted in transform function.");
                        txtTransformExpression.Select();
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
