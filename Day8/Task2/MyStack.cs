public class MyStack<T>
{
    private List<T> items = new List<T>();

    public void Push(T item) { items.Add(item); }

    public T Pop() 
    {
        if (items.Count == 0) throw new InvalidOperationException();
        T last = items[items.Count - 1];
        items.RemoveAt(items.Count - 1);
        return last;
    }

    public int Count() => items.Count;
}