using System.Text;

Console.OutputEncoding = Encoding.UTF8;

record Item(int Id, string Name, int Score, bool IsActive);

var items = new List<Item>();
for (int i = 1; i <= 30; i++)
{
    items.Add(new Item(i, "Item" + i, (i * 7) % 100, i % 3 != 0));
}

var naive1 = items.Where(x => x.IsActive).ToList();
var naive2 = naive1.Where(x => x.Score >= 50).ToList();
var naive3 = naive2.OrderByDescending(x => x.Score).ToList();
var naive4 = naive3.Select(x => new { x.Id, x.Name, x.Score }).ToList();

Console.WriteLine("Naive:");
if (naive4.Count() > 0)
{
    foreach (var x in naive4)
        Console.WriteLine(x.Id + " " + x.Name + " " + x.Score);
}
else
{
    Console.WriteLine("Empty");
}

Console.WriteLine();
Console.WriteLine("Optimized:");

var query = items
    .Where(x => x.IsActive && x.Score >= 50)
    .OrderByDescending(x => x.Score)
    .Select(x => new { x.Id, x.Name, x.Score });

var result = query.ToList();

if (result.Any())
{
    foreach (var x in result)
        Console.WriteLine(x.Id + " " + x.Name + " " + x.Score);
}
else
{
    Console.WriteLine("Empty");
}
