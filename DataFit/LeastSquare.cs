//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MathLib
{
    /// <summary>
    /// LeastSquare
    /// </summary>
    public class LeastSquare
    {
        #region private members
        private double[] dataX = null;
        private double[] dataY = null;
        private double[] errY = null;
        private double[] P = null;
        private int[] paramToVary = null;
        private int nPoints = 0;
        private int nParams = 0;
        private float eps = 0;
        private float lastStdError = 0;
        private int lastIterations = 0;
        private float lastChiSquare = 0;
        private CalculateValuesDalegate calcProcedure = null;
        private NewApproximationResultCallback resultCallback = null;
        #endregion

        #region public properties

        /// <summary>
        /// LastStdError - returns the Std Error for the last fit
        /// </summary>
        public float LastStdError
        {
            get { return lastStdError; }
        }

        /// <summary>
        /// LastIterations - returns the number of iterations for the last optimizaiton process
        /// </summary>
        public float LastIterations
        {
            get { return lastIterations; }
        }

        /// <summary>
        /// LastChiSquare - returns the Chi-squared test for the last optimization process
        /// </summary>
        public float LastChiSquare
        {
            get { return lastChiSquare; }
        }

        #endregion

        #region public methods
        /// <summary>
        /// CalculateValuesDalegate - call back funtion to calculate function values for specified parameters at data 
        /// </summary>
        /// <param name="X">array of data values</param>
        /// <param name="P">array of current parameters values</param>
        /// <param name="Y">array of function values to be returned</param>
        public delegate void CalculateValuesDalegate(double[] X, double[] P, ref double[] Y);

        /// <summary>
        /// NewApproximationResultCallback
        /// </summary>
        /// <param name="eps">last iteration error</param>
        /// <param name="stop">set to true if process should stop</param>
        public delegate void NewApproximationResultCallback(float eps, ref bool stop, LeastSquare leastSquareObject = null);

        /// <summary>
        /// LeastSquare - finds best least square approximation for a function to provided data
        /// </summary>
        /// <param name="X">data X</param>
        /// <param name="Y">data Y</param>
        /// <param name="P">parameters</param>
        /// <param name="PV">indexes of parameters to be varied</param>
        /// <param name="calcProcedure">function called to calculate Y values</param>
        public LeastSquare(double[] X, double[] Y, double[] P, int[] paramToVary, CalculateValuesDalegate calcProcedure, NewApproximationResultCallback resultCallback)
        {
            Debug.Assert(X != null && X.Length != 0, "Invalida data X provided");
            Debug.Assert(Y != null && Y.Length != 0, "Invalida data Y provided");
            Debug.Assert(X.Length == Y.Length, "X and Y data of different length");
            Debug.Assert(calcProcedure != null, "No calculate procedure provided");

            this.dataX = X;
            this.dataY = Y;
            this.P = P;
            this.paramToVary = paramToVary;
            this.calcProcedure = calcProcedure;
            this.resultCallback = resultCallback;
            this.nPoints = dataX.Length;
            this.nParams = paramToVary.Length;
        }

        /// <summary>
        /// LeastSquare - finds best least square approximation for a function to provided data, using error for each data
        /// </summary>
        /// <param name="X">data X</param>
        /// <param name="Y">data Y</param>
        ///<param name="Y">err Y</param>
        /// <param name="P">parameters</param>
        /// <param name="PV">indexes of parameters to be varied</param>
        /// <param name="calcProcedure">function called to calculate Y values</param>
        public LeastSquare(double[] X, double[] Y, double[] errY, double[] P, int[] paramToVary, CalculateValuesDalegate calcProcedure, NewApproximationResultCallback resultCallback)
        {
            Debug.Assert(X != null && X.Length != 0, "Invalida data X provided");
            Debug.Assert(Y != null && Y.Length != 0, "Invalida data Y provided");
            Debug.Assert(X.Length == Y.Length, "X and Y data of different length");
            Debug.Assert(calcProcedure != null, "No calculate procedure provided");

            this.dataX = X;
            this.dataY = Y;
            this.errY = errY;
            this.P = P;
            this.paramToVary = paramToVary;
            this.calcProcedure = calcProcedure;
            this.resultCallback = resultCallback;
            this.nPoints = dataX.Length;
            this.nParams = paramToVary.Length;
        }

        public double CalculateError()
        {
            double[] newY = new double[nPoints];
            double[] deltaY = new double[nPoints];
            calcProcedure(dataX, P, ref newY);
            double eps = 0;
            for (int i = 0; i < nPoints; i++)
            {
                deltaY[i] = dataY[i] - newY[i];
                eps = eps + deltaY[i] * deltaY[i];
            }

            return eps;
        }

        public float CalculateChiSquared()
        {
            float[] newY = new float[nPoints];
            float[] deltaY = new float[nPoints];
            calcProcedure(dataX, P, ref newY);
            float eps = 0;
            for (int i = 0; i < nPoints; i++)
            {
                deltaY[i] = dataY[i] - newY[i];
                if (dataY[i] != 0)
                {
                    eps = eps + deltaY[i] * deltaY[i] / (float)Math.Abs(dataY[i]);
                }
            }

            return eps;
        }

        /// <summary>
        /// CalculateFitGoodness - calculates Std Error, Chi Square and Sum of Squared errors
        /// </summary>
        /// <returns></returns>
        public void CalculateFitGoodness()
        {
            float[] newY = new float[nPoints];
            float[] deltaY = new float[nPoints];
            calcProcedure(dataX, P, ref newY);
            lastChiSquare = 0; lastStdError = 0; int n = 0;
            for (int i = 0; i < nPoints; i++)
            {
                float delta = dataY[i] - newY[i];
                if (dataY[i] != 0)
                {
                    lastChiSquare = lastChiSquare + delta * delta / (float)Math.Abs(dataY[i]);
                    n++;
                }

                lastStdError = lastStdError + delta * delta;
            }

            lastStdError = (nPoints - paramToVary.Length) > 0 ? (float)(lastStdError / (nPoints - paramToVary.Length)) : lastStdError;

            lastStdError = (float)Math.Sqrt(lastStdError);

            lastChiSquare = lastChiSquare / n;
        }


        /// <summary>
        /// CalculateStdError - calculates standard error
        /// </summary>
        /// <returns></returns>
        public float CalculateStdError()
        {
            return (float)Math.Sqrt(CalculateRSS());
        }

        /// <summary>
        /// CalculateRSS - calculates standard error
        /// </summary>
        /// <returns></returns>
        public float CalculateRSS()
        {
            float[] newY = new float[nPoints];
            float[] deltaY = new float[nPoints];
            calcProcedure(dataX, P, ref newY);
            float eps = 0;
            for (int i = 0; i < nPoints; i++)
            {
                deltaY[i] = dataY[i] - newY[i];
                eps = eps + (float)(deltaY[i] * deltaY[i]);
            }

            return (nPoints - paramToVary.Length) > 0 ? (float)(eps / (nPoints - paramToVary.Length)) : eps;
        }

        /// <summary>
        /// Optimize - finds the best least square fit
        /// </summary>
        /// <param name="paramDerivativeDelta">percentage of pramater to specify the delta used in calculating the derivatives</param>
        /// <param name="maxIterations">max number of iterations</param>
        /// <param name="dampingFactor">Marquardt factor</param>
        /// <returns></returns>
        public float Optimize(float paramDerivativeDelta = 0.01f, int maxIterations = 10, float dampingFactor = 0)
        {
            Matrix YP = new Matrix(nPoints, nParams);
            Matrix J = new Matrix(nParams, nParams);

            Matrix DY = new Matrix(nParams, 1);

            float[] newY = new float[nPoints];
            float[] newY2 = new float[nPoints];
            float[] deltaY = new float[nPoints];
            float[] deltaP = new float[nParams];

            bool stop = false;
            int iter = 0;
            float lastEps = CalculateError();

            float[] bestParameters = new float[P.Length];
            P.CopyTo(bestParameters, 0);

            for (int j = 0; j < nParams; j++)
            {
                deltaP[j] = P[paramToVary[j]] * paramDerivativeDelta;
                //if one of parameters has initial value 0, set delta to some small value
                if (deltaP[j] == 0) deltaP[j] = 0.1f;
            }

            //Check parameter delta for possible infinity derivatives
            for (int j = 0; j < nParams; j++)
            {
                bool hasInfinity = false;
                do
                {
                    P[paramToVary[j]] = P[paramToVary[j]] + deltaP[j];
                    calcProcedure(dataX, P, ref newY);

                    P[paramToVary[j]] = P[paramToVary[j]] - 2 * deltaP[j];
                    calcProcedure(dataX, P, ref newY2);

                    P[paramToVary[j]] = P[paramToVary[j]] + deltaP[j];

                    hasInfinity = newY.Where(y => float.IsNaN(y) || float.IsInfinity(y)).Any() || newY2.Where(y => float.IsNaN(y) || float.IsInfinity(y)).Any();
                    if (hasInfinity) deltaP[j] /= 10;
                    if (deltaP[j] == 0) return lastStdError = CalculateStdError(); //Cannot find proper delta for the parameter, exit

                } while (hasInfinity);
            }

            try
            {
                do
                {
                    stop = true;

                    calcProcedure(dataX, P, ref newY);

                    for (int i = 0; i < nPoints; i++)
                    {
                        deltaY[i] = dataY[i] - newY[i];
                    }

                    for (int j = 0; j < nParams; j++)
                    {
                        //Calculate derivatives of Ys vs parameters
                        P[paramToVary[j]] = P[paramToVary[j]] + deltaP[j];
                        calcProcedure(dataX, P, ref newY);

                        P[paramToVary[j]] = P[paramToVary[j]] - 2 * deltaP[j];
                        calcProcedure(dataX, P, ref newY2);

                        P[paramToVary[j]] = P[paramToVary[j]] + deltaP[j];

                        for (int i = 0; i < nPoints; i++)
                        {
                            YP.Values[i][j] = (newY[i] - newY2[i]) / 2 / deltaP[j];
                        }
                    }

                    //Calculate the Jacobian 
                    for (int j = 0; j < nParams; j++)
                    {
                        for (int k = 0; k < nParams; k++)
                        {
                            J.Values[j][k] = 0;
                            for (int i = 0; i < nPoints; i++)
                            {
                                if (errY != null && errY[i] != 0)
                                {
                                    J.Values[j][k] += ((YP.Values[i][j] * YP.Values[i][k]) / errY[i] / errY[i]);
                                }
                                else
                                {
                                    J.Values[j][k] += (YP.Values[i][j] * YP.Values[i][k]);
                                }
                            }
                        }
                    }

                    //Marquardt algorithm approach
                    //Apply the dampingFactor
                    for (int j = 0; j < nParams; j++)
                    {
                        J.Values[j][j] *= (1 + dampingFactor);
                    }

                    for (int k = 0; k < nParams; k++)
                    {
                        DY.Values[k][0] = 0;
                        for (int i = 0; i < nPoints; i++)
                        {
                            if (errY != null && errY[i] != 0)
                            {
                                DY.Values[k][0] += ((YP.Values[i][k] * deltaY[i]) / errY[i] / errY[i]);
                            }
                            else
                            {
                                DY.Values[k][0] += (YP.Values[i][k] * deltaY[i]);
                            }
                        }
                    }

                    //Matrix newP = Matrix.Product(JInv, DY);//Old version
                    float[] diffP = Matrix.SolveLinearSystem(J, DY);
                    for (int k = 0; k < nParams; k++)
                    {
                        //P[paramToVary[k]] = P[paramToVary[k]] + newP.Values[0][k];
                        P[paramToVary[k]] = P[paramToVary[k]] + diffP[k];
                    }

                    calcProcedure(dataX, P, ref newY);
                    eps = CalculateError();

                    if (resultCallback != null) resultCallback(eps, ref stop, this);
                    iter++;

                    if (float.IsNaN(eps) || eps > lastEps)
                    {
                        //Revert parameters and find the minima along the direction
                        for (int k = 0; k < nParams; k++)
                        {
                            P[paramToVary[k]] = P[paramToVary[k]] - diffP[k];
                        }
                        eps = CalculateError();
                        float[] oldP = new float[P.Length];
                        Array.Copy(P, oldP, P.Length);
                        float stepT = 0.1f;
                        float t = 0.0f;
                        do
                        {
                            float prevEps;
                            do
                            {
                                prevEps = eps;
                                t = t + stepT;
                                for (int k = 0; k < nParams; k++)
                                {
                                    P[paramToVary[k]] = oldP[paramToVary[k]] + diffP[k] * t;
                                }
                                eps = CalculateError();
                                if (t >= 1.0f)
                                {
                                    stop = true;
                                    break;
                                }
                            } while (eps < prevEps);

                            stepT /= -10;

                        } while (stepT < -0.0009f || stepT > 0.0009f);

                        if (eps < lastEps)
                        {
                            lastEps = eps;
                            P.CopyTo(bestParameters, 0);
                            stop = false; //We are still finding better solution
                        }
                    }
                    else
                    {
                        if (eps < lastEps)
                        {
                            lastEps = eps;
                            P.CopyTo(bestParameters, 0);
                            stop = false; //We are still finding better solution
                        }
                    }

                } while (!stop && iter < maxIterations);
            }
            catch (Exception es)
            {

            }

            bestParameters.CopyTo(P, 0);

            lastIterations = iter;

            return lastStdError = CalculateStdError();
        }


        /// <summary>
        /// OptimizeAsync - Runs optimization in background Task
        /// </summary>
        /// <param name="paramDerivativeDelta"></param>
        /// <param name="maxIterations"></param>
        /// <param name="dampingFactor"></param>
        public void OptimizeAsync(float paramDerivativeDelta = 0.01f, int maxIterations = 10, float dampingFactor = 0)
        {
            Task.Run(() => Optimize(paramDerivativeDelta, maxIterations, dampingFactor));
        }

        /// <summary>
        /// GetFitError - calculates sigma of those parameters used in the last fit
        /// </summary>
        /// <param name="paramDerivativeDelta"></param>
        /// <param name="sigmaParameters"></param>
        /// <param name="sigmaY"></param>
        public void GetFitError(double paramDerivativeDelta, out double[] sigmaParameters, out double[] sigmaY)
        {
            Matrix YP = new Matrix(nPoints, paramToVary.Length);
            Matrix J = new Matrix(paramToVary.Length, paramToVary.Length);
            Matrix JInv;
            Matrix DY = new Matrix(1, paramToVary.Length);

            double[] newY = new double[nPoints];
            double[] newY1 = new double[nPoints];
            double[] newY2 = new double[nPoints];
            double[] deltaY = new double[nPoints];
            double[] deltaP = new double[paramToVary.Length];
            sigmaParameters = new double[P.Length];
            sigmaY = new double[nPoints];

            for (int j = 0; j < paramToVary.Length; j++)
            {
                deltaP[j] = P[paramToVary[j]] * paramDerivativeDelta;
                //if one of parameters has initial value 0, set delta to some small value
                if (deltaP[j] == 0) deltaP[j] = 0.1f;
            }

            calcProcedure(dataX, P, ref newY);

            for (int i = 0; i < nPoints; i++)
            {
                deltaY[i] = dataY[i] - newY[i];
            }

            for (int j = 0; j < paramToVary.Length; j++)
            {
                //Calculate derivatives of Ys vs parameters
                P[paramToVary[j]] = P[paramToVary[j]] + deltaP[j];
                calcProcedure(dataX, P, ref newY1);
                P[paramToVary[j]] = P[paramToVary[j]] - 2 * deltaP[j];
                calcProcedure(dataX, P, ref newY2);
                P[paramToVary[j]] = P[paramToVary[j]] + deltaP[j];

                for (int i = 0; i < nPoints; i++)
                {
                    YP.Values[i][j] = (newY1[i] - newY2[i]) / 2 / deltaP[j];
                }
            }

            //Calculate the Jacobian 
            for (int j = 0; j < paramToVary.Length; j++)
            {
                for (int k = 0; k < paramToVary.Length; k++)
                {
                    J.Values[j][k] = 0;
                    for (int i = 0; i < nPoints; i++)
                    {
                        if (!double.IsNaN(YP.Values[i][j]) && !double.IsNaN(YP.Values[i][k]) && !double.IsInfinity(YP.Values[i][j]) && !double.IsInfinity(YP.Values[i][k]))
                        {
                            if (errY != null && errY[i] != 0)
                            {
                                J.Values[j][k] += ((YP.Values[i][j] * YP.Values[i][k]) / errY[i] / errY[i]);
                            }
                            else
                            {
                                J.Values[j][k] += (YP.Values[i][j] * YP.Values[i][k]);
                            }
                        }
                    }
                }
            }

            //Check if there is 0s row so we can find inverse matrix of the Jacobian, otherwise remove the zeroed row
            var nonZeroRows = new List<int>();
            for (int i = 0; i < paramToVary.Length; i++)
            {
                for (int k = 0; k < paramToVary.Length; k++)
                {
                    if (J.Values[i][k] != 0)
                    {
                        nonZeroRows.Add(i);
                        break;
                    }
                }
            }

            if (nonZeroRows.Count != paramToVary.Length)
            {
                //Make the corrected jacobian
                var JC = new Matrix(nonZeroRows.Count, nonZeroRows.Count);
                int row = 0;
                for (int i = 0; i < paramToVary.Length; i++)
                {
                    if (nonZeroRows.Contains(i))
                    {
                        int col = 0;
                        for (int j = 0; j < paramToVary.Length; j++)
                        {
                            if (nonZeroRows.Contains(j))
                            {
                                JC.Values[row][col] = J.Values[i][j];
                                col++;
                            }
                        }
                        row++;
                    }
                }

                J = JC;
                var newParamToVary = new int[nonZeroRows.Count];
                for (int i = 0; i < newParamToVary.Length; i++)
                {
                    newParamToVary[i] = paramToVary[nonZeroRows[i]];
                }

                paramToVary = newParamToVary;
            }


            //Inverse of the Jacobian
            JInv = Matrix.Inverse(J);

            float rss = CalculateRSS();

            //Fill the parameters error
            for (int i = 0; i < paramToVary.Length; i++)
            {
                sigmaParameters[paramToVary[i]] = (float)Math.Sqrt(Math.Abs(JInv.Values[i][i] * rss));
            }

            for (int i = 0; i < nPoints; i++)
            {
                for (int j = 0; j < paramToVary.Length; j++)
                {
                    sigmaY[i] += (YP.Values[i][j] * YP.Values[i][j] * sigmaParameters[paramToVary[j]] * sigmaParameters[paramToVary[j]]);
                }

                sigmaY[i] = (float)Math.Sqrt(sigmaY[i]);
            }

            CalculateFitGoodness();
        }

        /// <summary>
        /// GetFitErrorAllParameters - calculates sigma of all parameters
        /// </summary>
        /// <param name="paramDerivativeDelta"></param>
        /// <param name="sigmaParameters"></param>
        /// <param name="sigmaY"></param>
        public void GetFitErrorAllParameters(double paramDerivativeDelta, out double[] sigmaParameters, out double[] sigmaY)
        {
            Matrix YP = new Matrix(nPoints, nParams);
            Matrix J = new Matrix(nParams, nParams);
            Matrix JInv;
            Matrix DY = new Matrix(1, nParams);

            double[] newY = new double[nPoints];
            double[] newY2 = new double[nPoints];
            double[] deltaY = new double[nPoints];
            double[] deltaP = new double[nParams];
            sigmaParameters = new double[P.Length];
            sigmaY = new double[nPoints];

            for (int j = 0; j < nParams; j++)
            {
                deltaP[j] = P[j] * paramDerivativeDelta;
                //if one of parameters has initial value 0, set delta to some small value
                if (deltaP[j] == 0) deltaP[j] = 0.1f;
            }

            calcProcedure(dataX, P, ref newY);

            for (int i = 0; i < nPoints; i++)
            {
                deltaY[i] = dataY[i] - newY[i];
            }

            for (int j = 0; j < nParams; j++)
            {
                //Calculate derivatives of Ys vs parameters
                P[j] = P[j] + deltaP[j];
                calcProcedure(dataX, P, ref newY);
                P[j] = P[j] - 2 * deltaP[j];
                calcProcedure(dataX, P, ref newY2);
                P[j] = P[j] + deltaP[j];

                for (int i = 0; i < nPoints; i++)
                {
                    YP.Values[i][j] = (newY[i] - newY2[i]) / 2 / deltaP[j];
                    if (YP.Values[i][j] == 0)
                    {
                        YP.Values[i][j] = 0.0000001f * newY[i]; //place some very small value
                    }
                }
            }

            //Calculate the Jacobian 
            for (int j = 0; j < nParams; j++)
            {
                for (int k = 0; k < nParams; k++)
                {
                    J.Values[j][k] = 0;
                    for (int i = 0; i < nPoints; i++)
                    {
                        if (!double.IsNaN(YP.Values[i][j]) && !double.IsNaN(YP.Values[i][k]) && !double.IsInfinity(YP.Values[i][j]) && !double.IsInfinity(YP.Values[i][k]))
                        {
                            if (errY != null && errY[i] != 0)
                            {
                                J.Values[j][k] += ((YP.Values[i][j] * YP.Values[i][k]) / errY[i] / errY[i]);
                            }
                            else
                            {
                                J.Values[j][k] += (YP.Values[i][j] * YP.Values[i][k]);
                            }

                            if (float.IsNegativeInfinity(J.Values[j][k]))
                            {
                                J.Values[j][k] = float.MinValue;
                            }
                            else if (float.IsPositiveInfinity(J.Values[j][k]))
                            {
                                J.Values[j][k] = float.MaxValue;
                            }

                        }
                    }
                }
            }

            float rss = CalculateRSS();

            //Inverse of the Jacobian
            JInv = Matrix.Inverse(J);
            //Fill the parameters error
            for (int i = 0; i < nParams; i++)
            {
                sigmaParameters[i] = (float)Math.Sqrt(Math.Abs(JInv.Values[i][i] * rss));
            }

            for (int i = 0; i < nPoints; i++)
            {
                for (int j = 0; j < paramToVary.Length; j++)
                {
                    sigmaY[i] += (YP.Values[i][j] * YP.Values[i][j] * sigmaParameters[j] * sigmaParameters[j]);
                }

                sigmaY[i] = (float)Math.Sqrt(sigmaY[i]);
            }
        }

        #endregion
    }
}
