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
            errorProvider.SetError((Control)txtMin, "");

            chklColumns.Items.Clear();
            foreach(DataFilterInfo filterInfo in DataFilterInfo.Items)
            {
                int i = chklColumns.Items.Add(filterInfo);
                chklColumns.SetItemChecked(i, filterInfo.Selected);
            }
            chklColumns.SetSelected(0, true);
            currentFilter = chklColumns.Items[0] as DataFilterInfo;

            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
        }

        private void FilterDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    float v = 0;
                    if (float.TryParse(txtMin.Text, out v))
                    {
                        currentFilter.Min = v;
                        errorProvider.SetError((Control)txtMin, "");
                    }
                    else
                    {
                        errorProvider.SetError((Control)txtMin, "Invalid number");
                        e.Cancel = true;
                        return;
                    }

                    if (float.TryParse(txtMin.Text, out v))
                    {
                        currentFilter.Min = v;
                        errorProvider.SetError((Control)txtMin, "");
                    }
                    else
                    {
                        errorProvider.SetError((Control)txtMin, "Invalid number");
                        e.Cancel = true;
                        return;
                    }

                    bool hasSelected = false;
                    for (int i = 0; i < chklColumns.Items.Count; i++)
                    {
                        DataFilterInfo.Items[i].Selected = chklColumns.GetItemChecked(i);
                        hasSelected |= DataFilterInfo.Items[i].Selected;
                    }

                    this.DialogResult = hasSelected ? DialogResult.OK : DialogResult.Cancel;
                }
            }
        }


        private void chklColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Program.IsFromUser) return;
            currentFilter = chklColumns.Items[chklColumns.SelectedIndex] as DataFilterInfo;
            txtMax.Text = currentFilter.Max.ToString();
            txtMin.Text = currentFilter.Min.ToString();
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
    }
}
