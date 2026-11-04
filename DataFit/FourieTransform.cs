using System;
using System.Collections.Generic;
using System.Text;

namespace MathLib
{
    public class Transform
    {
        /// <summary>
        /// DFT - One dimensional Discrete Fourier transform
        /// </summary>
        /// <param name="y">the function values</param>
        /// <param name="a">out Cosine Fourier coefficients</param>
        /// <param name="b">out Sine Fourier coefficients</param>
        public static void DFT(double[] y, out double[] a, out double[] b)
        {
            int N = y.Length;
            //We can create only N/2 Fourier coefficients because of information available is N and is used 
            //for both sin and cos coefficients
            a = new double[N/2];
            b = new double[N/2];
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = 0; b[i] = 0;
                for (int j = 0; j < y.Length; j++)
                {
                    double f = (double)(j * i) / (double)N;
                    a[i] += y[j] * Math.Cos(2 * Math.PI * f);
                    b[i] += y[j] * Math.Sin(2 * Math.PI * f);
                }
                a[i] = a[i] * 2 / N;
                b[i] = b[i] * 2 / N;
            }
        }

        /// <summary>
        ///  DFTInv - Inverse One dimensional Discrete Fourier transform
        /// </summary>
        /// <param name="y">Ouput funcion values</param>
        /// <param name="a">Cosine Fourier coefficients</param>
        /// <param name="b">Sine Fourier coefficients</param>
        public static void DFTInv(out double[] y, double[] a, double[] b)
        {
            //From cos and sin coefficient we should be able to restore the 2N points in inverse Fourier transform
            int N = 2*a.Length;
            y = new double[N];
            for (int i = 0; i < y.Length; i++)
            {
                y[i] = a[0]/2;
                for (int j = 1; j < a.Length; j++)
                {
                    int n = j;
                    double f = 2 * Math.PI * (double)n * i / (double)N;
                    y[i] += (a[j] * Math.Cos(f)+ b[j] * Math.Sin(f));
                }
            }
        }
    }
}
