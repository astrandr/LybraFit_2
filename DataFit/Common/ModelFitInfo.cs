//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using LybraFit.Forms;
using FuncLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LybraFit.Common
{
    [Serializable]
    public class ModelFitInfo : ISerializable
    {
        public string ModelName;

        //These methods are used for binary serialization
        private string modelFunctionName;
        private string modelFunctionDescription;
        private string[] modelFunctionParameters;
        private bool isUserFunction;

        [NonSerialized]
        public FitFunction ModelFunction;

        [XmlIgnore]
        public bool IsInitialized = false;

        public float[] LastFitParameters;
        public float[] SigmaParameters;
        public float[] SigmaY;
        public float FitResultStdError;
        public float FitResultChiSquare;
        public List<DerivedValueInfo> DerivedValues;
        public ModelLog Log = null;
        public ModelFitInfo() { }

        public void LogFitResults(string key)
        {
            if (Log == null)
            {
                if (DerivedValues == null)
                {
                    DerivedValues = new List<DerivedValueInfo>();
                }
                Log = new ModelLog(ModelName, ModelFunction.Parameters, DerivedValues.Select(d => d.Name).ToArray());
            }

            DerivedValues.ForEach(d => d.UpdateValue(LastFitParameters, SigmaParameters));
            Log.AddRecord(key, LastFitParameters, DerivedValues.Select(d => d.Value).ToArray(), FitResultStdError);
        }

        public bool CanModifyDerivedValues()
        {
            return this.Log == null || this.Log.Records.Count == 0;
        }

        [OnSerializing()]
        internal void PrepareForSerialization(StreamingContext context)
        {
            modelFunctionName = ModelFunction.Name;
            modelFunctionDescription = ModelFunction.Description;
            modelFunctionParameters = ModelFunction.Parameters;
            isUserFunction = ModelFunction is UserFunction;
        }

        [OnDeserialized()]
        internal void PostSerializationProcess(StreamingContext context)
        {
            if (isUserFunction)
            {
                string compileErrors;
                try
                {
                    ModelFunction = UserFunction.Create(modelFunctionName, modelFunctionDescription, out compileErrors);
                }
                catch (Exception)
                {
                    InfoBox.ShowMessage(new string[] { "Failed to recreate function:", modelFunctionName }, "Error", null);
                }
            }
        }

        public ModelFitInfo(SerializationInfo info, StreamingContext context)
        {
            ModelName = info.GetString("ModelName");
            modelFunctionName = info.GetString("modelFunctionName");
            modelFunctionDescription = info.GetString("modelFunctionDescription");
            modelFunctionParameters = (string[])info.GetValue("modelFunctionParameters", typeof(string[]));
            isUserFunction = info.GetBoolean("isUserFunction");
            LastFitParameters = (float[])info.GetValue("LastFitParameters", typeof(float[]));
            SigmaParameters = (float[])info.GetValue("SigmaParameters", typeof(float[]));
            SigmaY = (float[])info.GetValue("SigmaY", typeof(float[]));
            FitResultStdError = (float)info.GetValue("FitResultStdError", typeof(float));
            FitResultChiSquare = (float)info.GetValue("FitResultStdError", typeof(float));
            DerivedValues = (List<DerivedValueInfo>)info.GetValue("DerivedValues", typeof(List<DerivedValueInfo>));
            Log = (ModelLog)info.GetValue("Log", typeof(ModelLog));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Model_Format_Version", 1);
            info.AddValue("ModelName", ModelName);
            info.AddValue("modelFunctionName", modelFunctionName);
            info.AddValue("modelFunctionDescription", modelFunctionDescription);
            info.AddValue("modelFunctionParameters", modelFunctionParameters);
            info.AddValue("isUserFunction", isUserFunction);
            info.AddValue("LastFitParameters", LastFitParameters);
            info.AddValue("SigmaParameters", SigmaParameters);
            info.AddValue("SigmaY", SigmaY);
            info.AddValue("FitResultStdError", FitResultStdError);
            info.AddValue("FitResultChiSquare", FitResultChiSquare);
            info.AddValue("DerivedValues", DerivedValues);
            info.AddValue("Log", Log);
        }
    }
}
