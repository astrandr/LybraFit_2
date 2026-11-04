using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class RegistrationForm : Form
    {
        public string FirstName { get { return txtFirstName.Text; } }
        public string LastName { get { return txtLastName.Text; } }

        public RegistrationForm()
        {
            InitializeComponent();
        }

        public string ShowDialog(string questionKey)
        {
            this.lblQuestionKey.Text = questionKey;

            if (this.ShowDialog() == DialogResult.OK)
            {
                return this.txtActivationKey.Text;
            }
            else
            {
                return null;
            }
        }

        private void RegistrationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(txtFirstName.Text))
                    {
                        MessageBox.Show("Enter first name");
                        e.Cancel = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(txtLastName.Text))
                    {
                        MessageBox.Show("Enter last name");
                        e.Cancel = true;
                        return;
                    }

                    if (string.IsNullOrEmpty(txtActivationKey.Text))
                    {
                        MessageBox.Show("Enter activation key");
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }
    }
}
