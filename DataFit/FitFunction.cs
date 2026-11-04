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

        protected float[] filteredX, filteredY;

        protected bool canInitParameters = false;

        protected string uniqueDescription; // description which is independent of parameters names and characters

        public string UniqueDescription
        {
            get { return uniqueDescription; }
            set { uniqueDescription = value; }
        }

        protected void CheckNumberOfPointsForInitialization(float[] X)
        {
            if (X.Length < parameters.Length)
            {
                throw new InitializationException(String.Format("At least {0} data points are needed!", parameters.Length));
            }

        }

        protected float Abs(float x) { return (float)Math.Abs(x); }

        public bool CanInitParameters { get { return canInitParameters; } }

        public virtual float[] InitParameters(float[] X, float[] Y) { return null; }

        public void SetUniqueDescription()
        {
            this.uniqueDescription = Parser.GetUniqueExpression(this.description, this.parameters);
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
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
        public void FilterData(float[] X, float[] Y)
        {
            List<float> fX = new List<float>();
            List<float> fY = new List<float>();

            for (int i = 0; i < X.Length; i++)
            {
                if (!float.IsInfinity(X[i]) && !float.IsInfinity(Y[i]) && !float.IsNaN(X[i]) && !float.IsNaN(Y[i]))
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
