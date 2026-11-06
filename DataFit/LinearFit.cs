//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Diagnostics;

namespace MathLib
{
    /// <summary>
    /// InitizationException
    /// </summary>
    public class InitializationException : Exception
    {
        /// <summary>
        /// InitializationException
        /// </summary>
        /// <param name="message"></param>
        public InitializationException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// LinearFit - Least square approximation to linear function y = a*x+b
    /// </summary>
    public class LinearFit
    {

        private static void SimpleLinearFunc(float[] X, float[] P, ref float[] Y)
        {
            for (int i = 0; i < X.Length; i++)
            {
                Y[i] = P[0] * X[i] + P[1];
            }
        }

        /// <summary>
        /// Optimize - refines a and b parameters to least square fit of provided data X and Y
        /// </summary>
        /// <param name="X">X data float[]</param>
        /// <param name="Y">Y data float[]</param>
        /// <param name="a">initial and output value of 'a' parameter</param>
        /// <param name="b">initial and output value of 'b' parameter</param>
        public static void Optimize(float[] X, float[] Y, ref float a, ref float b, float dampingFactor = 0)
        {
            float[] P = new float[] { a, b };
            LeastSquare ls = new LeastSquare(
                                                X, Y, P,
                                                new int[] { 0, 1 },
                                                new LeastSquare.CalculateValuesDalegate(SimpleLinearFunc),
                                                null
                                                );
            ls.Optimize(0.05f, 20, dampingFactor);
            a = P[0];
            b = P[1];
        }


        /// <summary>
        /// DLinearFitSolve - returns list of parameters for polynomial fit of given order. Zero index parameter is at first position of the array
        /// </summary>
        /// <param name="X">X data</param>
        /// <param name="Y">Y - func value data</param>
        /// <param name="N">Nth order of the polynomial fit</param>
        /// <returns></returns>
        public static float[] DLinearFitSolve(float[] X, float[] Y, int N)
        {
            Debug.Assert(X != null && Y != null, "Null arrays in LinearFitSolve");
            Debug.Assert(X.Length == Y.Length, "Arrays are not equal size in LinearFitSolve");

            if (X.Length < N)
            {
                throw new InitializationException("Data points less than arguments!");
            }

            if (X == null || Y == null || X.Length != Y.Length)
            {
                throw new System.ArgumentException("Invalid data passed to LinearFitSolve");
            }

            float[] P = new float[N];

            DMatrix A = new DMatrix(N, N);
            DMatrix B = new DMatrix(N, 1);
            double S = 0;
            for (int p = 0; p < N; p++)
            {
                for (int n = 0; n < N; n++)
                {
                    S = 0;
                    for (int i = 0; i < X.Length; i++)
                    {
                        S += Math.Pow(X[i], p) * Math.Pow(X[i], n);
                    }

                    A.Values[p][n] = S;
                }

                S = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    S += Math.Pow(X[i], p) * Y[i];
                }

                B.Values[p][0] = (float)S;
            }

            float[] result = new float[N];

            double[] dR = DMatrix.SolveLinearSystem(A, B);

            for (int i = 0; i < dR.Length; i++)
            {
                result[i] = (float)dR[i];
            }

            return result;
        }

        /// <summary>
        /// LinearFitSolve - returns list of parameters for polynomial fit of given order. Zero index parameter is at first position of the array
        /// </summary>
        /// <param name="X">X data</param>
        /// <param name="Y">Y - func value data</param>
        /// <param name="N">Nth order of the polynomial fit</param>
        /// <returns></returns>
        public static double[] LinearFitSolve(double[] X, double[] Y, int N)
        {
            Debug.Assert(X != null && Y != null, "Null arrays in LinearFitSolve");
            Debug.Assert(X.Length == Y.Length, "Arrays are not equal size in LinearFitSolve");

            if (X.Length < N)
            {
                throw new InitializationException("Data points less than arguments!");
            }

            if (X == null || Y == null || X.Length != Y.Length)
            {
                throw new System.ArgumentException("Invalid data passed to LinearFitSolve");
            }

            double[] P = new double[N];

            Matrix A = new Matrix(N, N);
            Matrix B = new Matrix(N, 1);
            double S = 0;
            for (int p = 0; p < N; p++)
            {
                for (int n = 0; n < N; n++)
                {
                    S = 0;
                    for (int i = 0; i < X.Length; i++)
                    {
                        S += Math.Pow(X[i], p) * Math.Pow(X[i], n);
                    }

                    A.Values[p][n] = (float)S;
                }

                S = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    S += Math.Pow(X[i], p) * Y[i];
                }

                B.Values[p][0] = (float)S;
            }

            float[] result = new float[N];

            return Matrix.SolveLinearSystem(A, B);
        }

        /// <summary>
        /// LinearFitSolveLine - returns list of parameters for fit to Y=A+B*x
        /// </summary>
        /// <param name="X">X data</param>
        /// <param name="Y">Y - func value data</param>
        /// <param name="N">Nth order of the polynomial fit</param>
        /// <returns></returns>
        public static double[] LinearFitSolveLine(double[] X, double[] Y)
        {
            Debug.Assert(X != null && Y != null, "Null arrays in LinearFitSolve");
            Debug.Assert(X.Length == Y.Length, "Arrays are not equal size in LinearFitSolve");
            
            int N = 2;

            if (X.Length < N)
            {
                throw new InitializationException("Data points less than arguments!");
            }

            if (X == null || Y == null || X.Length != Y.Length)
            {
                throw new System.ArgumentException("Invalid data passed to LinearFitSolve");
            }

            float[] P = new float[N];

            double S0 = X.Length, S1 = 0, S2 = 0, F = 0, L = 0;

            for (int i = 0; i < X.Length; i++)
            {
                S1 += X[i];
                S2 += (X[i] * X[i]);
                L += (X[i] * Y[i]);
                F += Y[i];
            }

            P[0] = (float)((L * S1 - F * S2) / (S1 * S1 - S0 * S2));
            P[1] = (float)((F * S1 - L * S0) / (S1 * S1 - S0 * S2));

            return P;
        }
    }
}
