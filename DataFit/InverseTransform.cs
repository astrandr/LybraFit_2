//Designed and Implemented by Andrey Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncLib
{
    public class InverseTransform
    {
        /// <summary>
        /// InverseData  - for each Y finds the X with precision eps and swaps the arrays
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="function"></param>
        /// <param name="minY"></param>
        /// <param name="maxY"></param>
        /// <param name="stepY"></param>
        /// <param name="eps"></param>
        /// <returns></returns>
        public static bool InverseData(ref float[] X, ref float[] Y, ref float[] Err, UserFunction function, float minY, float maxY, float stepY, float eps)
        {
            bool success = false;
            float[] P = new float[0];
            int length = (int)((maxY - minY) / stepY);
            float y = (int)(minY / stepY) * stepY;
            float x = 0;
            float[] newX = new float[length];
            float[] newY = new float[length];
            float[] newErr = new float[length];
            for (int i = 0; i < length; i++)
            {
                int j = 0;

                float loX = 0, hiX = 0, loY = 0, hiY = 0;
                for (j = 0; j < Y.Length-1; j++)
                {
                    if (Y[j + 1] >= Y[j])
                    {
                        if (y >= Y[j] && y <= Y[j + 1])
                        {
                            loX = X[j]; hiX = X[j + 1];
                            loY = Y[j]; hiY = Y[j + 1]; 
                            break;
                        }
                    }
                    else
                    {
                        if (y <= Y[j] && y >= Y[j + 1])
                        {
                            loX = X[j+1]; hiX = X[j];
                            loY = Y[j+1]; hiY = Y[j]; 
                        }
                    }
                }

                float xn, yn;
                do{
                xn = (loX + hiX) / 2;
                yn = function.Calculate(xn, P);
                if (y > yn) { loY = yn; loX = xn; }  else
                    if (y < yn) { hiY = yn; hiX = xn; }
                    else
                    {
                        x = xn; hiX = loX; break;
                    }
                }while( Math.Abs(hiX - loX) > eps);

                newX[i] = y;
                newY[i] = xn;
                newErr[i] = Math.Abs(hiX - loX);
                y += stepY;
            }

            X = newX;
            Y = newY;
            Err = newErr;

            return success;
        }
    }
}
