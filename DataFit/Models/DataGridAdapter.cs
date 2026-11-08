//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;
using UIBase;
using System.Drawing;

namespace DataFit
{
    class DataGridAdapter : Adapter
    {
        private Document doc = null;

        public DataGridAdapter(Control control, ViewController controller) : base(control, controller) { }

        private void NewDataFile(Document doc)
        {
            DataGridView grid = (DataGridView)this.control;
            Program.IsFromUser = false;
            grid.CancelEdit();
            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.ColumnCount = doc.ColumnCount;

            UpdateSettings(doc);
            Program.IsFromUser = true;
            for (int i = 0; i < doc.Data.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int rowIndex = grid.Rows.Add(doc.Data[i][0], doc.Data[i][1], doc.Data[i][2], doc.Data[i][3], doc.Data[i][4]);
            }

            grid.Refresh();

            this.doc = doc;
        }

        private void NewDataFiltered(Document doc)
        {
            DataGridView grid = (DataGridView)this.control;

            grid.SuspendLayout();

            Program.IsFromUser = false;

            int currentRowIndex = -1, currentColumnIndex = -1;
            if( grid.CurrentCell != null)
            {
                currentRowIndex = grid.CurrentCell.RowIndex;
                currentColumnIndex = grid.CurrentCell.ColumnIndex;
            }

            grid.Rows.Clear();
            grid.Columns.Clear();
            grid.ColumnCount = doc.ColumnCount;

            UpdateSettings(doc);
            Program.IsFromUser = true;
            for (int i = 0; i < doc.Data.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                int rowIndex = grid.Rows.Add(doc.Data[i][0], doc.Data[i][1], doc.Data[i][2], doc.Data[i][3], doc.Data[i][4]);
            }

            grid.Refresh();

            if (currentRowIndex != -1 && currentColumnIndex != -1 && currentRowIndex < grid.Rows.Count && currentColumnIndex < grid.Columns.Count)
            {
                grid.CurrentCell = grid.Rows[currentRowIndex].Cells[currentColumnIndex];
            }

            grid.ResumeLayout();

            this.doc = doc;
        }

        private void NewFitData(Document doc)
        {
            DataGridView grid = (DataGridView)this.control;
            Program.IsFromUser = false;
            if (doc.CurrentModel != null)
            {
                doc.GridColumnSettings[3].Visible = true;
                doc.GridColumnSettings[4].Visible = true;

                for (int i = 0; i < doc.Data.Count; i++)
                {
                    grid.Rows[i].Cells[3].Value = doc.Data[i][3];
                    grid.Rows[i].Cells[4].Value = doc.Data[i][4];
                }
            }
            else
            {
                doc.GridColumnSettings[3].Visible = false;
                doc.GridColumnSettings[4].Visible = false;
            }
            UpdateSettings(doc);
            grid.Refresh();
            this.doc = doc;
            Program.IsFromUser = true;
        }

        private void UpdateSettings(Document doc)
        {
            DataGridView grid = (DataGridView)this.control;
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                grid.Columns[i].Name = doc.GridColumnSettings[i].Name;
                grid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                grid.Columns[i].HeaderText = doc.GridColumnSettings[i].Name;

                string format = doc.GridColumnSettings[i].Format;
                grid.Columns[i].ValueType = typeof(double);
                if (format == "No Formatting")
                    grid.Columns[i].DefaultCellStyle.Format = "";
                else
                    if (format == "Numeric")
                        grid.Columns[i].DefaultCellStyle.Format = "N" + doc.GridColumnSettings[i].Digits.ToString();
                    else
                        if (format == "Scientific")
                            grid.Columns[i].DefaultCellStyle.Format = "E" + doc.GridColumnSettings[i].Digits.ToString();
                        else
                            if (format == "Custom")
                                grid.Columns[i].DefaultCellStyle.Format = doc.GridColumnSettings[i].CustomFormat;

                grid.Columns[i].ValueType = typeof(double);

                grid.Columns[i].AutoSizeMode = doc.GridColumnSettings[i].AutoWidth ? DataGridViewAutoSizeColumnMode.DisplayedCells : DataGridViewAutoSizeColumnMode.None;

                grid.Columns[i].Visible = doc.GridColumnSettings[i].Visible;

                grid.Columns[i].ReadOnly = doc.GridColumnSettings[i].ReadOnly;

                grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (grid.Columns[i].ReadOnly)
                {
                    grid.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }

                grid.Columns[i].DefaultCellStyle.NullValue = 0;
            }

            grid.Refresh();
        }

        public void InsertRow(int index)
        {
            DataGridView grid = (DataGridView)control;
            if (grid.CurrentCell != null)
            {
                grid.Rows.Insert(index, new DataGridViewRow());
                grid.CurrentCell = grid.Rows[index].Cells[0];
            }
        }

        protected override void SynchronizeWithDocument(UpdateMessage update)
        {

            switch (update.Type)
            {
                case UpdateMessages.NEW_DATA: NewDataFile((Document)update.Content); break;
                case UpdateMessages.DATA_FILTERED:
                case UpdateMessages.DATA_CHANGED: NewDataFiltered((Document)update.Content); break;
                case UpdateMessages.NEW_FIT:
                case UpdateMessages.MODELS_CHANGE: NewFitData( (Document)update.Content); break;
                case UpdateMessages.COLUMN_SETTINGS_CHANGED: UpdateSettings( (Document)update.Content); break;
                case UpdateMessages.DATA_INSERT: InsertRow((int)update.Content); break;
            }
        }
    }
}
