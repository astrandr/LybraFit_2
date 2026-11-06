//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib
{
    public class FourierTransform
    {
        /// <summary>
        /// GetAB - get Sin and Cos coefficients. This one centers the coordinate origin in the middle of the data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        public static void GetAB(double[] data, out double[] A, out double[] B)
        {
            A = new double[data.Length];
            B = new double[data.Length];
            int N = data.Length;
            double k = 2 * (double)Math.PI / N;

            for (int n = 0; n < N; n++)
            {
                A[n] = 0;
                B[n] = 0;
                for (int i = 0; i < N; i++)
                {
                    A[n] += data[i] * (double)Math.Sin(i * k * n);
                    B[n] += data[i] * (double)Math.Cos(i * k * n);

                }
                A[n] /= (double)Math.PI; A[n] /= N;
                B[n] /= (double)Math.PI; B[n] /= N;
            }

            A[0] /= 2;
        }

        /// <summary>
        /// GetAB2 - get Sin and Cos coefficients. This one does not center the coordinate origin in the middle of the data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        public static void GetAB2(double[] data, out double[] A, out double[] B)
        {
            A = new double[data.Length/2];
            B = new double[data.Length/2];
            int N = data.Length;
            double k = 2 * Math.PI / N;

            for (int n = 0; n < N/2; n++)
            {
                A[n] = 0;
                B[n] = 0;
                for (int i = 0; i < N; i++)
                {
                    double x = i * k;
                    A[n] += data[i] * (double)Math.Sin(x * n);
                    B[n] += data[i] * (double)Math.Cos(x * n);

                }
                //A[n] /= (double)Math.PI; 
                A[n] /= N/2;
                //B[n] /= (double)Math.PI; 
                B[n] /= N/2;
            }
        }


        /// <summary>
        /// GetModulePhase - returns modules and phases of Fourier transform
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Modules"></param>
        /// <param name="Phases"></param>
        public static void GetModulePhase(double[] data, out double[] Modules, out double[] Phases)
        {
            double[] A;
            double[] B;
            Modules = new double[data.Length];
            Phases = new double[data.Length];
            int N = data.Length;
            GetAB(data, out A, out B);

            for(int i = 0; i < N; i++)
            {
                Coordinates.CertesianToPolar(A[i], B[i], out Modules[i], out Phases[i]);
            }
        }

        /// <summary>
        /// GetModulePhase - returns modules and phases of Fourier transform
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Modules"></param>
        /// <param name="Phases"></param>
        public static void GetModulePhase2(double[] data, out double[] Modules, out double[] Phases)
        {
            double[] A;
            double[] B;
            Modules = new double[data.Length/2];
            Phases = new double[data.Length/2];
            int N = Modules.Length;
            GetAB2(data, out A, out B);

            for (int i = 0; i < N; i++)
            {
                Coordinates.CertesianToPolar(A[i], B[i], out Modules[i], out Phases[i]);
            }

            Modules[0] /= 2;
        }

        /// <summary>
        /// GetModulePhase - returns modules and phases of Fourier transform
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Modules"></param>
        /// <param name="Phases"></param>
        public static void GetModulePhaseToCos2(double[] data, out double[] Modules, out double[] Phases)
        {
            double[] A;
            double[] B;
            Modules = new double[data.Length / 2];
            Phases = new double[data.Length / 2];
            int N = Modules.Length;
            GetAB2(data, out A, out B);

            for (int i = 0; i < N; i++)
            {
                Modules[i] = (double)Math.Sqrt(A[i] * A[i] + B[i] * B[i]);
                Coordinates.CertesianToPolar(B[i], A[i], out Modules[i], out Phases[i]);
            }

            Modules[0] /= 2;
        }
    }
}
