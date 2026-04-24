public class Investor
{
    public void OnPriceChange(double price)
    {
        Console.WriteLine("Инвестор: Хм, цена теперь " + price + ". Покупать или нет?");
    }
}

public class NewsAgency
{
    public void PostNews(double price)
    {
        Console.WriteLine("Срочные новости! Акции стоят " + price + " рублей!");
    }   
}