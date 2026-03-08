
class Product
{
    public string Name;
    public double Price;
    public int Stock;
    public double TaxRate;
    public Product(string name, double price, int stock, double taxrate)
    {
        Name = name;
        Price = price;
        Stock = stock;
        TaxRate = taxrate;
    }

}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("---- STORE SYSTEM STARTED ----\n\n");
        List<Product> products = new List<Product>();
        double TotalTax = 0;

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Enter details for Product no {i + 1}:");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Stock: ");
            int stock = int.Parse(Console.ReadLine());

            Console.Write("Tax Rate in %age: ");
            double taxrate = double.Parse(Console.ReadLine());

            products.Add(new Product(name, price, stock, taxrate));
        }

        foreach (Product product in products)
        {
            TotalTax += (product.Price * (product.TaxRate / 100));
        }
        Console.WriteLine($"\nTotal Store Tax: {TotalTax}");

        Console.WriteLine("\n Low Stock Products:");
        foreach (Product product in products)
        {
            if (product.Stock < 10)
            {
                Console.WriteLine($"{product.Name} (Stock: {product.Stock})");
            }

        }
        Product most_expensive = products[0];

        foreach (Product product in products)
        {
            if (product.Price > most_expensive.Price)
            {
                most_expensive = product;
            }
        }
        Console.WriteLine("\nMost Expensive Product: ");
        Console.WriteLine($"{most_expensive.Name}--> Price: {most_expensive.Price} ");
    }
}
