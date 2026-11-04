//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Diagnostics;
using System.Drawing;

namespace MathLib
{
    public enum BinaryAlignment
    {
        Horizontal,
        Vertical
    }

    public class BinaryOperationNode : OperationNode
    {
        protected BinaryAlignment alignment;

        protected Node leftChild, rightChild;

        protected PointF leftChildShift, rightChildShift;

        public Node LeftChild
        {
            get { return leftChild; }
        }

        public Node RightChild
        {
            get { return rightChild; }
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

        public override RectangleF CalculateLayout(System.Drawing.Graphics g, System.Drawing.Font font)
        {

            //Handle and situation when we use '-' sign just in front of the argument but not as binary operation
            RectangleF leftRect = leftChild != null ? leftChild.CalculateLayout(g, font) : RectangleF.Empty;
            RectangleF rightRect = rightChild != null ? rightChild.CalculateLayout(g, font) : RectangleF.Empty;
            
            CalculateSizeAndPosition(g, font);

            if (alignment == BinaryAlignment.Horizontal)
            {
                //Handle and situation when we use '-' sign just in front of the argument but not as binary operation
                if (leftChild != null)
                {
                    leftChild.Move(-leftChild.TreeBoundingRectangle.Right + this.BoundingRectangle.Left, 0);
                }

                if (rightChild != null)
                {
                    rightChild.Move(-rightChild.TreeBoundingRectangle.Left + this.BoundingRectangle.Right, 0);
                }
            }

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

        public override void Move(float shiftX, float shiftY)
        {
            base.Move(shiftX, shiftY);
            if (leftChild != null)
            {
                //Handle and situation when we use '-' sign just in front of the argument but not as binary operation
                leftChild.Move(shiftX, shiftY);
            }

            if (rightChild != null)
            {
                rightChild.Move(shiftX, shiftY);
            }
        }

        public override void DrawTree(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            if (leftChild != null)
            {
                //Handle and situation when we use '-' sign just in front of the argument but not as binary operation;
                leftChild.DrawTree(g, font, brush, offsetX, offsetY);
            }

            if (rightChild != null)
            {
                rightChild.DrawTree(g, font, brush, offsetX, offsetY);
            }
            DrawNode(g, font, brush, offsetX, offsetY);
        }

        public override bool IsCorrect()
        {
            return leftChild != null && rightChild != null && leftChild.IsCorrect() && rightChild.IsCorrect();
        }
    }
}
