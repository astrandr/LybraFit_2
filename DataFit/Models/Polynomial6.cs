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
    public class Polynomial6Func : FitFunction
    {
        public Polynomial6Func()
        {
            this.canInitParameters = true;
            this.name = "Polynomial 6";
            this.description = "a+b*x+c*x^2+d*x^3+e*x^4+f*x^5+g*x^6";
            this.parameters = new string[] { "a", "b", "c", "d", "e", "f", "g" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = (((((P[6] * X[i] + P[5]) * X[i] + P[4]) * X[i] + P[3]) * X[i] + P[2]) * X[i] + P[1]) * X[i] + P[0];
            }
        }

        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            return LinearFit.LinearFitSolve(X, Y, 7);
        }
    }
}
