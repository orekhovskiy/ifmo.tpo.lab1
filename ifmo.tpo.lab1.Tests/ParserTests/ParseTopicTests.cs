using ifmo.tpo.lab1.Commons;
using static ifmo.tpo.lab1.Settings.Errors;
using static ifmo.tpo.lab1.Settings.AttributeOptions;
using NUnit.Framework;
using System;

namespace ifmo.tpo.lab1.Tests.ParserTests
{
    [TestFixture]
    class ParseTopicTests
    {
        [Test]
        public void NullTopicTest() 
        {
            string topic = null;
            var parseResult = Parser.ParseTopic(topic);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveAttributeRequiredError("Topic"),
                            (string)parseResult.Value);
        }

        [Test]
        public void TopicDoesNotExistTest1()
        {
            string topic = "abcabcabc";
            var parseResult = Parser.ParseTopic(topic);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNoDataFoundError("Topic", topic),
                            (string)parseResult.Value);
        }

        [Test]
        public void TopicDoesNotExistTest2()
        {
            string topic = "Test";
            var parseResult = Parser.ParseTopic(topic);
            Assert.IsFalse(parseResult.Success);
            Assert.AreEqual(GiveNoDataFoundError("Topic", topic),
                            (string)parseResult.Value);
        }

        [Test]
        public void ExistingTopicTest1()
        {
            string topic = "Football";
            var parseResult = Parser.ParseTopic(topic);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(topic, (string)parseResult.Value);
        }

        [Test]
        public void ExistingTopicTest2()
        {
            string topic = "Physics";
            var parseResult = Parser.ParseTopic(topic);
            Assert.IsTrue(parseResult.Success);
            Assert.AreEqual(topic, (string)parseResult.Value);
        }
    }
}
