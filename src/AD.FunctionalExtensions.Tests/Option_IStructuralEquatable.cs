using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_IStructuralEquatable
    {
        static readonly Random rnd = new Random();

        [TestMethod]
        public void Equals_True()
        {
            var value = rnd.Next();
            IStructuralEquatable a = Option.Some(value);
            IStructuralEquatable b = Option.Some(value);

            var comparerEqualsCalled = false;
            var comparer = new EqualityComparer(equals: (x, y) =>
            {
                AreEqual(value, x);
                AreEqual(value, y);

                comparerEqualsCalled = true;
                return true;
            });

            IsTrue(a.Equals(b, comparer));
            IsTrue(comparerEqualsCalled);
        }

        [TestMethod]
        public void Equals_False()
        {
            var aValue = rnd.Next();
            IStructuralEquatable a = Option.Some(aValue);
            int bValue;
            do { bValue = rnd.Next(); } while (bValue == aValue);
            IStructuralEquatable b = Option.Some(bValue);

            var comparerEqualsCalled = false;
            var comparer = new EqualityComparer(equals: (x, y) =>
            {
                AreEqual(aValue, x);
                AreEqual(bValue, y);

                comparerEqualsCalled = true;
                return false;
            });

            IsFalse(a.Equals(b, comparer));
            IsTrue(comparerEqualsCalled);
        }

        [TestMethod]
        public void Equals_FirstNone()
        {
            IStructuralEquatable a = Option<int>.None;
            IStructuralEquatable b = rnd.Next().Some();

            var comparer = new EqualityComparer(equals: (_, __) => throw new AssertFailedException($"'{nameof(IStructuralEquatable.Equals)}' must not be called"));

            IsFalse(a.Equals(b, comparer));
        }

        [TestMethod]
        public void Equals_SecondNone()
        {
            IStructuralEquatable a = rnd.Next().Some();
            IStructuralEquatable b = Option<int>.None;

            var comparer = new EqualityComparer(equals: (_, __) => throw new AssertFailedException($"'{nameof(IStructuralEquatable.Equals)}' must not be called"));

            IsFalse(a.Equals(b, comparer));
        }

        [TestMethod]
        public void Equals_BothNone()
        {
            IStructuralEquatable a = Option<int>.None;
            IStructuralEquatable b = Option<int>.None;

            var comparer = new EqualityComparer(equals: (_, __) => throw new AssertFailedException($"'{nameof(IStructuralEquatable.Equals)}' must not be called"));

            IsTrue(a.Equals(b, comparer));
        }

        [TestMethod]
        public void GetHashCode_Some()
        {
            var value = rnd.Next();
            IStructuralEquatable a = value.Some();

            var hashCode = rnd.Next();
            var comparer = new EqualityComparer(getHashCode: obj =>
            {
                AreEqual(value, obj);
                return hashCode;
            });

            AreEqual(hashCode, a.GetHashCode(comparer));
        }

        [TestMethod]
        public void GetHashCode_None()
        {
            IStructuralEquatable a = Option<int>.None;

            var comparer = new EqualityComparer(getHashCode: _ => throw new AssertFailedException($"'{nameof(IStructuralEquatable.GetHashCode)}' must not be called"));

            var expected = Option<int>.None.GetHashCode();
            AreEqual(expected, a.GetHashCode(comparer));
        }


        class EqualityComparer : IEqualityComparer
        {
            readonly Func<object, object, bool> equals;
            readonly Func<object, int> getHashCode;

            public EqualityComparer(
                Func<object, object, bool> equals = null,
                Func<object, int> getHashCode = null)
            {
                this.equals = equals;
                this.getHashCode = getHashCode;
            }

            public new bool Equals(object x, object y) => equals(x, y);

            public int GetHashCode(object obj) => getHashCode(obj);
        }
    }
}
