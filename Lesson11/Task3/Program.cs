using System;
using System.Reflection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

User u = new User();
u.Name = "Олена";
u.Email = "olena@mail.com";
u.Password = "12345";

Type t = typeof(User);

Console.WriteLine("Властивості з [Required]:");
PropertyInfo[] props = t.GetProperties();
for (int i = 0; i < props.Length; i++)
{
    object[] attrs = props[i].GetCustomAttributes(typeof(RequiredAttribute), true);
    if (attrs.Length > 0)
        Console.WriteLine("- " + props[i].Name);
}

[AttributeUsage(AttributeTargets.Property)]
class RequiredAttribute : Attribute
{
}

class User
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
