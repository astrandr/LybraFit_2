//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataFit.Common
{
    public class UndoTransformCommand : Command
    {
        private List<float[]> oldData = new List<float[]>();

        public UndoTransformCommand(Document doc)
            : base("Undo Transform", doc)
        {
            doc.Data.ForEach(item => oldData.Add(new float[]{item[0], item[1], item[2], item[3], item[4]}));
        }

        public override void Execute()
        {
            document.Data = oldData;
        }
    }
}
