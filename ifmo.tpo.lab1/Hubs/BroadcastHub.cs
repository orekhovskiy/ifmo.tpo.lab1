using ifmo.tpo.lab1.Models;
using ifmo.tpo.lab1.Commons;
using ifmo.tpo.lab1.Settings;
using static ifmo.tpo.lab1.Settings.AttributeOptions;
using static ifmo.tpo.lab1.Settings.Errors;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ifmo.tpo.lab1.Hubs
{
    public class BroadcastHub : Hub
    {
        public async Task GetSubscription(string action, string topic,
            string errors, string interval, string format, string order)
        {
            var parseResult = await Parser.Parse(action, topic, errors, interval, format, order);
            if (parseResult.Success)
            {
                var dictionary = (Dictionary<SubscriptionOptions, object>)parseResult.Value;
                var delayTime = (TimeSpan)dictionary[SubscriptionOptions.Interval];
                var parsedFormat = (string)dictionary[SubscriptionOptions.Format];
                var parsedOrder = (string)dictionary[SubscriptionOptions.Order];

                var parsedAction = (string) dictionary[SubscriptionOptions.Action];
                if (parsedAction == "help")
                {
                    var helpMessage = GetHelp();
                    await Clients.Caller.SendAsync("ReceiveResponse", new List<string>() { helpMessage });
                }

                var pages = await Requester.GetPages(topic);
                pages = SortPages(pages, parsedOrder);

                foreach (var pageTitle in pages)
                {
                    var data = Requester.GetPageByTitleLink(pageTitle, parsedFormat);
                    await Clients.Caller.SendAsync("ReceiveSubscription", data);
                    await Task.Delay(delayTime);
                }
            }
            else
            {
                var parsedErrors = Parser.ParseString(errors, AttributeOptions.Errors);
                if (parsedErrors.Success & (string)parsedErrors.Value == "detailed")
                {
                    await Clients.Caller.SendAsync("ReceiveResponse", parseResult.Value);
                }
                else
                {
                    var generalError = GiveGeneralError();
                    await Clients.Caller.SendAsync("ReceiveResponse", new List<string>() { generalError });
                }
            }
        }

        private List<string> SortPages(List<string> pages, string order)
        {
            switch (order)
            {
                case "alphabet":
                    pages.Sort();
                    break;
                case "random":
                    var rng = new Random();
                    int n = pages.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        var value = pages[k];
                        pages[k] = pages[n];
                        pages[n] = value;
                    }
                    break;
                default:
                    break;
            }
            return pages;
        }

        private string GetHelp()
            => "Help";
    }
}
