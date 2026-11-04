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
    public class TransformDialog : BaseTest
    {
        private TextBox txtTransformExpression = null;
        private ComboBox cboColumns = null;
        private Button btnOk = null;
        private Button btnCancel = null;

        public ComboBox ComboColumns
        {
            get { return cboColumns; }
            set { cboColumns = value; }
        }
        public TextBox TransformExpressionBox
        {
            get { return txtTransformExpression; }
            set { txtTransformExpression = value; }
        }


        protected TransformDialog(Window window): base(window)
        {
            this.txtTransformExpression = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtTransformExpression"));
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOK"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));
            this.cboColumns = window.Get<ComboBox>(SearchCriteria.ByAutomationId("cboColumns"));
        }

        public static TransformDialog FromMainWindow(Window mainWindow)
        {
            Window importDialogWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Transform"));
            return new TransformDialog(importDialogWindow);
        }

        public static TransformDialog FromMainMenu(Window mainWindow, string fileName = "", string path = "")
        {
            Window dialog = null;
            try
            {
                MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
                Menu filterDataMenuItem = menu.MenuItem("Data", "Transform");
                filterDataMenuItem.Click();
            }
            catch (Exception e)
            {
                Assert.Fail("Cannot open transform dialog\r\n" + e.Message);
            }

            dialog = mainWindow.ModalWindow(SearchCriteria.ByText("Transform")); //TODO set this as string constants in test
            if (dialog == null)
            {
                Assert.Fail("Could not find transform dialog");
            }

            return new TransformDialog(dialog);
        }

        public TransformDialog ClickOk()
        {
            btnOk.Click();
            Delay();
            return this;
        }

        public TransformDialog ClickCancel()
        {
            btnCancel.Click();
            Delay();
            return this;
        }

        public TransformDialog SelectColumn(string columnName)
        {
            cboColumns.Items.Where(item => item.Text == columnName).First().Select();
            return this;
        }

        public TransformDialog IfColumnsAre(string[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                Assert.AreEqual(columnNames[i], cboColumns.Items[i].Text, "Column name {i} is wrong");
            }

            return this;
        }

        public TransformDialog SetMathExpression(string mathExpression)
        {
            txtTransformExpression.SetValue(mathExpression);
            return this;
        }

        public new TransformDialog VerifyCentered(Window parent)
        {
            base.VerifyCentered(parent);
            return this;
        }

    }
}
