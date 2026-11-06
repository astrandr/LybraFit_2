//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class FilterDialog : Form
    {
        private DataFilterInfo currentFilter = null;

        public Point LeftTopPos;

        public FilterDialog()
        {
            InitializeComponent();
        }

        private void FilterDialog_Load(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            
            errorProvider.SetError((Control)txtMin, "");
            errorProvider.SetError((Control)txtMax, "");

            chklColumns.Items.Clear();
            foreach(DataFilterInfo filterInfo in DataFilterInfo.Items)
            {
                if (filterInfo.Visible)
                {
                    int i = chklColumns.Items.Add(filterInfo);
                    chklColumns.SetItemChecked(i, filterInfo.Selected);
                }
            }

            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;

            Program.IsFromUser = true;

            chklColumns.SetSelected(0, true);
        }

        private void chklColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;
            currentFilter = chklColumns.Items[chklColumns.SelectedIndex] as DataFilterInfo;
            txtMax.Text = currentFilter.Max.ToString(); txtMax.Enabled = currentFilter.Selected;
            txtMin.Text = currentFilter.Min.ToString(); txtMin.Enabled = currentFilter.Selected;
            Program.IsFromUser = true;
        }

        private void chklColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!Program.IsFromUser) return;
            Program.IsFromUser = false;
            currentFilter.Selected = e.NewValue == CheckState.Checked;
            txtMax.Enabled = currentFilter.Selected;
            txtMin.Enabled = currentFilter.Selected;
            if (txtMin.Enabled) txtMin.Select(); 
            Program.IsFromUser = true;
        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            Program.IsFromUser = false;
            foreach (int i in chklColumns.CheckedIndices)
            {
                chklColumns.SetItemCheckState(i, CheckState.Unchecked);
            } 
            Program.IsFromUser = true;
        }

        private void txtMin_Validating(object sender, CancelEventArgs e)
        {
            double v = 0;
            if (double.TryParse(txtMin.Text, out v))
            {
                currentFilter.Min = v;
                errorProvider.SetError((Control)txtMin, "");
            }
            else
            {
                errorProvider.SetError((Control)txtMin, "Invalid number");
                txtMin.Select();
                e.Cancel = true;
                return;
            }
        }

        private void txtMax_Validating(object sender, CancelEventArgs e)
        {
            double v = 0;
            if (double.TryParse(txtMax.Text, out v))
            {
                currentFilter.Max = v;
                errorProvider.SetError((Control)txtMax, "");
            }
            else
            {
                errorProvider.SetError((Control)txtMax, "Invalid number");
                txtMax.Select();
                e.Cancel = true;
                return;
            }
        }
        
        private void txtMinMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Up && chklColumns.SelectedIndex > 0)
            {
                chklColumns.SelectedIndex--; chklColumns.Select();
                this.Validate(); //Force parsing of the entry in the text box
                e.Handled = true;
            }

            if (e.Control && e.KeyCode == Keys.Down && chklColumns.SelectedIndex < chklColumns.Items.Count - 1)
            {
                chklColumns.SelectedIndex++; chklColumns.Select();
                this.Validate(); //Force parsing of the entry in the text box
                e.Handled = true;
            }

            if(e.KeyCode == Keys.Space)
            {
                CheckState currentCheckState = chklColumns.GetItemCheckState(chklColumns.SelectedIndex);
                chklColumns.SetItemCheckState(chklColumns.SelectedIndex, currentCheckState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
                this.Validate(); //Force parsing of the entry in the text box
            }
        }

        private void FilterDialog_KeyDown(object sender, KeyEventArgs e)
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

        private void FilterDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    double v = 0;
                    if (double.TryParse(txtMin.Text, out v))
                    {
                        currentFilter.Min = v;
                        errorProvider.SetError((Control)txtMin, "");
                    }
                    else
                    {
                        errorProvider.SetError((Control)txtMin, "Invalid number");
                        txtMin.Select();
                        e.Cancel = true;
                        return;
                    }

                    if (double.TryParse(txtMax.Text, out v))
                    {
                        currentFilter.Max = v;
                        errorProvider.SetError((Control)txtMax, "");
                    }
                    else
                    {
                        errorProvider.SetError((Control)txtMax, "Invalid number");
                        txtMax.Select();
                        e.Cancel = true;
                        return;
                    }

                    for (int i = 0; i < chklColumns.Items.Count; i++)
                    {
                        DataFilterInfo.Items[i].Selected = chklColumns.GetItemChecked(i);
                    }
                }
            }
        }

    }
}
