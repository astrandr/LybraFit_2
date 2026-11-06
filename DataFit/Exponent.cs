//Designed and Implemented by Andrey Batchvarov 2018
using System;
using MathLib;
using System.Collections.Generic;

namespace FuncLib
{
    [Serializable]
    public class ExponentFunction : FitFunction
    {
        public ExponentFunction()
        {
            this.canInitParameters = true;
            this.name = "Exponent";
            this.description = "a*Exp(b*x)+c";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Exp(P[1] * X[i]) + P[2];
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[3];
            double[] dy = new double[X.Length];
            double[] dy2 = new double[X.Length];
            double[] lndy = new double[X.Length];

            int N = X.Length >= 5 ? 5 : X.Length;

            double[] Pln = LinearFit.LinearFitSolve(X, Y, N);

            List<double> xx = new List<double>();
            List<double> yy = new List<double>();
            double k = 1;
            double dyAvrg = 0;
            for (int i = 0; i < X.Length; i++)
            {
                dy[i] = Pln[1] + 2 * Pln[2] * X[i];
                if (N > 3) dy[i] += 3 * Pln[3] * X[i] * X[i];
                if (N > 4) dy[i] += 4 * Pln[4] * X[i] * X[i] * X[i];
                dyAvrg += dy[i];
            }
            dyAvrg /= X.Length;

            if (dyAvrg < 0) k = -1;

            for (int i = 0; i < X.Length; i++)
            {
                if (dy[i] * k > 0)
                {
                    xx.Add(X[i]);
                    yy.Add((float)Math.Log(dy[i] * k));
                }
            }

            double[] pf = LinearFit.LinearFitSolveLine(xx.ToArray(), yy.ToArray());
            P[1] = pf[1];
            P[0] = (float)Math.Exp(pf[0]) / P[1] * k;

            for (int i = 0; i < X.Length; i++)
            {
                P[2] += Y[i] - P[0] * (float)Math.Exp(P[1] * X[i]);
            }

            P[2] /= X.Length;
            return P;
        }
    }
}
