using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Dialogs;
using Test.Common;
using TestStack.White.UIItems.WindowItems;
using Test.Tables;

namespace Test
{
    [TestClass]
    public class TableSettings
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
        /// TestNoFormat2Digits - test numeric formatting  with 2 digits
        /// </summary>
        [TestMethod(), Timeout(4 * BaseTest.TEST_TIMEOUT)]
        public void TestNoFormat2Digits()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Initial data format must be scientific
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0.00E+000", table.Cell(0, 4), "Value in table cell 0,4 not as expected");

            TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").SelectFormat(0).SetDigits(2)
                                                        .SelectItem("Y").SelectFormat(0).SetDigits(2)
                                                        .SelectItem("Err").SelectFormat(0).SetDigits(2)
                                                        .SelectItem("Model").SelectFormat(0).SetDigits(2)
                                                        .SelectItem("Diff").SelectFormat(0).SetDigits(2)
                                                        .ClickOk();

            table = new DataTable(mainWindow);
            Assert.AreEqual("0", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0", table.Cell(0, 4), "Value in table cell 0,4 not as expected");
        }

        /// <summary>
        /// TestScientific2Digits - test scientific formatting with 1 digits
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestScientific1Digits()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").SelectFormat(2).SetDigits(1)
                                                        .SelectItem("Y").SelectFormat(2).SetDigits(1)
                                                        .SelectItem("Err").SelectFormat(2).SetDigits(1)
                                                        .SelectItem("Model").SelectFormat(2).SetDigits(1)
                                                        .SelectItem("Diff").SelectFormat(2).SetDigits(1)
                                                        .ClickOk();

            DataTable table = new DataTable(mainWindow);

