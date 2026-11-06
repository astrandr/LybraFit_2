using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LybraFit.Common;
using System.Xml.Serialization;
using System.IO;
using FuncLib;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Test.Common;

namespace Test
{
    /// <summary>
    /// StructuresTest - tests different structure like ModelFitInfo, ModelLog, etc. for properties and serialization
    /// </summary>
    [TestClass]
    public class StructuresTest
    {
        /// <summary>
        /// CreateModelLog - create new ModelLog,set properties and record and verify them
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void CreateModelLog()
        {
            string[] parametersNames = new string[] { "Param1", "Param2", "Param3" };
            string[] derivedValueNames = new string[] { "ValueName1", "ValueName2", "ValueName3" };
            double[] values = new double[3] { 1, 2, 3 };
            double[] derivedValues = new double[3] { 4, 5, 6 };
            ModelLog log = new ModelLog("test log", parametersNames, derivedValueNames);
            Assert.AreEqual("test log", log.Name, "Invalid log name");
            Assert.AreEqual(0, log.Records.Count, "Invalid number of records");
            log.AddRecord("Record1", values, derivedValues, 0);
            Assert.AreEqual(1, log.Records.Count, "Invalid number of records");
            string expectedIdentifier = "";
            foreach (string n in parametersNames) expectedIdentifier += n + ":";
            foreach (string n in derivedValueNames) expectedIdentifier += n + ":";
            Assert.AreEqual(expectedIdentifier, log.Identifier, "Identifier not as expected");
        }

        /// <summary>
        /// SaveLoadModelLogINXml - tests if ModelLog can be serialized in XML format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadModelLogINXml()
        {
            string[] parametersNames = new string[] { "Param1", "Param2", "Param3" };
            string[] derivedValueNames = new string[] { "ValueName1", "ValueName2", "ValueName3" };
            double[] values = new double[3] { 1, 2, 3 };
            double[] derivedValues = new double[3] { 4, 5, 6 };
            ModelLog log = new ModelLog("test log", parametersNames, derivedValueNames);
            log.AddRecord("Record1", values, derivedValues);

            XmlSerializer s = new XmlSerializer(typeof(ModelLog), new Type[] { typeof(LogRecord) });
            StreamWriter f = File.CreateText(".\\ModelLog.xml");
            s.Serialize(f, log);
            f.Close();

            StreamReader r = File.OpenText(".\\ModelLog.xml");
            ModelLog logRead = (ModelLog)s.Deserialize(r);
            r.Close();

            Assert.AreEqual(log.Name, logRead.Name, "Log name not as expected");
            Assert.AreEqual(log.Records.Count, logRead.Records.Count, "Log records count not as expected");
        }

        /// <summary>
        /// SaveLoadModelFitInfo - tests if ModelFitInfo can be serialized in XML format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadModelFitInfoInXml()
        {
            string[] parametersNames = new string[] { "Param1", "Param2", "Param3" };
            string[] derivedValueNames = new string[] { "ValueName1", "ValueName2", "ValueName3" };
            double[] values = new double[3] { 1, 2, 3 };
            double[] derivedValues = new double[3] { 4, 5, 6 };
            ModelLog log = new ModelLog("test log", parametersNames, derivedValueNames);
            log.AddRecord("Record1", values, derivedValues);

            ModelFitInfo modelFitInfo = new ModelFitInfo();
            string compilerErrors = null;
            modelFitInfo.ModelFunction = UserFunction.Create("Function1", "2*a*x+b", out compilerErrors);
            modelFitInfo.ModelName = "Model1";
            modelFitInfo.Log = log;
            modelFitInfo.DerivedValues = new System.Collections.Generic.List<DerivedValueInfo>();
            modelFitInfo.DerivedValues.Add(new DerivedValueInfo() { Name = "ValueName1", CalcFunction = UserFunction.Create("kFromV", "b/a", out compilerErrors) });

            XmlSerializer s = new XmlSerializer(typeof(ModelFitInfo), new Type[] { typeof(LogRecord), typeof(UserFunction) });
            StreamWriter f = File.CreateText(".\\ModelFitInfo.xml");
            s.Serialize(f, modelFitInfo);
            f.Close();

            StreamReader r = File.OpenText(".\\ModelFitInfo.xml");
            ModelFitInfo modelFitInfoRead = (ModelFitInfo)s.Deserialize(r);
            r.Close();

            Assert.AreEqual(modelFitInfo.ModelName, modelFitInfoRead.ModelName, "Model name not as expected");
            Assert.AreEqual(modelFitInfo.ModelFunction.Name, modelFitInfoRead.ModelFunction.Name, "Function name is not as expected");
            Assert.AreEqual(modelFitInfo.ModelFunction.Description, modelFitInfoRead.ModelFunction.Description, "Function description not as expected");
            Assert.AreEqual(modelFitInfo.DerivedValues.Count, modelFitInfoRead.DerivedValues.Count, "Number of derived values not as expected");
            Assert.AreEqual(modelFitInfo.DerivedValues[0].Name, modelFitInfoRead.DerivedValues[0].Name, "Derived value name not as expected");

            double[] result = new double[3];

            modelFitInfoRead.ModelFunction = UserFunction.Create(modelFitInfoRead.ModelName, modelFitInfoRead.ModelFunction.Description, out compilerErrors);
            Assert.IsNotNull(modelFitInfoRead.ModelFunction, "Cannot recreate model function");
            Assert.IsTrue(string.IsNullOrEmpty(compilerErrors));
            modelFitInfoRead.ModelFunction.CalcProcedure(new double[] { 1, 2, 3 }, new double[] { 1, 2 }, ref result);
            Assert.AreEqual(4, result[0], "Result not calculated as expected. UserFunction is wrong");
            Assert.AreEqual(6, result[1], "Result not calculated as expected. UserFunction is wrong");
            Assert.AreEqual(8, result[2], "Result not calculated as expected. UserFunction is wrong");
        }

