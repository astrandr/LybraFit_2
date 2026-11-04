//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib
{
    /// <summary>
    /// Coordinates
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// CertesianToPolar - calculates polar coordinates from certesian coordinates.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="R">Radius</param>
        /// <param name="a">angle in radians from -Pi to +Pi</param>
        public static void CertesianToPolar(float x, float y, out float R, out float a)
        {
            R = (float)Math.Sqrt(x * x + y * y);
            a = (float)Math.Atan(y / x);
            if (x < 0)
            {
                if (y < 0)
                    a -= (float)Math.PI;
                else
                    a += (float)Math.PI;
            }
           
        }
    }
}
