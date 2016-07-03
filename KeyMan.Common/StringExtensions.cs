using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ballance.Kms.Common
{
    public static class StringExtentions
    {
        public static string TrimOrExpandToLength(this string stringToTrim, int desiredLength = 32)
        {
            if (stringToTrim.Length < desiredLength)
            {
                return string.Concat(stringToTrim, new string(' ', stringToTrim.Length - desiredLength));
            }
            if (stringToTrim.Length > desiredLength)
            {
                return stringToTrim.Substring(0, desiredLength);
            }
            return stringToTrim;
        }
    }
}
