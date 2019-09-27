using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_Equals
    {
        static readonly Random rnd = new Random();

        [TestMethod]
        public void Equal()
        {
            var value = rnd.Next();
            var a = Option.Some(value);
            var b = Option.Some(value);

            AreEqual(a, b);
        }

        [TestMethod]
        public void NotEqual()
        {
            var value = rnd.Next();
            var a = Option.Some(value);

            int next;
            do { next = rnd.Next(); } while (next == value);
            var b = Option.Some(next);

            AreNotEqual(a, b);
        }

        [TestMethod]
        public void FirstNone()
        {
            var a = Option<int>.None;
            var b = Option.Some(rnd.Next());

            AreNotEqual(a, b);
        }

        [TestMethod]
        public void SecondNone()
        {
            var a = Option.Some(rnd.Next());
            var b = Option<int>.None;

            AreNotEqual(a, b);
        }

        [TestMethod]
        public void BothNone()
        {
            var a = Option<int>.None;
            var b = Option<int>.None;

            AreEqual(a, b);
        }


        void AreEqual<TValue>(Option<TValue> a, Option<TValue> b) where TValue : notnull
        {
            Assert.AreEqual(a, b);
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        void AreNotEqual<TValue>(Option<TValue> a, Option<TValue> b) where TValue : notnull
        {
            Assert.AreNotEqual(a, b);
            Assert.IsFalse(a.Equals(b));
            Assert.IsTrue(a != b);
        }
    }
}
