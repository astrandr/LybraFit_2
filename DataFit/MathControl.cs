//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Drawing;

namespace MathLib
{
    public class Settings
    {
        private static float verticalSpacing = 2, horizontalSpacing = 4;
        private static float scaling = 1;
        public static void SetResolution(int dpi)
        {
            scaling = dpi / 96;
        }

        public static int VerticalSpacing
        {
            get
            {
                return (int)(verticalSpacing * scaling);
            }
        }

        public static int HorisontalSpacing
        {
            get
            {
                return (int)(horizontalSpacing * scaling);
            }
        }

        public static Pen GetPen()
        {
            return new Pen(Color.Black, (int)scaling);
        }

        public static SizeF CorrectSize(SizeF size)
        {
            return new SizeF(size.Width * scaling, size.Height * scaling);
        }

        public static float Scaling
        {
            get
            {
                return scaling;
            }
        }
    }

    public class MathControl
    {
        private Size size;

        public Size Size
        {
            get { return size; }
            set { size = value; }
        }
        public MathControl()
        {

        }
        public MathControl(int width, int height)
        {
            this.size = new Size(width, height);
        }

        public Image GetImage(string mathExpression, int resolution = 300, string fontFamily = "Calisto MT")
        {
            int fontIndex = 6; // font size 11;
            MathParser parser = new MathParser();
            Node mathTree = parser.BuildTree(mathExpression);
            if (mathTree == null) return null;
            if (fontIndex < parser.NestedLevels)
            {
                fontIndex = parser.NestedLevels;
            }
            int fontSize = Node.FontSizes[fontIndex - 1];
            Font font = new Font(fontFamily, fontSize, FontStyle.Regular);
            Settings.SetResolution(resolution);

            int width = size.Width, height = size.Height;
            Bitmap image = new Bitmap(size.Width, size.Height);
            image.SetResolution(resolution, resolution);

            RectangleF boundingBox = mathTree.CalculateLayout(Graphics.FromImage(image), font);
            bool scaled = false;
            int imageWidth = (int)(boundingBox.Width / Settings.Scaling);
            int imageHeight = (int)(boundingBox.Height / Settings.Scaling);
            float k = 1;
            if (imageWidth < width && imageHeight < height)
            {
                image = new Bitmap((int)boundingBox.Width, (int)boundingBox.Height);
                image.SetResolution(resolution, resolution);
            }
            else
            {
                if (imageWidth > width)
                {
                    k = (float)width / imageWidth;
                    scaled = true;
                }

                if (imageHeight > height)
                {
                    k = (float)height/imageHeight;
                    scaled = true;
                }
            }

            image = new Bitmap((int)(imageWidth * Settings.Scaling), (int)(imageHeight * Settings.Scaling));
            image.SetResolution(resolution, resolution);

            mathTree.DrawTree(Graphics.FromImage(image), font, new SolidBrush(Color.Black), -boundingBox.X, -boundingBox.Y);

            Bitmap resizedImage = new Bitmap((int)(imageWidth * k), (int)(imageHeight * k));
            Graphics.FromImage(resizedImage).DrawImage(image, 0, 0, (int)(imageWidth * k), (int)(imageHeight * k));
            return resizedImage;
        }

        public Image GetImage(Node mathTree, int resolution = 300, Font font = null)
        {

            Settings.SetResolution(resolution);

            int width = size.Width, height = size.Height;
            Bitmap image = new Bitmap(size.Width, size.Height);
            image.SetResolution(resolution, resolution);

            RectangleF boundingBox = mathTree.CalculateLayout(Graphics.FromImage(image), font);
            bool scaled = false;
            int imageWidth = (int)(boundingBox.Width / Settings.Scaling);
            int imageHeight = (int)(boundingBox.Height / Settings.Scaling);
            float k = 1;
            if (imageWidth < width && imageHeight < height)
            {
                image = new Bitmap((int)boundingBox.Width, (int)boundingBox.Height);
                image.SetResolution(resolution, resolution);
            }
            else
            {
                if (imageWidth > width)
                {
                    k = (float)width / imageWidth;
                    scaled = true;
                }

                if (imageHeight > height)
                {
                    k = (float)height / imageHeight;
                    scaled = true;
                }
            }

            image = new Bitmap((int)(imageWidth * Settings.Scaling), (int)(imageHeight * Settings.Scaling));
            image.SetResolution(resolution, resolution);

            mathTree.DrawTree(Graphics.FromImage(image), font, new SolidBrush(Color.Black), -boundingBox.X, -boundingBox.Y);

            Bitmap resizedImage = new Bitmap((int)(imageWidth * k), (int)(imageHeight * k));
            Graphics.FromImage(resizedImage).DrawImage(image, 0, 0, (int)(imageWidth * k), (int)(imageHeight * k));
            return resizedImage;
        }

        public bool DrawExpression(Graphics g, string mathExpression, Rectangle clientRectangle, Color bkColor, int resolution = 300, string fontFamily = "Calisto MT")
        {
            Settings.SetResolution(96);//default resolution
            int fontIndex = 6; // font size 11;
            MathParser parser = new MathParser();
            if (mathExpression =="")
            {
                g.FillRectangle(new SolidBrush(bkColor), clientRectangle);
                return false;
            }
            Node mathTree = parser.BuildTree(mathExpression);
            if (mathTree == null || !mathTree.IsCorrect()) return false;
            if (fontIndex < parser.NestedLevels)
            {
                fontIndex = parser.NestedLevels;
            }
            if (fontIndex > Node.FontSizes.Length) fontIndex = Node.FontSizes.Length - 1;
            int fontSize = Node.FontSizes[fontIndex];
            Font font = new Font(fontFamily, fontSize, FontStyle.Regular);

            RectangleF boundingBox = RectangleF.Empty;

            try
            {
                boundingBox = mathTree.CalculateLayout(g, font);
            }
            catch
            {                
                return false;
            }

            g.FillRectangle(new SolidBrush(bkColor), clientRectangle);

            if (boundingBox.Width <= clientRectangle.Width && boundingBox.Height <= clientRectangle.Height)
            {
                mathTree.DrawTree(g, font, new SolidBrush(Color.Black), -boundingBox.X + size.Width / 2 - boundingBox.Width / 2, -boundingBox.Y + size.Height / 2 - boundingBox.Height / 2);
            }
            else
            {
                Image image = GetImage(mathExpression, resolution, fontFamily);
                g.DrawImage(image, clientRectangle.Width / 2 - image.Width / 2, clientRectangle.Height / 2 - image.Height / 2);
            }

            return true;
        }
    }
}
