class Program
{
    static void Main(string[] args)
    {
        PaymentProcessor processor = new PaymentProcessor();

        ICreditPayment creditRef = processor;
        IDebitPayment debitRef = processor;

        decimal myAmount = 1250.00m;

        creditRef.ProcessPayment(myAmount);

        debitRef.ProcessPayment(myAmount);
    }
}