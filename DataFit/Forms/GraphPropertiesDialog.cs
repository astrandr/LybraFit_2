//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Common;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphControlLib;

namespace LybraFit.Forms
{
    public partial class GraphPropertiesDialog : Form
    {
        private string ERROR_INVALID_NUMBER = "Invalid number";
        private string ERROR_MAX_VALUE = "Max must be greater than Min value";
        private string ERROR_STEP_VALUE = "Step must be positive value";

        public GraphSettings Settings;       

        public Point LeftTopPos;

        public GraphPropertiesDialog()
        {
            InitializeComponent();
        }

        private void InitializeFromSettings()
        {
            this.chkXAxisAuto.Checked = Settings.XLimitAuto;
            this.txtXMax.Text = Settings.XMax.ToString();
            this.txtXMin.Text = Settings.XMin.ToString();
            this.txtXStep.Text = Settings.XStep.ToString();
            this.txtXPow.Text = Settings.XPow.ToString();

            this.chkYAxisAuto_CheckedChanged(null, null);

            this.chkYAxisAuto.Checked = Settings.YLimitAuto;
            this.txtYMax.Text = Settings.YMax.ToString();
            this.txtYMin.Text = Settings.YMin.ToString();
            this.txtYStep.Text = Settings.YStep.ToString();
            this.txtYPow.Text = Settings.YPow.ToString();

            this.chkXAxisAuto_CheckedChanged(null, null);

            this.pnlAxisColor.BackColor = Settings.AxisColor;
            this.pnlBackColor.BackColor = Settings.BackgroundColor;
            this.pnlGraphColor.BackColor = Settings.PlotAreaColor;

            this.lblTickLabelsExample.ForeColor = Settings.TickLabelsColor;
            this.lblTickLabelsExample.Font = Settings.TickLabelsFont;

            this.lblAxisLabelsExample.ForeColor = Settings.AxisLabelsColor;
            this.lblAxisLabelsExample.Font = Settings.AxisLabelsFont;

            lblAxisLabelsExample.BackColor = Settings.BackgroundColor;
            lblTickLabelsExample.BackColor = Settings.BackgroundColor;

            this.nmrAxisThickness.Value = Settings.AxisThickness;
            this.chkGridLines.Checked = Settings.ShowGridLines;
            this.nmrGridThickness.Value = Settings.GridThickness;
            this.nmrGridThickness.Enabled = chkGridLines.Checked;
            this.cboGridStyle.Enabled = chkGridLines.Checked;

            this.cboGridStyle.Text = Settings.GridStyle.ToString();
            this.pnlGridColor.BackColor = Settings.GridColor;
            btnGridColor.Enabled = chkGridLines.Checked;

            this.pnlMarkerColor.BackColor = Settings.DataColor;

            btnRestoreDefaults.Enabled = Program.HasDefaultGraphSettings;

            this.chkConnectDataPoints.Checked = Settings.ConnectDataPoints;
            this.cboDataLineStyle.Enabled = Settings.ConnectDataPoints;
            this.nmrDataLineWidth.Enabled = Settings.ConnectDataPoints;
            this.cboDataLineStyle.Text = Settings.DataLineStyle.ToString();
            this.nmrDataLineWidth.Value = Settings.DataLineWidth;
            switch(Settings.DataPointType)
            {
                case PlotDataPointType.Circle: this.rdoCircle.Checked = true; break;
                case PlotDataPointType.Point: this.rdoPoint.Checked = true; break;
                case PlotDataPointType.Diamond: this.rdoSquare.Checked = true; break;
                case PlotDataPointType.FilledDiamond: this.rdoFilledSquare.Checked = true; break;
            }
            this.nmrDataPointSize.Value = Settings.DataPointSize;

            this.pnlFitColor.BackColor = Settings.FitLineColor;
            this.cboFitLineStyle.Text = Settings.FitLineStyle.ToString();
            this.nmrFitLineWidth.Value = Settings.FitLineWidth;

            this.chkTitle.Checked = Settings.ShowTitle;
            this.txtTitle.Text =  Settings.Title;
            this.txtTitle.Font = Settings.TitleFont;
            this.chkShowModel.Checked = Settings.ShowModel;
            this.chkResults.Checked = Settings.ShowRegressionResult;

            this.chkYAxisVerticalTitle.Checked = Settings.VerticalTextYAxis;
        }

