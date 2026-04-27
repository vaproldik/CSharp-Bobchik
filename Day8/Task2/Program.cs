class Program
{
    static void Main()
    {
        MyStack<string> myStack = new MyStack<string>();
        myStack.Push("Действие 1");
        Console.WriteLine($"Удалено: {myStack.Pop()}");
    }
}