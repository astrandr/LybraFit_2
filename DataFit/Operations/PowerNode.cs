//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Diagnostics;
using System.Drawing;

namespace MathLib
{

    public class PowerNode : BinaryOperationNode
    {

        public PowerNode()
        {
            this.precedencePos = 5;
            this.symbol = "^";
            this.smallerFontDistance = 5;
        }

        public override bool CanAppendChild
        {
            get { return this.rightChild == null || this.leftChild == null; }
        }

        public override void AppendChild(Node node)
        {
            Debug.Assert(this.rightChild == null || this.leftChild == null);
            if (rightChild == null)
            {
                this.rightChild = node;
            }
            else
            {
                this.leftChild = node;
            }
            node.Parent = this;
        }

        public override void CalculateSizeAndPosition(Graphics g, Font font)
        {
            this.Size = new SizeF(0, 0);
        }

        public override RectangleF CalculateLayout(System.Drawing.Graphics g, System.Drawing.Font font)
        {

            boundingRectangle = RectangleF.Empty;

            //Do not enclose power in brackets
            if (rightChild is BracketNode)
            {
                (rightChild as BracketNode).DrawBrackets = false;
            }

            if (leftChild is BracketNode)
            {
                if ((leftChild as BracketNode).Body is SymbolNode)
                {
                    (leftChild as BracketNode).DrawBrackets = false;
                }
                else
                {
                    (leftChild as BracketNode).CanRemoveBrackets = false;
                }
            }

            RectangleF leftRect = leftChild.CalculateLayout(g, font);
            RectangleF rightRect = rightChild.CalculateLayout(g, GetSmallerFont(font, smallerFontDistance));

            leftChild.Move(-leftChild.TreeBoundingRectangle.Right, 0);
            //Do not enclose power in brackets
            if (rightChild is BracketNode)
            {
                rightChild.Move(-(rightChild as BracketNode).Size.Width, 0); //Avoid shift left because of bracket symbol
                (rightChild as BracketNode).DrawBrackets = false;
            }
            rightChild.Move(-rightChild.TreeBoundingRectangle.Left, (leftChild.TreeBoundingRectangle.Top - rightChild.TreeBoundingRectangle.Top) - rightChild.TreeBoundingRectangle.Height / 3);

            this.treeBoundingRectangle = RectangleF.Union(RectangleF.Union(leftChild.TreeBoundingRectangle, rightChild.TreeBoundingRectangle), this.boundingRectangle);
            this.Position = new PointF(0, -treeBoundingRectangle.Height - leftChild.Position.Y);
            return this.treeBoundingRectangle;

        }

        public override void Move(float shiftX, float shiftY)
        {
            this.boundingRectangle.Offset(shiftX, shiftY);
            this.treeBoundingRectangle.Offset(shiftX, shiftY);

            leftChild.Move(shiftX, shiftY);
            rightChild.Move(shiftX, shiftY);
        }

        public override void DrawTree(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            //Power node does not draw itself only children
            leftChild.DrawTree(g, font, brush, offsetX, offsetY);
            rightChild.DrawTree(g, GetSmallerFont(font, smallerFontDistance), brush, offsetX, offsetY);
        }
    }
}
