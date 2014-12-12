using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fleetcom.Library.Tests.Physics
{
    [TestClass]
    public class Velocity
    {
        [TestMethod]
        public void TimeToSpeed()
        {
            var time = Library.Physics.Velocity.TimeToSpeed(5, 0, 1);
            Assert.AreEqual(5, time);

            time = Library.Physics.Velocity.TimeToSpeed(12, 0, 3);
            Assert.AreEqual(4, time);

            time = Library.Physics.Velocity.TimeToSpeed(50, 0, 19);
            Assert.AreEqual(2.63, time, 0.01f);
        }

        [TestMethod]
        public void DistanceToStop()
        {
            var dist = Library.Physics.Velocity.DistanceToStop(5, 5, 1);
            Assert.AreEqual(12.5, dist, 0.1f);

            dist = Library.Physics.Velocity.DistanceToStop(12, 4, 3);
            Assert.AreEqual(24, dist, 0.1f);

            dist = Library.Physics.Velocity.DistanceToStop(50, 2.63f, 19);
            Assert.AreEqual(65.78f, dist, 0.01f);
        }
    }
}
