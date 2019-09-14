using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_CompareTo
    {
        [TestMethod]
        public void Compare()
        {
            var a = 1.Some();
            var b = 2.Some();

            IsTrue(a.CompareTo(b) < 0);
            IsTrue(a.CompareTo(a) == 0);
            IsTrue(b.CompareTo(a) > 0);
        }

        [TestMethod]
        public void Compare_FirstNone()
        {
            var a = Option<int>.None;
            var b = 2.Some();

            IsTrue(a.CompareTo(b) < 0);
            IsTrue(b.CompareTo(a) > 0);
        }

        [TestMethod]
        public void Compare_SecondNone()
        {
            var a = 1.Some();
            var b = Option<int>.None;

            IsTrue(a.CompareTo(b) > 0);
            IsTrue(b.CompareTo(a) < 0);
        }

        [TestMethod]
        public void CompareNone()
        {
            var a = Option<int>.None;
            var b = Option<int>.None;

            IsTrue(a.CompareTo(b) == 0);
        }

        [TestMethod]
        public void Compare_IComparable()
        {
            IComparable a = 1.Some();
            IComparable b = 2.Some();

            IsTrue(a.CompareTo(b) < 0);
            IsTrue(a.CompareTo(a) == 0);
            IsTrue(b.CompareTo(a) > 0);
        }
    }
}
