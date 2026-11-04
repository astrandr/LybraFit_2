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
    public class FilterTest : BaseTest
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
        /// FilterDialogPosition - Opens filter dialog and verifies it is centered with respect to the parent
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterDialogPosition()
        {
            //Start new empty data and verify filter dialog opens centered
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1").ClickOk();
            FilterDialog.FromMainMenu(mainWindow).VerifyCentered(mainWindow).ClickOk();
        }


        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterMenuItemStateOnEmptyAndNonEmptyData()
        {
            //Start new empty data and verify filter menu is disabled
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickOk();
            MenuBar menu = mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu"));
            Menu filterMenu = menu.MenuItem("Data", "Filter");
            Assert.IsTrue(!filterMenu.Enabled, "Filter menu must be disabled on empty data");
            menu.Click();

            //Enter some data one row and verify the filter menu item is enabled
            DataTable table = new DataTable(mainWindow);
            table.FocusCell(0, 0, true);
            table.SetValue("1").SetValue(0, 1, "3");
            Assert.IsTrue(filterMenu.Enabled, "Filter menu must be enabled on non empty data");

            //Delete data and verify filter menu is disabled
            table.FocusCell(0, 0); table.GetWindow().KeyIn(KeyboardInput.SpecialKeys.ESCAPE);
            table.GetWindow().KeyIn(KeyboardInput.SpecialKeys.DELETE); table.Delay();
            Assert.IsTrue(!filterMenu.Enabled, "Filter menu must be disabled on empty data");

            //Enter some data two rows and verify the filter menu item is enabled
            table.SetValue("1").SetValue("2").Delay();
            table = new DataTable(mainWindow);
            table.FocusCell(0, 1, true).SetValue("3").Delay(); table.SetValue(1, 1, "4");
            Assert.IsTrue(filterMenu.Enabled, "Filter menu must be enabled on non empty data");
        }

        /// <summary>
        /// FilterOnXandYOnlyNewData - Test the filter on X, Y and both and unchecking the filter
        /// </summary>
        [TestMethod(), Timeout(4 * BaseTest.TEST_TIMEOUT)]
        public void FilterOnXandYOnlyNewDataClickOk()
        {

            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            Assert.AreEqual(3, filterDialog.ChkColumnsList.Items.Count, "Wrong number of Filter columns count");
            //Set filter on X only
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err" }).CheckItem("X").SetMin("0").SetMax("5").ClickOk();
            DataTable table = new DataTable(mainWindow);
            for (int i = 0; i <= 5; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i), table.Cell(i, 1), "Wrong value in cell {i},1");
            }

            //Set filter on Y only
            filterDialog = FilterDialog.FromMainMenu(mainWindow);
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err" }).UnCheckItem("X").CheckItem("Y").SetMin("0").SetMax("6").ClickOk();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 3; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i), table.Cell(i, 1), "Wrong value in cell {i},1");
            }

            //Set filter on X and Y only
            filterDialog = FilterDialog.FromMainMenu(mainWindow);
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err" }).CheckItem("X").SetMin("0").SetMax("5").ClickOk();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 3; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i), table.Cell(i, 1), "Wrong value in cell {i},1");
            }

            //Remove filters on X and Y
            filterDialog = FilterDialog.FromMainMenu(mainWindow);
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err" }).UnCheckItem("X").UnCheckItem("Y").ClickOk();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 10; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i), table.Cell(i, 1), "Wrong value in cell {i},1");
            }

        }

        /// <summary>
        /// FilterUndo - Tests if the undo command works on filter
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterUndo()
        {

            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            Assert.AreEqual(3, filterDialog.ChkColumnsList.Items.Count, "Wrong number of Filter columns count");

            //Set filter on X only
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err" }).CheckItem("X").SetMin("0").SetMax("5").ClickOk();
            DataTable table = new DataTable(mainWindow);
            for (int i = 0; i <= 5; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0  after filter command");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i), table.Cell(i, 1), "Wrong value in cell {i},1  after  filter command");
            }

            //Click Undo from the menu and verify data is restored
            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Undo").Click();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 10; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0 after undo filter command");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i), table.Cell(i, 1), "Wrong value in cell {i},1 after undo filter command ");
            }
        }

        /// <summary>
        /// FilterOnModelClickOk - Test the filter on model values
        /// </summary>
        [TestMethod(), Timeout(4 * BaseTest.TEST_TIMEOUT)]
        public void FilterOnModelClickOkAndUndo()
        {

            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();
            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            Assert.AreEqual(5, filterDialog.ChkColumnsList.Items.Count, "Wrong number of Filter columns count");
            //Set filter on X only
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err", "Model", "Diff" }).CheckItem("Model").SetMin("0").SetMax("5").ClickOk();
            DataTable table = new DataTable(mainWindow);
            for (int i = 0; i <= 2; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 1), "Wrong value in cell {i},1");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 3), "Wrong value in cell {i},3 (Model value)");
            }

            //Click Undo from the menu and verify data is restored
            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Undo").Click();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 10; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 1), "Wrong value in cell {i},1");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 3), "Wrong value in cell {i},3 (Model value)");
            }

        }

        /// <summary>
        /// FilterClearAndCancel - Test the filter Cancel and Clear 
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterClearAndCancel()
        {

            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();
            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            Assert.AreEqual(5, filterDialog.ChkColumnsList.Items.Count, "Wrong number of Filter columns count");
            //Set filter on X only and click Cancel
            filterDialog.IfColumnsAre(new string[] { "X", "Y", "Err", "Model", "Diff" }).CheckItem("Model").SetMin("0").SetMax("5").ClickCancel();
            DataTable table = new DataTable(mainWindow);
            for (int i = 0; i <= 10; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 1), "Wrong value in cell {i},1");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 3), "Wrong value in cell {i},3 (Model value)");
            }

            //Set filter on X only and click Ok, verify data is filtered
            filterDialog = FilterDialog.FromMainMenu(mainWindow);
            filterDialog.UnCheckItem("Model").CheckItem("X").SetMin("0").SetMax("5").ClickOk();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 5; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 1), "Wrong value in cell {i},1");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 3), "Wrong value in cell {i},3 (Model value)");
            }

            //Set filter on X only and click Clear and Ok, verify data is restored without filter
            filterDialog = FilterDialog.FromMainMenu(mainWindow);
            filterDialog.Clear().ClickOk();
            table = new DataTable(mainWindow);
            for (int i = 0; i <= 10; i++)
            {
                Assert.AreEqual(String.Format("{0:E2}", i), table.Cell(i, 0), "Wrong value in cell {i},0");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 1), "Wrong value in cell {i},1");
                Assert.AreEqual(String.Format("{0:E2}", 2 * i + 1), table.Cell(i, 3), "Wrong value in cell {i},3 (Model value)");
            }

        }

        /// <summary>
        /// FilterWrongValues - Test the filter wrong values in Min and Max
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterWrongValues()
        {

            //Start new data, Set auto increment, auto calculate Y 
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1").ClickOk();

            //Open filter dialog
            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            //Set wrong value on min only and click Ok
            Assert.IsTrue(!filterDialog.CheckItem("X").SetMin("0a").SetMax("5").ClickOk().GetWindow().IsClosed, "Filter dialog can be closed when min value is worng");
            Assert.IsTrue(filterDialog.TxtMin.IsFocussed, "The focus must be on the min value box if value is wrong");

            //Set correct value on min wrong value on max only and click Ok
            Assert.IsTrue(!filterDialog.SetMin("0").SetMax("a5").ClickOk().GetWindow().IsClosed, "Filter dialog can be closed when min value is worng");
            Assert.IsTrue(filterDialog.TxtMax.IsFocussed, "The focus must be on the min value box if value is wrong");

            //Set correct values on min and max only and click Ok
            Assert.IsTrue(filterDialog.SetMin("0").SetMax("5").ClickOk().GetWindow().IsClosed, "Filter dialog cannot be closed when min and max values are corrected after beign wrong");
        }

        /// <summary>
        /// FilterCheckedAndClear - Test if checking and clearing columns works
        /// </summary>
        [TestMethod(), Timeout(4*BaseTest.TEST_TIMEOUT)]
        public void FilterCheckedAndClear()
        {

            //Start new data, Set auto increment, auto calculate Y 
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                                                  .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Open filter dialog
            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            //Check each column and verify if it is checked
            filterDialog.CheckItem("X").IfCheckedColumnsAre(new string[] { "X" })
                        .CheckItem("Y").IfCheckedColumnsAre(new string[] { "X", "Y" })
                        .CheckItem("Err").IfCheckedColumnsAre(new string[] { "X", "Y", "Err" })
                        .CheckItem("Model").IfCheckedColumnsAre(new string[] { "X", "Y", "Err", "Model" })
                        .CheckItem("Diff").IfCheckedColumnsAre(new string[] { "X", "Y", "Err", "Model", "Diff" })
                        .Clear().IfCheckedColumnsAre(new string[] { }).ClickOk();
        }

        /// <summary>
        /// FilterModelOnFilteredData - Test the filter on model values
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterModelOnFilteredData()
        {

            //Start new data, set autoincrement from 0 to 3 step 1, autocalculate on linear function
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("3").SetDataXIncrement("1")
                                                .ClickAutoCalculateY().SetExpression("2*x+1").ClickOk();

            //Enter values so that first and last values are beyond the line
            DataTable table = new DataTable(mainWindow);
            table.SetValue(0, 1, "-10").SetValue(3, 1, "11");

            //Filter only second and third point
            FilterDialog.FromMainMenu(mainWindow).CheckItem("X").SetMin("1").SetMax("2").ClickOk();

            //Add Linear approximation function
            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Verify the model function is fit only to available after filtering data
            table = new DataTable(mainWindow);
            Assert.AreEqual(String.Format("{0:E2}", 3), table.Cell(0, 2), "Wrong value for the model at 1");
            Assert.AreEqual(String.Format("{0:E2}", 5), table.Cell(1, 2), "Wrong value for the model at 2");

            //Clear filter
            FilterDialog.FromMainMenu(mainWindow).Clear().ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual(String.Format("{0:E2}", 0), table.Cell(0, 2), "Wrong value for the model at 0");
            Assert.AreEqual(String.Format("{0:E2}", 3), table.Cell(1, 2), "Wrong value for the model at 1");
            Assert.AreEqual(String.Format("{0:E2}", 5), table.Cell(2, 2), "Wrong value for the model at 2");
            Assert.AreEqual(String.Format("{0:E2}", 0), table.Cell(3, 2), "Wrong value for the model at 3");

            //Undo filtering (clearing filter which actually will limit data again)
            mainWindow.GetMenuBar(SearchCriteria.ByText("mainMenu")).MenuItem("Data", "Undo").Click();
            table = new DataTable(mainWindow);
            Assert.AreEqual(String.Format("{0:E2}", 3), table.Cell(0, 2), "Wrong value for the model at 1");
            Assert.AreEqual(String.Format("{0:E2}", 5), table.Cell(1, 2), "Wrong value for the model at 2");
        }

        /// <summary>
        /// FilterResetOnNewData - Test the filter is reset on new data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterResetOnNewData()
        {
            //Start new data, set autoincrement from 0 to 3 step 1, autocalculate on linear function
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("3").SetDataXIncrement("1")
                                                .ClickAutoCalculateY().SetExpression("2*x+1").ClickOk();

            //Filter only second and third point
            FilterDialog.FromMainMenu(mainWindow).CheckItem("X").IfMinXIsEnabled().SetMin("1").IfMaxXIsEnabled().SetMax("2").CheckItem("Y").ClickOk();
            FilterDialog.FromMainMenu(mainWindow).IfCheckedColumnsAre(new string[] { "X", "Y" }).ClickOk();

            //Start new data, set autoincrement from 0 to 3 step 1, autocalculate on linear function
            NewDataDialog.FromMainMenu(mainWindow).ClickOk();

            //Verify filter is cleared
            FilterDialog.FromMainMenu(mainWindow).IfCheckedColumnsAre(new string[] {}).IfMaxXIsNotEnabled().IfMinXIsNotEnabled().ClickOk();

        }

        /// <summary>
        /// FilterEditBoxEnabledOnItemChecked - Test the filter text boxes are enabled or disabled based whether corresponing columns is checked or not
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FilterEditBoxEnabledOnItemChecked()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();
            FilterDialog filterDialog = FilterDialog.FromMainMenu(mainWindow);

            Assert.AreEqual(5, filterDialog.ChkColumnsList.Items.Count, "Wrong number of Filter columns count");

            string[] columnNames = new string[] { "X", "Y", "Err", "Model", "Diff" };

            //Verify for each column text boxes are not enabled and check the columns
            foreach(var columnName in columnNames)
            {
                filterDialog.SelectItem(columnName).IfMinXIsNotEnabled().IfMaxXIsNotEnabled().CheckItem(columnName);
            }

            //Now, vor each column verify text boxes are enabled and uncheck the column
            foreach (var columnName in columnNames)
            {
                filterDialog.SelectItem(columnName).IfMinXIsEnabled().IfMaxXIsEnabled().UnCheckItem(columnName);
            }

            //Set filtering on first column and close with OK
            filterDialog.CheckItem("X").IfMinXIsEnabled().IfMaxXIsEnabled().SetMin("0").SetMax("1").ClickOk();
        }
    }
}
