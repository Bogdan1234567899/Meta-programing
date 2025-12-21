namespace StudentMetaFramework.Core.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class StringLengthAttribute : ValidationAttributeBase
{
    public StringLengthAttribute(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public int Min { get; }
    public int Max { get; }

    public override string? Validate(object? value, string propertyName)
    {
        if (value is null) return null;

        if (value is not string s)
            return ErrorMessage ?? $"{propertyName} is not a string.";

        if (s.Length < Min || s.Length > Max)
            return ErrorMessage ?? $"{propertyName} length must be between {Min} and {Max}.";

        return null;
    }
}
