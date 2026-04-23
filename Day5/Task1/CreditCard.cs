public class CreditCard : PaymentMethod
{
    public override void Pay(double amount)
    {
        Console.WriteLine($"[Кредитная карта]: Списано {amount} руб. Транзакция подтверждена.");
    }
}