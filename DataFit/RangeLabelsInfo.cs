//Designed and Implemented by Andrey Borissov Batchvarov 2018
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace AutoFormatNumber
{
    public class RangeLabelsInfo
    {
        private decimal min, max, step;
        private int pow;

        public decimal Min { get { return min; } }
        public decimal Max { get { return max; } }
        public decimal Pow { get { return pow; } }
        public decimal Step { get { return step; } }

        public RangeLabelsInfo(decimal min, decimal max, int pow, decimal step)
        {
            this.min = min;
            this.max = max;
            this.pow = pow;
            this.step = step;
        }
    }
}
