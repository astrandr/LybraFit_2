//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System;
using System.Drawing;

namespace MathLib
{
    public class DivisionNode : BinaryOperationNode
    {
        public DivisionNode()
        {
            this.symbol = "/"; //setting some number so the size of character is used
            this.precedencePos = 3;
            this.alignment = BinaryAlignment.Vertical;
        }

        public override void CalculateSizeAndPosition(Graphics g, Font font)
        {
            float width = Math.Max(leftChild.TreeBoundingRectangle.Width, rightChild.TreeBoundingRectangle.Width);
            size = new SizeF(width, 3 * Settings.VerticalSpacing);
            this.boundingRectangle = new RectangleF(0, 0, size.Width, size.Height);
            this.position = new PointF(0, 0);
        }

        public override RectangleF CalculateLayout(System.Drawing.Graphics g, System.Drawing.Font font)
        {
            //Remove brackets in division operands
            if (rightChild is BracketNode)
            {
                (rightChild as BracketNode).DrawBrackets = false;
            }

            if (leftChild is BracketNode)
            {
                (leftChild as BracketNode).DrawBrackets = false;
            }

            //Handle and situation when we use '-' sign just in front of the argument but not as binary operation
            RectangleF leftRect = leftChild != null ? leftChild.CalculateLayout(g, font) : RectangleF.Empty;
            RectangleF rightRect = rightChild != null ? rightChild.CalculateLayout(g, font) : RectangleF.Empty;

            RectangleF leftTreeBoundingRectangle = leftChild != null ? leftChild.TreeBoundingRectangle : RectangleF.Empty;
            RectangleF rightTreeBoundingRectangle = rightChild != null ? rightChild.TreeBoundingRectangle : RectangleF.Empty;

            float width = Math.Max(leftTreeBoundingRectangle.Width, rightTreeBoundingRectangle.Width);

            if (leftChild != null)
            {
                leftChild.Move(-leftTreeBoundingRectangle.Left + (width / 2 - leftTreeBoundingRectangle.Width / 2), -leftTreeBoundingRectangle.Height - (leftChild != null ? leftChild.TreeBoundingRectangle.Y : 0) - Settings.VerticalSpacing);
            }

            if (rightChild != null)
            {
                rightChild.Move(-rightTreeBoundingRectangle.Left + (width / 2 - rightTreeBoundingRectangle.Width / 2), -(rightChild != null ? rightChild.TreeBoundingRectangle.Y : 0) + Settings.VerticalSpacing);
            }
            this.position = new PointF(0, -leftTreeBoundingRectangle.Height);

            if (leftChild == null)
            {
                //Handle and situation when we use '-' sign just in front of the argument but not as binary operation
                this.treeBoundingRectangle = RectangleF.Union(rightChild.TreeBoundingRectangle, this.boundingRectangle);
            }
            else if (rightChild == null)
            {
                //Handle and situation when we use '-' sign just in front of the argument but not as binary operation
                this.treeBoundingRectangle = RectangleF.Union(leftChild.TreeBoundingRectangle, this.boundingRectangle);
            }
            else
            {
                this.treeBoundingRectangle = RectangleF.Union(RectangleF.Union(leftChild.TreeBoundingRectangle, rightChild.TreeBoundingRectangle), this.boundingRectangle);
            }

            return this.treeBoundingRectangle;

        }

        public override void DrawNode(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            float width = Math.Max(leftChild != null ? leftChild.TreeBoundingRectangle.Width : 0, rightChild != null ? rightChild.TreeBoundingRectangle.Width : 0);
            g.DrawLine(Settings.GetPen(),
                boundingRectangle.X + offsetX,
                boundingRectangle.Y + offsetY,
                boundingRectangle.X + offsetX + width,
                boundingRectangle.Y + offsetY);
        }
    }
}
