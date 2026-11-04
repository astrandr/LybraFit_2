//Designed and Implemented by Andrey Batchvarov 2018
using FuncLib;
using MathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataFit.Common
{
    [Serializable]
    public class ModelFitInfo
    {
        public string ModelName;

        //[XmlIgnore]
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
            Log.AddRecord(key, LastFitParameters, DerivedValues.Select(d => d.Value).ToArray());
        }

        public bool CanModifyDerivedValues()
        {
            return this.Log == null || this.Log.Records.Count == 0;
        }
    }
}
