using System;
using System.Diagnostics;

namespace AD.FunctionalExtensions
{
    public struct Option<TValue> : IEquatable<Option<TValue>>
    {
        public static Option<TValue> None { get; } = default;

        public static Option<TValue> Some(TValue value) => value != null ? new Option<TValue>(value) : None;


        readonly TValue value;
        readonly bool isSome;

        Option(TValue value)
        {
            Debug.Assert(value != null, $"'{nameof(value)}' must not be null.");
            this.value = value;
            isSome = true;
        }


        public bool IsSome(out TValue value)
        {
            if (isSome)
            {
                value = this.value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }


        public bool Equals(Option<TValue> other) =>
            AreBothNone(other) ||
            AreBothSome(other) && AreValuesEqual(other);

        public override bool Equals(object obj) => obj is Option<TValue> ? Equals((Option<TValue>)obj) : false;

        public override int GetHashCode() => isSome ? value.GetHashCode() : int.MinValue;


        bool AreBothNone(Option<TValue> other) => !(isSome || other.isSome);

        bool AreBothSome(Option<TValue> other) => isSome && other.isSome;

        bool AreValuesEqual(Option<TValue> other) => value.Equals(other.value);
    }

    public static class Option
    {
        public static Option<TValue> None<TValue>() => Option<TValue>.None;

        public static Option<TValue> Some<TValue>(TValue value) => Option<TValue>.Some(value);
    }
}
