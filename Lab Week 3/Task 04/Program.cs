
class Astronaut
{
    public string Name;
    public int Oxygen;
    public int Stamina;
    public bool IsConscious;
    public Astronaut(string name, int oxygen, int stamina, bool isconscious)
    {
        Name = name;
        Oxygen = oxygen;
        Stamina = stamina;
        IsConscious = isconscious;
    }
    public void OxygenRefill()
    {
        Console.WriteLine($"{Name} found an Oxygen Supply Station");
        Oxygen += 15;
        Oxygen = Math.Min(Oxygen, 100);
        CheckStatus();
    }
    public void MeteorHit()
    {
        Console.WriteLine($"{Name} got damaged by meteor");
        Oxygen -= 25;
        CheckStatus();
    }
    public void ShortBreak()
    {
        Console.WriteLine($"{Name} took a short rest");
        Stamina += 10;
        Stamina = Math.Min(Stamina, 100);
        CheckStatus();
    }
    public void EquipmentFailure()
    {
        Console.WriteLine($"{Name} fixed their equipment");
        Stamina -= 15;
        CheckStatus();
    }
    public void SmoothCycle()
    {
        Console.WriteLine($"{Name} had a smooth cycle");
        CheckStatus();
    }
    public void CheckStatus()
    {
        if (Oxygen <= 0)
        {
            IsConscious = false;
            Console.WriteLine($"{Name} has become UNCONSCIOUS!");
        }
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- SPACE MISSION STARTED ---\n");
        Astronaut a1 = new Astronaut("Saad", 100, 60, true);
        Astronaut a2 = new Astronaut("Hamza", 100, 60, true);
        Astronaut a3 = new Astronaut("Saim", 100, 60, true);
        Astronaut a4 = new Astronaut("Hassan", 100, 60, true);

        Astronaut[] crew = { a1, a2, a3, a4 };
        Random rand = new Random();

        for (int cycle = 1; cycle <= 10; cycle++)
        {
            Thread.Sleep(300);
            Console.WriteLine($"\nCycle {cycle}");

            foreach (Astronaut astronaut in crew)
            {
                if (!astronaut.IsConscious)
                {
                    continue;
                }
                int eventNum = rand.Next(5);

                if (eventNum == 0)
                {
                    Thread.Sleep(300);
                    astronaut.OxygenRefill();
                }
                else if (eventNum == 1)
                {
                    Thread.Sleep(300);
                    astronaut.MeteorHit();
                }
                else if (eventNum == 2)
                {
                    Thread.Sleep(300);
                    astronaut.ShortBreak();
                }
                else if (eventNum == 3)
                {
                    Thread.Sleep(300);
                    astronaut.EquipmentFailure();
                }
                else if (eventNum == 4)
                {
                    Thread.Sleep(300);
                    astronaut.SmoothCycle();
                }
            }
        }
        Console.WriteLine("\n--- FINAL RESULTS ---\n");
        Astronaut winner = null;
        int consciousCount = 0;

        foreach (Astronaut a in crew)
        {
            Thread.Sleep(300);
            string status = a.IsConscious ? "Conscious" : "Unconscious";
            Console.WriteLine($"{a.Name} -> Oxygen: {a.Oxygen}, Stamina: {a.Stamina} {status}");

            if (a.IsConscious)
            {
                consciousCount++;
                if (winner == null || a.Oxygen > winner.Oxygen)
                    winner = a;
            }
        }

        Console.WriteLine($"\nTotal Conscious Astronauts: {consciousCount}");
        if (winner != null)
        {
            Thread.Sleep(300);
            Console.WriteLine($"Winner: {winner.Name} (Highest Oxygen)");
        }
    }
}
