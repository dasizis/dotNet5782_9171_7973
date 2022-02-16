using System;
using System.Text;

namespace StringUtilities
{
    public static class StringUtilities
    {
        /// <summary>
        /// Prints a given enum
        /// each value in the following format:
        /// (numeric-value) - value
        /// </summary>
        /// <param name="enumType">The type of the enum</param>
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
        /// </summary>
        /// <example>CamelCaseText -> Camel case text</example>
        /// <param name="camelCase">The camelCase string</param>
        /// <returns>The readable text <see cref="string"/></returns>
        public static string CamelCaseToReadable(this string camelCase)
        {
            char[] letters = camelCase.ToCharArray();
            string result = "";

            foreach (var letter in letters)
            {
                result += char.IsUpper(letter) ? $" {letter}" : letter;
            }

            return letters[0] + result[2..];
        }

        /// <summary>
        /// Retutns a string in a title format
        /// </summary>
        /// <param name="title"><The title string/param>
        /// <returns>The string in a title format</returns>
        public static string ToTitleFormat(this string title)
        {
            return "=================================" +
                   title +
                   "=================================";
        }

        /// <summary>
        /// Indents a string by the wanted indention level
        /// </summary>
        /// <param name="str">The string to be indented</param>
        /// <param name="indentionLevel">The indention level</param>
        /// <returns></returns>
        public static string Indent(this string str, int indentionLevel = 1)
        {
            var indention = new string('\t', indentionLevel);

            return indention + str.Replace("\n", '\n' + indention);
        }
    }
}
