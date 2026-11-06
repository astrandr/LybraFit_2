//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class Geometric : FitFunction
    {
        public Geometric()
        {
            this.canInitParameters = true;
            this.name = "Geometric";
            this.description = "a*X^(b*X)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Pow(X[i], P[1] * X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[2];

            double[] lny = new double[X.Length];

            if (X.Length > 2)
            {
                //Select only positive values
                List<double> ys = new List<double>();
                List<double> xs = new List<double>();
                for (int i = 0; i < X.Length; i++)
                {
                    if (Y[i] > 0 && X[i] > 0)
                    {
                        ys.Add(Y[i]); xs.Add(X[i]);
                    }
                }
                for (int i = 0; i < xs.Count; i++)
                {
                    lny[i] = (double)Math.Log(ys[i]);
                }

                double b = 0;
                for (int i = 1; i < xs.Count - 1; i++)
                {
                    double dlny = ((lny[i] - lny[i - 1]) / (xs[i] - xs[i - 1]) + (lny[i + 1] - lny[i]) / (xs[i + 1] - xs[i])) / 2;
                    b += dlny / (1 + (double)Math.Log(xs[i]));
                }
                b /= (xs.Count - 2);

                double a = 0;
                for (int i = 0; i < xs.Count - 1; i++)
                {
                    a += ys[i] / ((double)Math.Pow(xs[i], b * xs[i]));
                }
                a /= (xs.Count - 1);


                P[0] = a;
                P[1] = b;

            }

            return P;
        }
    }
}
