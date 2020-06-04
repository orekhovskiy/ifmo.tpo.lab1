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
            var options = new SubscriptionOptions();

            var result = ParseString(action, Settings.Action);
            if (result.Success)
            {
                options.Action = (string)result.Value;
            }
            else
            {
                error += (string)result.Value;
            }

            result = ParseTopic(topic);

            result = ParseString(errors, Settings.Errors);
            if (result.Success)
            {
                options.Errors = (string)result.Value;
            }
            else
            {
                error += (string)result.Value;
            }

            result = ParseInterval(interval);

            result = ParseString(format, Settings.Format);


            return error == "" ? new Result(true, options) : new Result(false, error);
        }

        public static Result ParseTopic(object topic)
        {
            return new Result();
        }

        private Result ParseInterval(object interval)
        {
            return new Result();
        }

        public static Result ParseString(object value, Option option)
            => ParseString(value, option, true);

        public static Result ParseString(object value, Option option, bool nullable)
        {
            if (nullable == true && option.Default is null)
            {
                throw new Exception("Default value should not be null.");
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
    }
}
