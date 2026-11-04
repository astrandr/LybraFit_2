//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLib
{
    public class DataSmooth
    {
        /// <summary>
        /// MovingAverage
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="n">cound of points to use in smoothing</param>
        /// <returns></returns>
        public static float[] MovingAverage(float[] Y, int n)
        {
            float[] r = new float[Y.Length];
            float s = 0;
            int l = (int)((float)n/2.0f);
            for (int i = 0; i < r.Length; i++)
            {
                int lo = i >= l ? i - l : 0;
                int hi = i < r.Length - l ? i + l : r.Length - 1;
                int count = 0;
                s = 0;
                for (int j = lo; j <= hi; j++)
                {
                    s += Y[j];
                    count++;
                }

                r[i] = s / count;
            }

            return r;
        }
    }
}
