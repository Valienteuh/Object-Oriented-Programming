class Book
{
    public string Title;
    public string Author;
    public string Genre;
    public float Rating;
    public Book(string title, string author, string genre, float rating)
    {
        Title = title;
        Author = author;
        Genre = genre;
        Rating = rating;
    }
    public Book(Book b)
    {
        Title = b.Title;
        Author = b.Author;
        Genre = b.Genre;
        Rating = b.Rating;
    }
    public bool TopRated()
    {
        return Rating > 4.5;
    }
    public bool SearchByGenre(string genre)
    {
        return Genre == genre;
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Book> books = new List<Book>();
        List<Book> recommended = new List<Book>();

        books.Add(new Book("Life", "Saad", "Fiction", 4.5f));
        books.Add(new Book("Silent Night", "Ahmed Ali", "Fiction", 4.2f));
        books.Add(new Book("Lost Dreams", "Sarah Khan", "Fiction", 4.0f));
        books.Add(new Book("C# Programming Basics", "John Smith", "Education", 4.7f));
        books.Add(new Book("Space Exploration", "Neil Carter", "Science", 4.3f));

        Console.WriteLine("\n--- LIBRARY SYSTEM STARTED ---\n");

        Console.WriteLine("Top Rated Books:");
        foreach (Book book in books)
        {
            if (book.TopRated())
            {
                Console.WriteLine($"{book.Title} (Rating: {book.Rating})");
            }
        }
        Console.Write("\nEnter Genre to search: ");
        string searchGenre = Console.ReadLine();
        Console.WriteLine($"\nSearch Genre: {searchGenre}");
        Console.WriteLine("Results: ");

        foreach (Book book in books)
        {
            if (book.SearchByGenre(searchGenre))
            {
                Console.WriteLine(book.Title);
            }
        }

        Book highest = books[0];

        foreach (Book book in books)
        {
            if (book.Rating > highest.Rating)
            {
                highest = book;
            }
        }

        Book recommendedBook = new Book(highest);
        recommended.Add(recommendedBook);

        Console.WriteLine("\nRecommended Book Added:");
        Console.WriteLine($"{recommendedBook.Title}");

        Console.WriteLine("\n--- LIBRARY REPORT COMPLETE ---");
    }
}