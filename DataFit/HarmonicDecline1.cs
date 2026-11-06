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
    public class HarmonicDecline1 : FitFunction
    {
        public HarmonicDecline1()
        {
            this.canInitParameters = true;
            this.name = "Harmonic Decline 1";
            this.description = "a/(1+b*x)";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] / (1 + P[1] * X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[2];

            if (X.Length > 2)
            {
                List<float> ys = new List<float>();
                List<float> xs = new List<float>();
                for (int i = 0; i < X.Length; i++)
                {
                    if (Y[i] != 0)
                    {
                        ys.Add(Y[i]); xs.Add(X[i]);
                    }
                }
                float[] ry = new float[xs.Count];
                for (int i = 0; i < xs.Count; i++)
                {
                    ry[i] = 1 / ys[i];
                }
                FilterData(xs.ToArray(), ry);

                float[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);
                P[0] = 1 / Pln[0];
                P[1] = P[0] * Pln[1];

            }

            return P;
        }
    }
}
