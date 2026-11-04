using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LybraFit;
using LybraFit.Common;
using FuncLib;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using Test.Common;
using System.Drawing;

namespace Test
{
    [TestClass]
    public class ProjectTest
    {

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestSaveLoadWithDataOny()
        {

            Project project = new Project();

            project.Title = "SimpleDataProject";
            project.HasErrorY = true;
            project.DataXName = "X1";
            project.DataYName = "Y1";
            project.ErrDataName = "Err1";

            project.Data = new List<float[]>();
            project.Data.Add(new float[] { 1, 1, 0, 0, 0 });
            project.Data.Add(new float[] { 2, 1.2f, 0, 1, 0 });
            project.Data.Add(new float[] { 3, 1.3f, 0, 2, 0 });

            project.GridColumnSettings = new ColumnSettings[]{
                                                                new ColumnSettings(){ Name = "TX1", Alignment="Center", CustomFormat="", Format="Numeric", Digits=1,AutoWidth=true, TableFont = new Font("Arial", 9)},
                                                                new ColumnSettings(){ Name = "TY1", Alignment="Center", CustomFormat="#.##", Format="Numeric", Digits=2,AutoWidth=false, TableFont = new Font("Arial", 12)},
                                                                new ColumnSettings(){ Name = "TE1", Alignment="Center", CustomFormat="0#.#0", Format="Numeric", Digits=3,AutoWidth=true, TableFont = new Font("Century", 9)},
                                                                new ColumnSettings(){ Name = "TM1", Alignment="Center", CustomFormat="0.00", Format="Numeric", Digits=4,AutoWidth=false, TableFont = new Font("Century", 14)},
                                                                new ColumnSettings(){ Name = "TD1", Alignment="Center", CustomFormat="#.###", Format="Numeric", Digits=5,AutoWidth=true, TableFont = new Font("Verdana", 8)},
                                                             };

            project.Save(@".\TestData\SaveProject_DataOnly_p.prj");


            Project projectRead = Project.Load(@".\TestData\SaveProject_DataOnly_p.prj");

            //Verify Project main properties
            Assert.IsNotNull(projectRead.Data, "Failed to initialize data list");
            Assert.AreEqual(3, projectRead.Data.Count, "Failed to read all data");
            Assert.AreEqual(project.Title, projectRead.Title, "Title is not as expected");
            Assert.AreEqual(project.HasErrorY, projectRead.HasErrorY, "HasErrorY is not as expected");
            Assert.AreEqual(project.DataXName, projectRead.DataXName, "DataXName is not as expected");
            Assert.AreEqual(project.DataYName, projectRead.DataYName, "DataYName is not as expected");
            Assert.AreEqual(project.ErrDataName, projectRead.ErrDataName, "ErrDataName is not as expected");

            //Verify Column settings
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual(project.GridColumnSettings[i].Name, projectRead.GridColumnSettings[i].Name, "Column name not as expected for column:" + i);
                Assert.AreEqual(project.GridColumnSettings[i].Alignment, projectRead.GridColumnSettings[i].Alignment, "Column Alignment not as expected for column:" + i);
                Assert.AreEqual(project.GridColumnSettings[i].CustomFormat, projectRead.GridColumnSettings[i].CustomFormat, "Column CustomFormat not as expected for column:" + i);
                Assert.AreEqual(project.GridColumnSettings[i].Format, projectRead.GridColumnSettings[i].Format, "Column Format not as expected for column:" + i);
                Assert.AreEqual(project.GridColumnSettings[i].Digits, projectRead.GridColumnSettings[i].Digits, "Column Digits not as expected for columns:" + i);
                Assert.AreEqual(project.GridColumnSettings[i].AutoWidth, projectRead.GridColumnSettings[i].AutoWidth, "Column AutoWidth not as expected for columns:" + i);
                Assert.AreEqual(project.GridColumnSettings[i].TableFont, projectRead.GridColumnSettings[i].TableFont, "Column TableFont not as expected for columns:" + i);
            }

