//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class RatkowskyModel : FitFunction
    {
        public RatkowskyModel()
        {
            this.canInitParameters = true;
            this.name = "Ratkowsky Model";
            this.description = "a/(1+Exp(b-c*x))";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] / (1 + (double)Math.Exp(P[1] - X[i] * P[2]));
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[3];

            double[] ry = new double[X.Length];
            double[] dry = new double[X.Length - 1];
            double[] xx = new double[X.Length - 1];
            double a = 0, b = 0, c;

            if (X.Length > 2)
            {
                for (int i = 0; i < X.Length; i++)
                {
                    ry[i] = 1 / Y[i];
                }

                for (int i = 0; i < X.Length - 1; i++)
                {
                    dry[i] = (ry[i + 1] - ry[i]) / (X[i + 1] - X[i]);
                    xx[i] = (ry[i + 1] + ry[i]) / 2;
                }

                double[] pln = new LinearFunction().InitParameters(xx, dry);
                c = -pln[1];
                a = c / pln[0];

                for (int i = 0; i < X.Length; i++)
                {
                    b += ((double)Math.Log(a / Y[i] - 1) + c * X[i]);
                }
                b /= X.Length;

                P[0] = a;
                P[1] = b;
                P[2] = c;
            }
            return P;
        }
    }
}
