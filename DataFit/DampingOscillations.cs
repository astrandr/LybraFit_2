//Designed and Implemented by Andrey Batchvarov 2018
using System;
using MathLib;
using System.Collections.Generic;

namespace FuncLib
{
    [Serializable]
    public class DampingOscillations : FitFunction
    {
        public DampingOscillations()
        {
            this.canInitParameters = true;
            this.name = "Damping Oscillations";
            this.description = "a*Exp(-\u03b2*x)*Sin(\u03c9*x+d)+e";
            this.parameters = new string[] { "a", "\u03b2", "\u03c9", "d", "e" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * (float)Math.Exp(-P[1] * X[i]) * (float)Math.Sin(P[2] * X[i] + P[3]) + P[4];
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
            float PI = (float)Math.PI;
            float[] P = new float[5];
            P = new SinFunction().InitParameters(X, Y);

            return new float[] { P[0], 0, P[1], P[2], P[3] };
        }
    }
}
