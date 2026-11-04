//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFit.Common
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
