using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_CompareTo
    {
        static readonly Random rnd = new();

        [TestMethod]
        public void Compare()
        {
            var a = 1.Some();
            var b = 2.Some();

            IsTrue(a.CompareTo(b) < 0);
            IsTrue(a < b);
            AreEqual(0, a.CompareTo(a));
            IsTrue(a <= b);
            IsTrue(b >= a);
            IsTrue(b.CompareTo(a) > 0);
            IsTrue(b > a);
        }

        [TestMethod]
        public void Compare_FirstNone()
        {
            var a = Option<int>.None;
            var b = rnd.Next().Some();

            IsTrue(a.CompareTo(b) < 0);
            IsTrue(a < b);
            IsTrue(b.CompareTo(a) > 0);
            IsTrue(b > a);
        }

        [TestMethod]
        public void Compare_SecondNone()
        {
            var a = rnd.Next().Some();
            var b = Option<int>.None;

            IsTrue(a.CompareTo(b) > 0);
            IsTrue(a > b);
            IsTrue(b.CompareTo(a) < 0);
            IsTrue(b < a);
        }

        [TestMethod]
        public void CompareNone()
        {
            var a = Option<int>.None;
            var b = Option<int>.None;

            AreEqual(0, a.CompareTo(b));
            IsTrue(a <= b);
            IsTrue(a >= b);
        }

        [TestMethod]
        public void Compare_IComparable()
        {
            IComparable a = 1.Some();
            IComparable b = 2.Some();

            IsTrue(a.CompareTo(b) < 0);
            AreEqual(0, a.CompareTo(a));
            IsTrue(b.CompareTo(a) > 0);
        }
    }
}
