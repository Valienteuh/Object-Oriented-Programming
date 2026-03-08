using System;

class Calculator
{
    double a;
    double b;

    public Calculator(double x, double y)
    {
        a = x;
        b = y;
    }

    public double Add()
    {
        return a + b;
    }

    public double Subtract()
    {
        return a - b;
    }

    public double Multiply()
    {
        return a * b;
    }

    public double Divide()
    {
        return a / b;
    }
}

class Program
{
    static void Main()
    {
        Console.Clear();

        Calculator c = new Calculator(10, 5);

        Console.WriteLine(c.Add());
        Console.WriteLine(c.Subtract());
        Console.WriteLine(c.Multiply());
        Console.WriteLine(c.Divide());
    }
}