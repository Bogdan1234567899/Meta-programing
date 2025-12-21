using System.Globalization;

namespace StudentMetaFramework.Core.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class RangeAttribute : ValidationAttributeBase
{
    public RangeAttribute(double min, double max)
    {
        Min = min;
        Max = max;
    }

    public double Min { get; }
    public double Max { get; }

    public override string? Validate(object? value, string propertyName)
    {
        if (value is null) return null; // null допустимо, якщо нема Required

        try
        {
            var d = Convert.ToDouble(value, CultureInfo.InvariantCulture);
            if (d < Min || d > Max)
                return ErrorMessage ?? $"{propertyName} must be in range [{Min}; {Max}].";
        }
        catch
        {
            return ErrorMessage ?? $"{propertyName} has invalid numeric value.";
        }

        return null;
    }
}
