using System;

namespace Payments
{
    public class CardPayment : Payment, IRefundable
    {
        public string CardMasked { get; }
        public string? AuthCode { get; private set; }

        public CardPayment(decimal amount, string currency, string cardMasked)
            : base(amount, currency)
        {
            if (string.IsNullOrWhiteSpace(cardMasked))
                throw new ArgumentException("CardMasked must be provided.", nameof(cardMasked));

            CardMasked = cardMasked.Trim();
        }

        protected override void DoProcess()
        {
            AuthCode = Guid.NewGuid().ToString("N").Substring(0, 8);
            Console.WriteLine($"[CARD] Charged {Amount} {Currency} from {CardMasked}. AuthCode={AuthCode}");
        }

        public void Refund(decimal amount)
        {
            if (amount <= 0m)
                throw new ArgumentOutOfRangeException(nameof(amount), "Refund amount must be > 0.");

            if (amount > Amount)
                throw new ArgumentOutOfRangeException(nameof(amount), "Refund amount cannot exceed original payment amount.");

            Console.WriteLine($"[CARD-REFUND] Refunded {amount} {Currency} to {CardMasked} (AuthCode={AuthCode ?? "n/a"})");
        }
    }
}
