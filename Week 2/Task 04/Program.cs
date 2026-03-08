using System;

class Student
{
    public string Name;
    public double Marks;

    public void AddStudent()
    {
        Console.Write("Enter Name:");
        Name = Console.ReadLine();

        Console.Write("Enter Marks:");
        Marks = double.Parse(Console.ReadLine());
        Console.Clear();
    }

    public void ShowStudent()
    {
        Console.WriteLine(Name + " " + Marks);
    }

    public static double CalculateAggregate(Student[] s, int count)
    {
        double total = 0;

        for (int i = 0; i < count; i++)
        {
            total = total + s[i].Marks;
        }

        return total / count;
    }

    public static Student TopStudent(Student[] s, int count)
    {
        Student top = s[0];

        for (int i = 1; i < count; i++)
        {
            if (s[i].Marks > top.Marks)
            {
                top = s[i];
            }
        }

        return top;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Student[] s = new Student[10];
        int count = 0;
        int choice = 0;

        while (choice != 5)
        {
            Console.WriteLine("1 Add Student");
            Console.WriteLine("2 Show Students");
            Console.WriteLine("3 Calculate Aggregate");
            Console.WriteLine("4 Top Student");
            Console.WriteLine("5 Exit");

            Console.Write("Enter your Choice = ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                s[count] = new Student();
                s[count].AddStudent();
                count++;
            }

            else if (choice == 2)
            {
                Console.Clear();
                for (int i = 0; i < count; i++)
                {
                    s[i].ShowStudent();
                }
            }

            else if (choice == 3)
            {
                Console.Clear();
                double agg = Student.CalculateAggregate(s, count);
                Console.WriteLine("Aggregate: " + agg);
            }

            else if (choice == 4)
            {
                Console.Clear();
                Student top = Student.TopStudent(s, count);
                Console.WriteLine("Top Student: " + top.Name + " " + top.Marks);
            }
        }
    }
}