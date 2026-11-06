using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Test.Common;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;

namespace Test.Dialogs
{
    public class InformationDialog : BaseTest
    {
        private Button btnClose = null;

        public Button BtnClose
        {
            get { return btnClose; }
        }

        protected InformationDialog(Window window) : base(window)
        {
            this.btnClose = window.Get<Button>(SearchCriteria.ByText("Close"));
        }

        public static InformationDialog FromMainWindow(Window mainWindow)
        {
            Window dialog = null;
            try
            {
                var windows = mainWindow.ModalWindows();
                if (mainWindow.ModalWindows().Any())
                {
                    dialog = windows.Where(window => window.Title == "Information").FirstOrDefault();
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

            return new InformationDialog(dialog);
        }


        public InformationDialog ClickClose()
        {
            this.btnClose.Click();
            return this;
        }

        public InformationDialog Delay()
        {
            base.Delay();
            return this;
        }
    }
}
