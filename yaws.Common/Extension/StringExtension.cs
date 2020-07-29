using System;
using System.Collections.Generic;
using System.Text;

namespace yaws.Common.Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// First letter to uppercase
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToUpperFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
