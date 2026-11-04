//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Drawing;
using System.Windows.Forms;

namespace LybraFit.Forms
{
    public partial class MessageDialog : Form
    {
        public Point LeftTopPos;

        public MessageDialog()
        {
            InitializeComponent();
        }

        private Point CenterToParent(Control dialog)
        {
            Point p = Point.Empty;
            p.Y = (dialog.Bottom + dialog.Top) / 2 - (this.Height) / 2;
            p.X = (dialog.Right + dialog.Left) / 2 - (this.Width) / 2;
            return p;
        }

        public DialogResult Show(string caption, string[] message, Control parent)
        {
            this.txtMessage.Lines = message;
            this.Text = caption;
            this.LeftTopPos = CenterToParent(parent);
            return this.ShowDialog();
        }

        private void MessageDialog_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            btnNo.Focus();
        }
    }
}
