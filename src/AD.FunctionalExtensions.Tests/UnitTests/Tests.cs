using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AD.FunctionalExtensions.Tests.UnitTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void All_Units_are_equal()
        {
            Unit a = default;
            Unit b = new();
            Unit c = Unit.Value;

            AssertAreEqual(a, b);
            AssertAreEqual(b, c);
        }

        static void AssertAreEqual(Unit a, Unit b)
        {
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
            Assert.AreEqual(a, b);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.AreEqual(a.ToString(), b.ToString());
        }
    }
}
