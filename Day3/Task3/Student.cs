public sealed class Student : Person
{
    public double AverageGrade { get; set; }

    public Student(string name, int age, double grade) : base(name, age)
    {
        AverageGrade = grade;
    }
}