using System.Text;

namespace CW06;

public static class TextTransforms
{
    public static string Transform(string s, Func<string, string> strategy)
        => strategy(s);

    public static string TrimToUpper(string s)
        => s.Trim().ToUpperInvariant();

    public static string MaskDigits(string s)
    {
        var sb = new StringBuilder(s.Length);
        foreach (var ch in s)
            sb.Append(char.IsDigit(ch) ? '*' : ch);
        return sb.ToString();
    }
}
