//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System.Collections.Generic;

namespace LybraFit.Common
{
    public class UndoTransformCommand : Command
    {
        private List<double[]> oldData = new List<double[]>();

        public UndoTransformCommand(Document doc)
            : base("Undo Transform", doc)
        {
            doc.Data.ForEach(item => oldData.Add(new double[]{item[0], item[1], item[2], item[3], item[4]}));
        }

        public override void Execute()
        {
            document.Data = oldData;
        }
    }
}
