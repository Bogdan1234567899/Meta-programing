using System.Collections.Generic;

public interface IEventSource
{
    IEnumerable<AlertEvent> Read();
}

public interface IPriorityPolicy
{
    int GetPriority(AlertEvent e);
}

public interface IMessageFormatter
{
    string Format(AlertEvent e, int priority);
}

public interface INotificationChannel
{
    void Send(string message, string recipientGroup);
}

public interface ILogger
{
    void Log(string message);
}