        /// <summary>
        /// SaveLoadModelLogInBinary - tests if ModelLog can be serialized in binary format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadModelLogInBinary()
        {
            string[] parametersNames = new string[] { "Param1", "Param2", "Param3" };
            string[] derivedValueNames = new string[] { "ValueName1", "ValueName2", "ValueName3" };
            double[] values = new double[3] { 1, 2, 3 };
            double[] derivedValues = new double[3] { 4, 5, 6 };
            ModelLog log = new ModelLog("test log", parametersNames, derivedValueNames);
            log.AddRecord("Record1", values, derivedValues);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(".\\ModelLog.dat", FileMode.Create);
            formatter.Serialize(f, log);
            f.Close();

            f = new FileStream(".\\ModelLog.dat", FileMode.Open);
            ModelLog logRead = (ModelLog)formatter.Deserialize(f);
            f.Close();

            Assert.AreEqual(log.Name, logRead.Name, "Log name not as expected");
            Assert.AreEqual(log.Records.Count, logRead.Records.Count, "Log records count not as expected");
        }


        /// <summary>
        /// SaveLoadFitFunctionInBinary - tests if FitFunction can be serialized in binary format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadFitFunctionInBinary()
        {

            FitFunction fitFunction = new FitFunction() { Name = "ValueName1", Description = "a*x+b", Parameters = new string[] { "a", "b" } };
            
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(".\\FitFunction.dat", FileMode.Create);
            formatter.Serialize(f, fitFunction);
            f.Close();

            f = new FileStream(".\\FitFunction.dat", FileMode.Open);
            FitFunction fitFunctionRead = (FitFunction)formatter.Deserialize(f);
            f.Close();

            Assert.AreEqual(fitFunction.Name, fitFunctionRead.Name, "FitFunction Name not as expected");
        }

        /// <summary>
        /// SaveLoadDerivedValueInBinary - tests if DerivedValue can be serialized in binary format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadDerivedValueInBinary()
        {

            string compilerErrors;

            DerivedValueInfo derivedValue = new DerivedValueInfo() { Name = "ValueName1" };
            derivedValue.SetCalcFunction("b/a", out compilerErrors);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(".\\DerivedValue.dat", FileMode.Create);
            formatter.Serialize(f, derivedValue);
            f.Close();

            f = new FileStream(".\\DerivedValue.dat", FileMode.Open);
            DerivedValueInfo derivedValueRead = (DerivedValueInfo)formatter.Deserialize(f);
            f.Close();

            derivedValueRead.UpdateValue(new double[] { 1, 2 });
            Assert.AreEqual(0.5f, derivedValueRead.Value, "Result not calculated as expected. UserFunction is wrong");
        }

