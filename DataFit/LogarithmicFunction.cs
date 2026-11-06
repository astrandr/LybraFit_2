//Designed and Implemented by Andrey Batchvarov 2018
using System;
using MathLib;

namespace FuncLib
{
    [Serializable]
    public class LogarithmicFunction : FitFunction
    {
        public LogarithmicFunction()
        {
            this.canInitParameters = true;
            this.name = "Logarithmic";
            this.description = "a*Ln(b*x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Log(P[1] * X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float A = 0;
            float B = 0;
            float C = 0;

            if (X.Length > 2)
            {

                for (int i = 1; i < X.Length - 1; i++)
                {
                    float dy = ((Y[i] - Y[i - 1]) / (X[i] - X[i - 1]) + (Y[i + 1] - Y[i]) / (X[i + 1] - X[i])) / 2;
                    A += (dy * X[i]);
                }
                A /= (X.Length - 2);

                for (int i = 1; i < X.Length - 1; i++)
                {
                    float b = (float)Math.Exp((Y[i]) / A) / X[i];
                    B += b;
                }
                B /= (X.Length - 2);

            }

            return new float[] { A, B };
        }
    }
}

