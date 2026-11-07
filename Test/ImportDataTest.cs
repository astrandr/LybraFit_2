using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Windows.Automation;
using Test.Common;
using Test.Dialogs;
using Test.Tables;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;


namespace Test
{
    [TestClass]
    public class ImportDataTest 
    {
        TestStack.White.Application app = null;
        Window mainWindow = null;
        private const string testDataPath = @"..\..\Test\TestData";

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
        /// OpenSimpleFileNoHeaders - load file no headers, verify text as expected, verify column names are default X,Y, verify numbers in the table
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileNoHeaders()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "NoHeaderNoError.txt", testDataPath);
            
            Assert.AreEqual("NoHeaderNoError", importDialog.TxtTitle.Text, "Title not set as expected");
            Assert.AreEqual("1\t2\r\n3\t4\r\n5\t6", importDialog.TxtOriginal.Text, "File content not as expected");

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are default
            Assert.AreEqual("X", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("2.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("6.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");

            Assert.IsTrue(mainWindow.TitleBar.Name.Contains("NoHeaderNoError"), "Form title not set as expected");
        }

        /// <summary>
        /// OpenSimpleFileNoHeadersSetColumnNames - load file no headers, set column names, verify columns names are set correctly
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileNoHeadersSetColumnNames()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "NoHeaderNoError.txt", testDataPath);

