//Designed and Implemented by Andrey Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using MathLib;

namespace FuncLib
{
    [Serializable]
    public class CubicFunction : FitFunction
    {
        public CubicFunction()
        {
            this.canInitParameters = true;
            this.name = "Cubic";
            this.description = "a+b*x+c*x^2+d*x^3";
            this.parameters = new string[] { "a", "b", "c", "d" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = ((P[3] * X[i] + P[2]) * X[i] + P[1])*X[i]+P[0];
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);
            return LinearFit.LinearFitSolve(X, Y, 4);
        }
    }
}
