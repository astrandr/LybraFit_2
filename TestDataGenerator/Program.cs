using System;
using System.IO;

namespace TestDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateConstant("12.345", -10, 10, 1, 12.345);
            GenerateLinear("12.345x+3.567", -10, 10, 1, 12.345, 3.567);
            GeneratePolynomial3("12.345x^2-3.567x+7.891", -10, 10, 1, 12.345, -3.567, 7.891);
            GeneratePolynomial4("12.345x^3-3.567x^2+7.891x+0.923", -10, 10, 1, 12.345, -3.567, 7.891, 0.932);
            GeneratePolynomial5("12.345x^4-3.567x^3+7.891x^2+0.923x-9.359", -10, 10, 1, 12.345, -3.567, 7.891, 0.932, -9.359);
        }

        private static void GenerateConstant(string name, double x1, double x2, double stepX, double a)
        {
            var file = File.CreateText("Constant_" + name + ".txt");

            for (double x = x1; x <= x2; x += stepX)
            {
                var y = a;
                file.WriteLine("{0:F3}\t{1:F3}", x, y);
            }

            file.Close();
        }

        private static void GenerateLinear(string name, double x1, double x2, double stepX, double a, double b)
        {
            var file = File.CreateText("Linear_" + name + ".txt");

            for (double x = x1; x <= x2; x += stepX)
            {
                var y = a * x + b;
                file.WriteLine("{0:F3}\t{1:F3}", x, y);
            }

            file.Close();
        }

        private static void GeneratePolynomial3(string name, double x1, double x2, double stepX, double a1, double a2, double a3)
        {
            var file = File.CreateText("Polynomial3_" + name + ".txt");

            for (double x = x1; x <= x2; x += stepX)
            {
                var y = a1 * Math.Pow(x, 2) + a2 * x + a3;
                file.WriteLine("{0:F3}\t{1:F3}", x, y);
            }

            file.Close();
        }

        private static void GeneratePolynomial4(string name, double x1, double x2, double stepX, double a1, double a2, double a3, double a4)
        {
            var file = File.CreateText("Polynomial4_" + name + ".txt");

            for (double x = x1; x <= x2; x += stepX)
            {
                var y = a1 * Math.Pow(x, 3) + a2 * Math.Pow(x, 2) + a3 * x + a4;
                file.WriteLine("{0:F3}\t{1:F3}", x, y);
            }

            file.Close();
        }

        private static void GeneratePolynomial5(string name, double x1, double x2, double stepX, double a1, double a2, double a3, double a4, double a5)
        {
            var file = File.CreateText("Polynomial5_" + name + ".txt");

            for (double x = x1; x <= x2; x += stepX)
            {
                var y = a1 * Math.Pow(x, 4) + a2 * Math.Pow(x, 3) + a3 * Math.Pow(x, 2) + a4 * x + a5;
                file.WriteLine("{0:F3}\t{1:F3}", x, y);
            }

            file.Close();
        }
    }
}
