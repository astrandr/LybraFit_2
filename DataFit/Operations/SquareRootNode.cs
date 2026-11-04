//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Drawing;

namespace MathLib
{
    public class SquareRootNode : FunctionNode
    {
        public SquareRootNode(string symbol)
            : base(symbol)
        {

        }

        public override void CalculateSizeAndPosition(Graphics g, Font font)
        {
            size = new SizeF(10*Settings.Scaling, body.TreeBoundingRectangle.Height + 2 * Settings.VerticalSpacing);
            this.position = new PointF(0, -size.Height / 2);
            this.boundingRectangle = new RectangleF(position.X, position.Y, size.Width, size.Height);
        }

        public override RectangleF CalculateLayout(System.Drawing.Graphics g, System.Drawing.Font font)
        {
            (body as BracketNode).DrawBrackets = false;

            body.CalculateLayout(g, font);

            this.CalculateSizeAndPosition(g, font);

            this.treeBoundingRectangle = new RectangleF(body.TreeBoundingRectangle.X - size.Width,
                                                        body.TreeBoundingRectangle.Y - Settings.VerticalSpacing,
                                                        body.TreeBoundingRectangle.Width + size.Width,
                                                        body.TreeBoundingRectangle.Height + 2 * Settings.VerticalSpacing);
            return this.treeBoundingRectangle;
        }

        public override void DrawNode(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            float leftX = treeBoundingRectangle.Left + offsetX;
            float topY = treeBoundingRectangle.Top + offsetY;

            g.DrawLine(Settings.GetPen(), leftX, topY + size.Height / 2, leftX + size.Width / 2, topY + size.Height);
            g.DrawLine(Settings.GetPen(), leftX + size.Width / 2, topY + size.Height, leftX + size.Width, topY);
            g.DrawLine(Settings.GetPen(), leftX + size.Width, topY, leftX + size.Width + body.TreeBoundingRectangle.Width, topY);

            body.DrawTree(g, font, brush, offsetX, offsetY);

        }
    }
}
