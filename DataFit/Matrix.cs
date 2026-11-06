//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;

namespace MathLib
{
    /// <summary>
    /// Summary description for Matrix.
    /// </summary>
    public class Matrix
    {
        protected double[][] values;

        /// <summary>
        /// sizeX
        /// </summary>
        public readonly int sizeX;
        /// <summary>
        /// sizeY
        /// </summary>
        public readonly int sizeY;
        /// <summary>
        /// Values
        /// </summary>
        public double[][] Values
        {
            get { return this.values; }
            set { this.values = value; }
        }


        public static double minFloatNumber = double.MinValue;

        /// <summary>
        /// Matrix - default constructor
        /// </summary>
        public Matrix()
        {
        }

        /// <summary>
        /// Matrix
        /// </summary>
        /// <param name="sizeX"></param>
        /// <param name="sizeY"></param>
        public Matrix(int sizeY, int sizeX)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            AllocateMatrix();
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public Matrix Clone()
        {
            Matrix m = new Matrix(this.sizeY, this.sizeX);
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    m.Values[i][j] = this.values[i][j];
                }
            }

            return m;
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="endX"></param>
        /// <param name="endY"></param>
        /// <returns></returns>
        public Matrix Clone(int startX, int startY, int endX, int endY)
        {
            if (startX >= sizeX || startY >= sizeY || endX < startX || endY < startY)
            {
                throw new System.ArgumentException("Invalid range for clone matrix");
            }

            int x1 = (startX >= 0) ? startX : 0;
            int x2 = (endX < sizeX) ? endX : sizeX;
            int y1 = (startY >= 0) ? startY : 0;
            int y2 = (endY < sizeY) ? endY : sizeY;

            Matrix m = new Matrix(x2 - x1, y2 - y1);
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    m.Values[i - x1][j - y1] = this.values[i][j];
                }
            }

            return m;
        }

        /// <summary>
        /// Subtract
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Subtract(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] - b.values[x][y];
                }
            }

            return result;
        }

        /// <summary>
        /// Addition operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] + b.values[x][y];
                }
            }

            return result;
        }

        /// <summary>
        /// Multiplication operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] * b.values[x][y];
                }
            }

            return result;
        }

        /// <summary>
        /// Subtract float operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix a, float b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] - b;
                }
            }

            return result;
        }

        /// <summary>
        /// Add float operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix a, float b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] + b;
                }
            }

            return result;
        }

        /// <summary>
        /// Multiply with float operator
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix a, float b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] * b;
                }
            }

            return result;
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="a"></param>
        public void Add(Matrix a)
        {
            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    this.Values[x][y] += a.values[x][y];
                }
            }


        }

        /// <summary>
        /// Multiply
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Multiply(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    result.Values[x][y] = a.values[x][y] * b.values[x][y];
                }
            }

            return result;
        }

        public Matrix Transp()
        {
            Matrix result = new Matrix(this.sizeY, this.sizeX);
            for (int x = 0; x < this.sizeX; x++)
            {
                for (int y = 0; y < this.sizeY; y++)
                {
                    result.Values[y][x] = this.values[x][y];
                }
            }

            return result;
        }

        private static bool IsZero(float a)
        {
            //return Math.Abs(a) < minFloatNumber;
            return a == 0;
        }

        /// <summary>
        /// Inverse square matrix using Gauss-Jordan Elimination
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Matrix Inverse(Matrix a)
        {
            if (a.sizeX != a.sizeY) return null;
            float b;
            Matrix m = a.Clone();
            int size = a.sizeX;
            Matrix res = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                res.Values[i][i] = 1;
            }


            for (int k = 0; k < size - 1; k++)
            {
                for (int i = k + 1; i < size; i++)
                {
                    if (IsZero(m.Values[k][k]))
                    {
                        bool found = false;
                        //Search for non zero raw and swap them
                        for (int l = k + 1; l < size; l++)
                        {
                            if (!IsZero(m.Values[l][k]))
                            {
                                //Swap raws
                                for (int r = 0; r < size; r++)
                                {
                                    b = m.Values[l][r];
                                    m.Values[l][r] = m.Values[k][r];
                                    m.Values[k][r] = b;

                                    b = res.Values[l][r];
                                    res.Values[l][r] = res.Values[k][r];
                                    res.Values[k][r] = b;
                                }

                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            throw new System.InvalidOperationException("No inverse matrix exists. Zeros raw");
                        }
                    }
                    float t = -(float)(m.Values[i][k] / m.Values[k][k]);
                    for (int j = 0; j < size; j++)
                    {
                        m.Values[i][j] = m.Values[i][j] + t * m.Values[k][j];
                        res.Values[i][j] = res.Values[i][j] + t * res.Values[k][j];
                    }
                }
            }

            for (int k = size - 1; k > 0; k--)
            {
                for (int i = k - 1; i >= 0; i--)
                {
                    if (IsZero(m.Values[k][k]))
                    {
                        bool found = false;
                        //Seach for non zero raw and swap them
                        for (int l = k - 1; l >= 0; l--)
                        {
                            if (!IsZero(m.Values[l][k]))
                            {
                                //Swap raws
                                for (int r = 0; r < size; r++)
                                {
                                    b = m.Values[l][r];
                                    m.Values[l][r] = m.Values[k][r];
                                    m.Values[k][r] = b;

                                    b = res.Values[l][r];
                                    res.Values[l][r] = res.Values[k][r];
                                    res.Values[k][r] = b;
                                }

                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            throw new System.InvalidOperationException("No inverse matrix exists. Zeros raw");
                        }
                    }
                    float t = -(float)(m.Values[i][k] / m.Values[k][k]);
                    for (int j = 0; j < size; j++)
                    {
                        m.Values[i][j] = m.Values[i][j] + t * m.Values[k][j];
                        res.Values[i][j] = res.Values[i][j] + t * res.Values[k][j];
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    res.Values[i][j] = res.Values[i][j] / m.Values[i][i];
                    //if (float.IsInfinity(res.Values[i][j]))
                    //{
                    //    throw new System.InvalidOperationException("No inverse matrix exists.");
                    //}
                }
            }

            return res;
        }

        /// <summary>
        /// SolveLinearSystem - solves system of linear equations with A.X=B, where A is (NxN) and B is (1,N) size vector
        /// </summary>
        /// <param name="a"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static float[] SolveLinearSystem(Matrix A, Matrix B)
        {
            if (A.sizeX != A.sizeY) return null;
            float b;
            Matrix m = A.Clone();
            int size = A.sizeX;
            float[] res = new float[B.sizeY];

            for (int i = 0; i < B.sizeY; i++)
            {
                res[i] = B.Values[i][0];
            }


            for (int k = 0; k < size - 1; k++)
            {

                //Find row with much higher value
                int rowIndex = k;
                for (int l = k + 1; l < size; l++)
                {
                    if (Math.Abs(m.Values[l][k]) > 4 * Math.Abs(m.Values[k][k]))
                    {
                        rowIndex = l;
                    }
                }

                if (rowIndex != k)
                {
                    //Swap raws if we find one with larger value to devide
                    for (int r = 0; r < size; r++)
                    {
                        b = m.Values[rowIndex][r];
                        m.Values[rowIndex][r] = m.Values[k][r];
                        m.Values[k][r] = b;
                    }

                    b = res[rowIndex];
                    res[rowIndex] = res[k];
                    res[k] = b;
                }

                for (int i = k + 1; i < size; i++)
                {
                    float t = -(float)(m.Values[i][k] / m.Values[k][k]);
                    for (int j = 0; j < size; j++)
                    {
                        m.Values[i][j] = m.Values[i][j] + t * m.Values[k][j];
                    }
                    res[i] = res[i] + t * res[k];
                }
            }

            for (int k = size - 1; k > 0; k--)
            {
                if (IsZero(m.Values[k][k]))
                {
                    size = size - 1;
                    continue;
                }

                for (int i = k - 1; i >= 0; i--)
                {
                    float t = -(float)(m.Values[i][k] / m.Values[k][k]);
                    for (int j = 0; j < size; j++)
                    {
                        m.Values[i][j] = m.Values[i][j] + t * m.Values[k][j];
                    }
                    res[i] = res[i] + t * res[k];
                }
            }


            for (int j = 0; j < size; j++)
            {
                res[j] = res[j] / m.Values[j][j];
                if (float.IsInfinity(res[j]) || float.IsNaN(res[j]))
                {
                    throw new System.InvalidOperationException("No inverse matrix exists.");
                }
            }

            for (int i = size; i < res.Length; i++) res[i] = 0;

            return res;
        }

        /// <summary>
        /// Devide
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Devide(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.sizeX, a.sizeY);

            for (int x = 0; x < a.sizeX; x++)
            {
                for (int y = 0; y < a.sizeY; y++)
                {
                    if (a.values[x][y] == 0 && b.values[x][y] == 0) result.Values[x][y] = 1;
                    else result.Values[x][y] = a.values[x][y] / b.values[x][y];
                }
            }

            return result;
        }

        /// <summary>
        /// Product - return matrix multiplication of two matrixes
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Matrix Product(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.sizeY, b.sizeX);
            for (int i = 0; i < result.sizeY; i++)
            {
                for (int j = 0; j < result.sizeX; j++)
                {
                    float s = 0;
                    for (int k = 0; k < b.sizeY; k++)
                    {
                        s = s + a.Values[i][k] * b.Values[k][j];
                    }

                    result.Values[i][j] = s;
                }
            }

            return result;
        }

        public void GetRange(out float minValue, out float maxValue)
        {
            minValue = float.MaxValue;
            maxValue = float.MinValue;
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    if (minValue > values[x][y]) minValue = values[x][y];
                    if (maxValue < values[x][y]) maxValue = values[x][y];
                }
            }

        }

        protected void AllocateMatrix()
        {
            values = new double[sizeY][];
            for (int i = 0; i < sizeY; i++)
            {
                values[i] = new double[sizeX];
            }
        }
    }
}
