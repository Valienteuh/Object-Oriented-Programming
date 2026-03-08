using System;
using System.Collections.Generic;

class Product
{
    public int ID;
    public string Name;
    public double Price;
    public string Category;
    public string Brand;
    public string Country;

    public Product(int id, string name, double price, string category, string brand, string country)
    {
        ID = id;
        Name = name;
        Price = price;
        Category = category;
        Brand = brand;
        Country = country;
    }
}

class Program
{
    static void Main()
    {
        List<Product> products = new List<Product>();

        products.Add(new Product(1, "Phone", 80000, "Electronics", "Samsung", "Korea"));
        products.Add(new Product(2, "Laptop", 150000, "Electronics", "Dell", "USA"));

        double total = 0;

        foreach (Product p in products)
        {
            Console.WriteLine(p.ID + " " + p.Name + " " + p.Price + " " + p.Category + " " + p.Brand + " " + p.Country);
            total = total + p.Price;
        }

        Console.WriteLine("Total Worth: " + total);
    }
}