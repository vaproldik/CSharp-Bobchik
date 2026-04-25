class Program
{
    static void Main()
    {
        PasswordValidator validator = new PasswordValidator();
        try
        {
            Console.Write("Введите ваш пароль: ");
            string userPassword = Console.ReadLine();
            validator.ValidatePassword(userPassword);
        }
        catch (WeakPasswordException ex)
        {
            Console.WriteLine($"Поймали исключение: {ex.Message}");
        }
    }
}