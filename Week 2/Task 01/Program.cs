using System;

class Transaction
{
    public int TransactionId;
    public string ProductName;
    public int Amount;
    public string Date;

    public Transaction()
    {
    }

    public Transaction(Transaction t)
    {
        TransactionId = t.TransactionId;
        ProductName = t.ProductName;
        Amount = t.Amount;
        Date = t.Date;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Transaction t1 = new Transaction();

        t1.TransactionId = 1;
        t1.ProductName = "Book";
        t1.Amount = 500;
        t1.Date = "12-05-2026";

        Transaction t2 = new Transaction(t1);

        t2.ProductName = "Pen";

        Console.WriteLine(t1.TransactionId + " " + t1.ProductName + " " + t1.Amount + " " + t1.Date);
        Console.WriteLine(t2.TransactionId + " " + t2.ProductName + " " + t2.Amount + " " + t2.Date);
    }
}