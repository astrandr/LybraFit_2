//Designed and Implemented by Andrey Batchvarov 2018
using System;
using MathLib;
using System.Collections.Generic;

namespace FuncLib
{
    [Serializable]
    public class SinPeriod : FitFunction
    {
        public SinPeriod()
        {
            this.canInitParameters = true;
            this.name = "Sin (period)";
            this.description = "a*Sin(2*pi/T*x+\u03b1)+b";
            this.parameters = new string[] { "a", "T", "\u03b1", "b" };
        }

        public override void CalcProcedure(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * (float)Math.Sin(2 * PI / P[1] * X[i] + P[2]) + P[3];
            }
        }

        /// <summary>
        /// InitParameters
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public float[] InitParameters2(float[] X, float[] Y)
        {
            CheckNumberOfPointsForInitialization(X);
            float[] P = new float[4];
            float[] A, Ph; //Amlitudes and phases of fourier transform
            FourierTransform.GetModulePhase2(Y, out A, out Ph);

            int maxAIndex = 1;
            for (int i = 1; i < A.Length; i++)
            {
                if (A[i] > A[maxAIndex])
                {
                    maxAIndex = i;
                }
            }

            P[0] = A[maxAIndex];
            P[2] = Ph[maxAIndex];
            P[1] = X.Length * (X[1] - X[0]) / (float)maxAIndex; //Normalized to data spacing
            P[3] = A[0];
            return P;
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
            float[] P = new float[4];
            float[] Re = X, Im = Y; //Amlitudes and phases of fourier transform

            //Check if length is of power of 2 and round to closes if needed
            int length = X.Length;
            double l = Math.Log10(length) / Math.Log10(2);
            int m = (int)l;

            length = (int)Math.Pow(2, m);

            Re = new float[length];
            Im = new float[length];

            Array.Copy(Y, Re, Re.Length);

            FFTLibrary.Complex.FFT(1, m, Re, Im);

            int maxAmplIndex = 1;
            for (int i = 1; i < Re.Length / 2; i++)
            {
                if (Math.Sqrt(Re[i] * Re[i] + Im[i] * Im[i]) > Math.Sqrt(Re[maxAmplIndex] * Re[maxAmplIndex] + Im[maxAmplIndex] * Im[maxAmplIndex]))
                {
                    maxAmplIndex = i;
                }
            }

            P[0] = 2 * (float)Math.Sqrt(Re[maxAmplIndex] * Re[maxAmplIndex] + Im[maxAmplIndex] * Im[maxAmplIndex]);
            float period = length / maxAmplIndex * (X[X.Length - 1] - X[0]) / (X.Length - 1);
            P[1] = period; //Normalized to data spacing
            P[2] = (float)Math.Atan2(Re[maxAmplIndex], -Im[maxAmplIndex]) / 2 / PI;
            P[2] += (X[0] / period); // account for the shift
            P[2] = (P[2] - (int)P[2]) * 2 * PI;
            P[3] = (float)Math.Sqrt(Re[0] * Re[0] + Im[0] * Im[0]);
            return P;
        }

    }
}
