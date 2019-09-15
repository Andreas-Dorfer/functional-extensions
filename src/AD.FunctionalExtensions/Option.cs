using System;
using System.Collections;
using System.Collections.Generic;

namespace AD.FunctionalExtensions
{
    public struct Option<TValue> : IEquatable<Option<TValue>>, IStructuralEquatable, IComparable<Option<TValue>>, IComparable, IStructuralComparable
    {
        public static Option<TValue> None => default;

        public static Option<TValue> Some(TValue value) => new Option<TValue>(value);


        readonly TValue value;
        readonly bool isSome;

        Option(TValue value)
        {
            isSome = (this.value = value) != null;
        }


        public bool IsSome(out TValue value)
        {
            value = this.value;
            return isSome;
        }


        public override bool Equals(object obj) => obj is Option<TValue> && Equals((Option<TValue>)obj);

        public bool Equals(Option<TValue> other) => Equals(other, EqualityComparer<TValue>.Default);

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) =>
            other is Option<TValue> && Equals((Option<TValue>)other, (x, y) => comparer.Equals(x, y));

        public bool Equals(Option<TValue> other, IEqualityComparer<TValue> comparer) =>
            Equals(other, comparer.Equals);

        bool Equals(Option<TValue> other, Func<TValue, TValue, bool> equals) =>
            !(isSome || other.isSome) ||
            isSome && other.isSome && equals(value, other.value);


        public override int GetHashCode() => GetHashCode(EqualityComparer<TValue>.Default);

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => GetHashCode(obj => comparer.GetHashCode(obj));

        public int GetHashCode(IEqualityComparer<TValue> comparer) => GetHashCode(comparer.GetHashCode);

        int GetHashCode(Func<TValue, int> getHashCode) => isSome ? getHashCode(value) : int.MinValue;


        int IComparable.CompareTo(object obj) => obj is Option<TValue> ? CompareTo((Option<TValue>)obj) : throw new ArgumentException();

        public int CompareTo(Option<TValue> other) => CompareTo(other, Comparer<TValue>.Default);

        int IStructuralComparable.CompareTo(object other, IComparer comparer) =>
            other is Option<TValue> ? CompareTo((Option<TValue>)other, (x, y) => comparer.Compare(x, y)) : throw new ArgumentException();

        public int CompareTo(Option<TValue> other, IComparer<TValue> comparer) => CompareTo(other, comparer.Compare);

        int CompareTo(Option<TValue> other, Func<TValue, TValue, int> compare)
        {
            if (!isSome)
            {
                return other.isSome ? -1 : 0;
            }
            return !other.isSome ? 1 : compare(value, other.value);
        }


        public override string ToString() => isSome ? $"Some({value})" : "None";
    }

    public static class Option
    {
        public static Option<TValue> None<TValue>() => Option<TValue>.None;

        public static Option<TValue> Some<TValue>(TValue value) => Option<TValue>.Some(value);
    }
}
