using System;
using System.Linq;
using System.Threading.Tasks;
using ifmo.tpo.lab1.Commons;
using Microsoft.AspNetCore.Mvc;

namespace ifmo.tpo.lab1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OneTimeController : ControllerBase
    {
        // https://localhost:44395/api/onetime/page?title=Football&format=html
        [HttpGet]
        [ActionName("Page")]
        public async Task<string> GetPageByTitle([FromQuery] string title, [FromQuery] string format)
        {
            if (format != "html" && format != "title")
            {
                return "Wrong format.";
            }
            var page = await Requester.GetPageByTitle(title, format);
            return page ?? "No data found.";
        }

        // https://localhost:44395/api/onetime/random?topic=football
        [HttpGet]
        [ActionName("Random")]
        public async Task<string> GetRandomPage([FromQuery] string topic)
        {
            var rng = new Random();
            var pages = await Requester.GetPages(topic);
            if (!pages.Any()) return "No data found.";
            var title = pages[rng.Next(pages.Count)];
            var page = await Requester.GetPageByTitle(title, "html");
            return page;
        }
    }
}