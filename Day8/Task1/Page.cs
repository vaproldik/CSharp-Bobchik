public class Page
{
    public string Url;
    public string Title;

    public Page(string url, string title)
    {
        Url = url;
        Title = title;
    }

    public override string ToString()
    {
        return $"Имя {Title}, адрес: {Url}";
    }
}