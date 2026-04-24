public class LoggerMethods
{
    public static void ConsoleLog(string msg)
    {
        Console.WriteLine("[КОНСОЛЬ]: " + msg);
    }

    public static void FileLog(string msg)
    {
        Console.WriteLine("[ФАЙЛ (имитация)]: " + msg);
    }
}