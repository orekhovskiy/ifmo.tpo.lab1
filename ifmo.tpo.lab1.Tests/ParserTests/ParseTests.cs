using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ifmo.tpo.lab1.Commons;
using static ifmo.tpo.lab1.Settings.Errors;
using static ifmo.tpo.lab1.Settings.AttributeOptions;
using NUnit.Framework;

namespace ifmo.tpo.lab1.Tests.ParserTests
{
    [TestFixture]
    public class ParseTests
    {
        [Test]
        public async Task WrongActionTest()
        {
            var correctAttributes = new CorrectAttributes();
            var parseResult = await Parser.Parse("wrong", correctAttributes.Topic, correctAttributes.Errors,
                correctAttributes.Interval, correctAttributes.Format, correctAttributes.Order);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongOptionError("Action"), ((List<string>)parseResult.Value).First());
        }

        [Test]
        public async Task WrongTopicTest()
        {
            var correctAttributes = new CorrectAttributes();
            var topic = "abcabcabc";
            var parseResult = await Parser.Parse(correctAttributes.Action, topic, correctAttributes.Errors,
                correctAttributes.Interval, correctAttributes.Format, correctAttributes.Order);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNoDataFoundError("Topic", topic), ((List<string>)parseResult.Value).First());
        }

        [Test]
        public async Task WrongErrorsTest()
        {
            var correctAttributes = new CorrectAttributes();
            var parseResult = await Parser.Parse(correctAttributes.Action, correctAttributes.Topic, "wrong",
                correctAttributes.Interval, correctAttributes.Format, correctAttributes.Order);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongOptionError("Errors"), ((List<string>)parseResult.Value).First());
        }

        [Test]
        public async Task WrongIntervalTest()
        {
            var correctAttributes = new CorrectAttributes();
            var parseResult = await Parser.Parse(correctAttributes.Action, correctAttributes.Topic, correctAttributes.Errors,
                "wrong", correctAttributes.Format, correctAttributes.Order);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongTypeError("Interval", "Integer"), ((List<string>)parseResult.Value).First());
        }

        [Test]
        public async Task WrongFormatTest()
        {
            var correctAttributes = new CorrectAttributes();
            var parseResult = await Parser.Parse(correctAttributes.Action, correctAttributes.Topic, correctAttributes.Errors,
                correctAttributes.Interval, "wrong", correctAttributes.Order);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongOptionError("Format"), ((List<string>)parseResult.Value).First());
        }

        [Test]
        public async Task WrongOrderTest()
        {
            var correctAttributes = new CorrectAttributes();
            var parseResult = await Parser.Parse(correctAttributes.Action, correctAttributes.Topic, correctAttributes.Errors,
                correctAttributes.Interval, correctAttributes.Format, "wrong");
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveWrongOptionError("Order"), ((List<string>)parseResult.Value).First());
        }
    }

    public struct CorrectAttributes
    {
        public string Action { get => "query"; }
        public string Topic{ get => "Football"; }
        public string Errors { get => "detailed"; }
        public string Interval { get => "5"; }
        public string Format { get => "html"; }
        public string Order { get => "alphabet"; }
    }
}