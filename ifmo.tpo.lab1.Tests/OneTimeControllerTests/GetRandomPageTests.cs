using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ifmo.tpo.lab1.Controllers;
using NUnit.Framework;

namespace ifmo.tpo.lab1.Tests.OneTimeControllerTests
{
    [TestFixture]
    class GetRandomPageTests
    {
        [Test]
        public async Task WrongTopicTest()
        {
            var topic = "gasdfhs";
            var oneTimeController = new OneTimeController();
            var result = await oneTimeController.GetRandomPage(topic);
            Assert.AreEqual("No data found.", result);
        }

        [Test]
        public async Task CorrectAttributes()
        {
            var topic = "Physics";
            var oneTimeController = new OneTimeController();
            var result = await oneTimeController.GetRandomPage(topic);
            Assert.IsTrue("No data found." != result);
        }
    }
}
