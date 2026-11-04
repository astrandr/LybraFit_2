//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using FuncLib;
using LybraFit.Forms;
using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LybraFit.Common
{
    [Serializable()]
    public class DerivedValueInfo : ISerializable
    {
        [NonSerialized]
        public UserFunction CalcFunction;

        private string mathExpression;

        public string MathExpression
        {
            get { return mathExpression; }
            set { mathExpression = value; }
        }

        public string Name;

        public float Value;

        public float Sigma;

        public DerivedValueInfo()
        {

        }

        public static DerivedValueInfo Create(string name, string mathExpression, out string errors)
        {
            DerivedValueInfo derivedValueInfo = null;
            errors = null;
            try
            {
                UserFunction function = UserFunction.Create(name + "Calc", mathExpression, out errors);
                if (function != null)
                {
                    derivedValueInfo = new DerivedValueInfo { Name = name, CalcFunction = function, Value = 0 };
                }
            }
            catch
            {
                InfoBox.ShowMessage(new string[] { "Failed to recreate function for derived value:", name }, "Error", null);
            }

            return derivedValueInfo;

        }


        private void PostSerializationProcess(StreamingContext context)
        {
            string errors = null;
            try
            {
                UserFunction function = UserFunction.Create(Name + "Calc", mathExpression, out errors);
                if (function != null)
                {
                    this.CalcFunction = function;
                }
            }
            catch
            {
                InfoBox.ShowMessage(new string[] { "Failed to recreate function for derived value:", Name }, "Error", null);
            }
        }

        public bool SetCalcFunction()
        {
            string compilerErrors;
            return SetCalcFunction(mathExpression, out compilerErrors);
        }

        public bool SetCalcFunction(string mathExpression, out string errors)
        {
            errors = null;
            try
            {
                UserFunction function = UserFunction.Create(Name + "Calc", mathExpression, out errors);
                if (function != null)
                {
                    this.CalcFunction = function;
                    this.mathExpression = mathExpression;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void UpdateValue(float[] pv, float[] sigmas = null)
        {
            Value = CalcFunction.Calculate(0, pv);
            float ds = 0;

            if (sigmas != null)
            {
                for (int i = 0; i < sigmas.Length; i++)
                {
                    pv[i] += sigmas[i];
                    float dv = CalcFunction.Calculate(0, pv) - Value;
                    pv[i] -= sigmas[i];
                    ds += dv * dv;
                }

                Sigma = (float)Math.Sqrt(ds);
            }
            else
            {
                Sigma = 0;
            }
        }

        public DerivedValueInfo(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("Name");
            mathExpression = info.GetString("MathExpression");
            Value = (float)info.GetValue("Value", typeof(float));
            Sigma = (float)info.GetValue("Sigma", typeof(float));
            PostSerializationProcess(context);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("DerivedValue_Format_Version", 1);
            info.AddValue("Name", Name);
            info.AddValue("MathExpression", MathExpression);
            info.AddValue("Value", Value);
            info.AddValue("Sigma", Sigma);
        }
    }
}
