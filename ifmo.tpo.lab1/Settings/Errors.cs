using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ifmo.tpo.lab1.Settings
{
    public static class Errors
    {
        public static string GiveUnsupportedTypeError(string attr)
            => $"Attribute with the '{attr}' name has unsupported type.";
        public static string GiveWrongTypeError(string attr, string type)
            => $"Attribute with the '{attr}' name should has '{type} type.";
        public static string GiveNoDataFoundError(string attr, string value)
            => $"No data found by given value ('{value}') of the attribute named '{attr}'.";
        public static string GiveAttributeRequiredError(string attr)
            => $"Attribute with the '{attr}' name required.";
        public static string GiveWrongOptionError(string attr)
            => $"Value given to attribute '{attr}' is unexpected.";
        public static string GiveNegativeIntervalError()
            => $"Value given to attribute 'Interval' can not be negative.";
        public static string GiveZeroIntervalError()
            => $"Value given to attribute 'Interval' can not be zero.";
        public static string GiveGeneralError()
            => $"An error occured";
    }
}
