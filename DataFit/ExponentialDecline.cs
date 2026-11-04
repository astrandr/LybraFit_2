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
    public class ExponentialDecline : FitFunction
    {
        public ExponentialDecline()
        {
            this.canInitParameters = true;
            this.name = "Exponential Decline";
            this.description = "a*Exp(-b/x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * (float)Math.Exp(-P[1] / X[i] );
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];

            float[] lny = new float[X.Length];
            float[] rx = new float[X.Length];
            float k = 1;
            for (int i = 0; i < X.Length; i++)
            {
                if (Y[i] >= 0)
                {
                    lny[i] = (float)Math.Log(Y[i]);
                }
                else
                {
                    k = -1;
                    lny[i] = (float)Math.Log(-Y[i]);
                }
                rx[i] = 1 / X[i];
            }

            FilterData(rx, lny);

            if (filteredX.Length == 0 || filteredY.Length == 0 || filteredX.Length != filteredY.Length)
            {
                throw new InvalidDataForInitializationError();
            }

            float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

            P[0] = (float)Math.Exp(Pln[0])*k;
            P[1] = -Pln[1];

            return P;
        }
    }
}
