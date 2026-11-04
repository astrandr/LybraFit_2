//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Drawing;

namespace MathLib
{
    public class MultiplicationNode : BinaryOperationNode
    {
        public MultiplicationNode()
        {
            this.precedencePos = 2;
            this.alignment = BinaryAlignment.Horizontal;
            this.symbol = ".";
        }

        public override void CalculateSizeAndPosition(Graphics g, Font font)
        {
            this.size = g.MeasureString(this.symbol, font);


            if (leftChild is BracketNode || rightChild is BracketNode || leftChild is DivisionNode || rightChild is DivisionNode)
            {
                this.position = new PointF(0, - size.Height / 5);
            }
            else
            {
                size = new SizeF(size.Width / 2, size.Height);
                this.position = new PointF(-size.Width / 2, - size.Height / 6);
            }

            this.boundingRectangle = new RectangleF(-size.Width / 2, -size.Height/2, size.Width, size.Height);
        }

        public override void DrawNode(Graphics g, Font font, Brush brush, float offsetX, float offsetY)
        {
            g.DrawString(this.symbol, font, brush, boundingRectangle.X + offsetX+position.X, boundingRectangle.Y + offsetY+position.Y);
        }
    }
}
