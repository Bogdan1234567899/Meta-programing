using System;

public class AnalysisRunner
{
    public void Run(bool shouldFail)
    {
        Console.WriteLine("UI: analysis started");
        try
        {
            if (shouldFail) throw new Exception("Demo failure");
            var findings = 3;
            Console.WriteLine($"UI: analysis finished, findings={findings}");
        }
        catch
        {
            Console.WriteLine("UI: analysis failed");
        }
    }
}

class Program
{
    static void Main()
    {
        var runner = new AnalysisRunner();
        runner.Run(false);
        runner.Run(true);
    }
}
