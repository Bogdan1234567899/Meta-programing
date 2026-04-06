using System;
using System.Collections.Generic;

public class CampusEventSource : IEventSource
{
    private readonly List<AlertEvent> _events;

    public CampusEventSource(List<AlertEvent> events)
    {
        _events = events;
    }

    public IEnumerable<AlertEvent> Read()
    {
        return _events;
    }
}

public class DefaultPriorityPolicy : IPriorityPolicy
{
    public int GetPriority(AlertEvent e)
    {
        switch (e.EventType)
        {
            case "fire_alarm": return 1;
            case "medical_emergency": return 1;
            case "door_forced": return 2;
            case "network_outage": return 3;
            case "power_failure": return 1;
            default: return 5;
        }
    }
}

public class SimpleMessageFormatter : IMessageFormatter
{
    public string Format(AlertEvent e, int priority)
    {
        return "[Priority " + priority + "] " + e.EventType.ToUpper() + ": " + e.Message;
    }
}

public class EmailChannel : INotificationChannel
{
    public void Send(string message, string recipientGroup)
    {
        Console.WriteLine("[Email -> " + recipientGroup + "] " + message);
    }
}

public class SmsChannel : INotificationChannel
{
    public void Send(string message, string recipientGroup)
    {
        Console.WriteLine("[SMS -> " + recipientGroup + "] " + message);
    }
}

public class ConsoleChannel : INotificationChannel
{
    public void Send(string message, string recipientGroup)
    {
        Console.WriteLine("[Console -> " + recipientGroup + "] " + message);
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine("[LOG] " + message);
    }
}
