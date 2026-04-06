using System;
using System.Collections.Generic;
using System.Linq;

public sealed class AlertRouter
{
    private readonly IEventSource _source;
    private readonly IPriorityPolicy _policy;
    private readonly IMessageFormatter _formatter;
    private readonly IEnumerable<INotificationChannel> _channels;
    private readonly ILogger _logger;

    public AlertRouter(
        IEventSource source,
        IPriorityPolicy policy,
        IMessageFormatter formatter,
        IEnumerable<INotificationChannel> channels,
        ILogger logger)
    {
        _source = source ?? throw new ArgumentNullException(nameof(source));
        _policy = policy ?? throw new ArgumentNullException(nameof(policy));
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        _channels = channels ?? throw new ArgumentNullException(nameof(channels));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (!_channels.Any())
            throw new ArgumentException("At least one notification channel is required", nameof(channels));
    }

    public void Route()
    {
        var events = _source.Read();

        foreach (var e in events)
        {
            _logger.Log("Processing event: " + e.EventType);

            int priority = _policy.GetPriority(e);
            string message = _formatter.Format(e, priority);

            foreach (var channel in _channels)
            {
                channel.Send(message, e.TargetRole);
            }

            _logger.Log("Event " + e.EventType + " routed successfully");
        }
    }
}
