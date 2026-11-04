//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement

namespace LybraFit.Common
{
    public struct DeletedRow
    {
        public int index;
        public float[] data;
    }

    public class UndoDeleteCommand : Command
    {
        private DeletedRow[] rows;

        public UndoDeleteCommand(Document doc, DeletedRow[] rows)
            : base("Undo Delete", doc)
        {
            this.rows = rows;
        }

        public override void Execute()
        {
            for (int i = 0; i < rows.Length; i++)
            {
                document.Data.Insert(rows[i].index, rows[i].data);
            }
        }
    }
}
