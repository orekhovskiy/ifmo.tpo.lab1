using ifmo.tpo.lab1.Models;
using static ifmo.tpo.lab1.Settings.Errors;
using ifmo.tpo.lab1.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ifmo.tpo.lab1.Commons
{
    public static class Parser
    {
        public static Result Parse(string action, string topic, string errors, string interval, string format, string order)
        {
            var error = new List<string>();
            var options = new Dictionary<SubscriptionOptions, object>();

            var result = ParseString(action, AttributeOptions.Action);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Action, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            result = ParseTopic(topic);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Topic, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            result = ParseString(errors, AttributeOptions.Errors);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Errors, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            result = ParseInterval(interval);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Interval, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            result = ParseString(format, AttributeOptions.Format);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Format, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            result = ParseString(order, AttributeOptions.Order);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Order, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            return error.Any() ? new Result(false, error): new Result(true, options);
        }

        public static Result ParseTopic(string topic)
        {
            if (topic is null)
            {
                var error = GiveAttributeRequiredError("Topic");
                return new Result(false, error);
            }
            // TODO: wiki check
            if (Requester.IsTopicExists(topic))
            {
                return new Result(true, topic);
            }
            else
            {
                var error = GiveNoDataFoundError("Topic", topic);
                return new Result(false, error);
            }
            
        }

        public static Result ParseInterval(string interval)
        {
            if (interval is null) 
            {
                return new Result(true, TimeSpan.FromDays(1));
            }
            if (int.TryParse(interval, out var value))
            {
                if (value < 0)
                {
                    return new Result(false, GiveNegativeIntervalError());
                }
                if (value == 0)
                {
                    return new Result(false, GiveZeroIntervalError());
                }
                return new Result(true, TimeSpan.FromSeconds(value));
            }
            else
            {
                var error = GiveWrongTypeError("Interval", "Integer");
                return new Result(false, error);
            }
        }

        public static Result ParseString(string value, Option option)
            => ParseString(value, option, true);

        public static Result ParseString(string value, Option option, bool nullable)
        {
            if (value is null)
            {
                var error = GiveAttributeRequiredError(option.AttributeName);
                return nullable ? new Result(true, option.Default) : new Result(false, error);
            }
            if (option.Values.Contains(value))
            {
                return new Result(true, value);
            }
            else
            {
                var error = GiveWrongOptionError(option.AttributeName);
                return new Result(false, error);
            }
        }
    }
}
