using System.Runtime.CompilerServices;

namespace CW06;

public static class ConfigParser
{
    /// <summary>
    /// Parses one setting line in format: key=value
    /// </summary>
    public static (string key, string value) ParseSetting(
        string line,
        [CallerArgumentExpression("line")] string? lineExpr = null)
    {
        if (string.IsNullOrEmpty(line))
            throw new ArgumentNullException(lineExpr ?? nameof(line));

        var idx = line.IndexOf('=');
        if (idx < 0)
            throw new FormatException($"Invalid format for '{lineExpr ?? nameof(line)}': expected 'key=value'. Value: '{line}'");

        var key = line[..idx];
        var value = line[(idx + 1)..];
        return (key, value);
    }
}
