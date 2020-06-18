using System.Threading.Tasks;
using ifmo.tpo.lab1.Commons;
using NUnit.Framework;

namespace ifmo.tpo.lab1.Tests.RequesterTests
{
    [TestFixture]
    public class IsTopicExistsTests
    {
        [Test]
        public async Task DoesNotExistTest1()
        {
            var topic = "asgasdg";
            var exists = await Requester.IsTopicExists(topic);
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task DoesNotExistTest2()
        {
            var topic = "Test";
            var exists = await Requester.IsTopicExists(topic);
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task ExistsTest1()
        {
            var topic = "Football";
            var exists = await Requester.IsTopicExists(topic);
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsTest2()
        {
            var topic = "Physics";
            var exists = await Requester.IsTopicExists(topic);
            Assert.IsTrue(exists);
        }
    }
}