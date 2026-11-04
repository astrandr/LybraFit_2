using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Drawing;
using Test.Common;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.Factory;

namespace Test.Dialogs
{
    public class ModelsDialog : BaseTest
    {
        private TextBox txtFilter = null;

        public TextBox TxtFilter
        {
            get { return txtFilter; }
        }
        private Button btnOk = null;

        public Button BtnOk
        {
            get { return btnOk; }
        }
        private Button btnCancel = null;

        public Button BtnCancel
        {
            get { return btnCancel; }
        }
        private Button btnNew = null;

        public Button BtnNew
        {
            get { return btnNew; }
        }
        private Button btnEdit = null;

        public Button BtnEdit
        {
            get { return btnEdit; }
        }
        private Button btnRemove = null;

        public Button BtnRemove
        {
            get { return btnRemove; }
        }

        protected Table libFuncGrid = null;
        protected Table customFuncGrid = null;
        protected TabPage tabCustomModels = null;
        protected TabPage tabLibraryModels = null;
        protected Panel pictureBox = null;


        protected ModelsDialog(Window window) : base(window)
        {
            this.txtFilter = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtModelFilter"));
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOk"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));

            tabCustomModels = window.Get<TabPage>(SearchCriteria.ByText("Custom"));
            Assert.IsNotNull(tabCustomModels, "Cannot find tab page for custom models.");
            tabLibraryModels = window.Get<TabPage>(SearchCriteria.ByText("Library"));
            Assert.IsNotNull(tabLibraryModels, "Cannot find tab page for library models.");


            //Find the library functions tab and grid
            tabLibraryModels.Select();
            libFuncGrid = window.Get<Table>("dataGridFitFunctions");
            Assert.IsNotNull(libFuncGrid, "Cannot find the UI element Library functions grid");

            //Find custom functions tab controls
            tabCustomModels.Select();
            customFuncGrid = window.Get<Table>(SearchCriteria.ByAutomationId("dataGridCustomFunctions"));
            Assert.IsNotNull(customFuncGrid, "Cannot find the UI element user functions grid");

            this.btnNew = window.Get<Button>(SearchCriteria.ByText("New"));
            Assert.IsNotNull(btnNew, "Cannot find New function button.");
            this.btnRemove = window.Get<Button>(SearchCriteria.ByAutomationId("btnRemoveUserFunction"));
            Assert.IsNotNull(btnRemove, "Cannot find Remove function button.");
            this.btnEdit = window.Get<Button>(SearchCriteria.ByAutomationId("btnEditUserFunction"));
            Assert.IsNotNull(btnEdit, "Cannot find Edit function button.");

            pictureBox = window.Get<Panel>(SearchCriteria.ByAutomationId("pictFunctionPreview"));
        }

        public static ModelsDialog FromMainMenu(Window mainWindow)
        {
            Window dialog = null;
            try
            {
                MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
                Menu modelsMenu = menu.MenuItem("Analysis", "Add Models");
                modelsMenu.Click();
                dialog = mainWindow.ModalWindow(SearchCriteria.ByText("Models"));
                dialog = mainWindow.ModalWindow(SearchCriteria.ByText("Models"), InitializeOption.NoCache.AndIdentifiedBy("Models"));
            }
            catch (Exception)
            {
                if (dialog == null)
                {
                    throw new ControlNotFoundException("Models Dialog");
                }
                else
                {
                    throw;
                }
            }

            return new ModelsDialog(dialog);
        }

        public static ModelsDialog FromMainWindow(Window mainWindow)
        {
            Window dialog = null;
            try
            {
                var windows = mainWindow.ModalWindows();
                if (mainWindow.ModalWindows().Any())
                {
                    dialog = windows.Where(window => window.Title == "Models").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                if (dialog == null)
                {
                    throw new ControlNotFoundException("Models");
                }
                else
                {
                    throw;
                }
            }

            Assert.IsNotNull(dialog, "Cannot find math expression dialog window.");

            return new ModelsDialog(dialog);
        }


        public ModelsDialog ClickOk()
        {
            this.btnOk.Click(); Delay();
            return this;
        }

        public ModelsDialog ClickCancel()
        {
            this.btnCancel.Click(); Delay();
            return this;
        }

        public ModelsDialog SetFilter(string value)
        {
            this.txtFilter.Enter(value); Delay();
            return this;
        }

        public ModelsDialog ClearFilter(string value)
        {
            this.txtFilter.Enter(value); Delay();
            return this;
        }

        public string[] GetFilteredLibFunctionNames()
        {
            var rows = libFuncGrid.Rows;
            return rows.Select(row => row.Cells["Name"].Value.ToString()).ToArray<string>();
        }

        public ModelsDialog CheckLibFunction(string functionName)
        {
            libFuncGrid.Rows.Where(row => row.Cells["Name"].Value.ToString() == functionName).First().Cells[0].Value = true;
            return this;
        }

        public ModelsDialog UnCheckLibFunction(string functionName)
        {
            libFuncGrid.Rows.Where(row => row.Cells["Name"].Value.ToString() == functionName).First().Cells[0].Value = true;
            return this;
        }

        public ModelsDialog SetCustomModelsPage()
        {
            tabCustomModels.Select();
            return this;
        }

        public ModelsDialog SetLibraryModelsPage()
        {
            tabLibraryModels.Select();
            return this;
        }

        public string[] GetFilteredCustomFunctionNames()
        {
            var rows = customFuncGrid.Rows;
            return rows.Select(row => row.Cells["Name"].Value.ToString()).ToArray<string>();
        }

        public string GetFilteredCustomFunctionExpression(string name)
        {
            var rows = customFuncGrid.Rows;
            return rows.Where(row => row.Cells["Name"].Value.ToString() == name).Select(row =>row.Cells["Math Expression"].Value.ToString()).First();
        }

        public ModelsDialog SelectFunction(string name)
        {
            var rows = customFuncGrid.Rows;
            rows.Where(row => row.Cells["Name"].Value.ToString() == name).First().Select();
            return this;
        }
            

        public ModelsDialog ClickNew()
        {
            this.btnNew.Click();
            return this;
        }

        public ModelsDialog ClickRemove()
        {
            this.btnRemove.Click();
            return this;
        }

        public ModelsDialog Delay()
        {
            base.Delay();
            return this;
        }

        public ModelsDialog GetExpressionImage(ref Bitmap image)
        {
            image = new Bitmap(pictureBox.VisibleImage);
            return this;
        }
    }
}
