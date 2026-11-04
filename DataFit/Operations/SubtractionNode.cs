//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

namespace MathLib
{
    public class SubtractionNode : BinaryOperationNode
    {
        public SubtractionNode()
        {
            this.precedencePos = 1;
            this.alignment = BinaryAlignment.Horizontal;
            this.symbol = "-";
        }

        public override void DrawNode(System.Drawing.Graphics g, System.Drawing.Font font, System.Drawing.Brush brush, float offsetX, float offsetY)
        {
            g.DrawString("-", font, brush, boundingRectangle.X + offsetX, boundingRectangle.Y + offsetY);
        }

        public override bool IsCorrect()
        {
            return rightChild != null && rightChild.IsCorrect();
        }
    }
}
