using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Models
{
    public class Result
    {
        public bool Success { get; }
        public object Value { get; }

        public Result()
        {
            Success = default;
            Value = null;
        }

        public Result(bool success)
        {
            Success = success;
            Value = null;
        }

        public Result(bool success, object value)
        {
            Success = success;
            Value = value;
        }
    }
}
