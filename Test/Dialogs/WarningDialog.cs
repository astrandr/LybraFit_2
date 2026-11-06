using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Test.Common;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace Test.Dialogs
{
    public class WarningDialog : BaseTest
    {
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


        protected WarningDialog(Window window) : base(window)
        {
            this.btnOk = window.Get<Button>(SearchCriteria.ByAutomationId("btnOk"));
        }

        public static WarningDialog FromMainWindow(Window mainWindow)
        {
            Window dialog = null;
            try
            {
                var windows = mainWindow.ModalWindows();
                if (mainWindow.ModalWindows().Any())
                {
                    dialog = windows.Where(window => window.Title == "Warning").FirstOrDefault();
                }
            }
            catch (Exception)
            {
                if (dialog == null)
                {
                    throw new ControlNotFoundException("Warning");
                }
                else
                {
                    throw;
                }
            }

            Assert.IsNotNull(dialog, "Cannot find the warning dialog.");

            return new WarningDialog(dialog);
        }


        public WarningDialog ClickOk()
        {
            this.btnOk.Click(); Delay();
            return this;
        }

        public WarningDialog Delay()
        {
            base.Delay();
            return this;
        }
    }
}
