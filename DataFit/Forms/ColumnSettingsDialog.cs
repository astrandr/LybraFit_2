//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class ColumnSettingsDialog : Form
    {
        private static readonly string CUSTOMFORMAT_FILTER = @"^[0#]+\.?[#0]*";

        public ColumnSettings[] Settings;

        public Point LeftTopPos;

        public ColumnSettingsDialog()
        {
            InitializeComponent();

            foreach (string format in ColumnSettings.Formats)
            {
                cboFormat.Items.Add(format);
            }
        }

        public void InitializeFromSettings(Document doc)
        {
            ColumnSettings[] columnSettings = doc.GridColumnSettings; 
            this.listColumns.Items.Clear();
            listColumns.Items.Add(columnSettings[0]); //X Column
            listColumns.Items.Add(columnSettings[1]); //Y Column
            if(doc.HasErrorY)
            {
                listColumns.Items.Add(columnSettings[2]); //Err Column
            }
            if(doc.CurrentModel != null)
            {
                listColumns.Items.Add(columnSettings[3]); //Model Column
                listColumns.Items.Add(columnSettings[4]); //Difference Column
            }
            listColumns.SelectedIndex = 0;

            lblTableFont.Font = Settings[0].TableFont;
            this.chkAutoSort.Checked = Configuration.AutoSort;
        }

        private bool ConvertToSettings()
        {
            ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                errorProvider.SetError((Control)txtName, "Column name cannot be empty");
                txtName.Select();
                return false;
            }

            if (this.cboFormat.SelectedIndex == 3 && !Regex.IsMatch(txtCustomFormat.Text, CUSTOMFORMAT_FILTER))
            {
                this.errorProvider.SetError(this.txtCustomFormat, "Invalid format. Use #, 0 and decimal point, always start with 0 or # like 0.0#");
                this.txtCustomFormat.Select();
                return false;
            }

            settings.Name = txtName.Text;
            settings.Format = cboFormat.Text;
            settings.Digits = (uint)nmrDigits.Value;
            settings.CustomFormat = txtCustomFormat.Text;
            settings.AutoWidth = chkAutoWidth.Checked;
            settings.Visible = chkVisible.Checked;

            return true;
        }

        private void listColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
            txtName.Text = settings.Name;
            cboFormat.Text = settings.Format;
            nmrDigits.Value = settings.Digits;
            txtCustomFormat.Text = settings.CustomFormat;
            chkAutoWidth.Checked = settings.AutoWidth;
            chkVisible.Checked = settings.Visible;
            Program.IsFromUser = true;
        }

        private void UpdateCurrentSetting()
        {
            ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
            settings.Name = txtName.Text;
            settings.Alignment = "Right";
            settings.Digits = (uint)nmrDigits.Value;
            settings.CustomFormat = txtCustomFormat.Text;
            settings.Format = cboFormat.Text;
            settings.AutoWidth = chkAutoWidth.Checked;
        }


        private void cboAlignment_Validating(object sender, CancelEventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.Alignment = "Right";
                Program.IsFromUser = true;
            }
        }

        private void cboFormat_Validating(object sender, CancelEventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.Format = cboFormat.Text;
                Program.IsFromUser = true;
            }
        }

        private void nmrDigits_Validating(object sender, EventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.Digits = (uint)nmrDigits.Value;
                Program.IsFromUser = true;
            }
        }

        private void txtCustomFormat_Validating(object sender, CancelEventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                if (!Regex.IsMatch(txtCustomFormat.Text, CUSTOMFORMAT_FILTER))
                {
                    this.errorProvider.SetError(this.txtCustomFormat, "Invalid format. Use #, 0 and decimal point, always start with 0 or # like 0.0#");
                    this.txtCustomFormat.Focus();
                }
                else
                {
                    this.errorProvider.SetError(this.txtCustomFormat, "");
                    ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                    settings.CustomFormat = txtCustomFormat.Text;
                }
                Program.IsFromUser = true;
            }
        }

        private void ColumnSettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (!ConvertToSettings())
                    {
                        e.Cancel = true;
                    }

                    Configuration.AutoSort = this.chkAutoSort.Checked;
                }
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                if (string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    errorProvider.SetError((Control)txtName, "Column name cannot be empty");
                    txtName.Select();
                    Program.IsFromUser = true;
                    e.Cancel = true;
                    return;
                }
                else
                {
                    errorProvider.SetError((Control)txtName, "");
                    settings.Name = txtName.Text.Trim();
                }

                Program.IsFromUser = true;
            }
        }

        private void chkAutoWidth_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.AutoWidth = chkAutoWidth.Checked;
                Program.IsFromUser = true;
            }
        }

        private void chkVisible_CheckedChanged(object sender, EventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.Visible = chkVisible.Checked;
                Program.IsFromUser = true;
            }
        }

        private void ColumnSettingsDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            this.listColumns.Select();
        }

        private void ColumnSettingsDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }

            if (e.KeyCode == Keys.F1)
            {
                Help.ShowHelp(this, "LybraFitHelp.chm");
            }
        }

        private void listColumns_Enter(object sender, EventArgs e)
        {
            listColumns.BackColor = Color.White;
        }

        private void listColumns_Leave(object sender, EventArgs e)
        {
            listColumns.BackColor = System.Drawing.SystemColors.Control;
        }

        private void btnTableFont_Click(object sender, EventArgs e)
        {
            fontDialog.Font = ((ColumnSettings)listColumns.Items[0]).TableFont;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (fontDialog.Font.Size > 18)
                {
                    ((ColumnSettings)listColumns.Items[0]).TableFont = new Font(fontDialog.Font.FontFamily, 18, fontDialog.Font.Style);
                    InfoBox.ShowMessage("Table font size cannot be greater than 18.", this);
                }
                else
                {
                    ((ColumnSettings)listColumns.Items[0]).TableFont = fontDialog.Font;
                }
                lblTableFont.Font = ((ColumnSettings)listColumns.Items[0]).TableFont;
            }
        }

        private void cboFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtCustomFormat.Enabled = cboFormat.SelectedIndex == 3;
            this.nmrDigits.Enabled = cboFormat.SelectedIndex != 3;
        }
    }
}
