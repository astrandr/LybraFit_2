using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Test.Common;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.TabItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;

namespace Test.Dialogs
{
    public class ExpressionEditorDialog : BaseTest
    {

        private TextBox txtName = null;
        private TextBox txtExpression = null;
        private Button btnOk = null;
        private Button btnCancel = null;

        protected ExpressionEditorDialog(Window window) : base(window)
        {
            this.txtName = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtName"));
            this.txtExpression = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtExpression"));
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOk"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));
        }

        public static ExpressionEditorDialog FromMainWindow(Window mainWindow)
        {
            Window dialog = null;
            try
            {
                var windows = mainWindow.ModalWindows();
                if (mainWindow.ModalWindows().Any())
                {
                    dialog = windows.Where(window => window.Title == "Expression Editor").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                if (dialog == null)
                {
                    throw new ControlNotFoundException("Expression Editor Dialog");
                }
                else
                {
                    throw;
                }
            }

            Assert.IsNotNull(dialog, "Cannot find math expression dialog window.");

            return new ExpressionEditorDialog(dialog);
        }

        public ExpressionEditorDialog SetName(string name)
        {
            this.txtName.SetValue(name);
            return this;
        }

        public ExpressionEditorDialog IfNameIs(string name)
        {
            Assert.AreEqual(name, txtName.Text, "Math expression dialog, name is not as expected");
            return this;
        }

        public ExpressionEditorDialog SetExpression(string expression)
        {
            this.txtExpression.SetValue(expression);
            return this;
        }

        public ExpressionEditorDialog IfExpressionIs(string expression)
        {
            Assert.AreEqual(expression, txtExpression.Text, "Math expression dialog, expression is not as expected");
            return this;
        }

        public ExpressionEditorDialog ClickOk()
        {
            btnOk.Click();
            return this;
        }

        public ExpressionEditorDialog ClickCancel()
        {
            btnCancel.Click();
            return this;
        }

        public ExpressionEditorDialog Delay()
        {
            base.Delay();
            return this;
        }

    }
}
