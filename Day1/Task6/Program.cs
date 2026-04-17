class Program
{
    static void Main()
    {
        Console.WriteLine("Признаки: a - авто, в - вело, м - мото, с - самолет, п - поезд");
        Console.Write("Введите признак: ");
        char type = char.Parse(Console.ReadLine());

        switch (type)
        {
            case 'а': Console.WriteLine("Скорость: 180 км/ч"); break;
            case 'в': Console.WriteLine("Скорость: 30 км/ч"); break;
            case 'м': Console.WriteLine("Скорость: 220 км/ч"); break;
            case 'с': Console.WriteLine("Скорость: 900 км/ч"); break;
            case 'п': Console.WriteLine("Скорость: 140 км/ч"); break;
            default: Console.WriteLine("Неизвестный тип транспорта"); break;
        }
    }
}