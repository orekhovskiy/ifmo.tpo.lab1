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
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task GetSubscription(string action, string topic,
            string errors, string interval, string format, string order)
        {
            var parseResult = Parser.Parse(action, topic, errors, interval, format, order);
            if (parseResult.Success)
            {
                var dictionary = (Dictionary<SubscriptionOptions, object>)parseResult.Value;
                var delayTime = (TimeSpan)dictionary[SubscriptionOptions.Interval];
                while (true)
                {
                    var data = await Requester.GetData((Dictionary<SubscriptionOptions, object>)parseResult.Value);
                    await Clients.Caller.SendAsync("ReceiveSubscription", data);
                    await Task.Delay(delayTime);
                }
            }
            else
            {
                var parsedErrors = Parser.ParseString(errors, AttributeOptions.Errors);
                if (parsedErrors.Success & (string)parsedErrors.Value == "detailed")
                {
                    await Clients.Caller.SendAsync("ReceiveError", parseResult.Value);
                }
                else
                {
                    var generalError = GiveGeneralError();
                    await Clients.Caller.SendAsync("ReceiveError", new List<string>() { generalError });
                }
            }
        }
    }
}
