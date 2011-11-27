using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeaconpushSharp
{
    internal static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
