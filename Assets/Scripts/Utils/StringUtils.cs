using System;

namespace TinyLittleStudio.EXAMPLE_PROJECT
{
    public static class StringUtils
    {
        public static bool EqualsIgnoreCase(this string a, string b)
        {
            return String.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
