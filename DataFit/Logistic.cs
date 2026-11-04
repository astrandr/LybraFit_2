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
    public class Logistic : FitFunction
    {
        public Logistic()
        {
            this.canInitParameters = true;
            this.name = "Logistic";
            this.description = "a/(1+b*Exp(-c*x))";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] / (1 + P[1] * (float)Math.Exp(-P[2] * X[i]));
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];

            float[] ry = new float[X.Length];
            float[] dry = new float[X.Length - 2];
            float[] ryy = new float[X.Length - 2];

            if (X.Length > 2)
            {
                for (int i = 0; i < X.Length; i++)
                {
                    ry[i] = 1 / Y[i];//TODO: handle case when  Y[i] is 0
                }


                for (int i = 1; i < X.Length - 1; i++)
                {
                    dry[i - 1] = (ry[i + 1] - ry[i]) / (X[i + 1] - X[i]);
                    ryy[i - 1] = (ry[i] + ry[i + 1]) / 2;
                }


                FilterData(dry, ryy);

                float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

                P[0] = 1 / Pln[0];
                P[2] = -1 / Pln[1];

                float b = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    b = b + (P[0] / Y[i] - 1) * (float)Math.Exp(P[2] * X[i]);
                }

                b = b / X.Length;

                P[0] = 1 / Pln[0];
                P[1] = b;
                P[2] = -1 / Pln[1];




            }

            return P;
        }
    }
}
