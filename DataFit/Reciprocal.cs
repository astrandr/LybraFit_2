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
    public class Reciprocal : FitFunction
    {
        public Reciprocal()
        {
            this.canInitParameters = true;
            this.name = "Reciprocal";
            this.description = "1/(a+b*x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = 1 / (P[0] + P[1] * X[i]);
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];

            float[] ry = new float[X.Length];

            for (int i = 0; i < X.Length; i++)
            {
                ry[i] = 1 / Y[i];
            }

            FilterData(X, ry);

            float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

            P[0] = Pln[0];
            P[1] = Pln[1];

            return P;
        }
    }
}