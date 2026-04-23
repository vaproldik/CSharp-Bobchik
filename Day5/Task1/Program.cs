class Program
{
    static void Main(string[] args)
    {
        PaymentMethod[] payments = new PaymentMethod[3];

        payments[0] = new CreditCard();
        payments[1] = new PayPal();
        payments[2] = new Cash();

        double testAmount = 1550.75;

        foreach (PaymentMethod method in payments)
        {
            method.Pay(testAmount);
        }
    }
}