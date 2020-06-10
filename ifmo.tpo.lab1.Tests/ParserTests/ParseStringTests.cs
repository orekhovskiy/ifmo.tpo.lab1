using ifmo.tpo.lab1.Commons;
using static ifmo.tpo.lab1.Settings.Errors;
using static ifmo.tpo.lab1.Settings.AttributeOptions;
using NUnit.Framework;

namespace ifmo.tpo.lab1.Tests.ParserTests
{
    [TestFixture]
    class ParseStringTests
    {
        [Test]
        public void NullableStringTest1()
        {
            string str = null;
            var isNullable = true;
            var parseResult = Parser.ParseString(str, Errors, isNullable);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(Errors.Default,
                            (string)parseResult.Value);
        }

        [Test]
        public void NullableStringTest2()
        {
            string str = null;
            var parseResult = Parser.ParseString(str, Format);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(Format.Default,
                            (string)parseResult.Value);
        }

        [Test]
        public void NotNullableStringTest1()
        {
            string str = null;
            var isNullable = false;
            var parseResult = Parser.ParseString(str, Format, isNullable);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveAttributeRequiredError(Format.AttributeName),
                            (string)parseResult.Value);
        }

        [Test]
        public void NotNullableStringTest2()
        {
            string str = null;
            var isNullable = false;
            var parseResult = Parser.ParseString(str, Errors, isNullable);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveAttributeRequiredError(Errors.AttributeName),
                            (string)parseResult.Value);
        }

        [Test]
        public void WrongOptionStringTest1()
        {
            var str = "HTML";
            var parseResult = Parser.ParseString(str, Format);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongOptionError(Format.AttributeName),
                            (string)parseResult.Value);
        }

        [Test]
        public void WrongOptionStringTest2()
        {
            var str = "html";
            var parseResult = Parser.ParseString(str, Errors);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongOptionError(Errors.AttributeName),
                            (string)parseResult.Value);
        }

        [Test]
        public void CorrectOptionStringTest1()
        {
            var str = "html";
            var parseResult = Parser.ParseString(str, Format);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(str, 
                (string) parseResult.Value);
        }

        [Test]
        public void CorrectOptionStringTest2()
        {
            var str = "detailed";
            var parseResult = Parser.ParseString(str, Errors);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(str,
                (string)parseResult.Value);
        }
    }
}
