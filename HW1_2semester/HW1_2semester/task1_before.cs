using System;
using System.Collections.Generic;

public class Repo
{
    public List<string> GetData()
    {
        return new List<string> { "item1", "item2", "item3" };
    }
}

public class Service
{
    public string Execute()
    {
        var repo = new Repo(); // проблема: жорстка залежність
        var data = repo.GetData();
        return $"Processed {data.Count} items";
    }
}

class Program
{
    static void Main()
    {
        var service = new Service();
        Console.WriteLine(service.Execute());
    }
}
