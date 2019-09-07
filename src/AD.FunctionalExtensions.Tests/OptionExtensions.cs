﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                    onIsNone: () => throw new AssertFailedException("'onIsSome' expected"));

            AreEqual(matchedValue, actual);
        }

        [TestMethod]
        public void Match_IsNone()
        {
            var option = Option<int>.None;

            var matchedValue = rnd.NextDouble();

            var actual =
                option.Match(
                    onIsSome: _ => throw new AssertFailedException("'onIsNone' expected"),
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
    }
}
