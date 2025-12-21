namespace StudentMetaFramework.Core.Validation;

public abstract class ValidationAttributeBase : Attribute
{
    public string? ErrorMessage { get; set; }

    // return null if OK; else error text
    public abstract string? Validate(object? value, string propertyName);
}
