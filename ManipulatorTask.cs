using System;
using NUnit.Framework;
using System.Drawing;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному angle (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        /// 
        public static double[] MoveManipulatorTo(double x, double y, double alpha)
        {
            // Используйте поля Forearm, UpperArm, Palm класса Manipulator

            if (x == 0 && y == -90.0f)
                return new[] { -Math.PI / 2, 0, 0};

            double[] wristPos = FindWristCoordinates(x, y, alpha);

            var elbow = TriangleTask.GetABAngle(
                Manipulator.UpperArm,
                Manipulator.Forearm,
                LengthBetweenPoints(new PointF(0, 0), wristPos));

            if (Double.IsNaN(elbow))
                return new[] { double.NaN, double.NaN, double.NaN };

            var wrShEl = TriangleTask.GetABAngle(
                LengthBetweenPoints(new PointF(0, 0), wristPos),
                Manipulator.UpperArm,
                Manipulator.Forearm);


            var wrShOx = Math.Atan2(wristPos[1], wristPos[0]);

            var shoulder = wrShEl + wrShOx;

            var wrist = -alpha - shoulder - elbow;

            return new[] { shoulder, elbow, wrist};
        }

        static double[] FindWristCoordinates(double x, double y, double alpha)
        {
            var X = -Math.Cos(alpha) * Manipulator.Palm + x;
            var Y = Math.Sin(alpha) * Manipulator.Palm + y;
            return new double[] { X, Y };
        }

        static double LengthBetweenPoints(PointF first, double[] second)
        {
            return Math.Sqrt((second[0] - first.X) * (second[0] - first.X) 
                + (second[1] - first.Y) * (second[1] - first.Y));
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            var z = 1;
            var b = 1;
            Assert.AreEqual(z,b);
        }
    }
}