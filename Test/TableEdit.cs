using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestStack.White.UIItems.WindowItems;
using Test.Common;
using Test.Dialogs;
using Test.Tables;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.Finders;

namespace Test
{
    [TestClass]
    public class TableEdit : BaseTest
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

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestUnsortedDataEntrySort()
        {
            //Start new data, 
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickOk();

            //Enter manually data unsorted
            DataTable table = new DataTable(mainWindow);

            table.SetValue("3").SetValue("2").SetValue("1");
            Assert.AreEqual("3.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("2.00E+000", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("1.00E+000", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));

        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestSortedDataEntry()
        {
            //Start new data, 
            NewDataDialog.FromMainMenu(mainWindow).SetTitle("Sample").ClickOk();

            TableSettingsDialog.FromMainMenu(mainWindow).SelectAutoSort().ClickOk();

            //Enter manually data unsorted
            DataTable table = new DataTable(mainWindow);

            table.SetValue("3").SetValue("2").SetValue("1");
            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("2.00E+000", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("3.00E+000", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));

            table.FocusCell(1, 0).SetValue("-1");
            table = new DataTable(mainWindow);
            Assert.AreEqual("-1.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("3.00E+000", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));

            table.FocusCell(2, 0).SetValue("-2");
            table = new DataTable(mainWindow);
            Assert.AreEqual("-2.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("-1.00E+000", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("1.00E+000", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));

            table.FocusCell(0, 0).SetValue("12");
            table = new DataTable(mainWindow);
            Assert.AreEqual("-1.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("1.20E+001", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));

            table.SetValue(3, 0, "24");
            table = new DataTable(mainWindow);
            Assert.AreEqual("-1.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("1.20E+001", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));
            Assert.AreEqual("2.40E+001", table.Cell(3, 0), "Value in cel 3, 0 not as expected but:" + table.Cell(3, 0));

            table.SetValue(1, 0, "16");
            table = new DataTable(mainWindow);
            Assert.AreEqual("-1.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("1.20E+001", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("1.60E+001", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));
            Assert.AreEqual("2.40E+001", table.Cell(3, 0), "Value in cel 3, 0 not as expected but:" + table.Cell(3, 0));

            TableSettingsDialog.FromMainMenu(mainWindow).UnSelectAutoSort().ClickOk();
            table.SetValue(3, 0, "-14");
            table = new DataTable(mainWindow);
            Assert.AreEqual("-1.00E+000", table.Cell(0, 0), "Value in cel 0, 0 not as expected but:" + table.Cell(0, 0));
            Assert.AreEqual("1.20E+001", table.Cell(1, 0), "Value in cel 1, 0 not as expected but:" + table.Cell(1, 0));
            Assert.AreEqual("1.60E+001", table.Cell(2, 0), "Value in cel 2, 0 not as expected but:" + table.Cell(2, 0));
            Assert.AreEqual("-1.40E+001", table.Cell(3, 0), "Value in cel 3, 0 not as expected but:" + table.Cell(3, 0));
        }
    }
}
