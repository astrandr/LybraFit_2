//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

using System.Drawing;

namespace MathLib
{
    public class AdditionNode : BinaryOperationNode
    {
        public AdditionNode()
        {
            this.precedencePos = 1;
            this.symbol = "+";
            this.alignment = BinaryAlignment.Horizontal;
        }

        public override void DrawNode(Graphics g, Font font, Brush brush, float offsetX, float offsetY)
        {
            g.DrawString(this.symbol, font, brush, boundingRectangle.X + offsetX, boundingRectangle.Y + offsetY + 2);
        }

    }
}
