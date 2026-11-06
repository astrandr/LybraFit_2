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
    public class Lorentzian : FitFunction
    {
        public Lorentzian()
        {
            this.canInitParameters = true;
            this.name = "Lorentzian ( Cauchy )";
            this.description = "a/(1+(x-b)^2/c^2)";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] / (1 + (X[i] - P[1]) * (X[i] - P[1]) / P[2] / P[2]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];
            float[] ry = new float[X.Length];


            for (int i = 0; i < X.Length; i++)
            {
                ry[i] = 1 / Y[i];
            }

            FilterData(X, ry);

            float[] Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);

            P[1] = -Pln[1] / 2 / Pln[2];
            P[0] = 1 / (Pln[0] - P[1] * P[1] * Pln[2]);
            P[2] = (float)Math.Sqrt(1 / Pln[2] / P[0]);

            return P;
        }
    }
}
