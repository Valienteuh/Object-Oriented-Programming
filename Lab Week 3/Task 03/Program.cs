class ClockType
{
    public int Hours;
    public int Minutes;
    public int Seconds;
    public ClockType(int hours, int minutes, int seconds)
    {
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
    }
    public int ElapsedSeconds()
    {
        return (Hours * 3600) + (Minutes * 60) + Seconds;
    }
    public int RemainingSeconds()
    {
        return 86400 - ElapsedSeconds();
    }
    public int Difference(ClockType c)
    {
        return ElapsedSeconds() - c.ElapsedSeconds();
    }
    public void DisplayTime()
    {
        Console.WriteLine($"{Hours:D2}:{Minutes:D2}:{Seconds:D2}");
    }

}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- CLOCK ANALYZER STARTED ---");

        ClockType c1 = new ClockType(9, 15, 20);
        ClockType c2 = new ClockType(14, 40, 10);
        ClockType c3 = new ClockType(22, 10, 5);

        Console.Write($"Clock 1 --> ");
        c1.DisplayTime();
        Console.WriteLine($"Elapsed Seconds --> {c1.ElapsedSeconds()}");
        Console.WriteLine($"Remaining Seconds --> {c1.RemainingSeconds()}");

        Console.Write($"\nClock 2 --> ");
        c2.DisplayTime();
        Console.WriteLine($"Difference with Clock 1 --> {c2.Difference(c1)}");

        Console.Write($"\nClock 3 --> ");
        c3.DisplayTime();
        Console.WriteLine($"Remaining Seconds --> {c3.RemainingSeconds()}");

        Console.WriteLine("\n--- ANALYSIS COMPLETE ---");


    }
}
