public delegate void StockPriceHandler(double price);

public class StockMarket
{
    public event StockPriceHandler StockPriceChanged;

    public void NewPrice(double price)
    {
        Console.WriteLine("На бирже новая цена: " + price);
        StockPriceChanged?.Invoke(price);
    }
}