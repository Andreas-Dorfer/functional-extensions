using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_IStructuralEquatable
    {
        static readonly Random rnd = new();

        [TestMethod]
        public void Equals_True()
        {
            var value = rnd.Next();
            var a = Option.Some(value);
            var b = Option.Some(value);

            var comparerEqualsCalled = false;
            var comparer = new EqualityComparer<int>(equals: (x, y) =>
            {
                AreEqual(value, x);
                AreEqual(value, y);

                comparerEqualsCalled = true;
                return true;
            });

            IsTrue(a.Equals(b, comparer));
            IsTrue(((IStructuralEquatable)a).Equals(b, comparer));
            IsTrue(comparerEqualsCalled);
        }

        [TestMethod]
        public void Equals_False()
        {
            var aValue = rnd.Next();
            var a = Option.Some(aValue);
            int bValue;
            do { bValue = rnd.Next(); } while (bValue == aValue);
            var b = Option.Some(bValue);

            var comparerEqualsCalled = false;
            var comparer = new EqualityComparer<int>(equals: (x, y) =>
            {
                AreEqual(aValue, x);
                AreEqual(bValue, y);

                comparerEqualsCalled = true;
                return false;
            });

            IsFalse(a.Equals(b, comparer));
            IsFalse(((IStructuralEquatable)a).Equals(b, comparer));
            IsTrue(comparerEqualsCalled);
        }

        [TestMethod]
        public void Equals_FirstNone()
        {
            var a = Option<int>.None;
            var b = rnd.Next().Some();

            var comparer = new EqualityComparer<int>(equals: (_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IStructuralEquatable.Equals))));

            IsFalse(a.Equals(b, comparer));
            IsFalse(((IStructuralEquatable)a).Equals(b, comparer));
        }

        [TestMethod]
        public void Equals_SecondNone()
        {
            var a = rnd.Next().Some();
            var b = Option<int>.None;

            var comparer = new EqualityComparer<int>(equals: (_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IStructuralEquatable.Equals))));

            IsFalse(a.Equals(b, comparer));
            IsFalse(((IStructuralEquatable)a).Equals(b, comparer));
        }

        [TestMethod]
        public void Equals_BothNone()
        {
            var a = Option<int>.None;
            var b = Option<int>.None;

            var comparer = new EqualityComparer<int>(equals: (_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IStructuralEquatable.Equals))));

            IsTrue(a.Equals(b, comparer));
            IsTrue(((IStructuralEquatable)a).Equals(b, comparer));
        }

        [TestMethod]
        public void GetHashCode_Some()
        {
            var value = rnd.Next();
            var a = value.Some();

            var hashCode = rnd.Next();
            var comparer = new EqualityComparer<int>(getHashCode: obj =>
            {
                AreEqual(value, obj);
                return hashCode;
            });

            AreEqual(hashCode, a.GetHashCode(comparer));
            AreEqual(hashCode, ((IStructuralEquatable)a).GetHashCode(comparer));
        }

        [TestMethod]
        public void GetHashCode_None()
        {
            var a = Option<int>.None;

            var comparer = new EqualityComparer<int>(getHashCode: _ => throw new AssertFailedException(MustNotBeCalled(nameof(IStructuralEquatable.GetHashCode))));

            var expected = Option<int>.None.GetHashCode();
            AreEqual(expected, a.GetHashCode(comparer));
            AreEqual(expected, ((IStructuralEquatable)a).GetHashCode(comparer));
        }


        class EqualityComparer<T> : IEqualityComparer, IEqualityComparer<T>
        {
            readonly Func<object?, object?, bool> equals;
            readonly Func<object?, int> getHashCode;

            public EqualityComparer(
                Func<object?, object?, bool> equals = null!,
                Func<object?, int> getHashCode = null!)
            {
                this.equals = equals;
                this.getHashCode = getHashCode;
            }

            public new bool Equals(object? x, object? y) => equals(x, y);

            public bool Equals([AllowNull]T x, [AllowNull]T y) => equals(x, y);

            public int GetHashCode(object obj) => getHashCode(obj);

            public int GetHashCode([DisallowNull]T obj) => getHashCode(obj);
        }


        static string MustNotBeCalled(string name) => $"'{name}' must not be called";
    }
}
