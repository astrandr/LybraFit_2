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
    public class ReciprocalYield : FitFunction
    {
        public ReciprocalYield()
        {
            this.canInitParameters = true;
            this.name = "Reciprocal Yield";
            this.description = "x/(a+b*x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = X[i] / (P[0] + P[1] * X[i]);
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];
            float[] rdy = new float[X.Length - 1];
            float[] xx = new float[X.Length - 1];


            for (int i = 0; i < X.Length - 1; i++)
            {
                rdy[i] = (X[i + 1] - X[i]) / (Y[i + 1] - Y[i]);
                xx[i] = X[i];
            }
            FilterData(xx, rdy);

            float[] Pln = LinearFit.LinearFitSolve(filteredX, filteredY, 3);
            P[0] = Pln[0];
            P[1] = Pln[1] / 2;

            return P;
        }
    }
}