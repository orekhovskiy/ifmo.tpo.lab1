using ifmo.tpo.lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Commons
{
    public static class Requester
    {

        // to get a list of pages
        // en.wikipedia.org/w/api.php?action=query&list=categorymembers&
        // cmtitle=Category:<name>&format=json

        // to get a title of a page
        // en.wikipedia.org/api/rest_v1/page/title/<title>

        private static HttpClient client = new HttpClient();

        public static async Task<object> GetData(Dictionary<SubscriptionOptions, object> options)
        {
            var pages = await GetPages((string)options[SubscriptionOptions.Topic]);
            return null;
        }

        public static bool WikiCheck(string topic)
        {
            // TODO:
            return true;
        }

        private static async Task<List<string>>GetPages(string topic)
        {
            var pages = new List<string>();
            var query = GetListUrl(topic);

            //var query = "https://en.wikipedia.org/w/api.php?action=query&prop=extracts&format=json&exintro=&titles=Stack%20Overflow";
            var json = await GetRequest<string>(query);
            
            return pages;
        }

        private static async Task<T> GetRequest<T>(string url)
        {
            HttpResponseMessage responce = await client.GetAsync(url);
            if (responce.IsSuccessStatusCode)
            {
                return await responce.Content.ReadAsAsync<T>();
            }
            return default;
        }

        private static string GetListUrl(string topic)
            => $"https://en.wikipedia.org/w/api.php?action=query&list=categorymembers&cmtitle=Category:{topic}&format=json";

        private static string GetPageUrl(string title, string format)
            => $"https://en.wikipedia.org/api/rest_v1/page/{format}/{title}";
    }
}
