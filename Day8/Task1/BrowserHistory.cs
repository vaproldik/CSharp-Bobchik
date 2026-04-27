using System.Collections;

public class BrowserHistory
{
    private Stack history = new Stack();

    public void VisitPage(Page page)
    {
        history.Push(page);
        Console.WriteLine($"Переход на: {page.Title}");
    }

    public void GoBack()
    {
        if (history.Count > 0)
        {
          Page p = (Page)history.Pop();
          Console.WriteLine($"Вернулись со страницы: {p.Title}");
        }
    }

    public Page GetCurrentPage()
    {
        return (Page)history.Pop();
    }
}