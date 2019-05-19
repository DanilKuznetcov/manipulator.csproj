using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        /// <summary>
        /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
        /// </summary>
		public static double GetABAngle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c < 0
                || a > b + c || b > a + c || c > a + b
                || (a == 0 && b == 0 && c == 0))
                return Double.NaN;
            return Math.PI / 2 - Math.Asin((a * a + b * b - c * c) / (2 * a * b));
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(0, 1, 1, double.NaN)]
        [TestCase(4, 1, 1, double.NaN)]
        [TestCase(-1, 1, 1, double.NaN)]
        [TestCase(1, 1, 1, 1.0471975511965976d)]
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            var actualAngle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, actualAngle);
        }
    }
}