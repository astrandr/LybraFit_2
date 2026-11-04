//Designed and Implemented by Andrey Batchvarov 2018
using DataFit.Common;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphControlLib;

namespace DataFit.Forms
{
    public partial class GraphPropertiesDialog : Form
    {
        private string ERROR_INVALID_NUMBER = "Invalid number"; //TODO: See if this has to be in common or in application settings

        public GraphSettings Settings;

        private static GraphSettings defaultSettings;
        private static bool hasDefaultSettings = false;

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

            btnRestoreDefaults.Enabled = hasDefaultSettings;

            this.chkConnectDataPoints.Checked = Settings.ConnectDataPoints;
            this.cboDataLineStyle.Enabled = Settings.ConnectDataPoints;
            this.nmrDataLineWidth.Enabled = Settings.ConnectDataPoints;
            this.cboDataLineStyle.Text = Settings.DataLineStyle.ToString();
            this.nmrDataLineWidth.Value = Settings.DataLineWidth;
            switch(Settings.DataPointType)
            {
                case PlotDataPointType.Circle : this.rdoCircle.Checked = true; break;
                case PlotDataPointType.Point: this.rdoPoint.Checked = true; break;
                case PlotDataPointType.Square: this.rdoSquare.Checked = true; break;
                case PlotDataPointType.FilledSquare: this.rdoFilledSquare.Checked = true; break;
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

            if (rdoSquare.Checked) settings.DataPointType = PlotDataPointType.Square;
            else if (rdoFilledSquare.Checked) settings.DataPointType = PlotDataPointType.FilledSquare;
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
            ConvertToSettings(ref defaultSettings);
            hasDefaultSettings = true;
            btnRestoreDefaults.Enabled = true;
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            this.Settings = defaultSettings;
            InitializeFromSettings();
        }

        private void pnlPoint_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            int top = pnlPoint.Width / 4;
            int size = pnlPoint.Width / 2;
            e.Graphics.FillEllipse(brush, top, top, top+size, top+size);
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

        private void pnlCircle_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            int top = pnlCircle.Width / 4;
            int size = pnlCircle.Width / 2;
            e.Graphics.DrawEllipse(pen, top, top, top + size, top + size);
        }

        private void pnlFilledSquare_Paint(object sender, PaintEventArgs e)
        {
            Brush brush = new SolidBrush(Color.Black);
            int top = pnlPoint.Width / 4;
            int size = pnlPoint.Width / 2;
            e.Graphics.FillRectangle(brush, top, top, top + size, top + size);
        }

        private void pnlSquare_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            int top = pnlCircle.Width / 4;
            int size = pnlCircle.Width / 2;
            e.Graphics.DrawRectangle(pen, top, top, top + size, top + size);
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
