public class PayPal : PaymentMethod
{
    public override void Pay(double amount)
    {
        Console.WriteLine($"[PayPal]: Оплата {amount} руб. переведена на счет магазина.");
    }
}