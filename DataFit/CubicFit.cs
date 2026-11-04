//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;

namespace MathLib
{
    /// <summary>
    /// CubicFit - Least square approximation to cubic function y = a*x^2+b*x+c
    /// </summary>
    public class CubicFit
    {

        private static void SimpleCubicFunc(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = (P[0] * X[i] + P[1])*X[i]+P[2];
            }
        }

        /// <summary>
        /// Optimize - refines a,b and c parameters to least square fit of provided data X and Y
        /// </summary>
        /// <param name="X">X data float[]</param>
        /// <param name="Y">Y data float[]</param>
        /// <param name="a">initial and output value of 'a' parameter</param>
        /// <param name="b">initial and output value of 'b' parameter</param>
        /// <param name="c">initial and output value of 'c' parameter</param>
        public static void Optimize(float[] X, float[] Y, ref float a, ref float b, ref float c, float dampingFactor = 1)
        {
            float[] P = new float[] { a, b, c };
            LeastSquare ls = new LeastSquare(
                                                X, Y, P,
                                                new int[] { 0, 1, 2 },
                                                new LeastSquare.CalculateValuesDalegate(SimpleCubicFunc),
                                                null
                                                );
            ls.Optimize(0.05f, 20, dampingFactor);
            a = P[0];
            b = P[1];
            c = P[2];
        }

    }
}
