public class PasswordValidator
{
    public void ValidatePassword(string password)
    {
        if (password.Length < 8)
        {
            throw new WeakPasswordException("Ошибка: Пароль должен быть не менее 8 символов.");
        }
        Console.WriteLine("Пароль принят!");
    }
}