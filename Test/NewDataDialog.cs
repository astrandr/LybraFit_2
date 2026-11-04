using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Common;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.WindowsAPI;

namespace Test.Dialogs
{
    public class NewDataDialog : BaseTest
    {
        protected TextBox txtTitle = null;
        protected TextBox txtCol1Name = null;
        protected TextBox txtDataXMin = null;
        protected TextBox txtDataXMax = null;
        protected TextBox txtDataXIncrement = null;
        protected TextBox txtCol2Name = null;
        protected TextBox txtExpression = null;
        protected TextBox txtErrDataName = null;
        protected CheckBox chkMainDataXAuto = null;
        protected CheckBox chkMainDataYCalculated = null;
        protected CheckBox chkErrData = null;
        protected CheckBox chkErrDataCalculated = null;
        protected Button btnOk = null;
        protected Button btnCancel = null;

        public TextBox Title
        {
            get { return this.txtTitle; }
        }

        public TextBox Col1Name
        {
            get { return this.txtCol1Name; }
        }

        public TextBox DataXMin
        {
            get { return this.txtDataXMin; }
        }

        public TextBox DataXMax
        {
            get { return this.txtDataXMax; }
        }

        public TextBox DataXIncrement
        {
            get { return this.txtDataXIncrement; }
        }

        public TextBox Col2Name
        {
            get { return this.txtCol2Name; }
        }

        public TextBox Expression
        {
            get { return this.txtExpression; }
        }

        public TextBox ErrDataName
        {
            get { return this.txtErrDataName; }
        }

        protected NewDataDialog(Window window) : base (window)
        {
            this.txtTitle = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtTitle"));
            this.txtCol1Name = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtCol1Name"));
            this.txtDataXMin = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtDataXMin"));
            this.txtDataXMax = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtDataXMax"));
            this.txtDataXIncrement = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtDataXIncrement"));
            this.txtCol2Name = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtCol2Name"));
            this.txtExpression = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtExpression"));
            this.txtErrDataName = window.Get<TextBox>(SearchCriteria.ByAutomationId("txtErrDataName"));
            this.chkMainDataXAuto = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkMainDataXAuto"));
            this.chkMainDataYCalculated = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkMainDataYCalculated"));
            this.chkErrData = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkErrData"));
            this.chkErrDataCalculated = window.Get<CheckBox>(SearchCriteria.ByAutomationId("chkErrDataCalculated"));
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOk"));
            this.btnCancel = window.Get<Button>(SearchCriteria.ByAutomationId("btnCancel"));
        }

        public static NewDataDialog FromMainMenu(Window mainWindow)
        {
            Window dialog = null;
            try
            {
                MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
                Menu newData = menu.MenuItem("Data", "New");
                newData.Click();
                dialog = mainWindow.ModalWindow(SearchCriteria.ByText("New Data"));
                dialog = mainWindow.ModalWindow(SearchCriteria.ByText("New Data"), InitializeOption.WithCache.AndIdentifiedBy("New Data"));

            }
            catch (Exception)
            {
                if (dialog == null)
                {
                    throw new ControlNotFoundException("New Data Dialog");
                }
                else
                {
                    throw;
                }
            }

            return new NewDataDialog(dialog);
        }

        public NewDataDialog ClickOk()
        {
            this.btnOk.Click(); Delay();
            return this;
        }

        public NewDataDialog ClickCancel()
        {
            this.btnCancel.Click(); Delay();
            return this;
        }

        public NewDataDialog ClickAutoIncrement()
        {
            chkMainDataXAuto.Click(); Delay();
            return this;
        }

        public NewDataDialog ClickAutoCalculateY()
        {
            chkMainDataYCalculated.Click(); Delay();
            return this;
        }

        public NewDataDialog ClickCheckErrorData()
        {
            chkErrData.Click(); Delay();
            return this;
        }

        public NewDataDialog ClickCheckErrorDataCalculated()
        {
            chkErrDataCalculated.Checked = true; 
            Delay();
            return this;
        }

        public NewDataDialog SetTitle(string value)
        {
            this.txtTitle.Focus();
            this.txtTitle.Text = value; Delay();
            return this;
        }

        public NewDataDialog SetMinX(string value)
        {
            this.txtDataXMin.Focus();
            this.txtDataXMin.Text = value; Delay();
            return this;
        }

        public NewDataDialog SetMaxX(string value)
        {
            this.txtDataXMax.Focus();
            this.txtDataXMax.Text = value; Delay();
            return this;
        }

        public NewDataDialog SetDataXIncrement(string value)
        {
            this.txtDataXIncrement.Focus();
            this.txtDataXIncrement.Text = value; Delay();
            return this;
        }

        public NewDataDialog SetCol1Name(string value)
        {
            this.txtCol1Name.Focus();
            this.txtCol1Name.Text = value; Delay();
            return this;
        }

        public NewDataDialog SetCol2Name(string value)
        {
            this.txtCol2Name.Focus();
            this.txtCol2Name.Text = value; Delay();
            return this;
        }

        public NewDataDialog SetExpression(string value)
        {
            this.txtExpression.Focus();
            this.txtExpression.Text = value;
            return this;
        }

        public NewDataDialog ClearExpression()
        {
            string content = txtExpression.Text;
            txtExpression.Click(); txtExpression.KeyIn(KeyboardInput.SpecialKeys.END);
            while(txtExpression.Text.Length > 0 ) txtExpression.KeyIn(KeyboardInput.SpecialKeys.BACKSPACE);
            return this;
        }

        public NewDataDialog SetErrDataName(string value)
        {
            this.txtErrDataName.Focus();
            this.txtErrDataName.Text = value; Delay();
            return this;
        }

        public NewDataDialog KeyIn(KeyboardInput.SpecialKeys key)
        {
            window.KeyIn(key); Delay();
            return this;
        }

    }
}
