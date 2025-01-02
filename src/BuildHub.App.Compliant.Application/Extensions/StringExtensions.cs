using System.Text.RegularExpressions;

namespace BuildHub.App.Compliant.Application.Extensions;

public static class StringExtensions
{
    public static string SplitCamelCase(this string input)
    {
        return Regex
            .Replace(input, "([A-Z])", " $1", RegexOptions.Compiled)
            .Trim();
    }
}