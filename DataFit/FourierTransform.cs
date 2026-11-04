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
        public static void GetAB(float[] data, out float[] A, out float[] B)
        {
            A = new float[data.Length];
            B = new float[data.Length];
            int N = data.Length;
            float k = 2 * (float)Math.PI / N;

            for (int n = 0; n < N; n++)
            {
                A[n] = 0;
                B[n] = 0;
                for (int i = 0; i < N; i++)
                {
                    A[n] += data[i] * (float)Math.Sin(i * k * n);
                    B[n] += data[i] * (float)Math.Cos(i * k * n);

                }
                A[n] /= (float)Math.PI; A[n] /= N;
                B[n] /= (float)Math.PI; B[n] /= N;
            }

            A[0] /= 2;
        }

        /// <summary>
        /// GetAB2 - get Sin and Cos coefficients. This one does not center the coordinate origin in the middle of the data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        public static void GetAB2(float[] data, out float[] A, out float[] B)
        {
            A = new float[data.Length/2];
            B = new float[data.Length/2];
            int N = data.Length;
            float k = 2 * (float)Math.PI / N;

            for (int n = 0; n < N/2; n++)
            {
                A[n] = 0;
                B[n] = 0;
                for (int i = 0; i < N; i++)
                {
                    float x = i * k;
                    A[n] += data[i] * (float)Math.Sin(x * n);
                    B[n] += data[i] * (float)Math.Cos(x * n);

                }
                //A[n] /= (float)Math.PI; 
                A[n] /= N/2;
                //B[n] /= (float)Math.PI; 
                B[n] /= N/2;
            }
        }


        /// <summary>
        /// GetModulePhase - returns modules and phases of Fourier transform
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Modules"></param>
        /// <param name="Phases"></param>
        public static void GetModulePhase(float[] data, out float[] Modules, out float[] Phases)
        {
            float[] A;
            float[] B;
            Modules = new float[data.Length];
            Phases = new float[data.Length];
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
        public static void GetModulePhase2(float[] data, out float[] Modules, out float[] Phases)
        {
            float[] A;
            float[] B;
            Modules = new float[data.Length/2];
            Phases = new float[data.Length/2];
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
        public static void GetModulePhaseToCos2(float[] data, out float[] Modules, out float[] Phases)
        {
            float[] A;
            float[] B;
            Modules = new float[data.Length / 2];
            Phases = new float[data.Length / 2];
            int N = Modules.Length;
            GetAB2(data, out A, out B);

            for (int i = 0; i < N; i++)
            {
                Modules[i] = (float)Math.Sqrt(A[i] * A[i] + B[i] * B[i]);
                Coordinates.CertesianToPolar(B[i], A[i], out Modules[i], out Phases[i]);
            }

            Modules[0] /= 2;
        }
    }
}