        private bool ConvertToSettings(ref GraphSettings settings)
        {
            settings.XLimitAuto = chkXAxisAuto.Checked;
            if (!chkXAxisAuto.Checked)
            {
                if (!VerifyParseNumber(txtXMax, ref settings.XMax) ||
                    !VerifyParseNumber(txtXMin, ref settings.XMin) ||
                    !VerifyParseNumber(txtXStep, ref settings.XStep) ||
                    !VerifyParseInteger(txtXPow, ref settings.XPow))
                {
                    return false;
                }

                if (settings.XMax <= settings.XMin)
                {
                    errorProvider.SetError((Control)txtXMax, ERROR_MAX_VALUE);
                    txtXMax.Focus();
                    return false;
                }

                if (settings.XStep <= 0)
                {
                    errorProvider.SetError((Control)txtXStep, ERROR_STEP_VALUE);
                    txtXStep.Focus();
                    return false;
                }

            }


            settings.YLimitAuto = chkYAxisAuto.Checked;
            if (!chkYAxisAuto.Checked)
            {
                if (!VerifyParseNumber(txtYMax, ref settings.YMax) ||
                    !VerifyParseNumber(txtYMin, ref settings.YMin) ||
                    !VerifyParseNumber(txtYStep, ref settings.YStep) ||
                    !VerifyParseInteger(txtYPow, ref settings.YPow))
                {
                    return false;
                }

                if (settings.YMax <= settings.YMin)
                {
                    errorProvider.SetError((Control)txtYMax, ERROR_MAX_VALUE);
                    txtYMax.Focus();
                    return false;
                }

                if (settings.YStep <= 0)
                {
                    errorProvider.SetError((Control)txtYStep, ERROR_STEP_VALUE);
                    txtYStep.Focus();
                    return false;
                }
            }

            settings.AxisColor = this.pnlAxisColor.BackColor;
            settings.BackgroundColor = this.pnlBackColor.BackColor;
            settings.PlotAreaColor = this.pnlGraphColor.BackColor;

            settings.TickLabelsColor = this.lblTickLabelsExample.ForeColor;
            settings.TickLabelsFont = this.lblTickLabelsExample.Font;

            settings.AxisLabelsColor = lblAxisLabelsExample.ForeColor;
            settings.AxisLabelsFont = lblAxisLabelsExample.Font;

            settings.AxisThickness = (uint)this.nmrAxisThickness.Value;
            settings.ShowGridLines = this.chkGridLines.Checked;
            settings.GridThickness = (uint)this.nmrGridThickness.Value;

            settings.GridStyle = (DashStyle)DashStyle.Parse(typeof(DashStyle), cboGridStyle.Text);
            settings.GridColor = pnlGridColor.BackColor;

            settings.ConnectDataPoints = this.chkConnectDataPoints.Checked;
            settings.DataLineStyle = (DashStyle)DashStyle.Parse(typeof(DashStyle), this.cboDataLineStyle.Text);
            settings.DataLineWidth = (uint)nmrDataLineWidth.Value;
            settings.DataColor = this.pnlMarkerColor.BackColor;
            settings.DataPointSize = (uint)this.nmrDataPointSize.Value;

            if (rdoSquare.Checked) settings.DataPointType = PlotDataPointType.Diamond;
            else if (rdoFilledSquare.Checked) settings.DataPointType = PlotDataPointType.FilledDiamond;
            else if (rdoPoint.Checked) settings.DataPointType = PlotDataPointType.Point;
            else if (rdoCircle.Checked) settings.DataPointType = PlotDataPointType.Circle;

            settings.FitLineWidth = (uint)this.nmrFitLineWidth.Value;
            settings.FitLineStyle = (DashStyle)DashStyle.Parse(typeof(DashStyle), this.cboFitLineStyle.Text);
            settings.FitLineColor = this.pnlFitColor.BackColor;

            settings.ShowTitle = this.chkTitle.Checked;
            settings.Title = this.txtTitle.Text;
            settings.TitleFont = this.txtTitle.Font;
            settings.ShowRegressionResult = this.chkResults.Checked;
            settings.ShowModel = this.chkShowModel.Checked;

            settings.VerticalTextYAxis = chkYAxisVerticalTitle.Checked;

            return true;
        }

        private void GraphProperties_Load(object sender, EventArgs e)
        {
            InitializeFromSettings();
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            tabControl.Focus();
        }

        private void chkXAxisAuto_CheckedChanged(object sender, EventArgs e)
        {
            this.txtXMax.Enabled = !chkXAxisAuto.Checked;
            this.txtXMin.Enabled = !chkXAxisAuto.Checked;
            this.txtXStep.Enabled = !chkXAxisAuto.Checked;
            this.txtXPow.Enabled = !chkXAxisAuto.Checked;
        }

        private void chkYAxisAuto_CheckedChanged(object sender, EventArgs e)
        {
            this.txtYMax.Enabled = !chkYAxisAuto.Checked;
            this.txtYMin.Enabled = !chkYAxisAuto.Checked;
            this.txtYStep.Enabled = !chkYAxisAuto.Checked;
            this.txtYPow.Enabled = !chkYAxisAuto.Checked;
        }


