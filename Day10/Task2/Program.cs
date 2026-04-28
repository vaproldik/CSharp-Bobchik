public interface ITaxStrategy
{
    double CalculateTax(double amount);
}

public class USTax : ITaxStrategy
{
    public double CalculateTax(double amount)
    {
        return amount * 0.1;
    }
}

public class EUTax : ITaxStrategy
{
    public double CalculateTax(double amount)
    {
        return amount * 0.2;
    }
}

public class AsiaTax : ITaxStrategy
{
    public double CalculateTax(double amount)
    {
        return amount * 0.05;
    }
}

public class TaxCalculator
{
    private ITaxStrategy _strategy;

    public void SetStrategy(ITaxStrategy strategy)
    {
        _strategy = strategy;
    }

    public void Calculate(double amount)
    {
        if (_strategy != null)
        {
            double tax = _strategy.CalculateTax(amount);
            Console.WriteLine($"Сумма: {amount}, Налог: {tax}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        TaxCalculator calculator = new TaxCalculator();
        double price = 1000.0;

        calculator.SetStrategy(new USTax());
        Console.Write("США: ");
        calculator.Calculate(price);

        calculator.SetStrategy(new EUTax());
        Console.Write("Европа: ");
        calculator.Calculate(price);

        calculator.SetStrategy(new AsiaTax());
        Console.Write("Азия: ");
        calculator.Calculate(price);
    }
}