//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace DataFit
{
    public class StylishButton : Button
    {
        Bitmap backGroundImage = null;
        bool ButtonHover = false;
        bool ButtonPressed = false;

        public StylishButton() : base()
        {
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.HandlePaint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HandleMouseDown);
            this.MouseEnter += new System.EventHandler(this.HandleMouseEnter);
            this.MouseLeave += new System.EventHandler(this.HandleMouseLeave);
            this.MouseHover += new System.EventHandler(this.HandleMouseHover);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.HandleMouseUp);
            this.Enter += new EventHandler(this.HandleMouseEnter);
            this.Leave += new EventHandler(this.HandleMouseLeave);
            this.EnabledChanged += new EventHandler(EnabledOnOff);
        }

        protected void EnabledOnOff(object sender, EventArgs args)
        {
            backGroundImage = null;
            Refresh();
        }

        private void HandlePaint(object sender, PaintEventArgs e)
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
                if(!Enabled)
                {
                    g.FillRectangle(new SolidBrush(color4), innerRectangle2);
                    SizeF textSize = g.MeasureString(Text, Font);
                    g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(255, 140, 140, 140)), outlineRectangle.Width / 2 - textSize.Width / 2 + 1, outlineRectangle.Height / 2 - textSize.Height / 3);

                }
                else
                if (ButtonPressed)
                {
                    g.FillRectangle(new SolidBrush(color3), innerRectangle2);
                    SizeF textSize = g.MeasureString(Text, Font);
                    g.DrawString(Text, Font, new SolidBrush(Color.FromArgb(255, 80, 80, 80)), outlineRectangle.Width / 2 - textSize.Width / 2 + 1, outlineRectangle.Height / 2 - textSize.Height / 3);

                }
                else
                {
                    g.FillRectangle(new SolidBrush(color4), innerRectangle2);
                    SizeF textSize = g.MeasureString(Text, Font);
                    g.DrawString(Text, Font, new SolidBrush(ForeColor), outlineRectangle.Width / 2 - textSize.Width / 2 + 1, outlineRectangle.Height / 2 - textSize.Height / 3);

                }

                g.DrawRectangle(new Pen(color4), outlineRectangle);

            }

            e.Graphics.DrawImage(backGroundImage, 0, 0);

            if (ButtonHover)
            {
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 150, 150, 150), 2), outlineRectangle);
                e.Graphics.DrawRectangle(new Pen(Color.FromArgb(255, 220, 220, 220), 1), outlineRectangle);
            }
        }

        private void HandleMouseEnter(object sender, EventArgs e)
        {
            ButtonHover = true;
        }

        private void HandleMouseHover(object sender, EventArgs e)
        {
            ButtonHover = true;
        }

        private void HandleMouseLeave(object sender, EventArgs e)
        {
            ButtonHover = false;
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            ButtonPressed = true;
            backGroundImage = null;
            Refresh();
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            ButtonPressed = false;
            backGroundImage = null;
            Refresh();
        }
    }
}
