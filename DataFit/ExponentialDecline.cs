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

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Exp(-P[1] / X[i] );
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[2];

            double[] lny = new double[X.Length];
            double[] rx = new double[X.Length];
            double k = 1;
            for (int i = 0; i < X.Length; i++)
            {
                if (Y[i] >= 0)
                {
                    lny[i] = (double)Math.Log(Y[i]);
                }
                else
                {
                    k = -1;
                    lny[i] = (double)Math.Log(-Y[i]);
                }
                rx[i] = 1 / X[i];
            }

            FilterData(rx, lny);

            if (filteredX.Length == 0 || filteredY.Length == 0 || filteredX.Length != filteredY.Length)
            {
                throw new InvalidDataForInitializationError();
            }

            double[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

            P[0] = (double)Math.Exp(Pln[0])*k;
            P[1] = -Pln[1];

            return P;
        }
    }
}
