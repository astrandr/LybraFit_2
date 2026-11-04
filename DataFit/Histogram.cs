//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MathLib
{
    /// <summary>
    /// Histogram
    /// </summary>
    public class Histogram
    {
        /// <summary>
        /// Get - returns istogram of float data
        /// </summary>
        /// <param name="data">data as float array</param>
        /// <param name="levels">number of levels</param>
        /// <param name="lowLevel">low limit of the data</param>
        /// <param name="highLevel">high limit of the data</param>
        /// <returns></returns>
        public static float[] Get(float[] data, int levels, float lowLevel, float highLevel)
        {
            Debug.Assert(highLevel > lowLevel, "Incorrect low to high level");
            Debug.Assert(levels > 1, "Incorrect number of levels");
            Debug.Assert(data != null && data.Length > 0, "No data provided");

            float[] histogram = new float[levels];
            float levelWidth = (highLevel+1 - lowLevel) / levels;

            for (int i = 0; i < data.Length; i++)
            {
                int l = (int)((data[i] - lowLevel) / levelWidth);

                //Make entry only if data value is within provided limits
                if (l >= 0 && l < levels)
                {                   
                    histogram[l]++;
                }
            }

            return histogram;
        }
    }
}
