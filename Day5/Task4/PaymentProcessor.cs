public class PaymentProcessor : ICreditPayment, IDebitPayment
{
    void ICreditPayment.ProcessPayment(decimal amount)
    {
        Console.WriteLine($"[Credit System]: Списание суммы {amount} с кредитного лимита.");
    }

    void IDebitPayment.ProcessPayment(decimal amount)
    {
        Console.WriteLine($"[Debit System]: Списание суммы {amount} с собственных средств (дебет).");
    }
}