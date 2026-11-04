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
    public class Hoerl : FitFunction
    {
        public Hoerl()
        {
            this.canInitParameters = true;
            this.name = "Hoerl";
            this.description = "a*b^x*x^c";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * (float)Math.Pow(P[1], X[i]) * (float)Math.Pow(X[i], P[2]);
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];

            float[] lny = new float[X.Length];
            float[] dlny = new float[X.Length - 2];
            float[] rx = new float[X.Length - 2];

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


                for (int i = 1; i < X.Length - 1; i++)
                {
                    float dlnY = (lny[i + 1] - lny[i]) / (X[i + 1] - X[i]);
                    dlny[i - 1] = dlnY;
                    rx[i - 1] = 2 / (X[i] + X[i + 1]);
                }

                FilterData(rx, dlny);

                float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

                P[2] = Pln[1];
                P[1] = (float)Math.Exp(Pln[0]);

                float q = 0;
                int n = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    if (X[i] == 0) continue;
                    q += Y[i] / (float)Math.Pow(P[1], X[i]) / (float)Math.Pow(X[i], P[2]);
                    n++;
                }

                q /= n;

                P[0] = q*k;
            }

            return P;
        }
    }
}