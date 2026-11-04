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
using TestStack.White.UIItems.ListBoxItems;

namespace Test.Dialogs
{
    public class TableSettingsDialog : BaseTest
    {

        private TextBox txtName = null;
        private ListBox columnsList = null;
        private TextBox txtCustomFormat = null;
        private Button btnOk = null;
        private Button btnCancel = null;
        private ComboBox cboFormat = null;
        private Spinner nmrDigits = null;

        public Spinner NmrDigits
        {
            get { return nmrDigits; }
            set { nmrDigits = value; }
        }
        private CheckBox chkVisible = null;

        public ListBox ColumnsList
        {
            get { return columnsList; }
            set { columnsList = value; }
        }

        public TextBox TxtCustomFormat
        {
            get { return txtCustomFormat; }
            set { txtCustomFormat = value; }
        }

        public TextBox TxtName
        {
            get { return txtName; }
            set { txtName = value; }
        }
        private CheckBox chkAutoSort = null;

        protected TableSettingsDialog(Window window) : base(window)
        {
            this.txtCustomFormat = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtCustomFormat"));
            this.txtName = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtName"));
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOk"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));
            this.columnsList = window.Get<ListBox>(SearchCriteria.ByAutomationId("listColumns"));
            this.cboFormat = window.Get<ComboBox>(SearchCriteria.ByAutomationId("cboFormat"));
            this.nmrDigits = window.Get<Spinner>(SearchCriteria.ByAutomationId("nmrDigits"));
            this.chkVisible = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkVisible"));
            this.chkAutoSort = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkAutoSort"));
        }

        public static TableSettingsDialog FromMainWindow(Window mainWindow)
        {
            Window tableSettingsDialogWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Table Columns Settings"));
            return new TableSettingsDialog(tableSettingsDialogWindow);
        }

        public static TableSettingsDialog FromMainMenu(Window mainWindow, string fileName = "", string path = "")
        {
            Window dialog = null;
            try
            {
                MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
                Menu columnsSettingsMenuItem = menu.MenuItem("Settings", "Table");
                columnsSettingsMenuItem.Click();

            }
            catch (Exception e)
            {
                Assert.Fail("Cannot open columns settings dialog\r\n" + e.Message);
            }

            dialog = mainWindow.ModalWindow(SearchCriteria.ByText("Table Columns Settings")); //TODO set this as string constants in test
            if (dialog == null)
            {
                Assert.Fail("Could not find Table Columns Settings dialog");
            }

            return new TableSettingsDialog(dialog);
        }

        public TableSettingsDialog SelectItem(string columnName)
        {
            columnsList.Items.Where(item => item.Text == columnName).First().Select();
            return this;
        }

        public TableSettingsDialog SelectFormat(int formatIndex)
        {
            cboFormat.Select(formatIndex);
            return this;
        }

        public TableSettingsDialog SetDigits(int digits)
        {
            nmrDigits.Value = digits;
            return this;
        }

        public TableSettingsDialog ClickOk()
        {
            btnOk.Click();
            Delay();
            return this;
        }

        public TableSettingsDialog ClickCancel()
        {
            btnCancel.Click();
            Delay();
            return this;
        }

        public TableSettingsDialog SetName(string name)
        {
            txtName.Text = name;
            return this;
        }

        public TableSettingsDialog IfNameIs(string name)
        {
            Assert.AreEqual(name, txtName.Text, "Column name not as expected");
            return this;
        }

        public TableSettingsDialog ShowColumn()
        {
            chkVisible.Checked = true;
            return this;
        }

        public TableSettingsDialog HideColumn()
        {
            chkVisible.Checked = false;
            return this;
        }

        public TableSettingsDialog SelectAutoSort()
        {
            if(!this.chkAutoSort.Checked)
            {
                this.chkAutoSort.Click();
            }

            return this;
        }

        public TableSettingsDialog UnSelectAutoSort()
        {
            if (this.chkAutoSort.Checked)
            {
                this.chkAutoSort.Click();
            }

            return this;
        }

    }
}
