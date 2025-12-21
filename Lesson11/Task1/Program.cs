using System;
using System.Reflection;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

Student st = new Student();
st.Name = "Іван";
st.Age = 19;
st.Group = "KN-11";

Type t = typeof(Student);

Console.WriteLine("Клас: " + t.Name);
Console.WriteLine("Властивості:");

PropertyInfo[] props = t.GetProperties();
for (int i = 0; i < props.Length; i++)
{
    Console.WriteLine("- " + props[i].Name + " : " + props[i].PropertyType.Name);
}

class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Group { get; set; }
}
