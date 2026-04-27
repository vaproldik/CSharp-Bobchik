class Program
{
    static void Main()
    {
        BrowserHistory bh = new BrowserHistory();
        bh.VisitPage(new Page("google.com", "Поиск"));
        bh.VisitPage(new Page("github.com", "Код"));
        bh.GoBack();
        Console.WriteLine("Текущая страница: " + bh.GetCurrentPage().ToString());
    }
}