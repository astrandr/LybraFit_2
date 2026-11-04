using FuncLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataFit.Common
{
    [Serializable]
    public class DerivedValueInfo 
    {
        public string Name { get; set; }

        public UserFunction CalcFunction { get; set; }

        private string s;

        [XmlIgnore]
        public string MathExpression { get { return CalcFunction.Description; } set { s = value; } }

        public float Value { get; set; }

        public float Sigma { get; set; }

        //TODO Add method here to recalculate the value based on parameters and refactor the code in the app accordingly

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
                //TODO Add logging or some other action here
            }

            return derivedValueInfo;

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

    }
}
