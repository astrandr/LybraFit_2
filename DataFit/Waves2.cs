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
    public class Waves2 : FitFunction
    {
        public Waves2()
        {
            this.canInitParameters = true;
            this.name = "Sine Two Waves";
            this.description = "a*Sin(b*x+c)+d*Sin(e*x+f)+g";
            this.parameters = new string[] { "a", "b", "c", "d", "e", "f", "g" };
        }

        public override void CalcProcedure(double[] X, double[] P, ref double[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * Math.Sin(P[1] * X[i] + P[2]) + P[3] * Math.Sin(P[4] * X[i] + P[5]) + P[6];
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
            float[] P = new float[7];
            float[] A, Ph; //Amlitudes and phases of fourier transform
            FourierTransform.GetModulePhase2(Y, out A, out Ph);
            float[] dA = new float[A.Length]; dA[0] = 0;

            //Calculate second derivatives
            for (int i = 1; i < dA.Length - 1; i++)
            {
                dA[i] = (A[i + 1] - A[i]) - (A[i] - A[i - 1]);
            }

            int maxAIndex = 1;
            float maxA = float.MaxValue;
            for (int i = 1; i < A.Length; i++)
            {
                if (dA[i] < 0 && A[i] > A[maxAIndex] && A[i] < maxA)
                {
                    maxAIndex = i;
                }
            }

            P[0] = A[maxAIndex];
            P[2] = Ph[maxAIndex];
            P[1] = (float)maxAIndex / X.Length * 2 * (float)Math.PI / (X[1] - X[0]); //Normalized to data spacing

            int firstMaximaIndex = maxAIndex;
            //find second maxima
            maxAIndex = 1;
            for (int i = 1; i < A.Length; i++)
            {
                if (dA[i] < 0 && A[i] >= A[maxAIndex] && i != firstMaximaIndex)
                {
                    maxAIndex = i;
                }
            }

            P[3] = A[maxAIndex];
            P[5] = Ph[maxAIndex];
            P[4] = (float)maxAIndex / X.Length * 2 * (float)Math.PI / (X[1] - X[0]); //Normalized to data spacing

            P[6] = A[0];
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
            double[] P = new double[7];
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
            float[] A = new float[Re.Length/2];
            float[] dA = new float[Re.Length/2];
            for (int i = 1; i < A.Length; i++)
            {
                A[i] = (float)Math.Sqrt(Re[i] * Re[i] + Im[i] * Im[i]);
            }

            //Calculate second derivatives
            for (int i = 1; i < dA.Length - 1; i++)
            {
                dA[i] = (A[i + 1] - A[i]) - (A[i] - A[i - 1]);
            }

            int maxAmplIndex = 1;
            for (int i = 1; i < A.Length; i++)
            {
                if (A[i] > A[maxAmplIndex])
                {
                    maxAmplIndex = i;
                }
            }

            P[0] = 2 * (float)Math.Sqrt(Re[maxAmplIndex] * Re[maxAmplIndex] + Im[maxAmplIndex] * Im[maxAmplIndex]);
            double period = length / maxAmplIndex * (X[1] - X[0]);
            P[1] = 2 * PI / period; //Normalized to data spacing
            P[2] = Math.Atan2(Re[maxAmplIndex], -Im[maxAmplIndex]);
            P[2] += (X[0] / period) * 2 * PI; // account for the shift
            P[2] = (P[2] - (int)P[2]);

            //Search for second maxima
            int firstMaximaIndex = maxAmplIndex;
            float maxAmpl = float.MinValue;
            for (int i = 1; i < A.Length; i++)
            {
                if (dA[i] < 0 && i != firstMaximaIndex && A[i] > maxAmpl)
                {
                    maxAmplIndex = i;
                    maxAmpl = A[i];
                }
            }

            P[3] = 2 * (float)Math.Sqrt(Re[maxAmplIndex] * Re[maxAmplIndex] + Im[maxAmplIndex] * Im[maxAmplIndex]);
            period = length / maxAmplIndex * (X[1] - X[0]);
            P[4] = 2 * PI / period; //Normalized to data spacing
            P[5] = (float)Math.Atan2(Re[maxAmplIndex], -Im[maxAmplIndex]);
            P[5] += (X[0] / period) * 2 * PI; // account for the shift
            P[5] = (P[2] - (int)P[2]);
            
            P[6] = (float)Math.Sqrt(Re[0] * Re[0] + Im[0] * Im[0]);
            
            return P;
        }

    }
}
