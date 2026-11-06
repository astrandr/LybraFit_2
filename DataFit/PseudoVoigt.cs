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
    public class PseudoVoigt : FitFunction
    {
        public PseudoVoigt()
        {
            this.canInitParameters = true;
            this.name = "Pseudo-Voigt";
            this.description = "a*Exp(-(x-b)^2/(2*c^2))+d/(1+(x-e)^2/f^2)";
            this.parameters = new string[] { "a", "b", "c", "d", "e", "f" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Exp(-(X[i] - P[1]) * (X[i] - P[1]) / (2 * P[2] * P[2])) + P[3] / (1 + (X[i] - P[4]) * (X[i] - P[4]) / P[5] / P[5]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] PG = new float[3];

            //Init prameters of the Gaussian
            float[] lny = new float[X.Length];
            float k = 1;
            for (int i = 0; i < X.Length; i++)
            {
                if (Y[i] < 0)
                {
                    lny[i] = (float)Math.Log(-Y[i]);
                    k = -1;
                }
                else
                {
                    lny[i] = (float)Math.Log(Y[i]);
                }
            }

            FilterData(X, lny);

            float[] Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);

            PG[2] = (float)Math.Sqrt(-1.0f / 2 / Pln[2]);
            PG[1] = Pln[1] * PG[2] * PG[2];
            PG[0] = (float)Math.Exp(Pln[0] + PG[1] * PG[1] / PG[2] / PG[2] / 2) * k;


            //Init parameters for the Lorentzian
            float[] PL = new float[3];
            float[] ry = new float[X.Length];


            for (int i = 0; i < X.Length; i++)
            {
                ry[i] = 1 / Y[i];
            }

            FilterData(X, ry);

            Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);

            PL[1] = -Pln[1] / 2 / Pln[2];
            PL[0] = 1 / (Pln[0] - PL[1] * PL[1] * Pln[2]);
            PL[2] = (float)Math.Sqrt(1 / Pln[2] / PL[0]);

            float[] P = new float[6];

            //Take half Gaussian and half Lorentzian
            P[0] = PG[0] / 2;
            P[1] = PG[1];
            P[2] = PG[2];

            P[3] = PL[0] / 2;
            P[4] = PL[1];
            P[5] = PL[2];
            return P;
        }
    }
}
