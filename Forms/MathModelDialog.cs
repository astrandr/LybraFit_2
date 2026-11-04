using FuncLib;
using MathLib;
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
    public partial class MathModelDialog : Form
    {

        public string MathExpression { get { return this.txtExpression.Text; } }
        public string ModelName { get { return this.txtName.Text; } }

        private UserFunction userFunction = null;

        public Point LeftTopPos;

        public UserFunction UserFunction
        {
            get { return userFunction; }
        }

        public MathModelDialog()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(UserFunction userFunction)
        {
            this.txtName.Text = userFunction != null ? userFunction.Name : null;
            this.txtExpression.Text = userFunction != null ? userFunction.Description : null;
            this.userFunction = userFunction;

            txtName.Enabled = userFunction == null; //The dialog is open for entering new function
            return this.ShowDialog();
        }

        private void pictFunctionPreview_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            new MathControl(pictFunctionPreview.Width, pictFunctionPreview.Height).DrawExpression(g, txtExpression.Text, pictFunctionPreview.ClientRectangle, pictFunctionPreview.BackColor);
        }

        private void txtExpression_TextChanged(object sender, EventArgs e)
        {
            //pictFunctionPreview.Refresh();
            Graphics g = pictFunctionPreview.CreateGraphics();

            new MathControl(pictFunctionPreview.Width, pictFunctionPreview.Height).DrawExpression(g, txtExpression.Text, pictFunctionPreview.ClientRectangle, pictFunctionPreview.BackColor);

        }

        private void MathModelDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    InfoBox mBox = new InfoBox();
                    string compilerErrors;

                    errorProvider.SetError((Control)txtName, "");
                    errorProvider.SetError((Control)txtExpression, "");

                    if (String.IsNullOrEmpty(txtName.Text))
                    {
                        mBox.Show("Provide name for the model.", this);
                        errorProvider.SetError((Control)txtName, "Provide name for the model.");
                        txtName.Focus();
                        e.Cancel = true;
                        return;
                    }

                    if (String.IsNullOrEmpty(txtExpression.Text))
                    {
                        mBox.Show("Provide linear form of the math expression for the model.", this);
                        errorProvider.SetError((Control)txtExpression, "Provide linear form of the math expression for the model.");
                        txtExpression.Focus();
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        userFunction = UserFunction.Create(txtName.Text, txtExpression.Text, out compilerErrors);
                        if (userFunction == null)
                        {
                            mBox.Show("Invalid math expression!", this);
                            errorProvider.SetError((Control)txtExpression, "Invalid math expression!");
                            txtExpression.Focus();
                            e.Cancel = true;
                            return;
                        }

                    }
                    catch
                    {
                        mBox.Show("Internal error has occured while tryping to create the custom model!", this);
                    }
                }
            }
        }

        private void MathModelDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                e.Handled = true;
                this.Close();
            }

            if (e.KeyCode == Keys.F1)
            {
                e.Handled = true;
                Help.ShowHelp(this, "LybraFitHelp.chm");
            }
        }

        private void MathModelDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            if(this.userFunction == null)
            {
                txtName.Select();
            }
            else
            {
                txtExpression.Select();
            }
        }

        private void txtExpression_KeyDown(object sender, KeyEventArgs e)
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
