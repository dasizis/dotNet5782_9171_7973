using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringUtilities
{
    public static class StringUtilities
    {
        /// <summary>
        /// prints a given enum
        /// each value in the following format:
        /// (numeric-value) - value
        /// </summary>
        /// <param name="enumType">the type of the enum</param>
        /// <param name="numOfTabs">tabs before each enum value</param>
        public static string EnumToString(Type enumType)
        {
            StringBuilder result = new ();

            foreach (var option in Enum.GetValues(enumType))
            {
                result.Append($"\n{(int)option} - {CamelCaseToReadable(option.ToString())}");
            }

            return result.ToString();
        }

        /// <summary>
        /// Converts a cameCase string to readable text 
        /// Example: CamelCaseText -> Camel case text
        /// </summary>
        /// <param name="camelCase">the camelCase string</param>
        /// <returns>the readable text</returns>
        static string CamelCaseToReadable(this string camelCase)
        {
            char[] letters = camelCase.ToCharArray();
            string result = "";

            foreach (var l in letters)
            {
                string letter = l.ToString();
                result += letter == letter.ToUpper() ? $" {letter}" : letter;
            }

            return letters[0] + result[2..];
        }

        /// <summary>
        /// prints a title
        /// </summary>
        /// <param name="title">the title string</param>
        public static string ToTitleFormat(this string title)
        {
            return "=================================" +
                   title +
                   "=================================";
        }

        public static string Indent(this string str, int indentionLevel = 1)
        {
            var indention = new string('\t', indentionLevel);

            return indention + str.Replace("\n", '\n' + indention);
        }
    }
}
