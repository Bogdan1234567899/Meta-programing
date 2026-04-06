using System;
using System.Collections.Generic;
using System.Diagnostics;

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

public class RuleWithMetricsDecorator : IRule
{
    private IRule _inner;

    public string Name
    {
        get { return _inner.Name; }
    }

    public RuleWithMetricsDecorator(IRule innerRule)
    {
        _inner = innerRule;
    }

    public List<string> Check(string input)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        var results = _inner.Check(input);

        stopwatch.Stop();

        Console.WriteLine("[Metrics] Rule: " + _inner.Name
            + ", Time: " + stopwatch.Elapsed.TotalMilliseconds + " ms"
            + ", Errors: " + results.Count);

        return results;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- Without decorator ---");
        IRule rule1 = new UppercaseRule();
        var res1 = rule1.Check("hello");
        Console.WriteLine(string.Join(", ", res1));

        Console.WriteLine("--- With decorator ---");
        IRule rule2 = new RuleWithMetricsDecorator(new UppercaseRule());
        var res2 = rule2.Check("hello");
        Console.WriteLine(string.Join(", ", res2));

        Console.ReadLine();
    }
}
