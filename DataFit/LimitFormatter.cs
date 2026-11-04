//Designed and Implemented by Andrey Borissov Batchvarov 2018
using System;

namespace AutoFormatNumber
{
    public class LimitFormatter : NumberFormatter
    {
        public override RangeLabelsInfo GetRangeLabelsInfo(double min, double max, int minPow = 2, int recommendedCount = 10)
        {
            double span = max - min;
            double recommendedStep = span / recommendedCount;
            decimal recommendedStepNumber = 0;
            int recommendedStepPow = 0;

            NumberFormatter.ToScientific(recommendedStep, ref recommendedStepNumber, ref recommendedStepPow);

            double lMin = min, lMax = max;

            decimal maxNumber = 0, minNumber = 0;
            int loPow = 0, hiPow = 0, powCorr = 0;
            decimal minDistanceToRecommendedCounts = decimal.MaxValue;
            decimal bestStep = niceLabelsSteps[0];


            foreach (decimal step in niceLabelsSteps)
            {
                decimal counts = (decimal)(max - min) / (step * (decimal)Math.Pow(10, recommendedStepPow));
                if (minDistanceToRecommendedCounts > Math.Abs(recommendedCount - 2 - (int)counts))
                {
                    minDistanceToRecommendedCounts = Math.Abs(recommendedCount - 2 - counts);
                    bestStep = step;
                }
            }

            decimal realStep = (bestStep * (decimal)Math.Pow(10, recommendedStepPow));
            lMax = (double)realStep * (int)(max / (double)realStep + 1);
            lMin = (double)realStep * (Math.Floor(min / (double)realStep));

            NumberFormatter.ToScientific(lMin, ref minNumber, ref loPow);
            NumberFormatter.ToScientific(lMax, ref maxNumber, ref hiPow);

            if (minNumber != 0 && maxNumber != 0)
            {
                powCorr = (hiPow > loPow) ? hiPow : loPow;
            }
            else
            {
                powCorr = (minNumber != 0) ? loPow : hiPow;
            }

            //round the limits to multiples of the bestStep
            if (-minPow <= powCorr && powCorr <= minPow)
            {
                maxNumber = (decimal)lMax;
                minNumber = (decimal)lMin;
                bestStep = realStep;
                powCorr = 0;
            }
            else
            {
                //Adjust limits for same power of 10
                maxNumber = (maxNumber * (decimal)Math.Pow(10, hiPow - powCorr));
                minNumber = (minNumber * (decimal)Math.Pow(10, loPow - powCorr));
                bestStep = (bestStep * (decimal)Math.Pow(10, recommendedStepPow - powCorr));
            }



            return new RangeLabelsInfo(minNumber, maxNumber, powCorr, bestStep);
        }
    }
}
