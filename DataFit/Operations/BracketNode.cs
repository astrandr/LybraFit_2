//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Drawing;

namespace MathLib
{
    public class BracketNode : OperationNode
    {
        protected Node body;
        private bool drawBrackets = true;
        private bool canRemoveBrackets = true;

        public bool CanRemoveBrackets
        {
            get { return canRemoveBrackets; }
            set { canRemoveBrackets = value; }
        }

        public bool DrawBrackets
        {
            get { return drawBrackets; }
            set { drawBrackets = value; }
        }

        public Node Body
        {
            get { return body; }
            set { body = value; }
        }

        public override bool IsCorrect()
        {
            return body != null && body.IsCorrect();
        }

        public BracketNode()
        {
            this.symbol = "(";
        }

        public override void Move(float shiftX, float shiftY)
        {
            base.Move(shiftX, shiftY);
            body.Move(shiftX, shiftY);
        }

        public override void CalculateSizeAndPosition(Graphics g, Font font)
        {
            this.size = g.MeasureString(this.symbol, font);
            this.position = new PointF(-size.Width / 2, -size.Height / 2);
            this.boundingRectangle = new RectangleF(position.X, position.Y, size.Width, size.Height);
        }


        public override RectangleF CalculateLayout(System.Drawing.Graphics g, System.Drawing.Font font)
        {

            body.CalculateLayout(g, font);
            this.CalculateSizeAndPosition(g, font);
            Position = new PointF(0, body.TreeBoundingRectangle.Top);
            if (body is DivisionNode && canRemoveBrackets)
            {
                this.DrawBrackets = false;
            }
            size = this.DrawBrackets ? size : new SizeF(0, 0);

            this.treeBoundingRectangle = new RectangleF(body.TreeBoundingRectangle.X - size.Width/2, body.TreeBoundingRectangle.Y, body.TreeBoundingRectangle.Width + size.Width-1, body.TreeBoundingRectangle.Height);
            return this.treeBoundingRectangle;
        }

        public override void DrawNode(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            float leftX = TreeBoundingRectangle.Left + offsetX;
            float rightX = TreeBoundingRectangle.Right + offsetX;
            float topY = TreeBoundingRectangle.Y + offsetY;

            if (drawBrackets)
            {
                g.DrawArc(Settings.GetPen(), leftX, topY, size.Width, TreeBoundingRectangle.Height, 90, 180);
            }

            body.DrawTree(g, font, brush, offsetX, offsetY);

            if (drawBrackets)
            {
                g.DrawArc(Settings.GetPen(), rightX - size.Width, topY, size.Width, TreeBoundingRectangle.Height, 270, 180);
            }
        }
    }
}
