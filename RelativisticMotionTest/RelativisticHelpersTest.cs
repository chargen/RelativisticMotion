using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using RelativisticMotion;

namespace RelativisticMotionTest
{
    [TestClass]
    class RelativisticHelpersTest
    {
        [TestMethod]
        public void IsFasterThanLightSuccessfullyDetectsSlowerThanLight()
        {
            Assert.IsFalse(RelativisticHelpers.IsFasterThanLight(new Vector4(0, 0, 0, 0), new Vector4(0, 0, 0, 1), 2)); //Stationary
            Assert.IsFalse(RelativisticHelpers.IsFasterThanLight(new Vector4(0, 0, 0, 0), new Vector4(1, 0, 0, 1), 2));
            Assert.IsFalse(RelativisticHelpers.IsFasterThanLight(new Vector4(0, 0, 0, 0), new Vector4(2, 0, 0, 1), 2)); //Speed Of Light
        }

        [TestMethod]
        public void IsFasterThanLightSuccessfullyDetectsFasterThanLight()
        {
            Assert.IsTrue(RelativisticHelpers.IsFasterThanLight(new Vector4(0, 0, 0, 0), new Vector4(2.001f, 0, 0, 1), 2));
            Assert.IsTrue(RelativisticHelpers.IsFasterThanLight(new Vector4(0, 0, 0, 0), new Vector4(3, 0, 0, 1), 2));
            Assert.IsTrue(RelativisticHelpers.IsFasterThanLight(new Vector4(0, 0, 0, 0), new Vector4(4, 0, 0, 1), 2));
        }
    }
}
