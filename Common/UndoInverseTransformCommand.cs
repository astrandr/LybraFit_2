//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
{
    class UndoInverseTransformCommand : Command
    {

        private List<float[]> oldData = new List<float[]>();

        public UndoInverseTransformCommand(Document doc): base("Undo Inverse Transform", doc)
        {
            doc.Data.ForEach(item => oldData.Add(new float[] { item[0], item[1], item[2], item[3], item[4] }));
        }

        public override void Execute()
        {
            string firstColumnName = document.ColumnNames[0];
            document.ColumnNames[0] = document.ColumnNames[1];
            document.ColumnNames[1] = firstColumnName;
            document.Data = oldData;
        }
    }
}
