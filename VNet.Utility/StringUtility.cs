using System;


namespace VNet.Utility
{
    public static class StringUtility
    {
        public static string After(this string str, string searchTerm)
        {
            var idx = str.IndexOf(searchTerm, StringComparison.Ordinal);
            var result = idx >= 0 ? str[(idx + searchTerm.Length)..] : str;

            return result;
        }

        public static string Before(this string str, string searchTerm)
        {
            var idx = str.IndexOf(searchTerm, StringComparison.Ordinal);
            var result = idx >= 0 ? str[..idx] : str;

            return result;
        }

        public static bool IsNumber(this string s)
        {
            int tempInt;
            bool result = false;

            if (int.TryParse(s, out tempInt))
            {
                result = true;
            }

            return result;
        }

        public static string CreateRandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
