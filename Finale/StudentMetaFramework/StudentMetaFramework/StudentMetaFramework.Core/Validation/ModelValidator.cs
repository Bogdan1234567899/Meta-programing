using System.Reflection;

namespace StudentMetaFramework.Core.Validation;

public class ModelValidator
{
    public List<string> Validate(object obj)
    {
        var errors = new List<string>();

        var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var p in props)
        {
            var value = p.GetValue(obj);
            var attrs = p.GetCustomAttributes<ValidationAttributeBase>(inherit: true);

            foreach (var a in attrs)
            {
                var err = a.Validate(value, p.Name);
                if (!string.IsNullOrWhiteSpace(err))
                    errors.Add(err);
            }
        }

        return errors;
    }
}
