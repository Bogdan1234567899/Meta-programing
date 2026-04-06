using System;
using System.Collections.Generic;

public interface IRepo
{
    List<string> GetData();
}

public class SqlRepo : IRepo
{
    public List<string> GetData()
    {
        return new List<string> { "item1", "item2", "item3" };
    }
}

public class MemoryRepo : IRepo
{
    public List<string> GetData()
    {
        var list = new List<string>();
        list.Add("mem_item1");
        list.Add("mem_item2");
        return list;
    }
}

public class Service
{
    private IRepo _repo;

    public Service(IRepo repo)
    {
        _repo = repo;
    }

    public string Execute()
    {
        var data = _repo.GetData();
        return $"Processed {data.Count} items";
    }
}

class Program
{
    static void Main(string[] args)
    {
        IRepo repo1 = new SqlRepo();
        var service1 = new Service(repo1);
        Console.WriteLine("SqlRepo: " + service1.Execute());

        IRepo repo2 = new MemoryRepo();
        var service2 = new Service(repo2);
        Console.WriteLine("MemoryRepo: " + service2.Execute());

        Console.ReadLine();
    }
}
