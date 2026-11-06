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

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Pow(P[1], X[i]) * Math.Pow(X[i], P[2]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[3];

            double[] lny = new double[X.Length];
            double[] dlny = new double[X.Length - 2];
            double[] rx = new double[X.Length - 2];

            if (X.Length > 2)
            {
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


                for (int i = 1; i < X.Length - 1; i++)
                {
                    double dlnY = (lny[i + 1] - lny[i]) / (X[i + 1] - X[i]);
                    dlny[i - 1] = dlnY;
                    rx[i - 1] = 2 / (X[i] + X[i + 1]);
                }

                FilterData(rx, dlny);

                double[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

                P[2] = Pln[1];
                P[1] = (double)Math.Exp(Pln[0]);

                double q = 0;
                int n = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    if (X[i] == 0) continue;
                    q += Y[i] / (double)Math.Pow(P[1], X[i]) / (double)Math.Pow(X[i], P[2]);
                    n++;
                }

                q /= n;

                P[0] = q*k;
            }

            return P;
        }
    }
}