using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_IStructuralComparable
    {
        static readonly Random rnd = new Random();

        [TestMethod]
        public void Compare()
        {
            var aValue = rnd.Next();
            IStructuralComparable a = aValue.Some();
            int bValue;
            do { bValue = rnd.Next(); } while (bValue == aValue);
            IStructuralComparable b = bValue.Some();

            var compareValue = rnd.Next();
            var comparer = new Comparer((x, y) =>
            {
                AreEqual(aValue, x);
                AreEqual(bValue, y);

                return compareValue;
            });

            AreEqual(compareValue, a.CompareTo(b, comparer));
        }

        [TestMethod]
        public void Compare_FirstNone()
        {
            IStructuralComparable a = Option<int>.None;
            IStructuralComparable b = rnd.Next().Some();

            var comparer = new Comparer((_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IComparable.CompareTo))));

            IsTrue(a.CompareTo(b, comparer) < 0);
        }

        [TestMethod]
        public void Compare_SecondNone()
        {
            IStructuralComparable a = rnd.Next().Some();
            IStructuralComparable b = Option<int>.None;

            var comparer = new Comparer((_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IComparable.CompareTo))));

            IsTrue(a.CompareTo(b, comparer) > 0);
        }

        [TestMethod]
        public void Compare_BothNone()
        {
            IStructuralComparable a = Option<int>.None;
            IStructuralComparable b = Option<int>.None;

            var comparer = new Comparer((_, __) => throw new AssertFailedException(MustNotBeCalled(nameof(IComparable.CompareTo))));

            AreEqual(0, a.CompareTo(b, comparer));
        }


        class Comparer : IComparer
        {
            readonly Func<object, object, int> compare;

            public Comparer(Func<object, object, int> compare)
            {
                this.compare = compare;
            }

            public int Compare(object x, object y) => compare(x, y);
        }


        static string MustNotBeCalled(string name) => $"'{name}' must not be called";
    }
}
