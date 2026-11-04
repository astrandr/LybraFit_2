using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataFit;

namespace Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        public UnitTest1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            Document d = new Document();
            d.LoadFromFile(@"C:\Documents and Settings\Andrey\My Documents\Visual Studio 2008\Projects\DataFit\Test\bin\Debug\TestData1.txt");
            Assert.AreEqual(2,d.Data.Count , "Data not length invalid");
            Assert.AreEqual(4, d.Data[0].Length, "Invalid data lenght");
            Assert.AreEqual(4, d.Data[1].Length, "Invalid data lenght");
            Assert.AreEqual(0, d.Data[0][0], "Wrong data value");
            Assert.AreEqual(3, d.Data[0][1], "Wrong data value");
            Assert.AreEqual(1, d.Data[1][0], "Wrong data value");

        }
    }
}
