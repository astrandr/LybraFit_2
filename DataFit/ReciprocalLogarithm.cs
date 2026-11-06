//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class ReciprocalLogarithm : FitFunction
    {
        public ReciprocalLogarithm()
        {
            this.canInitParameters = true;
            this.name = "Reciprocal Logarithm";
            this.description = "1/(a+b*Ln(x))";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = 1 / (P[0] + P[1] * Math.Log(X[i])); //TODO What if X = 0 or denominator is 0
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];

            float[] ry = new float[X.Length];
            float[] dry = new float[X.Length - 1];

            if (X.Length > 2)
            {
                for (int i = 0; i < X.Length; i++)
                {
                    ry[i] = 1 / Y[i]; //TODO What if Y[i] = 0
                }

                float b = 0;
                for (int i = 1; i < X.Length - 1; i++)
                {
                    dry[i] = (ry[i + 1] - ry[i]) / (X[i + 1] - X[i]);
                    b += (dry[i] * (X[i + 1] + X[i]) / 2); //Take the average X between the two points used in calculation of the derivative
                }
                b /= (X.Length - 1);

                float a = 0;
                for (int i = 1; i < X.Length - 1; i++)
                {
                    a += (ry[i] - b * (float)Math.Log(X[i]));
                }
                a /= (X.Length - 1);

                P[0] = a;
                P[1] = b;
            }
            return P;
        }
    }
}

