class CalendarUtils
{
    private static int GetDaysInMonth(int m, int y)
    {
        if (m == 2) return ((y % 4 == 0 && y % 100 != 0) || (y % 400 == 0)) ? 29 : 28;
        if (m == 4 || m == 6 || m == 9 || m == 11) return 30;
        return 31;
    }

    public static void PrevDate(ref int d, ref int m, ref int y)
    {
        if (d > 1)
        {
            d--; 
        }
        else
        {
            if (m > 1)
            {
                m--; 
            }
            else
            {
                m = 12; 
                y--;    
            }
            d = GetDaysInMonth(m, y);
        }
    }
}

class Program
{
    static void Main()
    {
        int day = 1, month = 1, year = 2024;
        Console.WriteLine($"Текущая дата: {day:D2}.{month:D2}.{year}");

        CalendarUtils.PrevDate(ref day, ref month, ref year);

        Console.WriteLine($"Предыдущая дата: {day:D2}.{month:D2}.{year}");
    }
}