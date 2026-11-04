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
    public class Polynomial4Func : FitFunction
    {
        public Polynomial4Func()
        {
            this.canInitParameters = true;
            this.name = "Polynomial 4";
            this.description = "a+b*x+c*x^2+d*x^3+e*x^4";
            this.parameters = new string[] { "a", "b", "c", "d", "e" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = (((P[4] * X[i] + P[3]) * X[i] + P[2]) * X[i] + P[1]) * X[i] + P[0];
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            return LinearFit.DLinearFitSolve(X, Y, 5);
        }
    }
}
