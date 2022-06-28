
using System.Collections.Generic;
namespace KTB.DNet.Interface.Framework
{
    public static class StringHelper
    {
        public static List<string> Split(string str, int length, string delimeter = null)
        {
            List<string> result = new List<string>();
            if (string.IsNullOrEmpty(str))
            {
                return result;
            }

            string currentString = str.Trim();
            int lastIndex = 0;
            while (!string.IsNullOrEmpty(currentString))
            {

                string subString = SubString(currentString, length, out lastIndex);
                if (delimeter != null)
                {
                    if (currentString.Length > (lastIndex + 1))
                    {
                        if (currentString[lastIndex + 1].ToString() != delimeter)
                        {
                            subString = GetFirstSplited(subString, delimeter, out lastIndex);
                        }

                    }
                }

                result.Add(subString);
                currentString = SubStringFrom(currentString, lastIndex + 1).Trim();
            }

            return result;
        }

        public static string SubStringFrom(string str, int startIndex)
        {
            if (str.Length > startIndex)
            {
                return str.Substring(startIndex);
            }

            return string.Empty;
        }

        public static string SubString(string str, int length, out int lastIndex)
        {
            lastIndex = -1;
            if (!string.IsNullOrEmpty(str))
            {

                if (str.Length > length)
                {
                    lastIndex = length - 1;
                    return str.Substring(0, length);
                }
                else
                {
                    lastIndex = str.Length - 1;
                    return str;
                }
            }

            return string.Empty;
        }

        private static string GetFirstSplited(this string str, string delimeter, out int lastIndex)
        {
            lastIndex = str.Length - 1;
            int index = str.LastIndexOf(delimeter);
            if (index != -1)
            {
                lastIndex = index;
                return str.Substring(0, lastIndex);
            }

            return str;
        }

    }
}
