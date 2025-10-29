using System;
using System.Collections.Generic;
using Payments;

internal class Program
{
    static void Main(string[] args)
    {
        var payments = new List<Payment>
        {
            new CashPayment(100m, "USD"),
            new CardPayment(250.50m, "EUR", "**** **** **** 1234"),
            new CryptoPayment(0.015m, "BTC"),
        };

        foreach (var p in payments)
        {
            p.Processed += OnProcessed;
            p.Process();
        }

        Console.WriteLine();

        foreach (var p in payments)
        {
            if (p is IRefundable r)
            {
                r.Refund(50m);
            }
        }
    }

    private static void OnProcessed(object? sender, PaymentProcessedEventArgs e)
    {
        Console.WriteLine($"Processed: {e.Payment.GetType().Name} at {e.Payment.ProcessedUtc:O}");
    }
}
