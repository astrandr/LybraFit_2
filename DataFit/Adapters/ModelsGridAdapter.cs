//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Windows.Forms;
using UIBase;
using LybraFit.Common;

namespace LybraFit
{
    public class ModelsGridAdapter : Adapter
    {
        private Document doc = null;

        public ModelsGridAdapter(Control control, ViewController controller): base(control, controller)
        {
        }


        private void NewDataFile(Document doc)
        {
            DataGridView grid = (DataGridView)this.control;
            Program.IsFromUser = false;
            this.doc = doc;
            Program.IsFromUser = true;
        }

        private void NewFitData(Document doc)
        {
            DataGridView grid = (DataGridView)this.control;
            if (grid.CurrentRow != null && doc.Models.Count > 0)
            {
                grid.CurrentRow.Cells["ModelFitStdError"].Value = doc.Models[grid.CurrentRow.Cells[0].Value.ToString()].FitResultStdError.ToString();
            }

            this.doc = doc;
        }

        private void UpdateModels(Document doc)
        {
            Program.IsFromUser = false;
            string selectedModel = String.Empty;
            DataGridView grid = (DataGridView)this.control;
            grid.SuspendLayout();
            if (grid.CurrentRow != null)
            {
                selectedModel = grid.CurrentRow.Cells["ModelName"].Value.ToString();
            }

            grid.Rows.Clear();

            foreach(ModelFitInfo modelInfo in doc.Models.Values)
            {
                grid.Rows.Add(modelInfo.ModelName, modelInfo.FitResultStdError, modelInfo.ModelFunction.Description);
            }
            grid.Sort(new FloatNumbersRowComparer());
            grid.ClearSelection();
            grid.Refresh();
            Program.IsFromUser = true;

            //Set current model
            if (grid.Rows.Count > 0)
            {
                grid.Rows[0].Selected = true;
            }
            grid.ResumeLayout();
        }


        protected override void SynchronizeWithDocument(UpdateMessage update)
        {
            switch (update.Type)
            {
                case UpdateMessages.NEW_DATA: NewDataFile((Document)update.Content); break;
                case UpdateMessages.NEW_FIT: NewFitData((Document)update.Content); break;
                case UpdateMessages.MODELS_CHANGE: UpdateModels((Document)update.Content); break;
            }
        }
    }
}
