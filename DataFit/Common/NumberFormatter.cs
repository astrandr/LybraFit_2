//Designed and Implemented by Andrey Borissov Batchvarov 2018
using System;
using System.Text;

namespace AutoFormatNumber
{
    public class NumberFormatter
    {
        protected static decimal[] niceLabelsSteps = new decimal[] { .1m, .2m, .25m, .5m, 1, 2, 2.5m, 5, 10 };

        public static void ToScientific(double a, ref decimal number, ref int pow)
        {
            if (a == 0) { number = 0; pow = 0; return; }

            double b = a > 0 ? a : -a;
            pow = (int)Math.Log10(b);
            double k = b / Math.Pow(10, pow);

            if (k < 1) { pow--; }
            if (k >= 10) { pow++; }

            number = (decimal)(a / Math.Pow(10, pow));
        }

        /// <summary>
        /// ToString - returns a fixed digits representation of the number of pow is less than max pow otherwise scientific
        /// </summary>
        /// <param name="a"></param>
        /// <param name="maxPow"></param>
        /// <returns></returns>
        public static string FloatToString(double a, int digits, uint maxPow)
        {
            if (double.IsNaN(a) || double.IsInfinity(a)) return a.ToString();

            decimal number = 0;
            int pow = 0;
            NumberFormatter.ToScientific(a, ref number, ref pow);
            if (pow > -maxPow && pow < maxPow)
            {
               return String.Format("{0:F"+digits+"}", a);
            }
            else
            {
                return String.Format("{0:F"+digits+"}E{1}", number, pow);
            }
        }

        public virtual RangeLabelsInfo GetRangeLabelsInfo(double min, double max, int minPow = 2, int recommendedCount = 10)
        {
            double lMin = min, lMax = max;

            decimal maxNumber = 0, minNumber = 0;
            int loPow = 0, hiPow = 0, powCorr = 0;
            int minDistanceToRecommendedCount = int.MaxValue;
            decimal bestStep = niceLabelsSteps[0];

            NumberFormatter.ToScientific(min, ref minNumber, ref loPow);
            NumberFormatter.ToScientific(max, ref maxNumber, ref hiPow);

            powCorr = (hiPow > loPow) ? hiPow : loPow;

            //Adjust limits for same power of 10
            maxNumber = (maxNumber * (decimal)Math.Pow(10, hiPow-powCorr));
            minNumber = (minNumber * (decimal)Math.Pow(10, loPow-powCorr));

            foreach (decimal step in niceLabelsSteps)
            {
                int stepsCount = (int)((maxNumber - minNumber) / step);
                if (minDistanceToRecommendedCount > Math.Abs(stepsCount - recommendedCount))
                {
                    minDistanceToRecommendedCount = Math.Abs(stepsCount - recommendedCount);
                    bestStep = step;
                }
            }

            //round the limits to multiples of the bestStep
            if (-2 <= powCorr && powCorr <= 2)
            {
                maxNumber = bestStep * (int)(maxNumber / bestStep + 1);
                minNumber = bestStep * (int)(minNumber / bestStep - 1);
                maxNumber = (maxNumber * (decimal)Math.Pow(10, powCorr));
                minNumber = (minNumber * (decimal)Math.Pow(10, powCorr));
                bestStep = (bestStep * (decimal)Math.Pow(10, powCorr));
                powCorr = 0;
            }


            return new RangeLabelsInfo(minNumber, maxNumber, powCorr, bestStep);
        }
    }
}
