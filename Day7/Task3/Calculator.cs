public class Calculator
{
    public int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new DivisionByZeroException("Ошибка: Попытка деления на ноль в калькуляторе.");
        }
        return a / b;
    }
}