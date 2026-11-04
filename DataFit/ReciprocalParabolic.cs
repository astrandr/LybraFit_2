//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    [Serializable]
    public class ReciprocalParabolic : FitFunction
    {
        public ReciprocalParabolic()
        {
            this.canInitParameters = true;
            this.name = "Reciprocal Parabolic";
            this.description = "1/(a+b*x+c*x^2)";
            this.parameters = new string[] { "a", "b", "c" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = 1 / (P[0] + P[1] * X[i] + P[2] * X[i] * X[i]); //TODO What if denominator is 0
            }
        }

        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            float[] P = new float[3];

            float[] ry = new float[X.Length];


            for (int i = 0; i < X.Length; i++)
            {
                ry[i] = 1 / Y[i]; //TODO What if Y[i] = 0
            }

            return new ParabolicFunction().InitParameters(X, ry);
        }
    }
}

