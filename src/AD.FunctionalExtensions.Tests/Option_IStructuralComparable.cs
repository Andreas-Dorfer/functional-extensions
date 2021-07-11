using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_IStructuralComparable
    {
        static readonly Random rnd = new();

        [TestMethod]
        public void Compare()
        {
            var aValue = rnd.Next();
            var a = aValue.Some();
            int bValue;
            do { bValue = rnd.Next(); } while (bValue == aValue);
            var b = bValue.Some();

            var compareValue = rnd.Next();
            var comparer = new Comparer<int>((x, y) =>
            {
                AreEqual(aValue, x);
                AreEqual(bValue, y);

                return compareValue;
            });

            AreEqual(compareValue, a.CompareTo(b, comparer));
            AreEqual(compareValue, ((IStructuralComparable)a).CompareTo(b, comparer));
        }

        [TestMethod]
        public void Compare_FirstNone()
        {
            var a = Option<int>.None;
            var b = rnd.Next().Some();

            var comparer = new Comparer<int>((_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IComparable.CompareTo))));

            IsTrue(a.CompareTo(b, comparer) < 0);
            IsTrue(((IStructuralComparable)a).CompareTo(b, comparer) < 0);
        }

        [TestMethod]
        public void Compare_SecondNone()
        {
            var a = rnd.Next().Some();
            var b = Option<int>.None;

            var comparer = new Comparer<int>((_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IComparable.CompareTo))));

            IsTrue(a.CompareTo(b, comparer) > 0);
            IsTrue(((IStructuralComparable)a).CompareTo(b, comparer) > 0);
        }

        [TestMethod]
        public void Compare_BothNone()
        {
            var a = Option<int>.None;
            var b = Option<int>.None;

            var comparer = new Comparer<int>((_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IComparable.CompareTo))));

            AreEqual(0, a.CompareTo(b, comparer));
            AreEqual(0, ((IStructuralComparable)a).CompareTo(b, comparer));
        }


        class Comparer<T> : IComparer, IComparer<T>
        {
            readonly Func<object?, object?, int> compare;

            public Comparer(Func<object?, object?, int> compare)
            {
                this.compare = compare;
            }

            public int Compare(object? x, object? y) => compare(x, y);

            public int Compare([AllowNull]T x, [AllowNull]T y) => compare(x, y);
        }


        static string MustNotBeCalled(string name) => $"'{name}' must not be called";
    }
}
