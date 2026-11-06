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

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = 1 / (P[0] + P[1] * X[i]);
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            double[] P = new double[2];

            double[] ry = new double[X.Length];

            for (int i = 0; i < X.Length; i++)
            {
                ry[i] = 1 / Y[i];
            }

            FilterData(X, ry);

            double[] Pln = LinearFit.LinearFitSolveLine(filteredX, filteredY);

            P[0] = Pln[0];
            P[1] = Pln[1];

            return P;
        }
    }
}