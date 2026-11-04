//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
{
    public class UndoInsertCommand : Command
    {
        private int[] indexes;

        public int[] DataRowIndexes
        {
            get { return indexes; }
        }

        public UndoInsertCommand(Document doc, int[] indexes)  : base("Undo Paste Insert", doc)
        {
            this.indexes = indexes;
        }

        public override void Execute()
        {
            for (int index = indexes.Length - 1; index >= 0; index--)
            {
                document.Data.RemoveAt(indexes[index]);
            }
        }
    }
}
