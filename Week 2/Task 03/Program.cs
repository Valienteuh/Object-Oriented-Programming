using System;

class ATM
{
    public int Balance;
    public string[] History = new string[10];
    public int index = 0;

    public ATM(int b)
    {
        Balance = b;
    }

    public void Deposit(int amount)
    {
        Balance = Balance + amount;
        History[index] = "Deposit = " + amount;
        index++;
    }

    public void Withdraw(int amount)
    {
        if (amount <= Balance)
        {
            Balance = Balance - amount;
            History[index] = "Withdraw = " + amount;
            index++;
        }
        else
        {
            Console.WriteLine("Insufficient Balance");
        }
    }

    public void CheckBalance()
    {
        Console.WriteLine("Balance = " + Balance);
    }

    public void ShowHistory()
    {
        for (int i = 0; i < index; i++)
        {
            Console.WriteLine(History[i]);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        ATM a = new ATM(5000);

        a.Deposit(1000);
        a.Withdraw(500);
        a.CheckBalance();
        a.ShowHistory();
    }
}