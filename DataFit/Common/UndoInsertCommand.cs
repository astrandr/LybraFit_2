//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

namespace LybraFit.Common
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
