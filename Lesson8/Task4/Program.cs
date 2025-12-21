using System.Text;

Console.OutputEncoding = Encoding.UTF8;

record Product(int Id, string Name, string Category, decimal Price, bool IsActive);

var products = new List<Product>
{
    new Product(1, "C# Basics",      "Books",  12.50m, true),
    new Product(2, "Guide",     "Books",  20.00m, true),
    new Product(3, "Ooold Book",       "Books",   5.00m, false),
    new Product(4, "Notebook",       "Office",  3.20m, true),
    new Product(5, "Advanced C#",    "Books",  35.00m, true),
    new Product(6, "Pen",            "Office",  1.10m, true),
    new Product(7, "Novel",  "Books",  15.00m, true),
    new Product(8, "Sticker Pack",   "Other",   2.00m, true),
};

decimal min = 10m;
decimal max = 25m;

Func<Product, bool> filter = p =>
    p.IsActive &&
    p.Category == "Books" &&
    p.Price >= min &&
    p.Price <= max;

var q = products
    .Where(filter)
    .OrderBy(p => p.Price)
    .ThenBy(p => p.Name)
    .Select(p => new { p.Id, p.Name, p.Price });

foreach (var x in q)
{
    Console.WriteLine(x.Id + " | " + x.Name + " | " + x.Price);
}
