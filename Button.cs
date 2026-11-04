using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoolButtons
{
    public partial class Form1 : Form
    {
        Bitmap backGroundImage = null;
        bool buttonHover = false;
        bool buttonPressed = false;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(255, 245, 245, 245);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle outlineRectangle = e.ClipRectangle;
            outlineRectangle.Inflate(-1, -1);

            if (backGroundImage == null)
            {
                backGroundImage = new Bitmap(e.ClipRectangle.Width, e.ClipRectangle.Height);
                Graphics g = Graphics.FromImage(backGroundImage);

                int a1 = 245, a2 = 235, a3 = 235, a4 = 225;
                Color color1 = Color.FromArgb(255, a1, a1, a1);
                Color color2 = Color.FromArgb(255, a2, a2, a2);
                Color color3 = Color.FromArgb(255, a3, a3, a3);
                Color color4 = Color.FromArgb(255, a4, a4, a4);
                Rectangle innerRectangle1 = new Rectangle(1, 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height / 2);

                LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, innerRectangle1.Bottom), color1, color2);

                Rectangle innerRectangle2 = new Rectangle(1, e.ClipRectangle.Bottom / 2 + 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height / 2);

                g.FillRectangle(gradientBrush, innerRectangle1);
                if (buttonPressed)
                {
                    g.FillRectangle(new SolidBrush(color3), innerRectangle2);
                    SizeF textSize = g.MeasureString(button1.Text, button1.Font);
                    g.DrawString(button1.Text, button1.Font, new SolidBrush(Color.FromArgb(255, 80, 80, 80)), outlineRectangle.Width / 2 - textSize.Width / 2 + 1, outlineRectangle.Height / 2 - textSize.Height / 3);

                }
                else
                {
                    g.FillRectangle(new SolidBrush(color4), innerRectangle2);
                    SizeF textSize = g.MeasureString(button1.Text, button1.Font);
                    g.DrawString(button1.Text, button1.Font, new SolidBrush(button1.ForeColor), outlineRectangle.Width / 2 - textSize.Width / 2 + 1, outlineRectangle.Height / 2 - textSize.Height / 3);

                }

                g.DrawRectangle(new Pen(color4), outlineRectangle);

            }

            e.Graphics.DrawImage(backGroundImage, 0, 0);

            if (buttonHover)
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 170, 170, 220), 1), outlineRectangle);
            }
        }

        private void button1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            buttonHover = true;
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            buttonHover = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            buttonHover = false;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            buttonPressed = true;
            backGroundImage = null;
            button1.Refresh();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            buttonPressed = false;
            backGroundImage = null; 
            button1.Refresh();
        }
    }
}
