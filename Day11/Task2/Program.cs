class Program
{
    static void Main(string[] args)
    {
        IText myText = new PlainText("Привет, мир!");
        Console.WriteLine("Обычный: " + myText.GetContent());

        IText boldText = new BoldDecorator(myText);
        Console.WriteLine("Жирный: " + boldText.GetContent());

        IText multiStyleText = new UnderlineDecorator(new ItalicDecorator(new BoldDecorator(myText)));
        Console.WriteLine("Все стили: " + multiStyleText.GetContent());
    }
}