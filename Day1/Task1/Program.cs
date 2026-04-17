class Program
{
    static void Main()
    {
        Console.Write("Введите расстояние в метрах: ");
        int meters = int.Parse(Console.ReadLine());

        int kilometers = meters / 1000;

        Console.WriteLine($"Число полных километров: {kilometers}");
    }
}