            //Verify data
            for (int i = 0; i < project.Data.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Assert.AreEqual(project.Data[i][j], projectRead.Data[i][j], "Data not as expected at row:" + i);
                }
            }
        }


        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestSaveLoadDerviedValue()
        {
            string compileErrors = null;
            DerivedValueInfo derivedValue = new DerivedValueInfo();
            derivedValue.Name = "Value1";
            derivedValue.SetCalcFunction("X+2", out compileErrors);
            FileStream stream = null;

            stream = File.Create("DerivedValueTestBinary.dbn");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, derivedValue);
            stream.Close();

            stream = File.Open("DerivedValueTestBinary.dbn", FileMode.Open);
            object b = serializer.Deserialize(stream);
            stream.Close();

        }

        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void TestSaveInBinaryWithModelDerviedValuesAndLog()
        {
            string compileErrors = null;

            Project project = new Project();

            List<DerivedValueInfo> derivedValues = new List<DerivedValueInfo>();
            DerivedValueInfo derivedValue = new DerivedValueInfo();
            derivedValue.Name = "Value1";
            derivedValue.Value = 3;
            derivedValue.SetCalcFunction("A+2", out compileErrors);
            derivedValue.UpdateValue(new float[] { 2 });
            Assert.AreEqual(4, derivedValue.Value, "Derived fvalu is not correctly calculated");
            derivedValues.Add(derivedValue);
            project.Models = new ModelFitInfo[1];
            project.Models[0] = new ModelFitInfo() { ModelName = "Model1", ModelFunction = UserFunction.Create("SimpleFunc1", "A*x+2", out compileErrors), DerivedValues = derivedValues };
            project.Models[0].DerivedValues = new List<DerivedValueInfo>() { derivedValue };
            project.Models[0].Log = new ModelLog()
            {
                Name = "Log1",
                ValueNames = new string[] { "Val1", "Val2" },
                Records = new List<LogRecord>()
                                                    {
                                                        new LogRecord(){Key = "Record1", StdError=0.1f,Values= new float[]{ 1.0f, 2.0f}},
                                                        new LogRecord(){Key = "Record2", StdError=0.1f,Values= new float[]{ 1.1f, 2.3f}},
                                                    }
            };

            project.Save(@".\SaveProject_DerivedValues.prj");

            Project projectRead = Project.Load(@".\SaveProject_DerivedValues.prj");

            Assert.AreEqual(project.Models.Length, projectRead.Models.Length, "Models count not as expected");
            Assert.AreEqual(project.Models[0].ModelName, projectRead.Models[0].ModelName, "Model name not as expected");
            Assert.IsNotNull(projectRead.Models[0].ModelFunction, "Model function is not created");
            Assert.AreEqual(project.Models[0].ModelFunction.Name, projectRead.Models[0].ModelFunction.Name, "Model function name not as expected");
            Assert.AreEqual(project.Models[0].ModelFunction.Description, projectRead.Models[0].ModelFunction.Description, "Model function description not as expected");
            Assert.AreEqual(project.Models[0].DerivedValues.Count, projectRead.Models[0].DerivedValues.Count, "Derived values count not as expected");
            Assert.AreEqual(project.Models[0].DerivedValues[0].Name, projectRead.Models[0].DerivedValues[0].Name, "Derived value name not as expected");
            Assert.AreEqual(project.Models[0].DerivedValues[0].Value, projectRead.Models[0].DerivedValues[0].Value, "Derived value not as expected");

            //Verify model function and derived value functions can be used
            float[] result = new float[1];
            projectRead.Models[0].ModelFunction.CalcProcedure(new float[] { 2 }, new float[1] { 2 }, ref result);
            Assert.AreEqual(6, result[0], "Model function does not calculate properly");
            projectRead.Models[0].DerivedValues[0].UpdateValue(new float[] { 2 });
            Assert.AreEqual(4, projectRead.Models[0].DerivedValues[0].Value, "Derived value is not correctly calcualted");

            Assert.IsNotNull(projectRead.Models[0].Log, "Models log is null");
            Assert.AreEqual(project.Models[0].Log.Name, projectRead.Models[0].Log.Name, "Model log name not as expected");
            Assert.AreEqual(project.Models[0].Log.Identifier, projectRead.Models[0].Log.Identifier, "Model log Identifier not as expected");
            Assert.AreEqual(project.Models[0].Log.ValueNames.Length, projectRead.Models[0].Log.ValueNames.Length, "Model log Values length not as expected");
            Assert.AreEqual(project.Models[0].Log.Records.Count, projectRead.Models[0].Log.Records.Count, "Model log records length not as expected");

            for (int i = 0; i < project.Models[0].Log.ValueNames.Length; i++)
            {
                Assert.AreEqual(project.Models[0].Log.ValueNames[i], projectRead.Models[0].Log.ValueNames[i], "Model log value name not as expected at:" + i);
            }

            for (int i = 0; i < project.Models[0].Log.Records.Count; i++)
            {
                Assert.AreEqual(project.Models[0].Log.Records[i].Key, projectRead.Models[0].Log.Records[i].Key, "Model log record key not as expected at:" + i);
                Assert.AreEqual(project.Models[0].Log.Records[i].StdError, projectRead.Models[0].Log.Records[i].StdError, "Model log record StdError not as expected at:" + i);
                Assert.AreEqual(project.Models[0].Log.Records[i].Values.Length, projectRead.Models[0].Log.Records[i].Values.Length, "Model log record values length not as expected at:" + i);

                for (int j = 0; j < project.Models[0].Log.Records[i].Values.Length; j++)
                {
                    Assert.AreEqual(project.Models[0].Log.Records[i].Values[j], projectRead.Models[0].Log.Records[i].Values[j], "Model log record value not as expected at:" + i);
                }
            }
        }
    }
}
