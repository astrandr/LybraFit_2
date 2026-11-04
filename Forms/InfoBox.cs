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
    public partial class InfoBox : Form
    {
        private Point LeftTopPos;
        private static InfoBox infoBox = null;

        public InfoBox()
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

        public static DialogResult ShowMessage(string info, Control parent)
        {
            if (infoBox == null)
            {
                infoBox = new InfoBox();
            }
            return infoBox.Show(info, parent);
        }

        public static DialogResult ShowMessage(string info, string title, Control parent)
        {
            if (infoBox == null)
            {
                infoBox = new InfoBox();
            }
            return infoBox.Show(info, parent);
        }


        public static DialogResult ShowMessage(string[] info, string title, Control parent)
        {
            if (infoBox == null)
            {
                infoBox = new InfoBox();
            }
            return infoBox.Show(info, title, parent);
        }

        public DialogResult Show(string info, Control parent)
        {
            return this.Show(info, "", parent);
        }


        public DialogResult Show(string info, string title, Control parent)
        {
            return this.Show(new string[] { info }, title, parent);
        }

        public DialogResult Show(string[] info, string title, Control parent)
        {
            this.Text = title;
            this.txtInfo.Lines = info;

            //Adjust vertical size
            if (info != null && info.Length > 0)
            {
                int lineHeight;
                using (Graphics g = txtInfo.CreateGraphics())
                {
                    lineHeight = Convert.ToInt32(g.MeasureString("A", txtInfo.Font).Height);
                }
                if ((int)((info.Length + 1) * lineHeight) > this.txtInfo.Height)
                {
                    this.Height += (int)((info.Length + 1) * lineHeight - this.txtInfo.Height);
                }
                if (this.txtInfo.Height > 300) this.txtInfo.Height = 300;
            }

            //Adjust horizontal size
            int maxLineWidth = 0;
            using (Graphics g = txtInfo.CreateGraphics())
            {
                foreach (var line in info)
                {
                    int lineWidth = Convert.ToInt32(g.MeasureString(line, txtInfo.Font).Width);
                    if (lineWidth > maxLineWidth) maxLineWidth = lineWidth;
                }
            }

            if (maxLineWidth > txtInfo.Width)
            {
                if (maxLineWidth > 300) maxLineWidth = 300;
                this.Width += (maxLineWidth - txtInfo.Width);
                txtInfo.Width = maxLineWidth < 300 ? maxLineWidth : 300;
            }

            this.LeftTopPos = CenterToParent(parent);
            return this.ShowDialog();
        }

        private void InfoBox_Load(object sender, EventArgs e)
        {
            this.Left = LeftTopPos.X;
            this.Top = LeftTopPos.Y;
            btnClose.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
