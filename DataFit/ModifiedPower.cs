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
    public class ModifiedPower : FitFunction
    {
        public ModifiedPower()
        {
            this.canInitParameters = true;
            this.name = "Modified Power";
            this.description = "a*b^x";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Pow(P[1], X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];

            float[] lny = new float[X.Length];

            if (X.Length > 2)
            {
                float k = 1;

                for (int i = 0; i < X.Length; i++)
                {
                    if (Y[i] < 0)
                    {
                        k = -1;
                        lny[i] = (float)Math.Log(-Y[i]);
                    }
                    else
                    {
                        lny[i] = (float)Math.Log(Y[i]);
                    }
                }

                FilterData(X, lny);

                float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

                P[0] = (float)Math.Exp(Pln[0])*k;
                P[1] = (float)Math.Exp(Pln[1]);
            }

            return P;
        }
    }
}
