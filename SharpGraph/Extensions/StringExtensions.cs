using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGraph.Extensions
{
    public static class StringExtensions
    {
        public static char[] Replace(this char[] source, char[] oldChars, char newChar)
        {
            var replaced = new string(source);
            foreach (char oldChar in oldChars)
            {
                replaced = replaced.Replace(oldChar, newChar);
            }
            return replaced.ToCharArray();
        }

        public static string Replace(this string source, char[] oldChars, char newChar)
        {
            return new string(source.ToCharArray().Replace(oldChars, newChar));
        }
    }
}
