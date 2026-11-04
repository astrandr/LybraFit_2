//Designed and Implemented by Andrey Borissov Batchvarov 2018
//Copyright © Andrey Borissov Batchvarov. No copy and reproduction is allowed without provided license agreement
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using AutoFormatNumber;
using System.Windows.Forms;

namespace LybraFit
{
    public class TypeComparer : IComparer<Type>
    {

        public int Compare(Type x, Type y)
        {
            return String.Compare(x.Name, y.Name);
        }
    }

    public class FloatNumbersRowComparer : IComparer
    {

        public int Compare(object r1, object r2)
        {
            float x1 = (r1 as DataGridViewRow).Cells[1].Value == null ? 0 : float.Parse((r1 as DataGridViewRow).Cells[1].Value.ToString());
            if (float.IsPositiveInfinity(x1)) x1 = float.MaxValue; else if (float.IsNegativeInfinity(x1)) x1 = float.MinValue;
            float x2 = (r2 as DataGridViewRow).Cells[1].Value == null ? 0 : float.Parse((r2 as DataGridViewRow).Cells[1].Value.ToString());
            if (float.IsNaN(x2)) x2 = float.MaxValue; else if (float.IsNegativeInfinity(x2)) x2 = float.MinValue;

            return (x1 < x2) ? -1 : 1;
        }
    }

    public class Utility
    {
        /// <summary>
        /// GetArrayLimits - returns max and min values in the provided array
        /// </summary>
        /// <param name="data">data array</param>
        /// <param name="min">min value returned</param>
        /// <param name="max">max value returned</param>
        public static void GetArrayLimits(float[] data, ref float min, ref float max)
        {
            min = float.MaxValue;
            max = float.MinValue;
            for (int i = 0; i < data.Length; i++)
            {
                if (float.IsInfinity(data[i]) || float.IsNaN(data[i]) || float.IsNegativeInfinity(data[i]) || float.IsPositiveInfinity(data[i])) continue;
                if (min > data[i]) min = data[i];
                if (max < data[i]) max = data[i];
            }
        }

        /// <summary>
        /// GetReprFloat - returns representation of a float number which has no more than specified number is power
        ///                 examples: 
        ///                             GetReprFloat(0.26, 0) returns 2.6
        ///                             GetReprFloat(0.26, 1) returns 0.26
        ///                             GetReprFloat(0.026, 1) returns 2.6
        /// </summary>
        /// <param name="a">the float number to be processed</param>
        /// <param name="maxPowTrunc">max number of digits</param>
        /// <returns></returns>
        public static float GetReprFloat(float a, int maxPowTrunc)
        {
            double p = Math.Log10(Math.Abs(a));
            p = Math.Truncate(p) + ((p < 0) ? -1 : 0);
            int ip = (Math.Abs(p) > maxPowTrunc) ? ((p > 0) ? (int)p - maxPowTrunc : (int)p + maxPowTrunc) : 0;
            double g = Math.Pow(10, ip);
            double f = a / g;
            return (float)f;
        }

        /// <summary>
        /// GetRoundedLimits - adjusts step to be one of 1,2,2.5 and 5 (or 0.1, 0.2, 0.25 and 0.5) and change limits and count intervals between accordingly
        /// </summary>
        /// <param name="min">original min value</param>
        /// <param name="max">original max value</param>
        /// <param name="count">initial count of intervals between min and max</param>
        /// <param name="step">initial step</param>
        /// <param name="pow">the 10 pow of the result</param>
        public static void GetRoundedLimits(ref float min, ref float max, ref int count, ref float step, ref int pow)
        {
            step = (max - min) / (count - 1);
            float stepA = GetReprFloat(step, 0);
            float[] d = new float[] { 1, 2, 2.5f, 5 };
            float k = step / stepA;
            float minD = float.MaxValue;
            int index = -1;
            for (int i = 0; i < d.Length; i++)
            {
                if (Math.Abs(stepA - d[i]) < minD)
                {
                    minD = Math.Abs(stepA - d[i]);
                    index = i;
                }
            }
            step = d[index] * k;
            min = (float)(Math.Truncate(min / step) - 1) * step;
            max = (float)(Math.Truncate(max / step) + 1) * step;
            count = (int)((max - min) / step + 1);
            pow = (int)Math.Log10(k);
        }


        /// <summary>
        /// GetRoundedLimits - adjusts step to be one of 1,2,2.5 and 5 (or 0.1, 0.2, 0.25 and 0.5) and change limits and count intervals between accordingly
        /// </summary>
        /// <param name="min">original min value</param>
        /// <param name="max">original max value</param>
        /// <param name="count">initial count of intervals between min and max</param>
        /// <param name="step">initial step</param>
        /// <param name="pow">the 10 pow of the result</param>
        public static void GetRoundedLimits2(ref float min, ref float max, ref int count, ref float step, ref int pow)
        {
            RangeLabelsInfo rangeInfo = null;

            NumberFormatter formatter = new LimitFormatter();

            count = 10;

            rangeInfo = formatter.GetRangeLabelsInfo(min, max, 2, count);

            min = (float)rangeInfo.Min;
            max = (float)rangeInfo.Max;
            step = (float)rangeInfo.Step;
            pow = (int)rangeInfo.Pow;
        }

        /// <summary>
        /// RowsFromText - parser multiline text into data row, 5 float values each row. Returns null if there is an error
        /// </summary>
        /// <param name="text"></param>
        /// <param name="dataIndex"></param>
        /// <param name="splitChars"></param>
        /// <param name="ErrMessage">Error message during parsing</param>
        /// <returns></returns>
        public static List<float[]> RowsFromText(string text, out string ErrMessage, char[] splitChars = null)
        {
            List<float[]> Data = new List<float[]>();
            ErrMessage = "";
            if (splitChars == null) splitChars = new char[] { '\t', '\r', '\n', ',', ':', ' ',';' };
            string[] items;
            int index = 0;
            items = text.Trim().Split(splitChars).Where(s => !String.IsNullOrEmpty(s)).ToArray<string>();

            while (index < items.Length)
            {
                float[] lineData = new float[5];
                float f = 0;
                while (index < items.Length && string.IsNullOrEmpty(items[index])) index++;
                if (index < items.Length)
                {
                    if (!float.TryParse(items[index], out f))
                    {
                        ErrMessage = String.Format("Cannot parse value '{0}' at index {1}", items[index], index);
                        return null;
                    }
                }
                lineData[0] = f;
                index++;

                f = 0;
                while (index < items.Length && string.IsNullOrEmpty(items[index])) index++;
                if (index < items.Length)
                {
                    if (!float.TryParse(items[index], out f))
                    {

                        ErrMessage = String.Format("Cannot parse value {0} at index {1}", items[index], index);
                        return null;
                    }
                }
                lineData[1] = f;
                index++;

                Data.Add(lineData);

            }

            return Data;
        }
    }
}
