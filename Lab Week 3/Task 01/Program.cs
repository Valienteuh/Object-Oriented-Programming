
class Character
{
    public string Name;
    public int Health;
    public int Attack;
    public Character()
    {
    }
    public Character(Character c)
    {
        Name = c.Name + " Clone";
        Health = c.Health;
        Attack = c.Attack;
    }
    public void AttackEnemy(Character enemy)
    {
        enemy.Health -= Attack;

        if (enemy.Health < 0)
            enemy.Health = 0;

        Thread.Sleep(700);
        Console.WriteLine($"{Name} attacks {enemy.Name} --> {enemy.Name} Health: {enemy.Health}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Character c1 = new Character();
        Character c2 = new Character();

        Console.Write("Enter the Name of First Character: ");
        c1.Name = Console.ReadLine();

        Console.Write("Enter Health: ");
        c1.Health = int.Parse(Console.ReadLine());

        Console.Write("Enter Attack: ");
        c1.Attack = int.Parse(Console.ReadLine());


        Console.Write("\nEnter the Name of Second Character: ");
        c2.Name = Console.ReadLine();

        Console.Write("Enter Health: ");
        c2.Health = int.Parse(Console.ReadLine());

        Console.Write("Enter Attack: ");
        c2.Attack = int.Parse(Console.ReadLine());

        Character clone = new Character(c2);

        Console.WriteLine("\n--- BATTLE STARTED ---\n");

        int round = 1;
        bool cloneJoined = false;

        while (c1.Health > 0 && c2.Health > 0)
        {
            Thread.Sleep(700);
            Console.WriteLine($"\nRound {round}");

            c1.AttackEnemy(c2);

            if (c2.Health <= 0)
                break;

            c2.AttackEnemy(c1);

            if (c1.Health <= 0)
                break;

            if (round == 2 && !cloneJoined)
            {
                cloneJoined = true;
                Console.WriteLine($"\n{clone.Name} joins the battle!");
            }

            if (cloneJoined && clone.Health > 0 && c1.Health > 0)
            {
                clone.AttackEnemy(c1);
            }

            Thread.Sleep(700);
            round++;
        }

        if (c1.Health <= 0)
            Console.WriteLine($"{c1.Name} has been defeated! Winner: {c2.Name}");
        else
            Console.WriteLine($"{c2.Name} has been defeated! Winner: {c1.Name}");
    }
}
