//Designed and Implemented by Andrey Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;

namespace FuncLib
{
    [Serializable]
    public class BeatingWave : FitFunction
    {
        public BeatingWave()
        {
            this.canInitParameters = true;
            this.name = "Beating Wave";
            this.description = "a*Sin(b*x+c)*Cos(d*x+e)+f";
            this.parameters = new string[] { "a", "b", "c", "d", "e", "f" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * (float)Math.Sin(P[1] * X[i] + P[2]) * (float)Math.Cos(P[3] * X[i] + P[4]) + P[5];
            }
        }

        /// <summary>
        /// InitParameters
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public override float[] InitParameters(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);

            //we use here 2*Sin(A)*Cos(B) = Sin(A+B)+Sin(A-B)
            Waves2 waves2 = new Waves2();
            float[] pW = waves2.InitParameters(X, Y);
            float AB1 = pW[1], AB2 = pW[4]; //AB1 = A+B, AB2 = A-B;
            float[] p = new float[6];

            //The following code assumes frequencies and phases are positive numbers. 
            //The uncertainty comes from the fact that beating wave is represented as sum of two sine wave which is symmetrical expression
            p[0] = pW[0] + pW[3];
            p[1] = (pW[1] + pW[4]) / 2;
            p[2] = (pW[2] + pW[5]) / 2;
            p[3] = (pW[1] - pW[4]) / 2; if (p[3] < 0) p[3] = -p[3];
            p[4] = (pW[2] - pW[5]) / 2; if (p[4] < 0) p[4] = -p[4];
            p[5] = pW[6];

            return p;
        }
    }
}
