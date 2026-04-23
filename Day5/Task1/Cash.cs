public class Cash : PaymentMethod
{
    public override void Pay(double amount)
    {
        Console.WriteLine($"[Наличные]: Внесено {amount} руб. Печать чека...");
    }
}