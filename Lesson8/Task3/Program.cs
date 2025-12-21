using System.Text;

Console.OutputEncoding = Encoding.UTF8;

string input = "  Hello    world   HELLO   cSharp   world   ";

var words = input
    .OrEmpty()
    .NormalizeSpaces()
    .Words()
    .Distinct()
    .OrderBy(w => w);

foreach (var w in words)
{
    Console.WriteLine(w);
}

static class TextExt
{
    public static string OrEmpty(this string s)
    {
        return s ?? "";
    }

    public static string NormalizeSpaces(this string s)
    {
        if (s == null) return "";
        var sb = new StringBuilder();
        bool lastSpace = false;

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (c == ' ')
            {
                if (!lastSpace)
                {
                    sb.Append(' ');
                    lastSpace = true;
                }
            }
            else
            {
                sb.Append(c);
                lastSpace = false;
            }
        }

        return sb.ToString().Trim();
    }

    public static IEnumerable<string> Words(this string s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return Enumerable.Empty<string>();

        return s
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.ToLowerInvariant());
    }
}
