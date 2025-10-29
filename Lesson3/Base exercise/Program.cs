using System;
using System.Collections.Generic;
using CW03;
using CW03.Payments;

namespace CW03
{
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

            ProcessAll(payments);

            Console.WriteLine();
            Console.WriteLine("Refunds:");
            Console.WriteLine();

            var refundables = new List<IRefundable>();
            foreach (var p in payments)
            {
                if (p is IRefundable r)
                    refundables.Add(r);
            }

            RefundAll(refundables, 50m);

            Console.WriteLine();
            Console.WriteLine("Done.");

            Console.WriteLine();
            Console.WriteLine("TimeInterval demo:");

            var t1 = new TimeInterval("09:30-11:15");
            var t2 = new TimeInterval("10:00-12:00");

            Console.WriteLine(t1.ToString());
            Console.WriteLine(t2.ToString());

            Console.WriteLine($"t1 length (min): {(int)t1}");
            Console.WriteLine($"t1 overlaps t2: {t1.Overlaps(t2)}");
            Console.WriteLine($"t1[0]={t1[0]}, t1[1]={t1[1]}");
            Console.WriteLine($"t1[\"start\"]={t1["start"]}, t1[\"end\"]={t1["end"]}");

            var u = t1 + t2;
            var inter = t1 * t2;
            Console.WriteLine($"union={u}");
            Console.WriteLine($"inter={inter}");
        }

        static void ProcessAll(IEnumerable<Payment> payments)
        {
            foreach (var payment in payments)
            {
                payment.Processed += OnPaymentProcessed;
                payment.Process();
            }
        }

        private static void OnPaymentProcessed(object? sender, PaymentProcessedEventArgs e)
        {
            Console.WriteLine($"--> Processed event: {e.Payment.GetType().Name} at {e.Payment.ProcessedUtc:O}");
        }

        static void RefundAll(IEnumerable<IRefundable> refundables, decimal amountToRefundEach)
        {
            foreach (var r in refundables)
            {
                r.Refund(amountToRefundEach);
            }
        }
    }
}
