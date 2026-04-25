public class NumberProcessor
{
    public void Process(string input)
    {
        StringParser parser = new StringParser();
        try
        {
            parser.ParseToInt(input);
        }
        catch (FormatException ex)
        {
            throw new InvalidNumberException("Критическая ошибка при обработке числа.", ex);
        }
    }
}