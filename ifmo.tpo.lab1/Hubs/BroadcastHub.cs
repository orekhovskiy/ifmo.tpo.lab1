using ifmo.tpo.lab1.Models;
using ifmo.tpo.lab1.Commons;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Hubs
{
    public class BroadcastHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task GetSubscription(object? action, object? topic, 
            object? errors, object? interval, object? format, object? order)
        {
            await Clients.Caller.SendAsync("ReceiveError", 
                $"{action} {topic} {errors} {interval} {format} {order}");
            return;
            var parseResult = Parser.Parse(action, topic, errors, interval, format, order);
            if (parseResult.Success)
            {
                while (true)
                {
                    var data = Requester.GetData((SubscriptionOptions)parseResult.Value);
                    await Clients.Caller.SendAsync("ReceiveSubscription", data);
                    //await Task.Delay();
                }
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveError", parseResult.Value);
            }
        }
    }
}
