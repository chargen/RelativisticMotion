using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RelativisticMotion;

namespace RelativisticMotionTest
{
    [TestClass]
    public class RelativisticObservableTest
    {
        [TestMethod]
        [ExpectedException(typeof(CausalityViolationException))]
        public void FtlMotionIsImpossible()
        {
            BasicRelativisticObservable o = new BasicRelativisticObservable(new Vector4(0, 0, 0, 0), 10);
            o.Move(new Vector4(20, 0, 0, 1));
        }

        [TestMethod]
        public void ObjectAtRestIsAlwaysInSameLocation()
        {
            BasicRelativisticObservable o = new BasicRelativisticObservable(new Vector4(0, 0, 0, 0), 10);

            var locations = o.Observe(new Vector4(1, 2, 3, 4)).ToArray();
            Assert.AreEqual(1, locations.Length);
            Assert.AreEqual(new Vector4(0, 0, 0, 0), locations[0]);
        }

        [TestMethod]
        public void DistantObjectIsUnobservableAfterSmallTime()
        {
            BasicRelativisticObservable o = new BasicRelativisticObservable(new Vector4(0, 0, 0, 0), 10);

            var locations = o.Observe(new Vector4(11, 0, 0, 1)).ToArray();
            Assert.AreEqual(0, locations.Length);
        }

        [TestMethod]
        public void MovingObjectIsSuitablyDelayed()
        {
            //Object moving along XAxis at light speed
            BasicRelativisticObservable o = new BasicRelativisticObservable(new Vector4(0, 0, 0, 0), 10);
            o.Move(new Vector4(1f, 0, 0, 1));
            o.Move(new Vector4(2f, 0, 0, 2));
            o.Move(new Vector4(3f, 0, 0, 3));
            o.Move(new Vector4(4f, 0, 0, 4));

            //Stationary observer sitting at -5 on XAxis

            //Nothing can be seen at time 0
            var t0 = o.Observe(new Vector4(-100, 0, 0, 0)).ToArray();
            Assert.AreEqual(0, t0.Length);

            //the first location can be seen at time 10
            var t10 = o.Observe(new Vector4(-100, 0, 0, 10)).ToArray();
            Assert.AreEqual(1, t10.Length);
            Assert.AreEqual(new Vector4(0, 0, 0, 0), t10[0]);

            //the first 2 locations can be seen at time 11
            var t12 = o.Observe(new Vector4(-100, 0, 0, 12)).ToArray();
            Assert.AreEqual(2, t12.Length);
            Assert.AreEqual(new Vector4(1, 0, 0, 1), t12[0]);
            Assert.AreEqual(new Vector4(0, 0, 0, 0), t12[1]);

            //Everything can be seen at time 10000
            var t1000 = o.Observe(new Vector4(-100, 0, 0, 1000)).ToArray();
            Assert.AreEqual(5, t1000.Length);
        }
    }
}
