//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Drawing;

namespace MathLib
{
    public class Node
    {
        protected Node parent;
        protected string symbol;

        public string Symbol
        {
            get { return symbol; }
        }

        protected int precedencePos = 0;
        protected PointF position;
        protected PointF shift;
        protected RectangleF boundingRectangle;

        protected RectangleF treeBoundingRectangle;

        public static int[] FontSizes = new int[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28 };
        public static int BracketWidth = 10;

        public RectangleF BoundingRectangle
        {
            get { return boundingRectangle; }
            set { boundingRectangle = value; }
        }

        public RectangleF TreeBoundingRectangle
        {
            get { return treeBoundingRectangle; }
            set { treeBoundingRectangle = value; }
        }

        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }
        protected SizeF size;

        public SizeF Size
        {
            get { return size; }
            set { size = value; }
        }

        public int PrecedencePos
        {
            get { return precedencePos; }
            set { precedencePos = value; }
        }

        public Node Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public override string ToString()
        {
            return symbol;
        }

        public virtual bool IsCorrect() { return true;}

        public virtual bool CanAppendChild { get { return false; } }

        public virtual void AppendChild(Node node) { }

        public virtual void Move(float shiftX, float shiftY)
        {
            this.boundingRectangle.Offset(shiftX, shiftY);
            this.treeBoundingRectangle.Offset(shiftX, shiftY);
        }

        public virtual void CalculateSizeAndPosition(Graphics g, Font font)
        {
            this.size = g.MeasureString(this.symbol, font);
            this.position = new PointF(-size.Width/2, -size.Height/2);
            this.boundingRectangle = new RectangleF(position.X, position.Y, size.Width, size.Height);
        }

        public virtual RectangleF CalculateLayout(Graphics g, Font font)
        {
            this.CalculateSizeAndPosition(g, font);            
            this.treeBoundingRectangle = boundingRectangle;
            return this.boundingRectangle;
        }

        public virtual void DrawTree(Graphics g, Font font, Brush brush, float offsetX, float offsetY)
        {
            DrawNode(g, font, brush, offsetX, offsetY);
        }

        public virtual void DrawNode(Graphics g, Font font, Brush brush, float offsetX, float offsetY)
        {
            g.DrawString(this.symbol, font, brush, boundingRectangle.X + offsetX, boundingRectangle.Y + offsetY);
        }

        protected Font GetSmallerFont(Font font, int smallerFontDistance = 1)
        {
            int fontIndex = 0;

            //Find the index of the closest font size
            for (; fontIndex < FontSizes.Length && FontSizes[fontIndex] < font.Size; fontIndex++) ;

            int newFontIndex = fontIndex >= smallerFontDistance ? fontIndex - smallerFontDistance : 0;

            return new Font(font.FontFamily, FontSizes[newFontIndex], font.Style);

        }
    }
}
