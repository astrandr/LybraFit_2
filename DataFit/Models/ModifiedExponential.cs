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
    public class ReciprocalExponential : FitFunction
    {
        public ReciprocalExponential()
        {
            this.canInitParameters = true;
            this.name = "Reciprocal Exponential";
            this.description = "a*Exp(b/x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Exp(P[1] / X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[2];

            if (X.Length > 2)
            {
                double k = 1;
                List<double> ys = new List<double>();
                List<double> xs = new List<double>();
                for (int i = 0; i < X.Length; i++)
                {
                    if (Y[i] > 0)
                    {
                        ys.Add(Y[i]); xs.Add(X[i]);
                    }
                    else
                    {
                        ys.Add(-Y[i]); xs.Add(X[i]); k = -1;
                    }
                }

                double[] rx = new double[xs.Count];
                double[] lny = new double[ys.Count];
                for (int i = 0; i < xs.Count; i++)
                {
                    lny[i] = (double)Math.Log(ys[i]);
                    rx[i] = 1 / X[i];
                }

                FilterData(rx, lny);
                double[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);
                P[0] = (double)Math.Exp(Pln[0])*k;
                P[1] = Pln[1];

            }

            return P;
        }
    }
}
