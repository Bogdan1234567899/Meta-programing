public class AlertEvent
{
    public string EventType { get; set; }
    public string Message { get; set; }
    public string TargetRole { get; set; }

    public AlertEvent(string eventType, string message, string targetRole)
    {
        EventType = eventType;
        Message = message;
        TargetRole = targetRole;
    }
}
