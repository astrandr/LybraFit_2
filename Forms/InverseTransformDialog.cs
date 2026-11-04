//Designed and Implemented by Andrey Batchvarov 2018
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
    public partial class InverseTransformDialog : Form
    {
        InverseTransformSettings settings;
        
        public Point LeftTopPos;

        public InverseTransformDialog()
        {
            InitializeComponent();
        }

        public DialogResult ShowDialog(string[] columnNames, ref InverseTransformSettings settings)
        {
            this.settings = settings;
            txtMaxY.Text = settings.MaxY.ToString(); lblMaxYV.Text = "<= " + settings.MaxY.ToString();
            txtMinY.Text = settings.MinY.ToString(); lblMinYV.Text = ">= " + settings.MinY.ToString();
            txtStepY.Text = "0";
            txtPrecision.Text = AutoFormatNumber.NumberFormatter.FloatToString((float)Math.Abs(settings.MaxY * 0.001f), 4, 3);

            lblMaxY.Text = "max " + columnNames[1];
            lblMinY.Text = "min " + columnNames[1];
            lblStepY.Text = "step " + columnNames[1];
            return base.ShowDialog();
        }

        private bool CheckForErrors()
        {
            errorProvider.SetError((Control)txtMaxY, "");
            errorProvider.SetError((Control)txtMinY, "");
            errorProvider.SetError((Control)txtStepY, "");
            errorProvider.SetError((Control)txtPrecision, "");

            if(!float.TryParse(txtMaxY.Text, out settings.MaxY))
            {
                errorProvider.SetError((Control)txtMaxY, "Invalid value");
                txtMaxY.Focus();
                return false;
            }

            if (!float.TryParse(txtMinY.Text, out settings.MinY))
            {
                errorProvider.SetError((Control)txtMinY, "Invalid value");
                txtMinY.Focus();
                return false;
            }

            if (!float.TryParse(txtStepY.Text, out settings.StepY))
            {
                errorProvider.SetError((Control)txtStepY, "Invalid value");
                txtStepY.Focus();
                return false;
            }

            if (!float.TryParse(txtPrecision.Text, out settings.Eps))
            {
                errorProvider.SetError((Control)txtPrecision, "Invalid value");
                txtPrecision.Focus();
                return false;
            }

            if(settings.Eps <= 0)
            {
                errorProvider.SetError((Control)txtPrecision, "Value must be greater than 0.");
                txtPrecision.Focus();
                return false;
            }

            if (settings.MaxY <= settings.MinY)
            {
                errorProvider.SetError((Control)txtMinY, "Minimum value must be less than maximum of the range");
                txtMinY.Focus();
                return false;
            }

            if (settings.MaxY > settings.MaxYV)
            {
                errorProvider.SetError((Control)txtMaxY, "Value must be withing the input range");
                txtMaxY.Focus();
                return false;
            }

            if (settings.MinY < settings.MinYV)
            {
                errorProvider.SetError((Control)txtMinY, "Value must be withing the input range");
                txtMinY.Focus();
                return false;
            }

            if (settings.StepY <= 0)
            {
                errorProvider.SetError((Control)txtStepY, "Value must be greater than 0");
                txtStepY.Focus();
                return false;
            }

            if (settings.StepY >= settings.MaxY - settings.MinY)
            {
                errorProvider.SetError((Control)txtStepY, "Value must be less than the range interval.");
                txtStepY.Focus();
                return false;
            }

            return true;

        }
        private void InverseTransformDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (!CheckForErrors())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void InverseTransformDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void InverseTransformDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            txtMinY.Focus();
        }
    }
}
