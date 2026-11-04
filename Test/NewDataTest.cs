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

namespace Test
{
    [TestClass]
    public class NewDataTest
    {
        TestStack.White.Application app = null;
        Window mainWindow = null;

        [TestInitialize]
        public void InitializeTest()
        {
            app = TestStack.White.Application.Launch(@"LybraFit.exe");
            mainWindow = app.GetWindow("LybraFit");
            BaseTest.DelayTime = 1000;
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

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataSimpleUIState()
        {
            NewDataDialog newDataDialog;

            //Open and escape
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow);

            newDataDialog.Delay(); 
            newDataDialog.ClickCancel();

            //Open, set title and escape
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickCancel();

            //Test auto increment settings
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data");
            Assert.IsFalse(newDataDialog.DataXMin.Enabled); Assert.IsFalse(newDataDialog.DataXMax.Enabled); Assert.IsFalse(newDataDialog.DataXIncrement.Enabled);
            newDataDialog.ClickCancel();

            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement();
            Assert.IsTrue(newDataDialog.DataXMin.Enabled); Assert.IsTrue(newDataDialog.DataXMax.Enabled); Assert.IsTrue(newDataDialog.DataXIncrement.Enabled);
            newDataDialog.ClickCancel();

            //Test Auto Calculated settings
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data");
            Assert.IsFalse(newDataDialog.Expression.Enabled);
            newDataDialog.ClickCancel();

            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoCalculateY();
            Assert.IsTrue(newDataDialog.Expression.Enabled);
            newDataDialog.ClickCancel();

            //Test Auto Error Data settings
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data");
            Assert.IsFalse(newDataDialog.ErrDataName.Enabled);
            newDataDialog.ClickCancel();

            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickCheckErrorData();
            Assert.IsTrue(newDataDialog.ErrDataName.Enabled);
            newDataDialog.ClickCancel();
        }

        [TestMethod(), Timeout(4*BaseTest.TEST_TIMEOUT)]
        public void NewDataWrongRangeCancelUIState()
        {
            NewDataDialog newDataDialog;

            //Open and escape
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).ClickCancel();

            //Open, set title and escape
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickCancel();

            //Open, set title, autoincrement with some range and click cancel
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("1").ClickCancel();

            //Open, set title, autoincrement with some range and escape key
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("1").KeyIn(KeyboardInput.SpecialKeys.ESCAPE);

            //Open, set title, autoincrement with some wrong data and escape
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("0").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("100").SetMaxX("0").SetDataXIncrement("1").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("1000").SetDataXIncrement("0.00001").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("").SetMaxX("100").SetDataXIncrement("0").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("").SetDataXIncrement("0").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("").SetMaxX("100").SetDataXIncrement("1000").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("foo123").SetMaxX("100").SetDataXIncrement("1").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("foo123").SetDataXIncrement("1").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("foo123").ClickCancel();
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataWrongCalculationMathExpressionCancelUIState()
        {
            NewDataDialog newDataDialog;

            //Open, set wrong expression and click ok, verify expression text field is focused. In the end enter valid expression and close successfully
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoCalculateY().SetExpression("").ClickOk();
            Assert.IsTrue(newDataDialog.Expression.IsFocussed);
            newDataDialog.ClearExpression().SetExpression("!!23hjkakkfaf").ClickOk();
            Assert.IsTrue(newDataDialog.Expression.IsFocussed);
            newDataDialog.ClearExpression().SetExpression("2x").ClickOk();
            Assert.IsTrue(newDataDialog.Expression.IsFocussed);
            newDataDialog.ClearExpression().SetExpression("2x+Z").ClickOk();
            Assert.IsTrue(newDataDialog.Expression.IsFocussed);
            newDataDialog.ClearExpression().SetExpression("2*x").ClickOk().Delay();
            Assert.IsTrue(newDataDialog.GetWindow().IsClosed);

        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataWrongCalculationClickOk()
        {
            NewDataDialog newDataDialog;

            //Open, set wrong expression and escape
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoCalculateY().SetExpression("").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoCalculateY().SetExpression("!!23hjkakkfaf").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoCalculateY().SetExpression("2x").ClickCancel();
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoCalculateY().SetExpression("2x+Z").ClickCancel();
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        //Open, set title, autoincrement with wrong range and escape key
        public void NewDataWrongRangeOkUIState()
        {
            NewDataDialog newDataDialog;

            //No title
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.Title.IsFocussed);
            newDataDialog.ClickCancel();

            //no Column 1 name
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").SetCol1Name("").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.Col1Name.IsFocussed);
            newDataDialog.ClickCancel();

            //no Column 2 name
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").SetCol1Name("X").SetCol2Name("").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.Col2Name.IsFocussed);
            newDataDialog.ClickCancel();

            //no Min X
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").SetCol2Name("Y").ClickAutoIncrement().SetMinX("").SetMaxX("100").SetDataXIncrement("1").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.DataXMin.IsFocussed);
            newDataDialog.ClickCancel();

            //no Max X
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("").SetDataXIncrement("1").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.DataXMax.IsFocussed);
            newDataDialog.ClickCancel();

            //No increment X
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.DataXIncrement.IsFocussed);
            newDataDialog.ClickCancel();

