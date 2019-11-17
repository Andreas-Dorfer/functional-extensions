using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class OptionExtensions
    {
        static readonly Random rnd = new Random();

        [TestMethod]
        public void Some()
        {
            var value = rnd.Next();
            var a = Option.Some(value);
            var b = value.Some();

            AreEqual(a, b);
        }

        [TestMethod]
        public void AsOption_Class()
        {
            var value = new TestClass();
            var a = Option.Some(value);
            var b = value.Some();

            AreEqual(a, b);
        }

        [TestMethod]
        public void AsOption_NullableClass()
        {
            static TestClass? GetValue() => new TestClass();

            var value = GetValue();
            var a = Option.Create(value);
            var b = value.AsOption();

            AreEqual(a, b);
        }

        [TestMethod]
        public void AsOption_NullableClass_Null()
        {
            static TestClass? GetValue() => null;

            var value = GetValue();
            var a = Option.Create(value);
            var b = value.AsOption();

            AreEqual(a, b);
        }

        [TestMethod]
        public void AsOption_NullableInt()
        {
            static int? GetValue() => rnd.Next();

            var value = GetValue();
            var a = Option.Create(value);
            var b = value.AsOption();

            AreEqual(a, b);
        }

        [TestMethod]
        public void AsOption_NullableInt_Null()
        {
            static int? GetValue() => null;

            var value = GetValue();
            var a = Option.Create(value);
            var b = value.AsOption();

            AreEqual(a, b);
        }

        [TestMethod]
        public void Match_IsSome()
        {
            var value = rnd.Next();
            var option = value.Some();

            var matchedValue = rnd.NextDouble();

            var actual =
                option.Match(
                    onIsSome: v =>
                    {
                        AreEqual(value, v);
                        return matchedValue;
                    },
                    onIsNone: () => throw new AssertFailedException("'onIsNone' must not be called"));

            AreEqual(matchedValue, actual);
        }

        [TestMethod]
        public void Match_IsNone()
        {
            var option = Option<int>.None;

            var matchedValue = rnd.NextDouble();

            var actual =
                option.Match(
                    onIsSome: _ => throw new AssertFailedException("'onIsSome' must not be called"),
                    onIsNone: () => matchedValue);

            AreEqual(matchedValue, actual);
        }

        [TestMethod]
        public void IsSome_True()
        {
            var some = rnd.Next().Some();

            IsTrue(some.IsSome());
        }

        [TestMethod]
        public void IsSome_False()
        {
            var none = Option<int>.None;

            IsFalse(none.IsSome());
        }

        [TestMethod]
        public void IsNone_True()
        {
            var some = rnd.Next().Some();

            IsFalse(some.IsNone());
        }

        [TestMethod]
        public void IsNone_False()
        {
            var none = Option<int>.None;

            IsTrue(none.IsNone());
        }

        [TestMethod]
        public void Bind_SomeToSome()
        {
            var value = rnd.Next();
            var some = value.Some();

            var bound = rnd.NextDouble().Some();

            var actual =
                some.Bind(
                    binder: v =>
                    {
                        AreEqual(value, v);
                        return bound;
                    });

            AreEqual(bound, actual);
        }

        [TestMethod]
        public void Bind_SomeToNone()
        {
            var value = rnd.Next();
            var some = value.Some();

            var bound = Option<double>.None;

            var actual =
                some.Bind(
                    binder: v =>
                    {
                        AreEqual(value, v);
                        return bound;
                    });

            AreEqual(bound, actual);
        }

        [TestMethod]
        public void Bind_None()
        {
            var none = Option<int>.None;

            var actual =
                none.Bind<int, double>(
                    binder: _ => throw new AssertFailedException("'binder' must not be called"));

            IsTrue(actual.IsNone());
        }

        [TestMethod]
        public void Map_Some()
        {
            var value = rnd.Next();
            var some = value.Some();

            var mappedValue = rnd.NextDouble();

            var actual =
                some.Map(
                    mapper: v =>
                    {
                        AreEqual(value, v);
                        return mappedValue;
                    });

            AreEqual(mappedValue.Some(), actual);
        }

        [TestMethod]
        public void Map_None()
        {
            var none = Option<int>.None;

            var actual =
                none.Map<int, double>(
                    mapper: _ => throw new AssertFailedException("'mapper' must not be called"));

            IsTrue(actual.IsNone());
        }

        [TestMethod]
        public void MapNullable_Some()
        {
            static int? GetValue() => rnd.Next();
            static double? GetMappedValue() => rnd.NextDouble();

            int? value = GetValue();
            var some = value.AsOption();

            var mappedValue = GetMappedValue();

            var actual =
                some.MapNullable(
                    mapper: v =>
                    {
                        AreEqual(value, v);
                        return mappedValue;
                    });

            AreEqual(mappedValue.AsOption(), actual);
        }

        [TestMethod]
        public void MapNullable_SomeToNull()
        {
            static int? GetValue() => rnd.Next();

            int? value = GetValue();
            var some = value.AsOption();

            var actual =
                some.MapNullable<int, double>(
                    mapper: v =>
                    {
                        AreEqual(value, v);
                        return null;
                    });

            IsTrue(actual.IsNone());
        }

        [TestMethod]
        public void MapNullable_None()
        {
            var none = Option<int>.None;

            var actual =
                none.MapNullable<int, double>(
                    mapper: _ => throw new AssertFailedException("'mapper' must not be called"));

            IsTrue(actual.IsNone());
        }

        [TestMethod]
        public void MapNullable_Class_Some()
        {
            static int? GetValue() => rnd.Next();
            static TestClass? GetMappedValue() => new TestClass();

            int? value = GetValue();
            var some = value.AsOption();

            var mappedValue = GetMappedValue();

            var actual =
                some.MapNullable(
                    mapper: v =>
                    {
                        AreEqual(value, v);
                        return mappedValue;
                    });

            AreEqual(mappedValue.AsOption(), actual);
        }

        [TestMethod]
        public void MapNullable_Class_SomeToNull()
        {
            static int? GetValue() => rnd.Next();

            int? value = GetValue();
            var some = value.AsOption();

            var actual =
                some.MapNullable<int, TestClass>(
                    mapper: v =>
                    {
                        AreEqual(value, v);
                        return null;
                    });

            IsTrue(actual.IsNone());
        }

        [TestMethod]
        public void MapNullable_Class_None()
        {
            var none = Option<int>.None;

            var actual =
                none.MapNullable<int, TestClass>(
                    mapper: _ => throw new AssertFailedException("'mapper' must not be called"));

            IsTrue(actual.IsNone());
        }

        [TestMethod]
        public void DefaultValue_Some()
        {
            var some = 3.Some();

            var value = some.DefaultValue(5);

            AreEqual(3, value);
        }

        [TestMethod]
        public void DefaultValue_None()
        {
            var some = Option<int>.None;

            var value = some.DefaultValue(5);

            AreEqual(5, value);
        }

        [TestMethod]
        public void Filter_Some_True()
        {
            var some = 3.Some();

            var filtered = some.Filter(_ => _ == 3);

            AreEqual(some, filtered);
        }

        [TestMethod]
        public void Filter_Some_False()
        {
            var some = 3.Some();

            var filtered = some.Filter(_ => _ != 3);

            IsTrue(filtered.IsNone());
        }

        [TestMethod]
        public void Filter_None()
        {
            var none = Option<int>.None;

            var filtered = none.Filter(_ => true);

            IsTrue(filtered.IsNone());
        }
    }
}
