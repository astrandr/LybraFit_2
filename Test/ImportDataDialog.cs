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
    public class ImportDataDialog : BaseTest
    {
        private TextBox txtOriginal = null;
        private TextBox txtTitle = null;

        public TextBox TxtTitle
        {
            get { return txtTitle; }
            set { txtTitle = value; }
        }

        private Button btnOk = null;
        private Button btnCancel = null;
        private Spinner numHeaderLinesCount = null;
        private Spinner num1stColumnIndex = null;
        private Spinner num2ndColumnIndex = null;

        private CheckBox chkHasErrColumn = null;
        private CheckBox chkSkipErrors = null;

        public CheckBox ChkSkipErrors
        {
            get { return chkSkipErrors; }
            set { chkSkipErrors = value; }
        }

        public CheckBox ChkHasErrColumn
        {
            get { return chkHasErrColumn; }
        }

        public Spinner Num2ndColumnIndex
        {
            get { return num2ndColumnIndex; }
        }

        private Spinner num3rdColumnIndex = null;

        public Spinner Num3rdColumnIndex
        {
            get { return num3rdColumnIndex; }
        }

        public Spinner Num1stColumnIndex
        {
            get { return num1stColumnIndex; }
        }

        public Spinner NumHeaderLinesCount
        {
            get { return numHeaderLinesCount; }
        }

        private TextBox txt1stColumnName = null;

        public TextBox Txt1stColumnName
        {
            get { return txt1stColumnName; }
        }
        private TextBox txt2ndColumnName = null;

        public TextBox Txt2ndColumnName
        {
            get { return txt2ndColumnName; }
        }
        private TextBox txt3rdColumnName = null;

        public TextBox Txt3rdColumnName
        {
            get { return txt3rdColumnName; }
        }

        public TextBox TxtOriginal
        {
            get { return txtOriginal; }
            set { txtOriginal = value; }
        }

        protected ImportDataDialog(Window window)
            : base(window)
        {
            this.txtOriginal = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtOriginal"));
            this.txtTitle = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtTitle"));

            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOk"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));

            this.numHeaderLinesCount = window.Get<Spinner>(SearchCriteria.ByAutomationId("numHeaderLinesCount"));
            this.num1stColumnIndex = window.Get<Spinner>(SearchCriteria.ByAutomationId("num1stColumnIndex"));
            this.num2ndColumnIndex = window.Get<Spinner>(SearchCriteria.ByAutomationId("num2ndColumnIndex"));
            this.num3rdColumnIndex = window.Get<Spinner>(SearchCriteria.ByAutomationId("num3rdColumnIndex"));

            this.txt1stColumnName = window.Get<TextBox>(SearchCriteria.ByAutomationId("txt1stColumnName"));
            this.txt2ndColumnName = window.Get<TextBox>(SearchCriteria.ByAutomationId("txt2ndColumnName"));
            this.txt3rdColumnName = window.Get<TextBox>(SearchCriteria.ByAutomationId("txt3rdColumnName"));

            this.chkHasErrColumn = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkHasErrColumn"));
            this.chkSkipErrors = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkSkipErrors"));
            DelayTime = 10;
        }

        public static ImportDataDialog FromMainWindow(Window mainWindow)
        {
            Window importDialogWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Import and Paste Settings"));
            return new ImportDataDialog(importDialogWindow);
        }

        public static ImportDataDialog FromMainMenu(Window mainWindow, string fileName = "", string path = "")
        {
            Window dialog = null;
            try
            {
                MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
                Menu importDataMenuItem = menu.MenuItem("File", "Import ASCII");
                importDataMenuItem.Click();

                Window openFileDialog = mainWindow.ModalWindow(SearchCriteria.ByText("Open"));//TODO set this as string constants in test
                ComboBox fileNameBox = openFileDialog.Get<ComboBox>(SearchCriteria.ByControlType(ControlType.ComboBox));
                Button openButton = openFileDialog.Get<Button>(SearchCriteria.ByControlType(ControlType.Button).AndByText("Open"));//TODO set this as string constants in test
                if (path != "")
                {
                    fileNameBox.EditableText = path;
                    openButton.Click();
                }

                fileNameBox.EditableText = fileName;
                openButton.Click();

            }
            catch (Exception e)
            {
                Assert.Fail("Cannot open file\r\n" + e.Message);
            }

            dialog = mainWindow.ModalWindow(SearchCriteria.ByText("Import and Paste Settings")); //TODO set this as string constants in test
            if (dialog == null)
            {
                Assert.Fail("Could not find Import and Paste Settings dialog");
            }

            return new ImportDataDialog(dialog);
        }

        public ImportDataDialog SetColumnName(int columnIndex, string columnName)
        {
            switch (columnIndex)
            {
                case 1: this.txt1stColumnName.Text = columnName; break;
                case 2: this.txt2ndColumnName.Text = columnName; break;
                case 3: this.txt3rdColumnName.Text = columnName; break;
            }
            Delay();
            return this;
        }

        public ImportDataDialog SetColumnIndex(int columnIndex, int dataColumnIndex)
        {
            switch (columnIndex)
            {
                case 1: this.num1stColumnIndex.Value = dataColumnIndex; break;
                case 2: this.num2ndColumnIndex.Value = dataColumnIndex; break;
                case 3: this.num3rdColumnIndex.Value = dataColumnIndex; break;
            }
            Delay();
            return this;
        }

        public ImportDataDialog DecrementColumnIndex(int columnIndex)
        {
            switch (columnIndex)
            {
                case 1: this.num1stColumnIndex.Decrement(); break;
                case 2: this.num2ndColumnIndex.Decrement(); break;
                case 3: this.num3rdColumnIndex.Decrement(); break;
            }
            Delay();
            return this;
        }

        public ImportDataDialog IncrementColumnIndex(int columnIndex)
        {
            switch (columnIndex)
            {
                case 1: this.num1stColumnIndex.Increment(); break;
                case 2: this.num2ndColumnIndex.Increment(); break;
                case 3: this.num3rdColumnIndex.Increment(); break;
            }
            Delay();
            return this;
        }


        public ImportDataDialog ClickOk()
        {
            btnOk.Click();
            Delay();
            return this;
        }

        public ImportDataDialog ClickCancel()
        {
            btnCancel.Click();
            Delay();
            return this;
        }

        public ImportDataDialog SetHeaderLinesCount(int lines)
        {
            this.numHeaderLinesCount.Focus();
            this.numHeaderLinesCount.Value = lines;
            Delay();
            return this;
        }

        public ImportDataDialog SetHasError(bool hasErrorColumn)
        {
            this.chkHasErrColumn.Checked = hasErrorColumn;
            Delay();
            return this;
        }

    }
}
