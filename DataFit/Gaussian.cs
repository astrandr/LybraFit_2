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
    public class Gaussian : FitFunction
    {
        public Gaussian()
        {
            this.canInitParameters = true;
            this.name = "Gaussian";
            this.description = "a*Exp(-(x-b)^2/(2*c^2))";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Exp(-(X[i] - P[1]) * (X[i] - P[1]) / (2 * P[2] * P[2]));
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[3];

            double[] lny = new double[X.Length];
            double k = 1;
            for (int i = 0; i < X.Length; i++)
            {
                if (Y[i] < 0)
                {
                    lny[i] = (double)Math.Log(-Y[i]);
                    k = -1;
                }
                else
                {
                    lny[i] = (double)Math.Log(Y[i]);
                }
            }

            FilterData(X, lny);

            double[] Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);

            P[2] = (double)Math.Sqrt(-1.0f / 2 / Pln[2]);
            P[1] = Pln[1] * P[2] * P[2];
            P[0] = (double)Math.Exp(Pln[0] + P[1] * P[1] / P[2] / P[2] / 2)*k;


            return P;
        }
    }
}
