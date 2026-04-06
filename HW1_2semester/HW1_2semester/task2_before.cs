using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public record Finding(string Rule, string Message);

public class Analyzer
{
    public List<Finding> Run(string input)
    {
        var findings = new List<Finding>();

        if (input.Length < 5)
            findings.Add(new Finding("MinLength", "Too short"));

        if (Regex.IsMatch(input, @"\d"))
            findings.Add(new Finding("NoDigits", "Contains digits"));

        return findings;
    }
}

class Program
{
    static void Main()
    {
        var analyzer = new Analyzer();
        foreach (var f in analyzer.Run("ab12"))
            Console.WriteLine($"{f.Rule}: {f.Message}");
    }
}
