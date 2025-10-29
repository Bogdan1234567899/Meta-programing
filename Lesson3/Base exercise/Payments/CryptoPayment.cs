using System;

namespace Payments
{
    public class CryptoPayment : Payment
    {
        public string? TxHash { get; private set; }

        public CryptoPayment(decimal amount, string currency)
            : base(amount, currency)
        {
        }

        protected override void DoProcess()
        {
            TxHash = "0x" + Guid.NewGuid().ToString("N");
            Console.WriteLine($"[CRYPTO] Broadcast {Amount} {Currency}, tx={TxHash}");
        }
    }
}
