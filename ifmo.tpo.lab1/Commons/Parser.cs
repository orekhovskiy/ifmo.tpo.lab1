using ifmo.tpo.lab1.Models;
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
            var error = "";
            var options = new Dictionary<SubscriptionOptions, object>();

            var result = ParseString(action, Settings.Action);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Action, result.Value);
            }
            else
            {
                error += result.Value;
            }

            result = ParseTopic(topic);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Topic, result.Value);
            }
            else
            {
                error += result.Value;
            }

            result = ParseString(errors, Settings.Errors);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Errors, result.Value);
            }
            else
            {
                error += result.Value;
            }

            result = ParseInterval(interval);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Interval, result.Value);
            }
            else
            {
                error += result.Value;
            }

            result = ParseString(errors, Settings.Format);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Format, result.Value);
            }
            else
            {
                error += result.Value;
            }

            result = ParseString(errors, Settings.Order);
            if (result.Success)
            {
                options.Add(SubscriptionOptions.Order, result.Value);
            }
            else
            {
                error += result.Value;
            }

            return error == "" ? new Result(true, options) : new Result(false, error);
        }

        public static Result ParseTopic(object topic)
        {
            if (topic is null)
            {
                var error = "Attribute with the 'Topic' name required.\n";
                return new Result(false, error);
            }
            if (!(topic is string))
            {
                var error = "Attribute with the 'Topic' name should be type of String.\n";
            }
            // TODO: wiki check
            if (Requester.WikiCheck((string)topic))
            {
                return new Result(true, topic);
            }
            else
            {
                var error = "No data found by given value of the attribute named 'Topic'.\n";
                return new Result(false, error);
            }
        }

        public static Result ParseInterval(object interval)
        {
            if (interval is null) 
            {
                return new Result(true, TimeSpan.FromDays(1));
            }
            if (interval is int) 
            {
                return new Result(true, TimeSpan.FromSeconds((int)interval));
            }
            if (interval is TimeSpan) 
            {
                return new Result(true, interval);
            }
            var error = "Attribute with the 'Interval' name should be either Int or TimeSpan.\n";
            return new Result(false, error);
        }

        public static Result ParseString(object value, Option option)
            => ParseString(value, option, true);

        public static Result ParseString(object value, Option option, bool nullable)
        {
            if (nullable == true && option.Default is null)
            {
                throw new NullReferenceException("Default value should not be null.");
            }
            if (value is null)
            {
                var error = $"Attribute with the '{option}' name required.\n";
                return nullable == true ? new Result(true, option.Default) : new Result(false, error);
            }
            else if (!(value is string))
            {
                var error = $"Attribute with the '{option}' should be type of String.\n";
                return new Result(false, error);
            }
            else
            {
                if (option.Values.Contains((string)value))
                {
                    return new Result(true, value);
                }
                else
                {
                    var error = $"Value given to attribute '{option}' is incorrect.\n";
                    return new Result(false, error);
                }
            }
        }

        private static void ParseResult(Result result, ref object place, ref string error)
        {
            if (result.Success)
            {
                place = result.Value;
            }
            else
            {
                error += result.Value;
            }
        }
    }
}
