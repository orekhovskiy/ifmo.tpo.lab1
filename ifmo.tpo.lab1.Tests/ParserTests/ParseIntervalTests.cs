using ifmo.tpo.lab1.Commons;
using static ifmo.tpo.lab1.Settings.Errors;
using static ifmo.tpo.lab1.Settings.AttributeOptions;
using NUnit.Framework;
using System;

namespace ifmo.tpo.lab1.Tests.ParserTests
{
    [TestFixture]
    class ParseIntervalTests
    {
        [Test]
        public void NullIntervalTest()
        {
            string interval = null;
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(TimeSpan.FromDays(1),
                            (TimeSpan)parseResult.Value);
        }

        [Test]
        public void WrongTypeTest1()
        {
            var interval = "5 seconds";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongTypeError("Interval", "Integer"),
                            (string)parseResult.Value);
        }

        [Test]
        public void WrongTypeTest2()
        {
            var interval = TimeSpan.FromSeconds(5).ToString();
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongTypeError("Interval", "Integer"),
                            (string)parseResult.Value);
        }

        [Test]
        public void WrongTypeTest3()
        {
            var interval = "5,5";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongTypeError("Interval", "Integer"),
                            (string)parseResult.Value);
        }

        [Test]
        public void NegativeIntervalTest1()
        {
            var interval = "-5";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNegativeIntervalError(),
                            (string)parseResult.Value);
        }

        [Test]
        public void NegativeIntervalTest2()
        {
            var interval = "-1";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNegativeIntervalError(),
                            (string)parseResult.Value);
        }

        [Test]
        public void ZeroIntervalTest1()
        {
            var interval = "0";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveZeroIntervalError(),
                            (string)parseResult.Value);
        }

        [Test]
        public void ZeroIntervalTest2()
        {
            var interval = "-0";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveZeroIntervalError(),
                            (string)parseResult.Value);
        }

        [Test]
        public void PositiveIntervalTest1()
        {
            var interval = "5";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(TimeSpan.FromSeconds(5), (TimeSpan)parseResult.Value);
        }

        [Test]
        public void PositiveIntervalTest2()
        {
            var interval = "1";
            var parseResult = Parser.ParseInterval(interval);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(TimeSpan.FromSeconds(1), (TimeSpan)parseResult.Value);
        }
    }
}
