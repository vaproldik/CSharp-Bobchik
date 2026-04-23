class DateHelper
{
    private static bool IsLeapYear(int y)
    {
        return (y % 4 == 0 && y % 100 != 0) || (y % 400 == 0);
    }

    public static int MonthDays(int m, int y)
    {
         switch (m)
        {
            case 1:
            case 3:
            case 5:
            case 7:
            case 8:
            case 10:
            case 12:
                return 31;
            case 4:
            case 6:
            case 9:
            case 11:
                return 30;
            case 2:
                return IsLeapYear(y) ? 29 : 28;
            default:
                return 0; 
        }
    }
}

class Program
{
    static void Main()
    {
        int year = 2024; 
        Console.WriteLine($"Март {year}: {DateHelper.MonthDays(3, year)} дней");
        Console.WriteLine($"Февраль {year} (високосный): {DateHelper.MonthDays(2, year)} дней");
        Console.WriteLine($"Февраль 2023 (обычный): {DateHelper.MonthDays(2, 2023)} дней");
    }
}

