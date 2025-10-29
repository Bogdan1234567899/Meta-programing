using System;

namespace Payments
{
    public abstract class Payment
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public DateTime CreatedUtc { get; }
        public DateTime? ProcessedUtc { get; protected set; }

        public event EventHandler<PaymentProcessedEventArgs>? Processed;

        protected Payment(decimal amount, string currency)
        {
            if (amount <= 0m)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be > 0.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency must be non-empty.", nameof(currency));

            Amount = amount;
            Currency = currency.Trim();
            CreatedUtc = DateTime.UtcNow;
            ProcessedUtc = null;
        }

        public void Process()
        {
            ValidateInvariants();
            OnBeforeProcess();
            DoProcess();
            OnAfterProcess();
            OnProcessed(new PaymentProcessedEventArgs(this));
        }

        protected virtual void OnBeforeProcess()
        {
        }

        protected abstract void DoProcess();

        protected virtual void OnAfterProcess()
        {
            ProcessedUtc = DateTime.UtcNow;
        }

        protected virtual void OnProcessed(PaymentProcessedEventArgs e)
        {
            Processed?.Invoke(this, e);
        }

        private void ValidateInvariants()
        {
            if (Amount <= 0m)
                throw new InvalidOperationException("Amount must remain > 0.");

            if (string.IsNullOrWhiteSpace(Currency))
                throw new InvalidOperationException("Currency must remain non-empty.");
        }
    }

    public sealed class PaymentProcessedEventArgs : EventArgs
    {
        public Payment Payment { get; }

        public PaymentProcessedEventArgs(Payment payment)
        {
            Payment = payment ?? throw new ArgumentNullException(nameof(payment));
        }
    }
}
