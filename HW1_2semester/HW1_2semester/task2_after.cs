using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public record Finding(string Rule, string Message);

public interface IRule
{
    Finding Check(string input);
}

public class MinLengthRule : IRule
{
    public Finding Check(string input)
    {
        if (input.Length < 5)
            return new Finding("MinLength", "Too short");
        return null;
    }
}

public class NoDigitsRule : IRule
{
    public Finding Check(string input)
    {
        if (Regex.IsMatch(input, @"\d"))
            return new Finding("NoDigits", "Contains digits");
        return null;
    }
}

public class UppercaseRule : IRule
{
    public Finding Check(string input)
    {
        if (input != input.ToUpper())
            return new Finding("Uppercase", "Not all uppercase");
        return null;
    }
}

public class RuleFactory
{
    public static List<IRule> Create(string mode)
    {
        var rules = new List<IRule>();

        if (mode == "basic")
        {
            rules.Add(new MinLengthRule());
            rules.Add(new NoDigitsRule());
        }
        else if (mode == "strict")
        {
            rules.Add(new MinLengthRule());
            rules.Add(new NoDigitsRule());
            rules.Add(new UppercaseRule());
        }

        return rules;
    }
}

public class Analyzer
{
    private List<IRule> _rules;

    public Analyzer(List<IRule> rules)
    {
        _rules = rules;
    }

    public List<Finding> Run(string input)
    {
        var findings = new List<Finding>();
        foreach (var rule in _rules)
        {
            var result = rule.Check(input);
            if (result != null)
                findings.Add(result);
        }
        return findings;
    }
}

class Program
{
    static void Main(string[] args)
    {
        var rules1 = RuleFactory.Create("basic");
        var analyzer1 = new Analyzer(rules1);
        Console.WriteLine("--- Basic ---");
        foreach (var f in analyzer1.Run("ab12"))
            Console.WriteLine(f.Rule + ": " + f.Message);

        var rules2 = RuleFactory.Create("strict");
        var analyzer2 = new Analyzer(rules2);
        Console.WriteLine("--- Strict ---");
        foreach (var f in analyzer2.Run("ab12"))
            Console.WriteLine(f.Rule + ": " + f.Message);

        Console.ReadLine();
    }
}
