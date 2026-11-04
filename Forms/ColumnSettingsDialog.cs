//Designed and Implemented by Andrey Batchvarov 2018
using DataFit.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataFit.Forms
{
    public partial class ColumnSettingsDialog : Form
    {
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

        public void InitializeFromSettings(ColumnSettings[] Settings)
        {
            this.listColumns.Items.Clear();
            foreach (ColumnSettings columnSettings in Settings)
            {
                listColumns.Items.Add(columnSettings);
            }
            listColumns.SelectedIndex = 0;
        }

        private bool ConvertToSettings()
        {
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
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.CustomFormat = txtCustomFormat.Text;
                Program.IsFromUser = true;
            }
        }

        private void ColumnSettingsDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None)
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

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (Program.IsFromUser)
            {
                Program.IsFromUser = false;
                ColumnSettings settings = (ColumnSettings)listColumns.Items[listColumns.SelectedIndex];
                settings.Name = txtName.Text;
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
                DialogResult = DialogResult.OK;
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
    }
}
