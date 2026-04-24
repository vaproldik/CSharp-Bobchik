public class LogManager
{
    public void LogMessage(string message, LogHandler handler)
    {
        handler(message);
    }
}