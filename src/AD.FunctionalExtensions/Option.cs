using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AD.FunctionalExtensions
{
    public struct Option<TValue> : IEquatable<Option<TValue>>, IStructuralEquatable, IComparable<Option<TValue>>, IComparable, IStructuralComparable
    {
        public static Option<TValue> None => default;

        public static Option<TValue> Some([AllowNull]TValue value) => new Option<TValue>(value);


        readonly TValue value;
        readonly bool isSome;

        Option([AllowNull]TValue value)
        {
            isSome = (this.value = value) is { };
        }


        public bool IsSome([NotNullWhen(true), MaybeNullWhen(false)]out TValue value)
        {
            value = this.value;
            return isSome;
        }


        public override bool Equals(object obj) =>
            obj is Option<TValue> other && Equals(other);

        public bool Equals(Option<TValue> other) =>
            Equals(other, EqualityComparer<TValue>.Default);

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) =>
            other is Option<TValue> otherOption && Equals(otherOption, (x, y) => comparer.Equals(x, y));

        public bool Equals(Option<TValue> other, IEqualityComparer<TValue> comparer) =>
            Equals(other, comparer.Equals);

        bool Equals(Option<TValue> other, Func<TValue, TValue, bool> equals) =>
            AreBothNone(other) ||
            AreBothSome(other) && equals(value, other.value);

        bool AreBothNone(Option<TValue> other) =>
            !(isSome || other.isSome);

        bool AreBothSome(Option<TValue> other) =>
            isSome && other.isSome;


        public override int GetHashCode() =>
            GetHashCode(EqualityComparer<TValue>.Default);

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) =>
            GetHashCode(obj => comparer.GetHashCode(obj));

        public int GetHashCode(IEqualityComparer<TValue> comparer) =>
            GetHashCode(comparer.GetHashCode);

        int GetHashCode(Func<TValue, int> getHashCode) =>
            isSome ? getHashCode(value) : int.MinValue;


        int IComparable.CompareTo(object obj)
        {
            if (!(obj is Option<TValue> other)) throw new ArgumentException();
            return CompareTo(other);
        }

        public int CompareTo(Option<TValue> other) =>
            CompareTo(other, Comparer<TValue>.Default);

        int IStructuralComparable.CompareTo(object other, IComparer comparer)
        {
            if (!(other is Option<TValue> otherOption)) throw new ArgumentException();
            return CompareTo(otherOption, (x, y) => comparer.Compare(x, y));
        }

        public int CompareTo(Option<TValue> other, IComparer<TValue> comparer) =>
            CompareTo(other, comparer.Compare);

        int CompareTo(Option<TValue> other, Func<TValue, TValue, int> compare)
        {
            if (!isSome)
            {
                return other.isSome ? -1 : 0;
            }
            return !other.isSome ? 1 : compare(value, other.value);
        }


        public static bool operator ==(Option<TValue> a, Option<TValue> b) =>
            a.Equals(b);

        public static bool operator !=(Option<TValue> a, Option<TValue> b) =>
            !(a == b);

        public override string ToString() => isSome ? $"Some({value})" : "None";
    }

    public static class Option
    {
        public static Option<TValue> None<TValue>() => Option<TValue>.None;

        public static Option<TValue> Some<TValue>([AllowNull]TValue value) => Option<TValue>.Some(value);
    }
}
