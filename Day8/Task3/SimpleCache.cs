public class SimpleCache<T>: ICache<T>
{
    private List<T> storage = new List<T>();
    public void Add(T item) => storage.Add(item);
    public IEnumerable<T> GetAll() => storage;
    public void Clear() {  storage.Clear(); }
}