            Assert.AreEqual("0.0E+000", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.0E+000", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.0E+000", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1.0E+000", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0.0E+000", table.Cell(0, 4), "Value in table cell 0,4 not as expected");
        }

        /// <summary>
        /// TestNumeric2Digits - test numeric formatting with 1 digits
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestNumeric1Digits()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Initial data format must be scientific
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0.00E+000", table.Cell(0, 4), "Value in table cell 0,4 not as expected");


            TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Y").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Err").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Model").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Diff").SelectFormat(1).SetDigits(1)
                                                        .ClickOk();

            table = new DataTable(mainWindow);
            Assert.AreEqual("0.0", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.0", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.0", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1.0", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0.0", table.Cell(0, 4), "Value in table cell 0,4 not as expected");
        }

        /// <summary>
        /// TestNumeric2Digits - test numeric formatting with 1 digits
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestForCancel()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Initial data format must be scientific
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0.00E+000", table.Cell(0, 4), "Value in table cell 0,4 not as expected");

            TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Y").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Err").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Model").SelectFormat(1).SetDigits(1)
                                                        .SelectItem("Diff").SelectFormat(1).SetDigits(1)
                                                        .ClickCancel();

            table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 2), "Value in table cell 0,2 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 3), "Value in table cell 0,3 not as expected");
            Assert.AreEqual("0.00E+000", table.Cell(0, 4), "Value in table cell 0,4 not as expected");
        }

        /// <summary>
        /// TestForColumnNameChange - test for column name changes, including empty name
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestForColumnNameChange()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            TableSettingsDialog tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").SetName("").ClickOk();

            Assert.IsTrue(!tableSettingsDialog.GetWindow().IsClosed, "Column settings dialog shell not be closed with Ok if there is empty column name");
            Assert.IsTrue(tableSettingsDialog.TxtName.IsFocussed, "On empty name the focus must be on the column name text box");

            tableSettingsDialog.SetName("X1").SelectItem("Y").SetName("Y1").SelectItem("Model").SetName("Model1").SelectItem("Diff").SetName("Diff1").ClickOk();

            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("X1", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Y1", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Model1", table.ColumnName(3), "Column name at index 3 not change as expected");
            Assert.AreEqual("Diff1", table.ColumnName(4), "Column name at index 4 not change as expected");

            //Test setting dialog can be closed with empty column name
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X1").SetName("").ClickCancel();
            //Verify there is no change in column names
            table = new DataTable(mainWindow);
            Assert.AreEqual("X1", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Y1", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Model1", table.ColumnName(3), "Column name at index 3 not change as expected");
            Assert.AreEqual("Diff1", table.ColumnName(4), "Column name at index 4 not change as expected");
        }


        /// <summary>
        /// TestForColumnVisibleChange - test for initial column visibility
        /// </summary>
        [TestMethod(), Timeout(4 * BaseTest.TEST_TIMEOUT)]
        public void TestForColumnVisibleChange()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Test "X" column visibility change
            TableSettingsDialog tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").HideColumn().ClickOk();

            Assert.IsTrue(tableSettingsDialog.GetWindow().IsClosed, "Columns settings dialog is not closed");

            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("Y", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Err", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Model", table.ColumnName(2), "Column name at index 2 not change as expected");
            Assert.AreEqual("Diff", table.ColumnName(3), "Column name at index 3 not change as expected");

            //Test "Y" column visibility change
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X").ShowColumn().SelectItem("Y").HideColumn().ClickOk();

            Assert.IsTrue(tableSettingsDialog.GetWindow().IsClosed, "Columns settings dialog is not closed");

            table = new DataTable(mainWindow);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Err", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Model", table.ColumnName(2), "Column name at index 2 not change as expected");
            Assert.AreEqual("Diff", table.ColumnName(3), "Column name at index 3 not change as expected");

            //Test "Err" column visibility change
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("Y").ShowColumn().SelectItem("Err").HideColumn().ClickOk();

            Assert.IsTrue(tableSettingsDialog.GetWindow().IsClosed, "Columns settings dialog is not closed");

            table = new DataTable(mainWindow);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Y", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Model", table.ColumnName(2), "Column name at index 2 not change as expected");
            Assert.AreEqual("Diff", table.ColumnName(3), "Column name at index 3 not change as expected");

            //Test "Model" column visibility change
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("Err").ShowColumn().SelectItem("Model").HideColumn().ClickOk();

            Assert.IsTrue(tableSettingsDialog.GetWindow().IsClosed, "Columns settings dialog is not closed");

            table = new DataTable(mainWindow);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Y", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Err", table.ColumnName(2), "Column name at index 2 not change as expected");
            Assert.AreEqual("Diff", table.ColumnName(3), "Column name at index 3 not change as expected");

            //Test "Diff" column visibility change
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("Model").ShowColumn().SelectItem("Diff").HideColumn().ClickOk();

            Assert.IsTrue(tableSettingsDialog.GetWindow().IsClosed, "Columns settings dialog is not closed");

            table = new DataTable(mainWindow);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected");
            Assert.AreEqual("Y", table.ColumnName(1), "Column name at index 1 not change as expected");
            Assert.AreEqual("Err", table.ColumnName(2), "Column name at index 2 not change as expected");
            Assert.AreEqual("Model", table.ColumnName(3), "Column name at index 3 not change as expected");
        }
        /// <summary>
        /// TestForInitialColumnVisible - test for column visibility based on data generated
        /// </summary>
        [TestMethod(), Timeout(4 * BaseTest.TEST_TIMEOUT)]
        public void TestForInitialColumnVisible()
        {
            //Test for simple data only with X and Y columns
            //Start new simple data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickOk();

            //Verify only X and Y columns are visible
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual(2, table.ColsCount, "Columns not as expected 2 but:" + table.ColsCount);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected X but:" + table.ColumnName(0));
            Assert.AreEqual("Y", table.ColumnName(1), "Column name at index 1 not change as expected Y but:" + table.ColumnName(1));

            //Test "X" column visibility change
            TableSettingsDialog tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X");
            Assert.AreEqual(2, tableSettingsDialog.ColumnsList.Items.Count, "Columns in table settings not as expected 2 but:" + tableSettingsDialog.ColumnsList.Items.Count);
            Assert.AreEqual("X", tableSettingsDialog.ColumnsList.Items[0].Text, "Column name in table settings not as expected X but:" + tableSettingsDialog.ColumnsList.Items[0].Text);
            Assert.AreEqual("Y", tableSettingsDialog.ColumnsList.Items[1].Text, "Column name in table settings not as expected Y but:" + tableSettingsDialog.ColumnsList.Items[1].Text);
            tableSettingsDialog.ClickOk();

            //Test for data with X, Y and Err columns
            //Start new simple data with Err
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickCheckErrorData().ClickOk();

            //Verify only X and Y and Err columns are visible
            table = new DataTable(mainWindow);
            Assert.AreEqual(3, table.ColsCount, "Columns not as expected 3 but:" + table.ColsCount);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected X but:" + table.ColumnName(0));
            Assert.AreEqual("Y", table.ColumnName(1), "Column name at index 1 not change as expected Y but:" + table.ColumnName(1));
            Assert.AreEqual("Err", table.ColumnName(2), "Column name at index 2 not change as expected Err but:" + table.ColumnName(2));

            //Test X,Y and Err columns are visible in the table settings dialog
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow).SelectItem("X");
            Assert.AreEqual(3, tableSettingsDialog.ColumnsList.Items.Count, "Columns in table settings not as expected 2 but:" + tableSettingsDialog.ColumnsList.Items.Count);
            Assert.AreEqual("X", tableSettingsDialog.ColumnsList.Items[0].Text, "Column name in table settings not as expected X but:" + tableSettingsDialog.ColumnsList.Items[0].Text);
            Assert.AreEqual("Y", tableSettingsDialog.ColumnsList.Items[1].Text, "Column name in table settings not as expected Y but:" + tableSettingsDialog.ColumnsList.Items[1].Text);
            Assert.AreEqual("Err", tableSettingsDialog.ColumnsList.Items[2].Text, "Column name in table settings not as expected Err but:" + tableSettingsDialog.ColumnsList.Items[2].Text);
            tableSettingsDialog.ClickOk();

            //Test for model related columns
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorDataCalculated().ClickOk();

            //Add a model
            ModelsDialog.FromMainMenu(mainWindow).CheckLibFunction("Linear").ClickOk();

            //Verify only X, Y, Err, Model and Difference columns are visible
            table = new DataTable(mainWindow);
            Assert.AreEqual(5, table.ColsCount, "Columns not as expected 3 but:" + table.ColsCount);
            Assert.AreEqual("X", table.ColumnName(0), "Column name at index 0 not change as expected X but:" + table.ColumnName(0));
            Assert.AreEqual("Y", table.ColumnName(1), "Column name at index 1 not change as expected Y but:" + table.ColumnName(1));
            Assert.AreEqual("Err", table.ColumnName(2), "Column name at index 2 not change as expected Err but:" + table.ColumnName(2));
            Assert.AreEqual("Model", table.ColumnName(3), "Column name at index 3 not change as expected Model but:" + table.ColumnName(3));
            Assert.AreEqual("Diff", table.ColumnName(4), "Column name at index 4 not change as expected Diff but:" + table.ColumnName(4));

            //Test X,Y and Err columns are visible in the table settings dialog
            tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow);
            Assert.AreEqual(5, tableSettingsDialog.ColumnsList.Items.Count, "Columns in table settings not as expected 2 but:" + tableSettingsDialog.ColumnsList.Items.Count);
            Assert.AreEqual("X", tableSettingsDialog.ColumnsList.Items[0].Text, "Column name in table settings not as expected X but:" + tableSettingsDialog.ColumnsList.Items[0].Text);
            Assert.AreEqual("Y", tableSettingsDialog.ColumnsList.Items[1].Text, "Column name in table settings not as expected Y but:" + tableSettingsDialog.ColumnsList.Items[1].Text);
            Assert.AreEqual("Err", tableSettingsDialog.ColumnsList.Items[2].Text, "Column name in table settings not as expected Err but:" + tableSettingsDialog.ColumnsList.Items[2].Text);
            Assert.AreEqual("Model", tableSettingsDialog.ColumnsList.Items[3].Text, "Column name in table settings not as expected Model but:" + tableSettingsDialog.ColumnsList.Items[3].Text);
            Assert.AreEqual("Diff", tableSettingsDialog.ColumnsList.Items[4].Text, "Column name in table settings not as expected Diff but:" + tableSettingsDialog.ColumnsList.Items[4].Text);
            tableSettingsDialog.ClickOk();

        }


        /// <summary>
        /// TestCustomFormatting - test custom formatting
        /// </summary>
        [TestMethod(), Timeout(4 * BaseTest.TEST_TIMEOUT)]
        public void TestCustomFormatting()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x+1").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            //Initial data format must be scientific
            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Value in table cell 0,0 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Value in table cell 0,1 not as expected");
            Assert.AreEqual("1.00E+000", table.Cell(0, 2), "Value in table cell 0,2 not as expected");

            //Verify custom format is enabled or disabled
            var tableSettingsDialog = TableSettingsDialog.FromMainMenu(mainWindow);

            Assert.IsFalse(tableSettingsDialog.SelectItem("X").SelectFormat(0).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 0");
            Assert.IsTrue(tableSettingsDialog.SelectItem("X").SelectFormat(0).NmrDigits.Enabled, "Numeric for digits must be enabled for option 0");
            Assert.IsFalse(tableSettingsDialog.SelectItem("X").SelectFormat(1).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 1");
            Assert.IsTrue(tableSettingsDialog.SelectItem("X").SelectFormat(1).NmrDigits.Enabled, "Numeric for digits must be enabled for option 1");
            Assert.IsFalse(tableSettingsDialog.SelectItem("X").SelectFormat(2).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 2");
            Assert.IsTrue(tableSettingsDialog.SelectItem("X").SelectFormat(2).NmrDigits.Enabled, "Numeric for digits must be enabled for option 2");
            Assert.IsTrue(tableSettingsDialog.SelectItem("X").SelectFormat(3).TxtCustomFormat.Enabled, "Custom format box must be enabled for option 3");
            Assert.IsFalse(tableSettingsDialog.SelectItem("X").SelectFormat(3).NmrDigits.Enabled, "Numeric for digits must be disabled for option 3");
            tableSettingsDialog.TxtCustomFormat.SetValue("0.0#");

            Assert.IsFalse(tableSettingsDialog.SelectItem("Y").SelectFormat(0).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 0");
            Assert.IsFalse(tableSettingsDialog.SelectItem("Y").SelectFormat(1).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 1");
            Assert.IsFalse(tableSettingsDialog.SelectItem("Y").SelectFormat(2).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 2");
            Assert.IsTrue(tableSettingsDialog.SelectItem("Y").SelectFormat(3).TxtCustomFormat.Enabled, "Custom format box must be enabled for option 3");
            tableSettingsDialog.TxtCustomFormat.SetValue("1.2#"); tableSettingsDialog.TxtName.Focus();
            Assert.IsTrue(tableSettingsDialog.TxtCustomFormat.IsFocussed, "Invalid format focus must stay on format box");
            tableSettingsDialog.TxtCustomFormat.SetValue("0.0#"); tableSettingsDialog.TxtName.Focus();
            Assert.IsFalse(tableSettingsDialog.TxtCustomFormat.IsFocussed, "Valid format focus may leave format box");
            Assert.IsTrue(tableSettingsDialog.TxtName.IsFocussed, "Valid format focus may leave format box");

            Assert.IsFalse(tableSettingsDialog.SelectItem("Err").SelectFormat(0).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 0");
            Assert.IsFalse(tableSettingsDialog.SelectItem("Err").SelectFormat(1).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 1");
            Assert.IsFalse(tableSettingsDialog.SelectItem("Err").SelectFormat(2).TxtCustomFormat.Enabled, "Custom format box must be disabled for option 2");
            Assert.IsTrue(tableSettingsDialog.SelectItem("Err").SelectFormat(3).TxtCustomFormat.Enabled, "Custom format box must be enabled for option 3");
            tableSettingsDialog.TxtCustomFormat.SetValue("0.00#");

            tableSettingsDialog.ClickOk();

            table = new DataTable(mainWindow);
            Assert.AreEqual("0.0", table.Cell(0, 0), "Value in table cell 0,0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("1.0", table.Cell(0, 1), "Value in table cell 0,1 not as expected but:" + table.Cell(0, 1));
            Assert.AreEqual("1.00", table.Cell(0, 2), "Value in table cell 0,2 not as expected but:" + table.Cell(0, 2));
            Assert.AreEqual("1.0", table.Cell(1, 0), "Value in table cell 1,0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("3.0", table.Cell(1, 1), "Value in table cell 1,1 not as expected but:" + table.Cell(1, 1));
            Assert.AreEqual("1.732", table.Cell(1, 2), "Value in table cell 1,2 not as expected but:" + table.Cell(1, 2));
        }
    }
}
