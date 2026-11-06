//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib
{
    public class Stat
    {
        private double sum = 0;
        private int samples;

        public double Avrg { get { return sum / (double)samples; } }

        public static void Average(double[] data, out double avrg, out double sigma)
        {
            avrg = 0;
            sigma = 0;
            if (data == null || data.Length == 0) return;
            double sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }

            avrg = sum / data.Length;

            if (data.Length == 1) return;

            double sumHi2 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sumHi2 = sumHi2 + (data[i] - avrg) * (data[i] - avrg);
            }

            sigma = (double)Math.Sqrt(sumHi2 / (data.Length-1));
        }

        public void Add(double number)
        {
            sum += number;
            samples++;
        }
    }
}
