using System;

namespace AD.FunctionalExtensions
{
    public static class OptionExtensions
    {
        public static Option<T> Some<T>(this T value) => Option.Some(value);

        public static U Match<T, U>(this Option<T> option, Func<T, U> onIsSome, Func<U> onIsNone)
        {
            if (onIsSome is null) throw new ArgumentNullException(nameof(onIsSome));
            if (onIsNone is null) throw new ArgumentNullException(nameof(onIsNone));

            return option.IsSome(out var value) ? onIsSome(value) : onIsNone();
        }

        public static bool IsSome<T>(this Option<T> option) =>
            option.Match(
                onIsSome: _ => true,
                onIsNone: () => false);

        public static bool IsNone<T>(this Option<T> option) => !option.IsSome();
    }
}
