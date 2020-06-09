using ifmo.tpo.lab1.Models;
using static ifmo.tpo.lab1.Commons.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Commons
{
    public static class Parser
    {
        public static Result Parse(object? action, object? topic, object? errors, object? interval, object? format, object? order)
        {
            var error = new List<string>();
            var options = new Dictionary<SubscriptionOptions, object>();

            var result = ParseString(action, Settings.Action);
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

            result = ParseString(errors, Settings.Errors);
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

            result = ParseString(format, Settings.Format);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Format, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            result = ParseString(order, Settings.Order);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Order, result.Value);
            }
            else
            {
                error.Add((string)result.Value);
            }

            return error.Count() == 0 ? new Result(true, options) : new Result(false, error);
        }

        public static Result ParseTopic(object topic)
        {
            if (topic is null)
            {
                var error = GiveAttributeRequiredError("Topic");
                return new Result(false, error);
            }
            if (topic is System.Text.Json.JsonElement json)
            {
                /*if (!(topic is string))
                {
                    var error = GiveWrongTypeError("Topic", "string");
                    return new Result(false, error);
                }*/
                var value = json.GetString();
                // TODO: wiki check
                if (Requester.WikiCheck(value))
                {
                    return new Result(true, value);
                }
                else
                {
                    var error = GiveNoDataFoundError("Topic", value);
                    return new Result(false, error);
                }
            }
            else
            {
                var error = GiveUnsupportedTypeError("Topic");
                return new Result(false, error);
            }
            
        }

        public static Result ParseInterval(object interval)
        {
            if (interval is null) 
            {
                return new Result(true, TimeSpan.FromDays(1));
            }
            if (interval is System.Text.Json.JsonElement json)
            {
                int value;
                if (int.TryParse(json.GetString(), out value))
                {
                    return new Result(true, TimeSpan.FromSeconds(value));
                }
                else
                {
                    var error = GiveWrongTypeError("Interval", "Integer");
                    return new Result(false, error);
                }
            }
            else
            {
                var error = GiveUnsupportedTypeError("Interval");
                return new Result(false, error);
            }
        }

        public static Result ParseString(object value, Option option)
            => ParseString(value, option, true);

        public static Result ParseString(object value, Option option, bool nullable)
        {
            if (value is null)
            {
                var error = GiveAttributeRequiredError(option.AttributeName);
                return nullable == true ? new Result(true, option.Default) : new Result(false, error);
            }

            if (value is System.Text.Json.JsonElement json)
            {
                /*if (!(value is string))
                {
                    var error = GiveWrongTypeError(option.AttributeName, "string");
                    return new Result(false, error);
                }*/
                var strValue = json.GetString();
                if (option.Values.Contains(strValue))
                {
                    return new Result(true, strValue);
                }
                else
                {
                    var error = GiveWrongOptionError(option.AttributeName);
                    return new Result(false, error);
                }
            }
            else
            {
                var error = GiveUnsupportedTypeError(option.AttributeName);
                return new Result(false, error);
            }
        }
    }
}
