using System;
using System.IO;

class MUser
{
    public string Username;
    public string Password;
    public string Role;

    public void LoadUser(string line)
    {
        string[] data = line.Split(',');
        Username = data[0];
        Password = data[1];
        Role = data[2];
    }
}

class Program
{
    static void Main(string[] args)
    {
        MUser[] users = new MUser[10];
        int count = 0;

        StreamReader file = new StreamReader("users.txt");
        string line;

        while ((line = file.ReadLine()) != null)
        {
            users[count] = new MUser();
            users[count].LoadUser(line);
            count++;
        }

        file.Close();

        Console.Write("Enter Username: ");
        string u = Console.ReadLine();

        Console.Write("Enter Password: ");
        string p = Console.ReadLine();

        bool found = false;

        for (int i = 0; i < count; i++)
        {
            if (users[i].Username == u && users[i].Password == p)
            {
                Console.WriteLine("Login Successful");
                Console.WriteLine("Role: " + users[i].Role);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("User Not Found");
        }
    }
}