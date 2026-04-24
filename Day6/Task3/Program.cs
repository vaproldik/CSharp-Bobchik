class Program
{
    static void Main(string[] args)
    {
        StockMarket market = new StockMarket();

        Investor investor = new Investor();
        NewsAgency news = new NewsAgency();

        market.StockPriceChanged += investor.OnPriceChange;
        market.StockPriceChanged += news.PostNews;

        market.NewPrice(120.5);
        Console.WriteLine("-----------------------------");
        market.NewPrice(135.2);
    }
}