public class CacheManager<T>
{
    private readonly ICache<T> _cache;

    public CacheManager(ICache<T> cache)
    {
        _cache = cache;
    }
    public void PrintCache()
    {
        Console.WriteLine("--- Содержимое кэша ---");
        foreach (var item in _cache.GetAll())
        {
            Console.WriteLine($"- {item}");
        }
    }
    public int Count()
    {
        int count = 0;
        foreach (var item in _cache.GetAll())
        {
            count++;
        }
        return count;
    }
}