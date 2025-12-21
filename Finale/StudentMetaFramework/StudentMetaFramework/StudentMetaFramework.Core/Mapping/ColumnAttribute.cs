namespace StudentMetaFramework.Core.Mapping;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class ColumnAttribute : Attribute
{
    public ColumnAttribute(string name) => Name = name;
    public string Name { get; }
}
