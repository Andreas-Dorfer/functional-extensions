using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AD.FunctionalExtensions.Tests
{
    [TestClass]
    public class Option_IsSome
    {
        static readonly Random rnd = new Random();

        [TestMethod]
        public void Int()
        {
            var value = rnd.Next();
            var option = Option.Some(value);

            if (!option.IsSome(out var optionValue)) Fail(SomeExpected);

            AreEqual(value, optionValue);
        }

        [TestMethod]
        public void Int_None()
        {
            var option = Option<int>.None;

            IsFalse(option.IsSome(out var _), NoneExpected);
        }

        [TestMethod]
        public void Double()
        {
            var value = rnd.NextDouble();
            var option = Option.Some(value);

            if (!option.IsSome(out var optionValue)) Fail(SomeExpected);

            AreEqual(value, optionValue);
        }

        [TestMethod]
        public void Double_None()
        {
            var option = Option<double>.None;

            IsFalse(option.IsSome(out var _), NoneExpected);
        }

        [TestMethod]
        public void String()
        {
            var value = RandomString();
            var option = Option.Some(value);

            if (!option.IsSome(out var optionValue)) Fail(SomeExpected);

            AreEqual(value, optionValue);
        }

        [TestMethod]
        public void String_Empty()
        {
            var option = Option.Some("");

            if (!option.IsSome(out var optionValue)) Fail(SomeExpected);

            AreEqual("", optionValue);
        }

        [TestMethod]
        public void String_None()
        {
            var option = Option<string>.None;

            IsFalse(option.IsSome(out var _), NoneExpected);
        }

        [TestMethod]
        public void Class()
        {
            var value = new TestClass();
            var option = Option.Some(value);

            if (!option.IsSome(out var optionValue)) Fail(SomeExpected);

            AreEqual(value, optionValue);
        }

        [TestMethod]
        public void Class_None()
        {
            var option = Option<TestClass>.None;

            IsFalse(option.IsSome(out var _), NoneExpected);
        }

        [TestMethod]
        public void Class_Null()
        {
            var option = Option<TestClass>.Some(null);

            IsFalse(option.IsSome(out var _), NoneExpected);
        }

        [TestMethod]
        public void NullableInt()
        {
            int? value = rnd.Next();
            var option = Option.Some(value);

            if (!option.IsSome(out var optionValue)) Fail(SomeExpected);

            AreEqual(value, optionValue);
        }

        [TestMethod]
        public void NullableInt_None()
        {
            var option = Option<int?>.None;

            IsFalse(option.IsSome(out var _), NoneExpected);
        }

        [TestMethod]
        public void NullableInt_Null()
        {
            var option = Option<int?>.Some(null);

            IsFalse(option.IsSome(out var _), NoneExpected);
        }


        string RandomString() => Guid.NewGuid().ToString();

        class TestClass
        { }


        const string SomeExpected = "Some expected";
        const string NoneExpected = "None expected";
    }
}