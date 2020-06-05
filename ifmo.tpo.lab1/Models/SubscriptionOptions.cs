using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Models
{
    public class SubscriptionOptions
    {
        public string Action { get; set; }
        public string Topic { get; set; }
        public string Errors { get; set; }
        public TimeSpan Interval { get; set; }
        public string Format { get; set; }
        public string Order { get; set; }

        public SubscriptionOptions()
        {

        }

        public SubscriptionOptions(string action, string topic, string errors,
            TimeSpan interval, string format, string order)
        {
            Action = action;
            Topic = topic;
            Errors = errors;
            Interval = interval;
            Format = format;
            Order = order;
        }
    }
}
