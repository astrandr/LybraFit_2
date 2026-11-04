using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.WindowsAPI;
using Test.Dialogs;
using Test.Common;
using Test.Tables;
using System.Windows.Automation;

namespace Test
{
    [TestClass]
    public class TransformTest : BaseTest
    {
        TestStack.White.Application app = null;
        Window mainWindow = null;

        [TestInitialize]
        public void InitializeTest()
        {
            app = TestStack.White.Application.Launch(@"LybraFit.exe");
            mainWindow = app.GetWindow("LybraFit");
            BaseTest.DelayTime = 2000;
        }

        [TestCleanup]
        public void FinalizeTest()
        {
            try
            {
                mainWindow.Close();
            }
            catch
            {
            }
            finally
            {
                app.Close();
            }
        }


        /// <summary>
        /// TransformDialogMenuStateOnStart
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformDialogMenuStateOnStart()
        {
            //Verify menu Transform is disabled on start
            Assert.IsTrue(!mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Transform").Enabled, "Menu Transform must be disabled on start");
            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).Click(); //Click on the main menu to close the submenu
        }

        /// <summary>
        /// TransformDialogMenuStateOnEmptyData
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformDialogMenuStateOnEmptyData()
        {
            //Verify menu item 'Transform' is disabled on new but empty data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickOk();
            Assert.IsTrue(!mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Transform").Enabled, "Menu Transform must be disabled on empty data");
            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).Click(); //Click on the main menu to close the submenu

        }

        /// <summary>
        /// TransformDialogPosition - Opens transform dialog and verifies it is centered with respect to the parent
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformDialogPosition()
        {
            //Start new non empty data and verify menu item 'Transform' is enabled and transform dialog opens centered
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1").ClickOk();
            TransformDialog.FromMainMenu(mainWindow).VerifyCentered(mainWindow).ClickCancel();
        }

        /// <summary>
        /// TransformOnXYErr - Verifies transformation on X and Y and that no other columns are available for transformation
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformOnXYErr()
        {

            //Start new non tabulated data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                                                  .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickOk();

            //Verify transform on X column and that only X and Y columns can be transformed
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");
            TransformDialog.FromMainMenu(mainWindow).IfColumnsAre(new string[]{"X", "Y"}).SelectColumn("X").SetMathExpression("X+1").ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("2.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("3.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");

            //Verify transform on Y column
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Incorrect data in cell 0,1");
            Assert.AreEqual("3.00E+000", table.Cell(1, 1), "Incorrect data in cell 1,1");
            Assert.AreEqual("5.00E+000", table.Cell(2, 1), "Incorrect data in cell 2,1");
            TransformDialog.FromMainMenu(mainWindow).SelectColumn("Y").SetMathExpression("2*X").ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.30E+001", table.Cell(0, 1), "Incorrect data in cell 0,1");
            Assert.AreEqual("3.90E+001", table.Cell(1, 1), "Incorrect data in cell 1,1");
            Assert.AreEqual("6.50E+001", table.Cell(2, 1), "Incorrect data in cell 2,1");
        }

        /// <summary>
        /// TransformOnXYErrUndo - Verifies transformation on X and Y and undo command restores the data before transform
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformOnXYErrUndo()
        {

            //Start new non tabulated data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                                                  .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickOk();

            //Verify transform on X column and that only X and Y columns can be transformed
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");
            TransformDialog.FromMainMenu(mainWindow).IfColumnsAre(new string[] { "X", "Y" }).SelectColumn("X").SetMathExpression("X+1").ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("2.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("3.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");

            //Verify transform on Y column
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Incorrect data in cell 0,1");
            Assert.AreEqual("3.00E+000", table.Cell(1, 1), "Incorrect data in cell 1,1");
            Assert.AreEqual("5.00E+000", table.Cell(2, 1), "Incorrect data in cell 2,1");
            TransformDialog.FromMainMenu(mainWindow).SelectColumn("Y").SetMathExpression("2*X").ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.30E+001", table.Cell(0, 1), "Incorrect data in cell 0,1");
            Assert.AreEqual("3.90E+001", table.Cell(1, 1), "Incorrect data in cell 1,1");
            Assert.AreEqual("6.50E+001", table.Cell(2, 1), "Incorrect data in cell 2,1");

            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Undo").Click();
            table = new DataTable(mainWindow);
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Incorrect data in cell 0,1");
            Assert.AreEqual("3.00E+000", table.Cell(1, 1), "Incorrect data in cell 1,1");
            Assert.AreEqual("5.00E+000", table.Cell(2, 1), "Incorrect data in cell 2,1");

            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Undo").Click();
            table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");

        }

        /// <summary>
        /// TransformDialogWrongExpression - Opens transform dialog and verifies it cannot be closed with Ok on wrong data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformDialogWrongExpression()
        {
            //Start new non empty data and verify menu item 'Transform' is enabled and transform dialog opens centered
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1").ClickOk();
            TransformDialog transformDialog = TransformDialog.FromMainMenu(mainWindow).SetMathExpression("").ClickOk();
            Assert.IsTrue(!transformDialog.GetWindow().IsClosed, "Transform dialog shell not be closed with Ok on empty math expression");
            Assert.IsTrue(transformDialog.TransformExpressionBox.IsFocussed, "On empty math expression the focus is to be on the math expression text box");
            transformDialog.SetMathExpression("2*x3-3;3").ClickOk();
            Assert.IsTrue(!transformDialog.GetWindow().IsClosed, "Transform dialog shell not be closed with Ok on wrong math expression");
            Assert.IsTrue(transformDialog.TransformExpressionBox.IsFocussed, "On empty math expression the focus is to be on the math expression text box");

            //Verify it can be closed on wrong math expression
            transformDialog.ClickCancel();
            Assert.IsTrue(transformDialog.GetWindow().IsClosed, "Transform dialog cannot be closed with Cancel on wrong math expression");
        }

        /// <summary>
        /// TransformDialogCancelNoChanges - Verifies transformation dialog can be closed with Cancel and there will be no chages
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TransformDialogCancelNoChanges()
        {

            //Start new non tabulated data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                                                  .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickOk();

            //Verify transform on X column and that only X and Y columns can be transformed
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");
            TransformDialog.FromMainMenu(mainWindow).IfColumnsAre(new string[] { "X", "Y" }).SelectColumn("X").SetMathExpression("X+1").ClickCancel();
            table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Incorrect data in cell 0,0");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Incorrect data in cell 1,0");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Incorrect data in cell 2,0");
        }
    }
}
