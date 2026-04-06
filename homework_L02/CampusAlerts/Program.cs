using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Positive scenario ===");
        Console.WriteLine();

        var events = new List<AlertEvent>
        {
            new AlertEvent("fire_alarm", "Fire detected in building A", "Security"),
            new AlertEvent("network_outage", "WiFi down in library", "Admin"),
            new AlertEvent("door_forced", "Door forced open in lab 3", "Security"),
            new AlertEvent("medical_emergency", "Student fainted in gym", "Teacher"),
            new AlertEvent("power_failure", "Power lost in dormitory B", "Admin")
        };

        var source = new CampusEventSource(events);
        var policy = new DefaultPriorityPolicy();
        var formatter = new SimpleMessageFormatter();
        var logger = new ConsoleLogger();

        var channels = new List<INotificationChannel>
        {
            new EmailChannel(),
            new SmsChannel(),
            new ConsoleChannel()
        };

        var router = new AlertRouter(source, policy, formatter, channels, logger);
        router.Route();

        Console.WriteLine();
        Console.WriteLine("=== Negative scenario (Fail-fast) ===");
        Console.WriteLine();

        try
        {
            var badRouter = new AlertRouter(source, policy, formatter, null, logger);
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine("Fail-fast caught: Missing dependency - " + ex.ParamName);
        }

        try
        {
            var emptyChannels = new List<INotificationChannel>();
            var badRouter2 = new AlertRouter(source, policy, formatter, emptyChannels, logger);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Fail-fast caught: " + ex.Message);
        }

        Console.ReadLine();
    }
}
