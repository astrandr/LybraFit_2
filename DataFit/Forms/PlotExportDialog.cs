//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using AutoFormatNumber;
using LybraFit.Common;
using FuncLib;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class PlotExportDialog : Form
    {
        ModelFitInfo fitInfo = null;

        public Point LeftTopPos;

        private int currentUnits;
        private int dpi;
        private float width, height, margins;
        private const float inchToCm = 2.54f;

        private float aspectRatio = 1;
        private Document doc;

        public PlotExportDialog()
        {
            InitializeComponent();
            Program.IsFromUser = false;
            cboUnits.SelectedIndex = 0;

            errorProvider.SetError((Control)cboDPI, "");
            errorProvider.SetError((Control)txtWidth, "");
            errorProvider.SetError((Control)txtHeight, "");
            errorProvider.SetError((Control)txtMargins, "");

            currentUnits = cboUnits.SelectedIndex;
            cboDPI.SelectedIndex = 0;
            chkAspect.Checked = true;
            Program.IsFromUser = true;
        }

        public Size ShowDialog(ref int currentUnits, ref SizeF size, ref string title, ref string subTitle, Document doc, ref int resolution, ref bool landScape, ref int pixelsMargins)
        {
            Program.IsFromUser = false;
            this.doc = doc;
            aspectRatio = size.Width / size.Height;
            cboPaper.SelectedIndex = 0;
            cboUnits.SelectedIndex = currentUnits;
            this.txtHeight.Text = size.Height.ToString();
            this.txtWidth.Text = size.Width.ToString();
            this.txtTitle.Text = title;
            this.fitInfo = doc.CurrentModel;
            this.txtMargins.Text = pixelsMargins.ToString();
            this.txtSubTitle.Text = "";
            btnApplyParametersLegend.Enabled = chkSubTitle.Checked && fitInfo != null;
            btnApplyParametersValues.Enabled = chkSubTitle.Checked && fitInfo != null;
            btnApplyDerivedValues.Enabled = chkSubTitle.Checked && fitInfo != null && fitInfo.DerivedValues != null && fitInfo.DerivedValues.Count > 0;
            subTitle = String.Empty;

            Program.IsFromUser = true;

            if (this.ShowDialog() == DialogResult.OK)
            {
                resolution = dpi;
                size = new SizeF(width, height);
                title = txtTitle.Text;
                currentUnits = cboUnits.SelectedIndex;
                if (chkSubTitle.Checked)
                {
                    subTitle = txtSubTitle.Text;
                }
                int widthPixels = 0;
                int heightPixels = 0;
                switch (currentUnits)
                {
                    case 0: widthPixels = (int)width; heightPixels = (int)height; pixelsMargins = (int)margins; dpi = Constants.STANDARD_DPI; break;
                    case 1: widthPixels = (int)(width * dpi); heightPixels = (int)(height * dpi); pixelsMargins = (int)(margins * dpi); break;
                    case 2: widthPixels = (int)(width / inchToCm * dpi); heightPixels = (int)(height / inchToCm * dpi); pixelsMargins = (int)(margins / inchToCm * dpi); break;
                }
                resolution = dpi;
                landScape = rdoLandscape.Checked;
                return new Size(widthPixels, heightPixels);
            }
            else return Size.Empty;
        }

        private void PlotExportDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (DialogResult == DialogResult.OK)
                {
                    if (!float.TryParse(txtWidth.Text, out width))
                    {
                        errorProvider.SetError((Control)txtWidth, "Invalid number");
                        e.Cancel = true;
                        return;
                    }
                    else errorProvider.SetError((Control)txtWidth, "");

                    if (!float.TryParse(txtHeight.Text, out height))
                    {
                        errorProvider.SetError((Control)txtHeight, "Invalid number");
                        e.Cancel = true;
                        return;
                    }
                    else errorProvider.SetError((Control)txtHeight, "");

                    if (!int.TryParse(cboDPI.Text, out dpi))
                    {
                        errorProvider.SetError((Control)cboDPI, "Invalid number");
                        e.Cancel = true;
                        return;
                    }
                    else errorProvider.SetError((Control)cboDPI, "");

                    if (!float.TryParse(txtMargins.Text, out margins))
                    {
                        errorProvider.SetError((Control)cboDPI, "Invalid number");
                        e.Cancel = true;
                        return;
                    }
                    else errorProvider.SetError((Control)cboDPI, "");
                }
            }
        }

        private void chkSubTitle_CheckedChanged(object sender, EventArgs e)
        {
            txtSubTitle.Enabled = chkSubTitle.Checked;

            btnApplyParametersValues.Enabled = chkSubTitle.Checked && fitInfo != null;
            btnApplyParametersLegend.Enabled = chkSubTitle.Checked && fitInfo != null;
            btnApplyDerivedValues.Enabled = chkSubTitle.Checked && fitInfo != null && fitInfo.DerivedValues != null && fitInfo.DerivedValues.Count > 0;
        }

        private void PlotExportDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
        }

        private void cboUnits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;

            errorProvider.SetError((Control)cboDPI, "");
            errorProvider.SetError((Control)txtWidth, "");
            errorProvider.SetError((Control)txtHeight, "");
            errorProvider.SetError((Control)txtMargins, "");

            if (!int.TryParse(cboDPI.Text, out dpi))
            {
                errorProvider.SetError((Control)cboDPI, "Invalid resolution (DPI) value");
                return;
            }

            if (!float.TryParse(txtWidth.Text, out width))
            {
                errorProvider.SetError((Control)txtWidth, "Invalid value");
                return;
            }

            if (!float.TryParse(txtHeight.Text, out height))
            {
                errorProvider.SetError((Control)txtHeight, "Invalid value");
                return;
            }

            if (!float.TryParse(txtMargins.Text, out margins))
            {
                errorProvider.SetError((Control)txtMargins, "Invalid value");
                return;
            }

            switch (currentUnits)
            {
                case 0: switch (cboUnits.SelectedIndex)
                    {
                        case 1: // Convert from pixels to inches
                            width = width / dpi;
                            height = height / dpi;
                            margins = margins / dpi;
                            break;
                        case 2: // convert from pixels to cm
                            width = width / dpi * inchToCm;
                            height = height / dpi * inchToCm;
                            margins = margins / dpi * inchToCm;
                            break;
                    }
                    break;
                case 1: switch (cboUnits.SelectedIndex)
                    {
                        case 2: // Convert from inches to cm
                            width = width * inchToCm;
                            height = height * inchToCm;
                            margins = margins * inchToCm;
                            break;
                        case 0: // convert from inches to pixels
                            width = width * dpi;
                            height = height * dpi;
                            margins = margins * dpi;
                            break;
                    }
                    break;
                case 2: switch (cboUnits.SelectedIndex)
                    {
                        case 1: // Convert from cm to inches
                            width = height / inchToCm;
                            margins = margins / inchToCm;
                            break;
                        case 0: // convert from cm to pixels
                            width = width / inchToCm * dpi;
                            height = height / inchToCm * dpi;
                            margins = margins / inchToCm * dpi;
                            break;
                    }
                    break;
            }

            currentUnits = cboUnits.SelectedIndex;

            if (cboUnits.SelectedIndex == 0)
            {
                //pixels
                txtWidth.Text = String.Format("{0:F0}", width);
                txtHeight.Text = String.Format("{0:F0}", height);
                txtMargins.Text = String.Format("{0:F0}", margins);
            }
            else
            {
                //inches or cm
                txtWidth.Text = String.Format("{0:F2}", width);
                txtHeight.Text = String.Format("{0:F2}", height);
                txtMargins.Text = String.Format("{0:F2}", margins);
            }

            Program.IsFromUser = true;
        }

        private void txtWidth_TextChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;

            if (chkAspect.Checked)
            {
                if (float.TryParse(txtWidth.Text, out width))
                {
                    height = width / aspectRatio;
                    if (currentUnits == 0)
                    {
                        txtHeight.Text = String.Format("{0:F0}", height);
                    }
                    else
                    {
                        txtHeight.Text = String.Format("{0:F2}", height);
                    }
                }
            }
            Program.IsFromUser = true;
        }

        private void chkAspect_CheckedChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;

            if (chkAspect.Checked)
            {
                if (!float.TryParse(txtWidth.Text, out width))
                {
                    errorProvider.SetError((Control)txtWidth, "Invalid value");
                    chkAspect.Checked = false;
                    return;
                }

                if (!float.TryParse(txtHeight.Text, out height))
                {
                    errorProvider.SetError((Control)txtHeight, "Invalid value");
                    chkAspect.Checked = false;
                    return;
                }

                aspectRatio = width / height;

                txtHeight.Enabled = false;
            }
            else
            {
                txtHeight.Enabled = true;
            }

            Program.IsFromUser = true;

        }

        private void cboPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;

            if (cboPaper.SelectedIndex != 0)
            {

                float[] paperWidth = new float[] { width, 8.3f, 5.8f, 4.1f };
                float[] paperHeight = new float[] { height, 11.7f, 8.3f, 5.8f };

                if (!int.TryParse(cboDPI.Text, out dpi))
                {
                    errorProvider.SetError((Control)cboDPI, "Invalid resolution (DPI) value");
                    return;
                }
                else
                {
                    errorProvider.SetError((Control)cboDPI, "");
                }

                if (!float.TryParse(txtMargins.Text, out margins))
                {
                    errorProvider.SetError((Control)txtMargins, "Invalid value");
                    return;
                }
                else
                {
                    errorProvider.SetError((Control)txtMargins, "");
                }

                switch (currentUnits)
                {
                    case 0: margins = margins / dpi; break;
                    case 2: margins = margins / inchToCm; break;
                }


                width = rdoPortrait.Checked ? paperWidth[cboPaper.SelectedIndex] : paperHeight[cboPaper.SelectedIndex];
                height = chkAspect.Checked ? width / aspectRatio : (rdoPortrait.Checked ? paperHeight[cboPaper.SelectedIndex] : paperWidth[cboPaper.SelectedIndex]);
                if (rdoLandscape.Checked && height > paperWidth[cboPaper.SelectedIndex])
                {
                    height = rdoPortrait.Checked ? paperHeight[cboPaper.SelectedIndex] : paperWidth[cboPaper.SelectedIndex];
                    width = chkAspect.Checked ? height * aspectRatio : (rdoPortrait.Checked ? paperWidth[cboPaper.SelectedIndex] : paperHeight[cboPaper.SelectedIndex]);

                }

                txtWidth.Text = String.Format("{0:F2}", width);
                txtHeight.Text = String.Format("{0:F2}", height);
                txtMargins.Text = String.Format("{0:F2}", margins);

                cboUnits.SelectedIndex = 1;
                currentUnits = 1;
            }

            Program.IsFromUser = true;
        }

        private void btnApplyParametersLegend_Click(object sender, EventArgs e)
        {
            string expression = Parser.ReplaceParametersWithGreekSymbols(fitInfo.ModelFunction.Description, fitInfo.ModelFunction.Parameters);
            expression = expression.Replace("x", doc.GridColumnSettings[0].Name);
            string line = String.Format("{0}({1}) = ", doc.GridColumnSettings[1].Name, doc.GridColumnSettings[0].Name) + expression;
            for (int i = 0; i < fitInfo.ModelFunction.Parameters.Length; i++)
            {
                if (Parser.greekSymbols.ContainsKey(fitInfo.ModelFunction.Parameters[i]))
                {
                    line = line + ", " + Parser.greekSymbols[fitInfo.ModelFunction.Parameters[i]] + "=" + NumberFormatter.FloatToString(fitInfo.LastFitParameters[i], 3, 4);

                }
                else
                {
                    line = line + ", " + fitInfo.ModelFunction.Parameters[i] + "=" + NumberFormatter.FloatToString(fitInfo.LastFitParameters[i], 3, 4);
                }
            }
            txtSubTitle.Text = line;
        }

        private void btnDerivedValues_Click(object sender, EventArgs e)
        {
            string line = "";
            for (int i = 0; i < fitInfo.DerivedValues.Count; i++)
            {
                if (i>0)
                {
                    line += ",  ";
                }

                if (Parser.greekSymbols.ContainsKey(fitInfo.DerivedValues[i].Name))
                {
                    line = line + Parser.greekSymbols[fitInfo.DerivedValues[i].Name] + " = " + NumberFormatter.FloatToString(fitInfo.DerivedValues[i].Value, 3, 4);

                }
                else
                {
                    line = line + fitInfo.DerivedValues[i].Name + " = " + NumberFormatter.FloatToString(fitInfo.DerivedValues[i].Value, 3, 4);
                }
            }
            txtSubTitle.Text = line;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void btnApplyParametersValues_Click(object sender, EventArgs e)
        {
            string[] formattedValues = new string[fitInfo.ModelFunction.Parameters.Length];
            for (int i = 0; i < fitInfo.ModelFunction.Parameters.Length; i++)
            {
                formattedValues[i] = NumberFormatter.FloatToString((double)fitInfo.LastFitParameters[i], 3, 4); //Those numbers are to be placed in configuration and be configurable
            }

            string expression = Parser.ReplaceParametersWithValues(fitInfo.ModelFunction.Description,
                                                 fitInfo.ModelFunction.Parameters,
                                                 fitInfo.LastFitParameters,
                                                 formattedValues);
            expression = expression.Replace("x", doc.GridColumnSettings[0].Name);
            string line = String.Format("{0}({1}) = ", doc.GridColumnSettings[1].Name, doc.GridColumnSettings[0].Name) + expression;
            txtSubTitle.Text = line;
        }

        private void PlotExportDialog_KeyDown(object sender, KeyEventArgs e)
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
