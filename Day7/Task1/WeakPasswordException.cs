public class WeakPasswordException : Exception
{
    public WeakPasswordException() : base("Пароль слишком короткий!") { }

    public WeakPasswordException(string message) : base(message) { }

    public WeakPasswordException(string message, Exception innerException)
        : base(message, innerException) { }
}