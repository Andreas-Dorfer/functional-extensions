using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class OptionEquals
    {
        readonly Random rnd = new Random();

        [TestMethod]
        public void Int()
        {
            var value = rnd.Next();
            var a = Option.Some(value);
            var b = Option.Some(value);

            AreEqual(a, b);
        }

        [TestMethod]
        public void Int_NotEqual()
        {
            var value = rnd.Next();
            var a = Option.Some(value);

            int next;
            do { next = rnd.Next(); } while (next == value);
            var b = Option.Some(next);

            AreNotEqual(a, b);
        }

        [TestMethod]
        public void Int_FirstNone()
        {
            var a = Option<int>.None;
            var b = Option.Some(rnd.Next());

            AreNotEqual(a, b);
        }

        [TestMethod]
        public void Int_SecondNone()
        {
            var a = Option.Some(rnd.Next());
            var b = Option<int>.None;

            AreNotEqual(a, b);
        }

        [TestMethod]
        public void Int_BothNone()
        {
            var a = Option<int>.None;
            var b = Option<int>.None;

            AreEqual(a, b);
        }
    }
}
