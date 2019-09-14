using System;
using System.Collections;
using System.Collections.Generic;

namespace AD.FunctionalExtensions
{
    public struct Option<TValue> : IEquatable<Option<TValue>>, IStructuralEquatable
    {
        public static Option<TValue> None { get; } = default;

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

        bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer) => other is Option<TValue> && Equals((Option<TValue>)other, comparer);

        bool Equals(Option<TValue> other, IEqualityComparer comparer) =>
            AreBothNone(other) ||
            AreBothSome(other) && AreValuesEqual(other, comparer);

        bool AreBothNone(Option<TValue> other) => !(isSome || other.isSome);

        bool AreBothSome(Option<TValue> other) => isSome && other.isSome;

        bool AreValuesEqual(Option<TValue> other, IEqualityComparer comparer) =>
            comparer.Equals(value, other.value);


        public override int GetHashCode() => GetHashCode(EqualityComparer<TValue>.Default);

        int IStructuralEquatable.GetHashCode(IEqualityComparer comparer) => GetHashCode(comparer);

        int GetHashCode(IEqualityComparer comparer) =>
            isSome ? comparer.GetHashCode(value) : int.MinValue;
    }

    public static class Option
    {
        public static Option<TValue> None<TValue>() => Option<TValue>.None;

        public static Option<TValue> Some<TValue>(TValue value) => Option<TValue>.Some(value);
    }
}
