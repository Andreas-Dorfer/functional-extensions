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

        public static Option<U> Bind<T, U>(this Option<T> option, Func<T, Option<U>> binder)
        {
            if (binder is null) throw new ArgumentNullException(nameof(binder));

            return option.Match(
                onIsSome: binder,
                onIsNone: Option.None<U>);
        }

        public static Option<U> Map<T, U>(this Option<T> option, Func<T, U> mapper)
        {
            if (mapper is null) throw new ArgumentNullException(nameof(mapper));

            return option.Bind(
                value => mapper(value).Some());
        }
    }
}
