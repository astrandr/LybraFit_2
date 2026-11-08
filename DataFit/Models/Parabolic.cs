//Designed and Implemented by Andrey Batchvarov 2018
using System;
using MathLib;

namespace FuncLib
{
    [Serializable]
    public class ParabolicFunction : FitFunction
    {
        public ParabolicFunction()
        {
            this.canInitParameters = true;
            this.name = "Parabolic";
            this.description = "a+b*x+c*x^2";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = (P[2] * X[i] + P[1]) * X[i] + P[0];
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            return LinearFit.LinearFitSolve(X, Y, 3);
        }
    }
}