            Assert.AreEqual("1\t2\r\n3\t4\r\n5\t6", importDialog.TxtOriginal.Text, "File content not as expected");
            importDialog.SetColumnName(1, "t").SetColumnName(2, "V").ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);
            //Verify column names are default
            Assert.AreEqual("t", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("V", table.ColumnName(1), "Invalid 2nd column name");
        }

        /// <summary>
        /// OpenSimpleFileNoHeadersRemoveAllRowsAsHeaderLines - open file with no header, set header line same as lines in the file, verify no data is in the grid
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileNoHeadersRemoveAllRowsAsHeaderLines()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "NoHeaderNoError.txt", testDataPath);

            Assert.AreEqual("1\t2\r\n3\t4\r\n5\t6", importDialog.TxtOriginal.Text, "File content not as expected");
            importDialog.SetHeaderLinesCount(3).ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            Assert.AreEqual(1, table.RowsCount, "Rows count had to be 1");
        }

        /// <summary>
        /// OpenSimpleFileNoHeadersHeaderLinesMoreThanRowsCount - open file with no header, set header line same as lines in the file, verify dialog doesn't close on Ok
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileNoHeadersHeaderLinesMoreThanRowsCount()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "NoHeaderNoError.txt", testDataPath);
            importDialog.SetHeaderLinesCount(6).ClickOk(); 
            Assert.IsTrue(importDialog.GetWindow().IsClosed);
        }

        /// <summary>
        /// OpenSimpleFileColumnHeaders - open file with one header line, set header line to 1, verify column names as in the file and data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeaders()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);

            Assert.AreEqual("X1\tY1\r\n0\t1\r\n3\t4\r\n5\t7\r\n6\t9", importDialog.TxtOriginal.Text, "File content not as expected");

            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1);

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("9.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");
        }

        /// <summary>
        /// OpenSimpleFileColumnHeaderSkipHeaderLine - open file with one header line, set header line to 2, verify column names are from the first dat aline, verify rest of the data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeaderSkipHeaderLine()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);

            Assert.AreEqual("X1\tY1\r\n0\t1\r\n3\t4\r\n5\t7\r\n6\t9", importDialog.TxtOriginal.Text, "File content not as expected");

            //Skip one line from the header
            importDialog.SetHeaderLinesCount(2);

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("0", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("3.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("4.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("5.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("7.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("6.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("9.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
        }

        /// <summary>
        /// OpenFile4HeaderLineSkipProperly - open file with 4 header linse, set header line to 3, verify column names are from the last header line, verify rest of the data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenFile4HeaderLineSkipProperly()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "Header4Raws.txt", testDataPath);
            Assert.AreEqual("Header4Raws", importDialog.TxtTitle.Text, "Title not set as expected");

            Assert.AreEqual("This is an example of more complex data file\r\n#Intended to test veriety of data files\r\n//Created by Andrey Batchjvarov\r\nXM\tYM\tErr\t\t\tX2\t\tY2\t\tErr2\tX3\t\tY3\t\tErr3\r\n1\t21\t0.12\t\t1.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n2\t42\t0.12\t\t2.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n3\t63\t0.12\t\t3.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n4\t84\t0.12\t\t4.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n5\t105\t0.12\t\t5.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n6\t126\t0.12\t\t6.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n7\t147\t0.12\t\t7.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n8\t168\t0.12\t\t8.3\t\t2.3\t\t0.14\t1\t\t3\t\t1\r\n", importDialog.TxtOriginal.Text, "File content not as expected");

            //Skip one line from the header
            importDialog.SetHeaderLinesCount(4);

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("XM", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("YM", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("2.10E+001", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("2.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("4.20E+001", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("3.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("6.30E+001", table.Cell(2, 1), "Cell value not as expected (2,1)");

            Assert.AreEqual(9, table.RowsCount, "Row count not as expcetd");
        }

        /// <summary>
        /// OpenSimpleFileColumnHeaders - open file with one header line, set header line to 1, swap column indexes, verify column names as in the file and data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeadersSwapColumns()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.IncrementColumnIndex(1);
            Assert.AreEqual("Y1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            importDialog.DecrementColumnIndex(2);
            Assert.AreEqual("X1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected"); 
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("Y1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("X1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("0.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");
            Assert.AreEqual("9.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
        }

        /// <summary>
        /// OpenSimpleFileColumnHeaderssetSameColumnIndex1 - open file with one header line, set header line to 1, set two column to same column from data file, verify column names as in the file and data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeaderssetSameColumnIndex1()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.IncrementColumnIndex(1);
            Assert.AreEqual("Y1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("Y1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("9.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("9.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");
        }

        /// <summary>
        /// OpenSimpleFileColumnHeaderssetSameColumnIndex2 - open file with one header line, set header line to 1, set two column to same column from data file, verify column names as in the file and data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeadersSetSameColumnIndex2()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.DecrementColumnIndex(2);
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("X1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("X1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("0.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");

        }
        /// <summary>
        /// OpenSimpleFileChangeTitle - load file no headers, change title, verify new title is set on main form caption, title bar
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileChangeTitle()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "NoHeaderNoError.txt", testDataPath);

            Assert.AreEqual("NoHeaderNoError", importDialog.TxtTitle.Text, "Title not set as expected");

            importDialog.TxtTitle.Text = "Title was changed";

            importDialog.ClickOk();

            Assert.IsTrue(mainWindow.TitleBar.Name.Contains("Title was changed"), "Form title not set as expected");
        }

        /// <summary>
        /// OpenSimpleFileColumnHeadersSetSameColumnIndexOutOfLimits - open file with one header line, set header line to 1, set column indexes less than 1
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeadersSetSameColumnIndexOutOfLimits()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            //Try to decrement column index less than 0
            importDialog.DecrementColumnIndex(1);
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            importDialog.DecrementColumnIndex(2);
            Assert.AreEqual("X1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            importDialog.DecrementColumnIndex(1);
            importDialog.DecrementColumnIndex(2);
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("X1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("X1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("0.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");

        }

        /// <summary>
        /// OpenSimpleFileColumnHeadersSetSameColumnIndexOutOfLimits2 - open file with one header line, set header line to 1, set column indexes more than data columns
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileColumnHeadersSetSameColumnIndexOutOfLimits2()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.IncrementColumnIndex(1).IncrementColumnIndex(1).IncrementColumnIndex(1);
            Assert.AreEqual("", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            importDialog.IncrementColumnIndex(2).IncrementColumnIndex(2).IncrementColumnIndex(2);
            Assert.AreEqual("", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            //Try to close the dialog with wrong column indexes
            importDialog.ClickOk();
            Assert.IsFalse(importDialog.GetWindow().IsClosed, "Import dialog should not accept invalid column index");
            importDialog.DecrementColumnIndex(1).DecrementColumnIndex(1).DecrementColumnIndex(1);

            //Try to close the dialog with wrong column indexes
            importDialog.ClickOk();
            Assert.IsFalse(importDialog.GetWindow().IsClosed, "Import dialog should not accept invalid column index");
            importDialog.DecrementColumnIndex(2).DecrementColumnIndex(2).DecrementColumnIndex(2);

            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("5.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("9.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");

        }

        /// <summary>
        /// OpenEmptyFile - open empty file
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenEmptyFile()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "Empty.txt", testDataPath);
            Assert.AreEqual("Empty", importDialog.TxtTitle.Text, "Title not set as expected");

            Assert.AreEqual("X", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual(1, table.RowsCount, "Row counts not as expected 1");
        }

        /// <summary>
        /// OpenFileDifferentColumnCount - Open File with Different Column Count
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenFileDifferentColumnCount()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "DifferentColumnCounts.txt", testDataPath);
            Assert.AreEqual("DifferentColumnCounts", importDialog.TxtTitle.Text, "Title not set as expected");
            
            importDialog.SetHeaderLinesCount(1).TxtTitle.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual(6, table.RowsCount, "Row counts not as expected 1");

            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("2.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("3.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("5.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");
            Assert.AreEqual("7.00E+000", table.Cell(4, 0), "Cell value not as expected (4,0)");
            Assert.AreEqual("8.00E+000", table.Cell(4, 1), "Cell value not as expected (4,1)");
        }

        /// <summary>
        /// OpenFileDifferentDelimiters - Open File with Different Delimiters
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenFileDifferentDelimiters()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "DifferentDelimiters.txt", testDataPath);
            Assert.AreEqual("DifferentDelimiters", importDialog.TxtTitle.Text, "Title not set as expected");

            importDialog.SetHeaderLinesCount(1).TxtTitle.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("X1", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("Y1", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual(8, table.RowsCount, "Row counts not as expected 1");

            Assert.AreEqual("0.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("1.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("2.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("2.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("3.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("5.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("6.00E+000", table.Cell(3, 1), "Cell value not as expected (3,1)");
            Assert.AreEqual("7.00E+000", table.Cell(4, 0), "Cell value not as expected (4,0)");
            Assert.AreEqual("8.00E+000", table.Cell(4, 1), "Cell value not as expected (4,1)");
            Assert.AreEqual("9.00E+000", table.Cell(5, 0), "Cell value not as expected (5,0)");
            Assert.AreEqual("1.00E+001", table.Cell(5, 1), "Cell value not as expected (5,1)");
            Assert.AreEqual("1.10E+001", table.Cell(6, 0), "Cell value not as expected (6,0)");
            Assert.AreEqual("1.20E+001", table.Cell(6, 1), "Cell value not as expected (6,1)");
        }

        /// <summary>
        /// OpenSimpleFileVaryHeaderLines - open file with one header line, change header lines count and verify the state
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileVaryHeaderLines()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.SetHeaderLinesCount(2).Txt1stColumnName.Focus();
            Assert.AreEqual("0", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.SetHeaderLinesCount(5).Txt1stColumnName.Focus();
            Assert.AreEqual("6", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("9", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.SetHeaderLinesCount(3).Txt1stColumnName.Focus();
            Assert.AreEqual("3", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("4", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("3", table.ColumnName(0), "Invalid 1st column name");
            Assert.AreEqual("4", table.ColumnName(1), "Invalid 2nd column name");

            Assert.AreEqual("5.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("7.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("6.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("9.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
        }

        /// <summary>
        /// OpenSimpleFileSetErrorColumn - open file with one header line and no error data column, set error column to nothing and to one of the other columns
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileNoErrorColumn()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNamesNoErrorColumn.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            Assert.IsFalse(importDialog.ChkHasErrColumn.Enabled, "Error column check should be disabled");
            Assert.IsFalse(importDialog.Txt3rdColumnName.Enabled, "Caption for error column should be disabled");

            importDialog.ClickOk();

            var table = new DataTable(mainWindow);

            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
        }

        /// <summary>
        /// OpenSimpleFileSetErrorColumn - open file with one header line with error data column, set error column index and verify name and values are imported correctly
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileHasErrorColumn()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNamesHasErrorColumn.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNamesHasErrorColumn", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            Assert.AreEqual("Err", importDialog.Txt3rdColumnName.Text, "Third column name not updated as expected");

            Assert.IsTrue(importDialog.ChkHasErrColumn.Enabled, "Error column check is not initialized correctly");
            Assert.IsFalse(importDialog.Txt3rdColumnName.Enabled, "Caption for error column should not be enabled");

            importDialog.SetHasError(true);
            Assert.IsTrue(importDialog.Txt3rdColumnName.Enabled, "Caption for error column should be enabled");

            importDialog.Txt3rdColumnName.Text = "";
            importDialog.ClickOk();
            Assert.IsFalse(importDialog.GetWindow().IsClosed, "Import dialog should not close if no error column name is specified");

            importDialog.Txt3rdColumnName.Text = "TestError";
            importDialog.ClickOk();
            Assert.IsTrue(importDialog.GetWindow().IsClosed, "Import dialog should close if error column name is specified");

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("TestError", table.ColumnName(2), "Invalid error column name");

            importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNamesHasErrorColumn.txt", testDataPath);
            importDialog.SetColumnIndex(3, 3).ClickOk();

            table = new DataTable(mainWindow);

            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,2)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,2)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,2)");

            Assert.AreEqual("2.00E-001", table.Cell(0, 2), "Cell value not as expected (0,2)");
            Assert.AreEqual("4.00E-001", table.Cell(1, 2), "Cell value not as expected (1,2)");
            Assert.AreEqual("5.00E-001", table.Cell(2, 2), "Cell value not as expected (2,2)");
        }


        /// <summary>
        /// OpenSimpleFileSetErrorColumn - open file with with 5 columns, set column indexes to 1,3, and 5, verify dat aimport
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileWith5Columns()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNamesHasErrorColumnWith5Columns.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNamesHasErrorColumnWith5Columns", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("A", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt3rdColumnName.Text, "Third column name not updated as expected");

            Assert.IsTrue(importDialog.ChkHasErrColumn.Enabled, "Error column check is not initialized correctly");
            Assert.IsFalse(importDialog.Txt3rdColumnName.Enabled, "Caption for error column should not be enabled");

            importDialog.SetHasError(true);
            Assert.IsTrue(importDialog.Txt3rdColumnName.Enabled, "Caption for error column should be enabled");

            importDialog.SetColumnIndex(2, 3).SetColumnIndex(3, 5).ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            //Verify column names are imported correctly from the file
            Assert.AreEqual("Err", table.ColumnName(2), "Invalid error column name");

            table = new DataTable(mainWindow);

            Assert.AreEqual("1.00E+000", table.Cell(0, 1), "Cell value not as expected (0,2)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,2)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,2)");

            Assert.AreEqual("2.00E-001", table.Cell(0, 2), "Cell value not as expected (0,2)");
            Assert.AreEqual("4.00E-001", table.Cell(1, 2), "Cell value not as expected (1,2)");
            Assert.AreEqual("5.00E-001", table.Cell(2, 2), "Cell value not as expected (2,2)");
        }

        /// <summary>
        /// OpenSimpleFileRemoveLinesAsHeaderDeleteColumnNames - remove all files as header and verify the column names are still verified against empty strings
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void OpenSimpleFileRemoveLinesAsHeaderDeleteColumnNames()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "SimpleColumnNames.txt", testDataPath);
            Assert.AreEqual("SimpleColumnNames", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(6).Txt1stColumnName.Focus();
            Assert.AreEqual("6", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("9", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.SetColumnName(1, "").ClickOk();
            Assert.IsFalse(importDialog.GetWindow().IsClosed, "Import dialog should not close if column name is empty");

            importDialog.SetColumnName(1, "X").SetColumnName(2,"").ClickOk();
            Assert.IsFalse(importDialog.GetWindow().IsClosed, "Import dialog should not close if column name is empty");

            importDialog.SetColumnName(1, "X").SetColumnName(2, "Y").SetHasError(true).SetColumnName(3,"").ClickOk();
            Assert.IsFalse(importDialog.GetWindow().IsClosed, "Import dialog should not close if column name is empty");

            importDialog.SetColumnName(1, "X").SetColumnName(2, "Y").SetHasError(true).SetColumnName(3, "Err").ClickOk();
            Assert.IsTrue(importDialog.GetWindow().IsClosed, "Import dialog should not close if column name is empty");
        }

        /// <summary>
        /// FloatData - test parsing double data
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void FloatData()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "FloatData.txt", testDataPath);
            Assert.AreEqual("FloatData", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            Assert.AreEqual("1.23E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("5.68E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("9.10E-009", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("1.00E+011", table.Cell(1, 1), "Cell value not as expected (1,1)");
        }

        /// <summary>
        /// MissingDataCancel - test parsing file with missing data, like only one number or zero in a line
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void MissingDataCancel()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "MissingData.txt", testDataPath);
            Assert.AreEqual("MissingData", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            Window errorMessageWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Parsing error"));
            Assert.IsNotNull(errorMessageWindow, "Parsing error window is not shown");
            Button okButton = errorMessageWindow.Get<Button>(SearchCriteria.ByControlType(ControlType.Button));
            okButton.Click();
            
            importDialog = ImportDataDialog.FromMainWindow(mainWindow);
            importDialog.ClickCancel();
        }

        /// <summary>
        /// MissingDataOk - test parsing file with missing data, like only one number or zero in a line
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void MissingDataOk()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "MissingData.txt", testDataPath);
            Assert.AreEqual("MissingData", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            Window errorMessageWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Parsing error"));
            Assert.IsNotNull(errorMessageWindow, "Parsing error window is not shown");
            Button okButton = errorMessageWindow.Get<Button>(SearchCriteria.ByControlType(ControlType.Button));
            okButton.Click();

            importDialog = ImportDataDialog.FromMainWindow(mainWindow);
            importDialog.ChkSkipErrors.Checked = true;
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            Assert.AreEqual("1.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("2.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("3.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("4.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("6.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("7.00E+000", table.Cell(2, 1), "Cell value not as expected (2,1)");
            Assert.AreEqual("9.00E+000", table.Cell(3, 0), "Cell value not as expected (3,0)");
            Assert.AreEqual("1.00E+001", table.Cell(3, 1), "Cell value not as expected (3,1)");
        }

        /// <summary>
        /// WrongDataOk - test parsing file with missing data, like only one number or zero in a line
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void WrongDataOk()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "WrongData.txt", testDataPath);
            Assert.AreEqual("WrongData", importDialog.TxtTitle.Text, "Title not set as expected");
            //Skip one line from the header
            importDialog.SetHeaderLinesCount(1).Txt1stColumnName.Focus();
            Assert.AreEqual("X1", importDialog.Txt1stColumnName.Text, "First column name not updated as expected");
            Assert.AreEqual("Y1", importDialog.Txt2ndColumnName.Text, "Second column name not updated as expected");

            importDialog.ClickOk();

            Window errorMessageWindow = mainWindow.ModalWindow(SearchCriteria.ByText("Parsing error"));
            Assert.IsNotNull(errorMessageWindow, "Parsing error window is not shown");
            Button okButton = errorMessageWindow.Get<Button>(SearchCriteria.ByControlType(ControlType.Button));
            okButton.Click();

            importDialog = ImportDataDialog.FromMainWindow(mainWindow);
            importDialog.ChkSkipErrors.Checked = true;
            importDialog.ClickOk();

            //Verify data in the grid
            DataTable table = new DataTable(mainWindow);

            Assert.AreEqual("5.00E+000", table.Cell(0, 0), "Cell value not as expected (0,0)");
            Assert.AreEqual("9.00E+000", table.Cell(0, 1), "Cell value not as expected (0,1)");
            Assert.AreEqual("6.00E+000", table.Cell(1, 0), "Cell value not as expected (1,0)");
            Assert.AreEqual("7.00E+000", table.Cell(1, 1), "Cell value not as expected (1,1)");
            Assert.AreEqual("9.00E+000", table.Cell(2, 0), "Cell value not as expected (2,0)");
            Assert.AreEqual("1.00E+001", table.Cell(2, 1), "Cell value not as expected (2,1)");
        }

        /// <summary>
        /// HeaderAutodetect - test if header lines are autodetected based on if they contain non-numeric string
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void HeaderAutodetect()
        {
            ImportDataDialog importDialog = ImportDataDialog.FromMainMenu(mainWindow, "Header4Raws.txt", testDataPath);
            Assert.AreEqual(4, importDialog.NumHeaderLinesCount.Value, "Header lines are not detected correctly");
            importDialog.ClickCancel();
        }

    }
}
