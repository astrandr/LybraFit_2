//Designed and Implemented by Andrey Borissov Batchvarov 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathLib
{
    public class Stat
    {
        private float sum = 0;
        private int samples;

        public float Avrg { get { return sum / (float)samples; } }

        public static void Average(float[] data, out float avrg, out float sigma)
        {
            avrg = 0;
            sigma = 0;
            if (data == null || data.Length == 0) return;
            float sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }

            avrg = sum / data.Length;

            if (data.Length == 1) return;

            float sumHi2 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sumHi2 = sumHi2 + (data[i] - avrg) * (data[i] - avrg);
            }

            sigma = (float)Math.Sqrt(sumHi2 / (data.Length-1));
        }

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

            sigma = Math.Sqrt(sumHi2 / (data.Length - 1));
        }

        public void Add(float number)
        {
            sum += number;
            samples++;
        }
    }
}
