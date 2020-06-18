using ifmo.tpo.lab1.Commons;
using static ifmo.tpo.lab1.Settings.Errors;
using static ifmo.tpo.lab1.Settings.AttributeOptions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Tests.ParserTests
{
    [TestFixture]
    class ParseTopicTests
    {
        [Test]
        public async Task NullTopicWithActionHelpTest() 
        {
            string topic = null;
            var parseResult = await Parser.ParseTopic(topic, "help");
            Assert.IsTrue(parseResult.Success);
        }

        [Test]
        public async Task NullTopicTest()
        {
            string topic = null;
            var parseResult = await Parser.ParseTopic(topic, "query");
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveAttributeRequiredError("Topic"),
                (string)parseResult.Value);
        }

        [Test]
        public async Task TopicDoesNotExistTest1()
        {
            string topic = "abcabcabc";
            var parseResult = await Parser.ParseTopic(topic, "query");
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNoDataFoundError("Topic", topic),
                            (string)parseResult.Value);
        }

        [Test]
        public async Task TopicDoesNotExistTest2()
        {
            string topic = "Test";
            var parseResult = await Parser.ParseTopic(topic, "query");
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNoDataFoundError("Topic", topic),
                            (string)parseResult.Value);
        }

        [Test]
        public async Task ExistingTopicTest1()
        {
            string topic = "Football";
            var parseResult = await Parser.ParseTopic(topic, "query");
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(topic, (string)parseResult.Value);
        }

        [Test]
        public async Task ExistingTopicTest2()
        {
            string topic = "Physics";
            var parseResult = await Parser.ParseTopic(topic, "query");
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(topic, (string)parseResult.Value);
        }
    }
}
