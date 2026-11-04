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
    public class ModifiedHoerl : FitFunction
    {
        public ModifiedHoerl()
        {
            this.canInitParameters = true;
            this.name = "Modified Hoerl";
            this.description = "a*b^(1/x)*x^c";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * (float)Math.Pow(P[1], 1 / X[i]) * (float)Math.Pow(X[i], P[2]);
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];

            float[] lny = new float[X.Length];
            float[] dlny = new float[X.Length];
            float[] rx = new float[X.Length];

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

            int N = X.Length >= 5 ? 5 : X.Length;

            float[] Pln = LinearFit.LinearFitSolve(X, lny, N);

            List<float> xx = new List<float>();
            List<float> yy = new List<float>();

            for (int i = 0; i < X.Length; i++)
            {
                dlny[i] = Pln[1] + 2 * Pln[2] * X[i];
                if (N > 3) dlny[i] += 3 * Pln[3] * X[i] * X[i];
                if (N > 4) dlny[i] += 4 * Pln[4] * X[i] * X[i] * X[i];
            }

            FilterData(X, dlny);

            Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);
            P[2] = Pln[1];
            P[1] = (float)Math.Exp(-Pln[2]);

            float q = 0;
            int n = 0;
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] == 0) continue;
                q += Y[i] / (float)Math.Pow(P[1], 1 / X[i]) / (float)Math.Pow(X[i], P[2]);
                n++;
            }

            q /= n;

            P[0] = q*k;


            return P;
        }
    }
}
