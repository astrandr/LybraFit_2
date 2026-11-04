//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Diagnostics;
using System.Drawing;

namespace MathLib
{
    public class FunctionNode : OperationNode
    {

        protected string[] parameters;
        protected Node body;


        public Node Body
        {
            get { return body; }
            set
            {
                body = value;
            }
        }
        public override bool IsCorrect()
        {
            return body != null && body.IsCorrect();
        }

        public FunctionNode(string symbol)
        {
            this.symbol = symbol;
            this.smallerFontDistance = 0;
            this.precedencePos = 6;
        }

        public override void Move(float shiftX, float shiftY)
        {
            base.Move(shiftX, shiftY);
            body.Move(shiftX, shiftY);
        }

        public override void CalculateSizeAndPosition(Graphics g, Font font)
        {
            this.size = g.MeasureString(this.symbol, font);
            this.position = new PointF(-size.Width, -size.Height / 2);
            this.boundingRectangle = new RectangleF(position.X, position.Y, size.Width, size.Height);
        }

        public override RectangleF CalculateLayout(System.Drawing.Graphics g, System.Drawing.Font font)
        {
            this.CalculateSizeAndPosition(g, font);
            (body as BracketNode).CanRemoveBrackets = false;
            //body.CalculateLayout(g, GetSmallerFont(font, smallerFontDistance));
            body.CalculateLayout(g, font);
            body.Move(-body.TreeBoundingRectangle.Left, 0);
            this.treeBoundingRectangle = RectangleF.Union(body.TreeBoundingRectangle, this.boundingRectangle);
            return this.treeBoundingRectangle;
        }

        public override void DrawNode(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            base.DrawNode(g, font, brush, offsetX, offsetY);
            body.DrawTree(g, font, brush, offsetX, offsetY);
        }

        public override bool CanAppendChild
        {
            get
            {
                return this.body == null;
            }
        }

        public override void AppendChild(Node node)
        {
            Debug.Assert(this.body != null);
            this.body = node;
            node.Parent = this;
        }
    }
}
