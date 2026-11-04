using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Common;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.WindowsAPI;

namespace Test.Tables
{
    public class DataTable : BaseTest
    {
        private Table table = null;
        private string automationIdentifier = "";

        public int RowsCount { get { return table.Rows.Count; } }

        public DataTable(Window window, string automationIdentifier = "dataGridView") : base(window)
        {
            this.automationIdentifier = automationIdentifier;
            table = window.Get<Table>(automationIdentifier);
        }

        public DataTable SetValue(string value, bool delay = true)
        {
            window.Keyboard.Enter(value);
            table.KeyIn(KeyboardInput.SpecialKeys.RETURN);
            if (delay)
            {
                Delay();
            }

            return this;
        }

        public DataTable SetValue(int row, int col, string value, bool delay = true)
        {
            FocusCell(row, col, delay);
            window.Keyboard.Enter(value);
            table.KeyIn(KeyboardInput.SpecialKeys.RETURN);
            if (delay) Delay();
            return this;
        }

        public DataTable FocusCell(int row, int col, bool delay = true)
        {
            table.Rows[row].Cells[col].Click();
            if (delay) { Delay(); }
            return this;
        }

        public DataTable SelectRow(int row, bool delay = true)
        {
            try
            {
                table.Focus();
                table.Rows[row].Select();
            }
            catch (System.Windows.Automation.ElementNotAvailableException ex)
            {
                table.Focus();
                table.Rows[row].Select();
            }

            if (delay) { Delay(); }
            return this;
        }

        public string Cell(int row, int col)
        {
            return table.Rows[row].Cells[col].Value.ToString();
        }

        public string ColumnName(int col)
        {
            TableHeader header = table.Header;
            if (col > header.Columns.Count-1)
            {
                throw new System.IndexOutOfRangeException("Column index out of range, column count is:" + header.Columns.Count);
            }
            return header.Columns[col].Name.ToString();
        }
    }
}
