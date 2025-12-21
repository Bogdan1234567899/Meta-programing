namespace StudentMetaFramework.Core.Validation;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class RequiredAttribute : ValidationAttributeBase
{
    public override string? Validate(object? value, string propertyName)
    {
        if (value is null)
            return ErrorMessage ?? $"{propertyName} is required.";

        if (value is string s && string.IsNullOrWhiteSpace(s))
            return ErrorMessage ?? $"{propertyName} is required.";

        return null;
    }
}
