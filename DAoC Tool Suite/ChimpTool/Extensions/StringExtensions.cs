using System.Globalization;

namespace DAoCToolSuite.ChimpTool.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string s)
        {
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(s.ToLower());
        }
    }
}
