using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Common;
using TestStack.White.UIItems;
using TestStack.White.UIA;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.WindowsAPI;
using System.Windows.Automation;
using TestStack.White.UIItems.ListBoxItems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Test.Dialogs
{
    public class FilterDialog : BaseTest
    {
        private TextBox txtMin = null;
        private TextBox txtMax = null;
        private ListBox chkColumnsList = null;
        private Button btnOk = null;
        private Button btnCancel = null;
        private Button btnClear = null;

        public ListBox ChkColumnsList
        {
            get { return chkColumnsList; }
            set { chkColumnsList = value; }
        }
        public TextBox TxtMin
        {
            get { return txtMin; }
            set { txtMin = value; }
        }        
        public TextBox TxtMax
        {
            get { return txtMax; }
            set { txtMax = value; }
        }

        protected FilterDialog(Window window) : base(window)
        {
            this.txtMin = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtMin"));
            this.txtMax = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtMax"));
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOK"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));
            this.btnClear = window.Get<Button>(SearchCriteria.ByAutomationId("btnClear"));
            this.chkColumnsList = window.Get<ListBox>(SearchCriteria.ByAutomationId("chklColumns"));
        }

        public static FilterDialog FromMainWindow(Window mainWindow)
        {
            Window importDialogWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Data Filter"));
            return new FilterDialog(importDialogWindow);
        }

        public static FilterDialog FromMainMenu(Window mainWindow, string fileName = "", string path = "")
        {
            Window dialog = null;
            try
            {
                MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
                Menu filterDataMenuItem = menu.MenuItem("Data", "Filter");
                filterDataMenuItem.Click();

            }
            catch (Exception e)
            {
                Assert.Fail("Cannot open filter dialog\r\n" + e.Message);
            }

            dialog = mainWindow.ModalWindow(SearchCriteria.ByText("Data Filter")); //TODO set this as string constants in test
            if (dialog == null)
            {
                Assert.Fail("Could not find Data Filter dialog");
            }

            return new FilterDialog(dialog);
        }

        public FilterDialog ClickOk()
        {
            btnOk.Click();
            Delay();
            return this;
        }

        public FilterDialog ClickCancel()
        {
            btnCancel.Click();
            Delay();
            return this;
        }

        public FilterDialog Clear()
        {
            btnClear.Click();
            Delay();
            return this;
        }

        public FilterDialog CheckItem(string columnName)
        {
            chkColumnsList.Check(columnName);
            return this;
        }

        public FilterDialog UnCheckItem(string columnName)
        {
            chkColumnsList.UnCheck(columnName);
            return this;
        }

        public FilterDialog SelectItem(string columnName)
        {
            chkColumnsList.Items.Where(item => item.Text == columnName).First().Select();
            return this;
        }

        public FilterDialog SetMin(string minValue)
        {
            txtMin.SetValue(minValue);
            return this;
        }

        public FilterDialog SetMax(string maxValue)
        {
            txtMax.SetValue(maxValue);
            return this;
        }

        public FilterDialog IfColumnsAre(string[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                Assert.AreEqual(columnNames[i], chkColumnsList.Items[i].Text, "Column name {i} is wrong");
            }

            return this;
        }

        public FilterDialog IfCheckedColumnsAre(string[] checkedColumnNames)
        {
            foreach (var column in checkedColumnNames)
            {
                Assert.IsTrue(chkColumnsList.Items.Any(item => item.Text == column && item.Checked), "Column { item.Text} is not checked");
            }
            return this;
        }

        public FilterDialog IfMinXIsEnabled()
        {
            Assert.IsTrue(this.txtMin.Enabled, "Text box for min must be enabled");
            return this;
        }

        public FilterDialog IfMaxXIsEnabled()
        {
            Assert.IsTrue(this.txtMax.Enabled, "Text box for min must be enabled");
            return this;
        }

        public FilterDialog IfMinXIsNotEnabled()
        {
            Assert.IsTrue(!this.txtMin.Enabled, "Text box for min shell not be enabled");
            return this;
        }

        public FilterDialog IfMaxXIsNotEnabled()
        {
            Assert.IsTrue(!this.txtMax.Enabled, "Text box for max shell not be enabled");
            return this;
        }

        public new FilterDialog VerifyCentered(Window parent)
        {
            base.VerifyCentered(parent);
            return this;
        }

    }
}
