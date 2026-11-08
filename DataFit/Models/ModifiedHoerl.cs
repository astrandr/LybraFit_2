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

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Pow(P[1], 1 / X[i]) * (double)Math.Pow(X[i], P[2]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[3];

            double[] lny = new double[X.Length];
            double[] dlny = new double[X.Length];
            double[] rx = new double[X.Length];

            double k = 1;

            for (int i = 0; i < X.Length; i++)
            {
                if (Y[i] < 0)
                {
                    k = -1;
                    lny[i] = (double)Math.Log(-Y[i]);
                }
                else
                {
                    lny[i] = (double)Math.Log(Y[i]);
                }
            }

            int N = X.Length >= 5 ? 5 : X.Length;

            double[] Pln = LinearFit.LinearFitSolve(X, lny, N);

            List<double> xx = new List<double>();
            List<double> yy = new List<double>();

            for (int i = 0; i < X.Length; i++)
            {
                dlny[i] = Pln[1] + 2 * Pln[2] * X[i];
                if (N > 3) dlny[i] += 3 * Pln[3] * X[i] * X[i];
                if (N > 4) dlny[i] += 4 * Pln[4] * X[i] * X[i] * X[i];
            }

            FilterData(X, dlny);

            Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);
            P[2] = Pln[1];
            P[1] = (double)Math.Exp(-Pln[2]);

            double q = 0;
            int n = 0;
            for (int i = 0; i < X.Length; i++)
            {
                if (X[i] == 0) continue;
                q += Y[i] / (double)Math.Pow(P[1], 1 / X[i]) / (double)Math.Pow(X[i], P[2]);
                n++;
            }

            q /= n;

            P[0] = q*k;


            return P;
        }
    }
}
