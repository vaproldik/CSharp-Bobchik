using System.Text;

class Task9_StringBuilder
{
    static void Main()
    {
        StringBuilder sb = new StringBuilder("Я изучаю основы программирования на C#");

        Console.WriteLine("До удаления: " + sb.ToString());

        sb.Remove(8, 7);

        Console.WriteLine("После удаления: " + sb.ToString());
    }
}