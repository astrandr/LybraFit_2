//Designed and Implemented by Andrey Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using MathLib;

namespace FuncLib
{
    [Serializable]
    public class CosFunction : FitFunction
    {
        public CosFunction()
        {
            this.canInitParameters = true;
            this.name = "Cos";
            this.description = "a*Cos(\u03c9*x+\u03b1)+b";
            this.parameters = new string[] { "a", "\u03c9", "\u03b1", "b" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Cos(P[1] * X[i] + P[2]) + P[3];
            }
        }


        /// <summary>
        /// InitParameters
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public double[] InitParameters2(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);
            double[] P = new double[4];
            double[] A, Ph; //Amlitudes and phases of fourier transform
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
            P[2] = Ph[maxAIndex] - Math.PI / 2;
            P[2] = (P[2] % (2 * Math.PI));
            P[1] = maxAIndex / X.Length * 2 * Math.PI / (X[1] - X[0]); //Normalized to data spacing
            P[3] = A[0];
            return P;
        }

        /// <summary>
        /// InitParameters
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public override double[] InitParameters(double[] X, double[] Y)
        {
            CheckNumberOfPointsForInitialization(X);
            double[] P = new double[4];
            double[] Re = X, Im = Y; //Amlitudes and phases of fourier transform

            //Check if length is of power of 2 and round to closes if needed
            int length = X.Length;
            double l = Math.Log10(length) / Math.Log10(2);
            int m = (int)l;

            length = (int)Math.Pow(2, m);

            Re = new double[length];
            Im = new double[length];

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

            P[0] = 2 * Math.Sqrt(Re[maxAmplIndex] * Re[maxAmplIndex] + Im[maxAmplIndex] * Im[maxAmplIndex]);
            double period = length / maxAmplIndex * (X[1] - X[0]);
            P[1] = 2 * PI / period; //Normalized to data spacing
            P[2] = Math.Atan2(Im[maxAmplIndex], Re[maxAmplIndex])/2/PI;
            P[2] += (X[0] / period); // account for the shift
            P[2] = (P[2] - (int)P[2]) * 2 * PI;
            P[3] = Math.Sqrt(Re[0] * Re[0] + Im[0] * Im[0]);
            return P;
        }

    }
}
