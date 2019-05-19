using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var elbowPos = FindCoordinates(0, 0, shoulder, Manipulator.UpperArm);
            var wristPos = FindCoordinates(elbowPos.X,
                                           elbowPos.Y,
                                           shoulder + Math.PI + elbow,
                                           Manipulator.Forearm);
            var horizontal = shoulder + Math.PI + elbow;
            var palmEndPos = FindCoordinates(wristPos.X,
                                             wristPos.Y,
                                             horizontal + Math.PI + wrist,
                                             Manipulator.Palm);
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        static PointF FindCoordinates(double preX, double preY, double angle, float length)
        {
            var x = Math.Cos(angle) * length + preX;
            var y = Math.Sin(angle) * length + preY;
            var coordinates = new PointF((float)x, (float)y);
            return coordinates;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Manipulator.Forearm, Distance(joints[0], joints[1]));
        }

        float Distance(PointF first, PointF second)
        {
            var difX = second.X - first.X;
            var difY = second.Y - second.Y;
            return (float)Math.Sqrt(difX * difX + difY * difY);
        }
    }
}