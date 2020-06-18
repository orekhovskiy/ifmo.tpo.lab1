using System.Collections.Generic;
using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ifmo.tpo.lab1.Models;
using ifmo.tpo.lab1.Settings;

namespace ifmo.tpo.lab1.Commons
{
    public static class Requester
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<List<string>> GetPages(string topic)
        {
            var query = GetListUrl(topic);
            var json = await GetJson<ResponseJson>(query);

            return json.Query.CategoryMembers
                .Where(cm => cm.Ns == 0)
                .Select(cm => cm.Title).ToList();
        }

        public static string GetPageByTitleLink(string title, string format)
        {
            return GetPageUrl(title, format);
        }

        public static async Task<string> GetPageByTitle(string title, string format)
        {
            var url = GetPageUrl(title, format);
            HttpResponseMessage response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return default;
            }
        }

        public static async Task<bool> IsTopicExists(string topic)
        {
            var query = GetListUrl(topic);
            var json = await GetJson<ResponseJson>(query);
            return json.Query.CategoryMembers.Any();
        }

        private static async Task<T> GetJson<T>(string url)
        {
            var data = await GetRequest(url);
            T jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data.ToString());
            return jsonObject;
        }

        private static async Task<object> GetRequest(string url)
        {
            HttpResponseMessage response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<object>();

            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return default;
            }
        }

        private static string GetListUrl(string topic)
            => $"https://en.wikipedia.org/w/api.php?action=query&list=categorymembers&cmtitle=Category:{topic}&format=json"
                .Replace(" ", "_");

        private static string GetPageUrl(string title, string format)
            => $"https://en.wikipedia.org/api/rest_v1/page/{format}/{title}"
                .Replace(" ", "_");
    }
}