        //TODO: Those methods may have to be in a utility or something common for different forms can use them
        private bool VerifyParseNumber(TextBox textBox, ref float v)
        {
            errorProvider.SetError((Control)textBox, "");
            if (!float.TryParse(textBox.Text, out v))
            {
                errorProvider.SetError((Control)textBox, ERROR_INVALID_NUMBER);
                textBox.Focus();
                return false;
            }
            return true;
        }

        //TODO: Those methods may have to be in a utility or something common for different forms can use them
        private bool VerifyParseInteger(TextBox textBox, ref int v)
        {
            errorProvider.SetError((Control)textBox, "");
            if (!int.TryParse(textBox.Text, out v))
            {
                errorProvider.SetError((Control)textBox, ERROR_INVALID_NUMBER);
                return false;
            }
            return true;
        }

        private void GraphPropertiesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if(!ConvertToSettings(ref Settings))
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlBackColor.BackColor;
            if(colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlBackColor.BackColor = colorDialog.Color;
                lblAxisLabelsExample.BackColor = colorDialog.Color;
                lblTickLabelsExample.BackColor = colorDialog.Color;
            }
        }

        private void btnGraphColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlGraphColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlGraphColor.BackColor = colorDialog.Color;
            }
        }

        private void btnAxisColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlAxisColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlAxisColor.BackColor = colorDialog.Color;
            }
        }



        private void btnGridColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlGridColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlGridColor.BackColor = colorDialog.Color;
            }
        }


        private void chkGridLines_CheckedChanged(object sender, EventArgs e)
        {
            nmrGridThickness.Enabled = chkGridLines.Checked;
            cboGridStyle.Enabled = chkGridLines.Checked;
            btnGridColor.Enabled = chkGridLines.Checked;
        }


        private void btnTickLabelsColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblTickLabelsExample.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblTickLabelsExample.ForeColor = colorDialog.Color;
            }
        }

        private void btnAxisLabelsColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = lblAxisLabelsExample.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                lblAxisLabelsExample.ForeColor = colorDialog.Color;
            }
        }

        private void btnTickLabelsFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = lblTickLabelsExample.Font;
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.lblTickLabelsExample.Font = fontDialog.Font;
            }
        }

        private void btnAxisLabelsFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = lblAxisLabelsExample.Font;
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.lblAxisLabelsExample.Font = fontDialog.Font;
            }
        }

        private void btnSetDefaults_Click(object sender, EventArgs e)
        {
            ConvertToSettings(ref Program.DefaultGraphSettings);
            Program.SaveDefaultGraphSettings();
            btnRestoreDefaults.Enabled = true;
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            this.Settings = Program.DefaultGraphSettings;
            InitializeFromSettings();
        }

        private void btnMarkerColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlMarkerColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlMarkerColor.BackColor = colorDialog.Color;
            }
        }

        private void chkConnectDataPoints_CheckedChanged(object sender, EventArgs e)
        {
            cboDataLineStyle.Enabled = chkConnectDataPoints.Checked;
            nmrDataLineWidth.Enabled = chkConnectDataPoints.Checked;
        }

        private void pnlPoint_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            int top = 1;
            int size = pnlPoint.Width - 2;
            e.Graphics.FillEllipse(brush, top, top, size, size);
        }

        private void pnlCircle_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            int top = 2;
            int size = pnlCircle.Width - 4;
            e.Graphics.DrawEllipse(pen, top, top, size, size);
        }

        private void pnlFilledSquare_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            int top = 1;
            int size = pnlPoint.Width - 2;
            e.Graphics.FillPolygon(brush, new Point[] { new Point(top, top + size / 2), new Point(top + size / 2, top), new Point(top + size, top + size / 2), new Point(top + size / 2, top + size) });
        }

        private void pnlSquare_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2);
            int top = 1;
            int size = pnlCircle.Width - 4;
            e.Graphics.DrawPolygon(pen, new Point[] { new Point(top, top + size / 2), new Point(top + size / 2, top), new Point(top + size, top + size / 2), new Point(top + size / 2, top + size) });
        }

        private void btnFitLineColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = pnlFitColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                pnlFitColor.BackColor = colorDialog.Color;
            }
        }

        private void chkTitle_CheckedChanged(object sender, EventArgs e)
        {
            txtTitle.Enabled = chkTitle.Checked;
            btnTitleFont.Enabled = chkTitle.Checked;
            chkShowModel.Enabled = chkTitle.Checked;
        }

        private void btnTitleFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = txtTitle.Font;
            if(fontDialog.ShowDialog() == DialogResult.OK)
            {
                txtTitle.Font = fontDialog.Font;
            }
        }

        private void chkShowModel_CheckedChanged(object sender, EventArgs e)
        {
            chkResults.Enabled = chkShowModel.Checked;
        }

        private void GraphPropertiesDialog_KeyDown(object sender, KeyEventArgs e)
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
                Help.ShowHelp(this, "LybraFitHelp.chm", HelpNavigator.Topic, "Entering data");
            }
        }
    }
}
