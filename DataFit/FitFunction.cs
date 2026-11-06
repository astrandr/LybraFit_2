//Designed and Implemented by Andrey Batchvarov 2018
using MathLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class FitFunction : BaseFunction
    {
        public static float PI = (float)Math.PI;

        protected double[] filteredX, filteredY;

        protected bool canInitParameters = false;

        protected string uniqueDescription; // description which is independent of parameters names and characters

        public string UniqueDescription
        {
            get { return uniqueDescription; }
            set { uniqueDescription = value; }
        }

        protected void CheckNumberOfPointsForInitialization(double[] X)
        {
            if (X.Length < parameters.Length)
            {
                throw new InitializationException(String.Format("At least {0} data points are needed!", parameters.Length));
            }

        }

        protected float Abs(float x) { return (float)Math.Abs(x); }

        public bool CanInitParameters { get { return canInitParameters; } }

        public virtual double[] InitParameters(double[] X, double[] Y) { return null; }

        public void SetUniqueDescription()
        {
            this.uniqueDescription = Parser.GetUniqueExpression(this.description, this.parameters);
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// FilterData - removes values like Infinity and NaN from the data
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="filteredX"></param>
        /// <param name="filteredY"></param>
        public void FilterData(double[] X, double[] Y)
        {
            List<double> fX = new List<double>();
            List<double> fY = new List<double>();

            for (int i = 0; i < X.Length; i++)
            {
                if (!double.IsInfinity(X[i]) && !double.IsInfinity(Y[i]) && !double.IsNaN(X[i]) && !double.IsNaN(Y[i]))
                {
                    fX.Add(X[i]);
                    fY.Add(Y[i]);
                }
            }

            filteredX = fX.ToArray();
            filteredY = fY.ToArray();

        }
    }
}
