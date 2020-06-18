using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ifmo.tpo.lab1.Controllers;
using NUnit.Framework;

namespace ifmo.tpo.lab1.Tests.OneTimeControllerTests
{
    [TestFixture]
    class GetPageByTitleTests
    {
        [Test]
        public async Task WrongFormatTest()
        {
            var title = "football";
            var format = "xml";
            var oneTimeController = new OneTimeController();
            var result = await oneTimeController.GetPageByTitle(title, format);
            Assert.AreEqual("Wrong format.", result);
        }

        [Test]
        public async Task WrongTitleTest()
        {
            var title = "adgsdfkgh";
            var format = "html";
            var oneTimeController = new OneTimeController();
            var result = await oneTimeController.GetPageByTitle(title, format);
            Assert.AreEqual("No data found.", result);
        }

        [Test]
        public async Task CorrectAttributesTest()
        {
            var title = "Football";
            var format = "html";
            var oneTimeController = new OneTimeController();
            var result = await oneTimeController.GetPageByTitle(title, format);
            Assert.IsTrue("No data found." != result && "Wrong format." != result);
        }
    }
}
