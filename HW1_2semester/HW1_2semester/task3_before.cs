using System;
using System.Collections.Generic;

public interface IRule
{
    string Name { get; }
    List<string> Check(string input);
}

public class UppercaseRule : IRule
{
    public string Name => "UppercaseRule";

    public List<string> Check(string input)
    {
        if (input == input.ToUpperInvariant()) return new List<string>();
        return new List<string> { "Input is not uppercase" };
    }
}

class Program
{
    static void Main()
    {
        IRule rule = new UppercaseRule();
        Console.WriteLine(string.Join(", ", rule.Check("hello")));
    }
}
