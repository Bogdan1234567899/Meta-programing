using System;
using System.Collections.Generic;

public class EventBus
{
    private Dictionary<string, List<Action<object>>> _subscribers
        = new Dictionary<string, List<Action<object>>>();

    public void Subscribe(string eventName, Action<object> handler)
    {
        if (!_subscribers.ContainsKey(eventName))
            _subscribers[eventName] = new List<Action<object>>();

        _subscribers[eventName].Add(handler);
    }

    public void Publish(string eventName, object payload = null)
    {
        if (_subscribers.ContainsKey(eventName))
        {
            foreach (var handler in _subscribers[eventName])
            {
                handler(payload);
            }
        }
    }
}

public class AnalysisRunner
{
    private EventBus _bus;

    public AnalysisRunner(EventBus bus)
    {
        _bus = bus;
    }

    public void Run(bool shouldFail)
    {
        _bus.Publish("analysis_started");

        try
        {
            if (shouldFail)
                throw new Exception("Demo failure");

            int findings = 3;
            _bus.Publish("analysis_finished", findings);
        }
        catch (Exception ex)
        {
            _bus.Publish("analysis_failed", ex.Message);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var bus = new EventBus();

        bus.Subscribe("analysis_started", data =>
            Console.WriteLine("UI: analysis started"));

        bus.Subscribe("analysis_finished", data =>
            Console.WriteLine("UI: analysis finished, findings=" + data));

        bus.Subscribe("analysis_failed", data =>
            Console.WriteLine("UI: analysis failed - " + data));

        bus.Subscribe("analysis_started", data =>
            Console.WriteLine("[Telemetry] run started"));

        bus.Subscribe("analysis_finished", data =>
            Console.WriteLine("[Telemetry] ok, findings: " + data));

        bus.Subscribe("analysis_failed", data =>
            Console.WriteLine("[Telemetry] error: " + data));

        var runner = new AnalysisRunner(bus);

        Console.WriteLine("=== Success ===");
        runner.Run(false);

        Console.WriteLine();

        Console.WriteLine("=== Fail ===");
        runner.Run(true);

        Console.ReadLine();
    }
}
