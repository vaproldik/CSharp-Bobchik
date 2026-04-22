public abstract class Person
{
    public string FullName { get; set; }
    public int Age { get; set; }

    protected Person(string name, int age)
    {
        FullName = name;
        Age = age;
    }
}