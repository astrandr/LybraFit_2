//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class Root : FitFunction
    {
        public Root()
        {
            this.canInitParameters = true;
            this.name = "Root";
            this.description = "a*b^(1/x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Pow(P[1], 1 / X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];

            float[] lny = new float[X.Length];
            float[] xx = new float[X.Length];

            for (int i = 0; i < X.Length; i++)
            {
                lny[i] = (float)Math.Log(Y[i]); //TODO What if Y[i] <= 0
                xx[i] = 1 / X[i]; //TODO what X is 0
            }

            float[] pln = new LinearFunction().InitParameters(xx, lny);

            P[0] = (float)Math.Exp(pln[0]);
            P[1] = (float)Math.Exp(pln[1]);

            return P;
        }
    }
}
