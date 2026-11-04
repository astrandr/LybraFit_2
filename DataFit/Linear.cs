//Designed and Implemented by Andrey Batchvarov 2018
using System;
using MathLib;

namespace FuncLib
{
    [Serializable]
    public class LinearFunction : FitFunction
    {
        public LinearFunction()
        {
            this.canInitParameters = true;
            this.name = "Linear";
            this.description = "a+b*x";
            this.parameters = new string[] { "a", "b" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] + P[1] * X[i];
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            return LinearFit.LinearFitSolveLine(X, Y);
        }
    }
}
