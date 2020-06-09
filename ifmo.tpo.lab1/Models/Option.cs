using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Models
{
    public class Option
    {
        public string Default { get; }
        public List<string> Values { get; }
        public string AttributeName { get; }

        public Option(string name, List<string> values, string def)
        {
            AttributeName = name;
            Values = values;
            Default = def;
        }
        public Option(List<string> values)
        {
            Values = values;
            Default = null;
        }
    }
}
