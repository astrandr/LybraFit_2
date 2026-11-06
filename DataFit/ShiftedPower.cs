//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class ShiftedPower : FitFunction
    {
        public ShiftedPower()
        {
            this.canInitParameters = true;
            this.name = "Shifted Power";
            this.description = "a*(x-b)^c";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Pow((X[i] - P[1]), P[2]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];

            float[] lny = new float[X.Length];
            float[] rdlny = new float[X.Length - 1];
            float[] xx = new float[X.Length - 1];
            float a = 0, b, c;

            if (X.Length > 2)
            {
                float k = 1;

                for (int i = 0; i < X.Length; i++)
                {
                    if (Y[i] < 0)
                    {
                        k = -1;
                        lny[i] = (float)Math.Log(-Y[i]);
                    }
                    else
                    {
                        lny[i] = (float)Math.Log(Y[i]);
                    }
                }

                for (int i = 0; i < X.Length - 1; i++)
                {
                    rdlny[i] = (X[i + 1] - X[i]) / (lny[i + 1] - lny[i]); //TODO What if denominator is 0?
                    xx[i] = (X[i + 1] + X[i]) / 2;
                }

                float[] pln = new LinearFunction().InitParameters(xx, rdlny);
                c = 1 / pln[1];
                b = -c * pln[0];

                for (int i = 0; i < X.Length; i++)
                {
                    a += (Y[i] / (float)Math.Pow(X[i] - b, c));
                }
                a /= X.Length;

                P[0] = a*k;
                P[1] = b;
                P[2] = c;
            }
            return P;
        }
    }
}