            //Increment X = 0
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").ClickAutoIncrement().SetMinX("0").SetMaxX("100").SetDataXIncrement("0").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.DataXIncrement.IsFocussed);
            newDataDialog.ClickCancel();

            //Negative increment
            newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample data").SetMinX("0").SetMaxX("100").SetDataXIncrement("-1").ClickOk();
            Assert.IsFalse(newDataDialog.GetWindow().IsClosed); Assert.IsTrue(newDataDialog.DataXIncrement.IsFocussed);
            newDataDialog.ClickCancel();

        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataSimple()
        {
            //Start new data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickOk();

            DataTable table = new DataTable(mainWindow);
            table.SetValue("1").SetValue("2");
            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("2.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            table.SetValue(0, 1, "3");
            table = new DataTable(mainWindow);
            Assert.AreEqual("3.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            table.Delay();
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementX()
        {
            //Start new data, set auto increment, verify values in the first column
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1").ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i < 10; i++)
            {
                Assert.AreEqual(i.ToString() + ".00E+000", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
                Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected (1, {0})", i));
            }

            Assert.AreEqual("1.00E+001", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
            Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected ({0}, 1)", i));

            //Test negative data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").SetMinX("-10").SetMaxX("0").SetDataXIncrement("1").ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual("-1.00E+001", table.Cell(0, 0), String.Format("Cell value not as expected ({0},0)", 0));
            Assert.AreEqual("0.00E+000", table.Cell(0, 1), String.Format("Cell value not as expected ({0}, 1)", 0));
            for (i = 1; i < 10; i++)
            {
                Assert.AreEqual((i - 10).ToString() + ".00E+000", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
                Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected (1, {0})", i));
            }

            //Test auto increment must be off now
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().ClickOk();
            table = new DataTable(mainWindow);
            Assert.AreEqual(1, table.RowsCount);

            //Start new data, close new data dialog with Enter key
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1").KeyIn(KeyboardInput.SpecialKeys.RETURN);

            table = new DataTable(mainWindow);
            for (i = 0; i < 10; i++)
            {
                Assert.AreEqual(i.ToString() + ".00E+000", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
                Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected (1, {0})", i));
            }

            Assert.AreEqual("1.00E+001", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
            Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected ({0}, 1)", i));

            //New negative data but hit esc, no changes in the table are to occur
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").SetMinX("-10").SetMaxX("0").SetDataXIncrement("1").KeyIn(KeyboardInput.SpecialKeys.ESCAPE);
            table = new DataTable(mainWindow);
            for (i = 0; i < 10; i++)
            {
                Assert.AreEqual(i.ToString() + ".00E+000", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
                Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected (1, {0})", i));
            }

            Assert.AreEqual("1.00E+001", table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
            Assert.AreEqual("0.00E+000", table.Cell(i, 1), String.Format("Cell value not as expected ({0}, 1)", i));

        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementXandCalculateY()
        {
            //Start new data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x").ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 10; i++)
            {
                string s1 = String.Format("{0:E2}", i);
                Assert.AreEqual(s1, table.Cell(i, 0), String.Format("Cell value not as expected ({0},1)", i));
                string s2 = String.Format("{0:E2}", 2 * i);
                Assert.AreEqual(s2, table.Cell(i, 1), String.Format("Cell value not as expected ({0},1)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementXandCalculateY1Row()
        {
            //Start new data
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("1").SetMaxX("1").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("2*x").ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 1; i <= 0; i++)
            {
                string s1 = String.Format("{0:E2}", i);
                Assert.AreEqual(s1, table.Cell(i, 0), String.Format("Cell value not as expected in ({0},0)", i));
                string s2 = String.Format("{0:E2}", 2 * i);
                Assert.AreEqual(s2, table.Cell(i, 1), String.Format("Cell value not as expected in ({0},1)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataCalculateY()
        {
            //Start new data, Set auto calculate, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoCalculateY().SetExpression("2*x").ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 5; i++)
            {
                table.SetValue(i, 0, i.ToString());
                table = new DataTable(mainWindow); //Table must be updated otherwise it doesn't work for some reason, click event make ui element not actual
                string s1 = String.Format("{0:E2}", i);
                Assert.AreEqual(s1, table.Cell(i, 0), String.Format("Cell value not as expected in ({0},0)", i));
                string s2 = String.Format("{0:E2}", 2 * i);
                Assert.AreEqual(s2, table.Cell(i, 1), String.Format("Cell value not as expected in ({0},1)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataCalculateYandErr()
        {
            //Start new data, Set auto calculate, autocalculate Err enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoCalculateY().SetExpression("x*x").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 5; i++)
            {
                table.SetValue(i, 0, i.ToString());
                table = new DataTable(mainWindow); //Table must be updated otherwise it doesn't work for some reason, click event make ui element not actual
                string expectedX = String.Format("{0:E2}", i);
                Assert.AreEqual(expectedX, table.Cell(i, 0), String.Format("Cell value not as expected in ({0},0)", i));
                string expectedY = String.Format("{0:E2}", i * i);
                Assert.AreEqual(expectedY, table.Cell(i, 1), String.Format("Cell value not as expected in ({0},1)", i));
                string expectedErr = String.Format("{0:E2}", i);
                Assert.AreEqual(expectedErr, table.Cell(i, 2), String.Format("Cell value not as expected in ({0},1)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataManualYAutocalculateErr()
        {
            //Start new data, Set auto calculate, autocalculate Err enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 5; i++)
            {
                table.SetValue(i, 0, i.ToString());
                table.SetValue(i, 1, (i*i).ToString());
                table = new DataTable(mainWindow); //Table must be updated otherwise it doesn't work for some reason, click event make ui element not actual
                string expectedX = String.Format("{0:E2}", i);
                Assert.AreEqual(expectedX, table.Cell(i, 0), String.Format("Cell value not as expected in ({0},0)", i));
                string expectedY = String.Format("{0:E2}", i * i);
                Assert.AreEqual(expectedY, table.Cell(i, 1), String.Format("Cell value not as expected in ({0},1)", i));
                string expectedErr = String.Format("{0:E2}", i);
                Assert.AreEqual(expectedErr, table.Cell(i, 2), String.Format("Cell value not as expected in ({0},2)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementXAutoCalculateYVisibleError()
        {
            //Start new data, Set auto increment, auto calculate Y and set error data visible
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("x*x").ClickCheckErrorData().ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 10; i++)
            {
                string s1 = String.Format("{0:E2}", i);
                Assert.AreEqual(s1, table.Cell(i, 0), String.Format("Cell value not as expected in ({0},0)", i));
                string s2 = String.Format("{0:E2}", i * i);
                Assert.AreEqual(s2, table.Cell(i, 1), String.Format("Cell value not as expected in ({0},1)", i));
                string s3 = String.Format("{0:E2}", 0);
                Assert.AreEqual(s3, table.Cell(i, 2), String.Format("Cell value not as expected in ({0},2)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementXAutoCalculateError()
        {
            //Start new data, Set auto increment and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickCheckErrorData().ClickCheckErrorDataCalculated();
            newDataDialog.ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 5; i++)
            {
                table.SetValue(i, 1, (i * i).ToString());
                table = new DataTable(mainWindow); //Table must be updated otherwise it doesn't work for some reason, click event make ui element not actual
                string s3 = String.Format("{0:E2}", i);
                Assert.AreEqual(s3, table.Cell(i, 2), String.Format("Cell value not as expected in ({0},2)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementXAutoCalculateYAutoCalculateError()
        {
            //Start new data, Set auto increment, auto calculate Y and auto set error data, enter data in first column and verify the second one is calculated correctly
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickAutoIncrement().SetMinX("0").SetMaxX("10").SetDataXIncrement("1")
                .ClickAutoCalculateY().SetExpression("x*x").ClickCheckErrorData().ClickCheckErrorDataCalculated().ClickOk();

            DataTable table = new DataTable(mainWindow);
            int i;
            for (i = 0; i <= 10; i++)
            {
                string s1 = String.Format("{0:E2}", i);
                Assert.AreEqual(s1, table.Cell(i, 0), String.Format("Cell value not as expected in ({0},0)", i));
                string s2 = String.Format("{0:E2}", i * i);
                Assert.AreEqual(s2, table.Cell(i, 1), String.Format("Cell value not as expected in ({0},1)", i));
                string s3 = String.Format("{0:E2}", i);
                Assert.AreEqual(s3, table.Cell(i, 2), String.Format("Cell value not as expected in ({0},2)", i));
            }
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataWrongCaptions()
        {
            string wrongString = @"jkhfaslkjhflkjh129837109lkjhflkjahflkja hjdflkjapoqieq oieqw 12287491749JKHIUHKJhkjh" +
                                  "kjhfpsdghpowhfghshgkjlhlkhalhgagajkhfaslkjhflkjh129837109lkjhflkjahflkja hjdflkjapoqieq oieqw 12287491749JKHIUHKJhkjh" +
                                  "kjhfpsdghpowhfghshgkjlhlkhalhgagajkhfaslkjhflkjh129837109lkjhflkjahflkja hjdflkjapoqieq oieqw 12287491749JKHIUHKJhkjh";
            string limit120 = wrongString.Substring(0, 120);
            string limit42 = wrongString.Substring(0, 42);

            NewDataDialog.FromMainMenu(mainWindow).SetTitle(wrongString).SetCol1Name(wrongString).SetCol2Name(wrongString).ClickCheckErrorData().SetErrDataName(wrongString).ClickOk();
            NewDataDialog newDataDialog = NewDataDialog.FromMainMenu(mainWindow);
            string actualTitle = newDataDialog.Title.Text;
            string col1Name = newDataDialog.Col1Name.Text;
            string col2Name = newDataDialog.Col2Name.Text;
            string errDataName = newDataDialog.ErrDataName.Text;
            newDataDialog.ClickOk();
            Assert.AreEqual(limit120, actualTitle, "Title not as expected");
            Assert.AreEqual(limit42, col1Name, "Col1 name not as expected");
            Assert.AreEqual(limit42, col2Name, "Col2 name not as expected");
            Assert.AreEqual(limit42, errDataName, "Error data name not as expected");
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataColumnNames()
        {
            //Start new data, missing column names
            NewDataDialog newDataDialog = NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").SetCol1Name("").SetCol2Name("Y1").ClickOk();
            Assert.IsTrue(newDataDialog.Col1Name.IsFocussed, "Failed on empty 1st column name");
            newDataDialog.SetCol1Name("X1").SetCol2Name("").ClickOk();
            Assert.IsTrue(newDataDialog.Col2Name.IsFocussed, "Failed on empty 2nd column name");
            newDataDialog.SetCol1Name("").SetCol2Name("Y1").ClickOk();
            Assert.IsTrue(newDataDialog.Col1Name.IsFocussed, "Failed on empty 1st column name");
            newDataDialog.SetCol1Name("X1").SetCol2Name(" ").ClickOk();
            Assert.IsTrue(newDataDialog.Col2Name.IsFocussed, "Failed on space for 2nd column name");
            newDataDialog.SetCol1Name(" ").SetCol2Name("Y1").ClickOk();
            Assert.IsTrue(newDataDialog.Col1Name.IsFocussed, "Failed on space for 1st column name");
            newDataDialog.SetCol1Name("X1").SetCol2Name("Y1").ClickCheckErrorData().SetErrDataName("").ClickOk();
            Assert.IsTrue(newDataDialog.ErrDataName.IsFocussed, "Failed on empty error column name");
            newDataDialog.SetCol1Name("X1").SetCol2Name("Y1").SetErrDataName(" ").ClickOk();
            Assert.IsTrue(newDataDialog.ErrDataName.IsFocussed, "Failed on space for error column name");

            //Set correct names
            newDataDialog.SetCol1Name("X1").SetCol2Name("Y1").SetErrDataName("Err").ClickOk();
            Assert.IsTrue(newDataDialog.GetWindow().IsClosed);

            DataTable table = new DataTable(mainWindow);
            Assert.AreEqual("X1", table.ColumnName(0), "Column 1 name not set correctly");
            Assert.AreEqual("Y1", table.ColumnName(1), "Column 2 name not set correctly");
        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void NewDataAutoIncrementXRowNumbers()
        {
            Assert.Fail("This is to be tested manually. Check row numbering");
        }
    }
}
