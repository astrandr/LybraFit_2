//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Common;
using FuncLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class GenerateDataDialog : Form
    {
        public NewDataSettings Settings;

        public Point LeftTopPos;

        public GenerateDataDialog()
        {
            InitializeComponent();
        }

        private bool VerifyParseNumber(TextBox textBox, ref float v)
        {
            errorProvider.SetError((Control)textBox, "");
            if (!float.TryParse(textBox.Text, out v))
            {
                errorProvider.SetError((Control)textBox, Constants.ERROR_INVALID_NUMBER);
                textBox.Focus();
                return false;
            }
            return true;
        }

        private bool ConvertToSettings()
        {
            errorProvider.SetError((Control)txtTitle, "");
            errorProvider.SetError((Control)txtDataXMax, "");
            errorProvider.SetError((Control)txtDataXMin, "");
            errorProvider.SetError((Control)txtDataXIncrement, "");
            errorProvider.SetError((Control)txtErrDataName, "");
            errorProvider.SetError((Control)txtCol1Name, "");
            errorProvider.SetError((Control)txtCol2Name, "");
            errorProvider.SetError((Control)txtErrDataName, "");

            if (String.IsNullOrEmpty(txtTitle.Text))
            {
                errorProvider.SetError((Control)txtTitle, "No title is specified.");
                txtTitle.Focus();
                return false;
            }

            Settings.Title = txtTitle.Text;

            if (!VerifyParseNumber(txtDataXMin, ref Settings.DataXMin) ||
                !VerifyParseNumber(txtDataXMax, ref Settings.DataXMax) ||
                !VerifyParseNumber(txtDataXIncrement, ref Settings.DataXIncrement))
            {
                return false;
            };

            Settings.DataXAutoIncrement = chkMainDataXAuto.Checked;

            if (chkMainDataXAuto.Checked)
            {

                if (Settings.DataXMax < Settings.DataXMin)
                {
                    errorProvider.SetError((Control)txtDataXMax, "Max value must be greater than min value");
                    txtDataXMax.Focus();
                    return false;
                }

                if (Settings.DataXIncrement <= 0)
                {
                    errorProvider.SetError((Control)txtDataXIncrement, "Incorrect increment");
                    txtDataXIncrement.Focus();
                    return false;
                }

                if ((Settings.DataXMax - Settings.DataXMin) / Settings.DataXIncrement > 100000)
                {
                    errorProvider.SetError((Control)txtDataXIncrement, "Data count cannot exceed 100000");
                    txtDataXIncrement.Focus();
                    return false;
                }
            }

            if (String.IsNullOrEmpty(txtCol1Name.Text.Trim()))
            {
                errorProvider.SetError((Control)txtCol1Name, "Missing name for column 1.");
                txtCol1Name.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(txtCol2Name.Text.Trim()))
            {
                errorProvider.SetError((Control)txtCol2Name, "Missing name for column 2.");
                txtCol2Name.Focus();
                return false;
            }

            if (chkErrData.Checked && String.IsNullOrEmpty(txtErrDataName.Text.Trim()))
            {
                errorProvider.SetError((Control)txtErrDataName, "Missing name for data errors.");
                txtErrDataName.Focus();
                return false;
            }

            Settings.DataXName = txtCol1Name.Text.Trim();
            Settings.DataYName = txtCol2Name.Text.Trim();
            Settings.ErrDataName = txtErrDataName.Text.Trim();
            Settings.HasErrData = chkErrData.Checked;
            Settings.ErrDataCalculatedStd = chkErrDataCalculated.Checked;
            Settings.CalculateYData = chkMainDataYCalculated.Checked;
            Settings.YFunction = null;

            if (chkMainDataYCalculated.Checked)
            {
                string compileErrors = String.Empty;
                UserFunction userFunction = UserFunction.Create("Transform", txtExpression.Text.Trim(), out compileErrors);
                if(userFunction == null || !String.IsNullOrEmpty(compileErrors))
                {
                    errorProvider.SetError((Control)txtExpression, "Invalid math expression!");
                    txtExpression.Focus();
                    return false;
                }

                if (userFunction.Parameters.Length > 0)
                {
                    errorProvider.SetError((Control)txtExpression, "No parameters are accepted in calculated Y function.");
                    txtExpression.Focus();
                    return false;
                }
                Settings.YFunction = userFunction;
            }

            return true;
        }

        private void InitializeFromSettings()
        {
            this.txtTitle.Text = Settings.Title;
            this.txtCol1Name.Text = Settings.DataXName;
            this.txtCol2Name.Text = Settings.DataYName;
            this.chkErrData.Checked = Settings.HasErrData;
            this.txtErrDataName.Text = Settings.ErrDataName;
            this.chkMainDataXAuto.Checked = Settings.DataXAutoIncrement;
            this.txtDataXMin.Text = Settings.DataXMin.ToString();
            this.txtDataXMax.Text = Settings.DataXMax.ToString();
            this.txtDataXIncrement.Text = Settings.DataXIncrement.ToString();
            this.chkErrDataCalculated.Checked = Settings.ErrDataCalculatedStd;
            this.chkMainDataYCalculated.Checked = Settings.CalculateYData;
            this.txtExpression.Text = Settings.YFunction != null ? Settings.YFunction.Description : "";
        }

        private void NewDataDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (!ConvertToSettings())
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void NewDataDialog_Load(object sender, EventArgs e)
        {
            InitializeFromSettings();
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
        }

        private void chkMainDataXAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtDataXIncrement.Enabled = chkMainDataXAuto.Checked;
            txtDataXMin.Enabled = chkMainDataXAuto.Checked;
            txtDataXMax.Enabled = chkMainDataXAuto.Checked;
        }

        private void chkErrData_CheckedChanged(object sender, EventArgs e)
        {
            this.txtErrDataName.Enabled = chkErrData.Checked;
            this.chkErrDataCalculated.Enabled = chkErrData.Checked;
        }

        private void chkMainDataYCalculated_CheckedChanged(object sender, EventArgs e)
        {
            txtExpression.Enabled = chkMainDataYCalculated.Checked;
        }

        private void NewDataDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
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
                Help.ShowHelp(this, "LybraFitHelp.chm", HelpNavigator.Topic, "Entering data");
            }
        }

        private void txtExpression_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
