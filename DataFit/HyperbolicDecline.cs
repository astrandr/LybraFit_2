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
    public class HyperbolicDecline : FitFunction
    {
        public HyperbolicDecline()
        {
            this.canInitParameters = true;
            this.name = "Hyperbolic Decline";
            this.description = "q*(1+b/a*x)^(-1/b)";
            this.parameters = new string[] { "a", "b", "q" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[2] * (float)Math.Pow(1 + P[1] / P[0] * X[i], -1 / P[1]);
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];

            float[] lny = new float[X.Length];
            float[] dlny = new float[X.Length - 2];
            float[] xx = new float[X.Length - 2];

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
                    dlny[i - 1] = -1 / dlnY;
                    xx[i - 1] = (X[i] + X[i + 1]) / 2;
                }


                FilterData(xx, dlny);

                float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

                P[0] = Pln[0]*k;
                P[1] = Pln[1];

                float q = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    q += Y[i] / (float)Math.Pow(1 + P[1] / P[0] * X[i], -1 / P[1]);
                }

                q /= X.Length;

                P[2] = q;
            }

            return P;
        }
    }
}
