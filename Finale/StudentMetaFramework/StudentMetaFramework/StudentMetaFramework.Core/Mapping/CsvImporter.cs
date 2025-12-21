using System.Globalization;
using System.Reflection;

namespace StudentMetaFramework.Core.Mapping;

public record CsvRowError(int LineNumber, string Message, string RawLine);

public class CsvImportResult<T>
{
    public List<T> Items { get; } = new();
    public List<CsvRowError> Errors { get; } = new();
}

public class CsvImporter
{
    // Мінімальна реалізація: без підтримки лапок/ескейпінгу CSV.
    // Формат очікується простий: значення розділені комою (або ; якщо коми немає).
    public CsvImportResult<T> Import<T>(string path) where T : new()
    {
        if (!File.Exists(path))
            throw new FileNotFoundException("CSV file not found", path);

        var lines = File.ReadAllLines(path);
        if (lines.Length == 0)
            throw new InvalidOperationException("CSV is empty.");

        var separator = DetectSeparator(lines[0]);
        var header = SplitLine(lines[0], separator);

        // map "ColumnName" -> index
        var colIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < header.Length; i++)
            colIndex[header[i].Trim()] = i;

        // Build property mapping list: property -> index
        var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var map = new List<(PropertyInfo Prop, int Index)>();

        foreach (var p in props)
        {
            if (p.GetCustomAttribute<IgnoreAttribute>() is not null)
                continue;

            var colAttr = p.GetCustomAttribute<ColumnAttribute>();
            if (colAttr is null)
                continue; // property not mapped

            if (!colIndex.TryGetValue(colAttr.Name, out var idx))
                throw new InvalidOperationException($"Column '{colAttr.Name}' not found in header for type {typeof(T).Name}.");

            if (!p.CanWrite)
                continue;

            map.Add((p, idx));
        }

        var result = new CsvImportResult<T>();

        // Data rows start from line 2 => index 1
        for (int lineIdx = 1; lineIdx < lines.Length; lineIdx++)
        {
            var raw = lines[lineIdx];
            if (string.IsNullOrWhiteSpace(raw))
                continue;

            var parts = SplitLine(raw, separator);
            var obj = new T();
            bool rowOk = true;

            foreach (var (prop, idx) in map)
            {
                var text = idx < parts.Length ? parts[idx].Trim() : "";

                if (!TryConvert(text, prop.PropertyType, out var value))
                {
                    result.Errors.Add(new CsvRowError(
                        lineIdx + 1,
                        $"Cannot convert value '{text}' to {prop.PropertyType.Name} for property {prop.Name}.",
                        raw
                    ));
                    rowOk = false;
                    break;
                }

                try
                {
                    prop.SetValue(obj, value);
                }
                catch (Exception ex)
                {
                    result.Errors.Add(new CsvRowError(
                        lineIdx + 1,
                        $"Failed to set property {prop.Name}: {ex.Message}",
                        raw
                    ));
                    rowOk = false;
                    break;
                }
            }

            if (rowOk)
                result.Items.Add(obj);
        }

        return result;
    }

    private static char DetectSeparator(string headerLine)
        => headerLine.Contains(';') && !headerLine.Contains(',') ? ';' : ',';

    private static string[] SplitLine(string line, char separator)
        => line.Split(separator);

    private static bool TryConvert(string text, Type targetType, out object? value)
    {
        // handle Nullable<T>
        var nullableUnderlying = Nullable.GetUnderlyingType(targetType);
        if (nullableUnderlying is not null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                value = null;
                return true;
            }
            targetType = nullableUnderlying;
        }

        if (targetType == typeof(string))
        {
            value = text;
            return true;
        }

        if (targetType == typeof(int))
        {
            if (int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i))
            {
                value = i;
                return true;
            }
            value = null;
            return false;
        }

        if (targetType == typeof(double))
        {
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
            {
                value = d;
                return true;
            }
            value = null;
            return false;
        }

        if (targetType == typeof(bool))
        {
            if (bool.TryParse(text, out var b))
            {
                value = b;
                return true;
            }
            value = null;
            return false;
        }

        // last resort
        try
        {
            value = Convert.ChangeType(text, targetType, CultureInfo.InvariantCulture);
            return true;
        }
        catch
        {
            value = null;
            return false;
        }
    }
}
