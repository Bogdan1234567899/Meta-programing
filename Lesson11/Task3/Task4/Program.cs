using System;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

object obj = Activator.CreateInstance(typeof(Product));

Console.WriteLine("Створили об'єкт через Activator:");
Console.WriteLine(obj);

class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return "Product { Id=" + Id + ", Name=" + Name + ", Price=" + Price + " }";
    }
}