        /// <summary>
        /// SaveLoadDerivedValuesListInBinary - tests if DerivedValue can be serialized in binary format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadDerivedValuesListInBinary()
        {

            string compilerErrors;
            List<DerivedValueInfo> derivedValues = new List<DerivedValueInfo>();
            DerivedValueInfo derivedValue = new DerivedValueInfo() { Name = "ValueName1" };
            derivedValue.SetCalcFunction("b/a", out compilerErrors);
            derivedValues.Add(derivedValue);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(".\\DerivedValuesList.dat", FileMode.Create);
            formatter.Serialize(f, derivedValues);
            f.Close();

            f = new FileStream(".\\DerivedValuesList.dat", FileMode.Open);
            List<DerivedValueInfo> derivedValuesRead = (List<DerivedValueInfo>)formatter.Deserialize(f);
            f.Close();

            //Verify the calcfunction can be used
            foreach (var dv in derivedValuesRead)
            {
                dv.UpdateValue(new double[] { 1, 2 });
                Assert.AreEqual(0.5f, dv.Value, "Result not calculated as expected. UserFunction is wrong");
            }
        }


        /// <summary>
        /// SaveLoadModelLogInBinary - tests if ModelLog can be serialized in binary format
        /// </summary>
        [TestMethod(), Timeout(BaseTest.TEST_TIMEOUT)]
        public void SaveLoadModelFitInfoInBinary()
        {
            string[] parametersNames = new string[] { "Param1", "Param2", "Param3" };
            string[] derivedValueNames = new string[] { "ValueName1", "ValueName2", "ValueName3" };
            double[] values = new double[3] { 1, 2, 3 };
            double[] derivedValues = new double[3] { 4, 5, 6 };
            ModelLog log = new ModelLog("test log", parametersNames, derivedValueNames);
            log.AddRecord("Record1", values, derivedValues);

            ModelFitInfo modelFitInfo = new ModelFitInfo();
            string compilerErrors = null;
            //modelFitInfo.ModelFunction = new FitFunction() { Name = "ValueName1", Description = "a*x+b", Parameters = new string[] { "a", "b" } }; 
            modelFitInfo.ModelFunction = UserFunction.Create("User Function", "a*x+b", out compilerErrors); 
            modelFitInfo.ModelName = "Model1";
            modelFitInfo.Log = log;
            modelFitInfo.DerivedValues = new System.Collections.Generic.List<DerivedValueInfo>();
            modelFitInfo.DerivedValues.Add(new DerivedValueInfo() { Name = "ValueName1", MathExpression = "b/a"});
            modelFitInfo.DerivedValues[0].SetCalcFunction();

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream f = new FileStream(".\\ModelFitInfo.dat", FileMode.Create);
            formatter.Serialize(f, modelFitInfo);
            f.Close();

            f = new FileStream(".\\ModelFitInfo.dat", FileMode.Open);
            ModelFitInfo modelFitInfoRead = (ModelFitInfo)formatter.Deserialize(f);
            f.Close();

            Assert.AreEqual(modelFitInfo.ModelName, modelFitInfoRead.ModelName, "Model name not as expected");
            Assert.AreEqual(modelFitInfo.ModelFunction.Name, modelFitInfoRead.ModelFunction.Name, "Function name is not as expected");
            Assert.AreEqual(modelFitInfo.ModelFunction.Description, modelFitInfoRead.ModelFunction.Description, "Function description not as expected");
            Assert.AreEqual(modelFitInfo.DerivedValues.Count, modelFitInfoRead.DerivedValues.Count, "Number of derived values not as expected");
            Assert.AreEqual(modelFitInfo.DerivedValues[0].Name, modelFitInfoRead.DerivedValues[0].Name, "Derived value name not as expected");

            double[] result = new double[3];

            Assert.IsNotNull(modelFitInfoRead.ModelFunction, "Model function is not created");

            modelFitInfoRead.ModelFunction.CalcProcedure(new double[] { 1, 2, 3 }, new double[] { 1, 2 }, ref result);
            Assert.AreEqual(3, result[0], "Result not calculated as expected. UserFunction is wrong");
            Assert.AreEqual(4, result[1], "Result not calculated as expected. UserFunction is wrong");
            Assert.AreEqual(5, result[2], "Result not calculated as expected. UserFunction is wrong");

            //Verify the derived values
            //Verify the calcfunction can be used
            foreach (var dv in modelFitInfo.DerivedValues)
            {
                dv.UpdateValue(new double[] { 1, 2 });
                Assert.AreEqual(0.5f, dv.Value, "Result not calculated as expected. UserFunction is wrong");
            }
        }

    }
}
