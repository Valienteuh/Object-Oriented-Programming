using System.ComponentModel;
using System.ComponentModel.Design;

namespace MyApp
{
    public class Homescreen
    {
        static void Main(string[] arg)
        {
            Console.Clear();
            menu();


        }
        static void menu()
        {
            string Input;
            int Choice;
            bool Run = true;
            while (Run)
            {
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("______________________________");
                Color(2);
                Console.WriteLine("  Welcome to Login Page -__-");
                Color(15);
                Console.WriteLine("______________________________");
                Console.WriteLine("1. Sign in");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("3. Exit");
                Console.WriteLine("______________________________");
                Console.Write("Enter Your Choice = ");
                Input = Console.ReadLine() ?? "";

                if (Input != "")
                {
                    if (int.TryParse(Input, out Choice))
                    {
                        if (Choice < 4 && Choice > 0)
                        {
                            switch (Choice)
                            {
                                case 1:
                                    Sign_In();
                                    break;
                                case 2:

                                    Sign_Up();
                                    break;
                                case 3:

                                    Color(12);
                                    Console.WriteLine("Exiting...");
                                    Thread.Sleep(2000);
                                    Run = false;

                                    break;
                            }
                        }
                        else
                        {
                            Color(12);
                            Console.Write($"\n\n  Enter a Valid Choice between 1-3 BRO, \"{Choice}\" is not Valid...!!!");
                            Color(15);
                        }
                    }
                    else
                    {
                        Color(12);
                        Console.Write("\n\n  Only Numeric Keywords are Allowed you DUMB...!!!");
                        Color(15);
                    }
                }
                else
                {
                    Color(12);
                    Console.Write("\n\n  Blank SPACES are Not Allowed you DUMB Chigga...!!!");
                    Color(15);

                }
                if (Run != false)
                {
                    Color(10);
                    Console.Write("\n\n [Press ENTER to Continue]");
                    Color(15);
                    Console.Read();
                }
            }



        }
        static void Color(int colorCode)
        {
            Console.ForegroundColor = (ConsoleColor)colorCode;
        }

        static void Sign_Up()
        {
            string Path = "Accounts.txt";
            using (StreamWriter File = new StreamWriter(Path, true))
            {
                string Input = "";
                bool InValid = true;
                while (InValid)
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("______________________________");
                    Color(2);
                    Console.WriteLine("    Enter your Credentials   ");
                    Color(15);
                    Console.WriteLine("______________________________");
                    Console.Write("\n Enter Username = ");
                    Input = Console.ReadLine() ?? "";
                    if (Input.Length > 8)
                    {
                        Color(12);
                        Console.WriteLine("\nUsername must not exceed 8 Characters...!\n");
                        Color(14);
                        Console.Write("\n\n [Press ENTER to Continue]");
                        Color(15);
                        Console.Read();
                    }
                    else if (Input == "")
                    {
                        Color(12);
                        Console.WriteLine("\nBlank Field in Username is not Allowed...!\n");
                        Color(14);
                        Console.Write("\n\n [Press ENTER to Continue]");
                        Color(15);
                        Console.Read();
                    }
                    else
                    {
                        bool Valid = true;

                        for (int i = 0; i < Input.Length; i++)
                        {
                            if (Input[i] == ' ')
                            {
                                Color(12);
                                Console.WriteLine("\nSpaces are not Allowed in Username...!\n");
                                Color(14);
                                Console.Write("\n\n [Press ENTER to Continue]");
                                Color(15);
                                Console.Read();
                                Valid = false;
                                break;
                            }
                            else if ((Input[i] > 32 && Input[i] < 48) ||
                                     (Input[i] > 57 && Input[i] < 65) ||
                                     (Input[i] > 90 && Input[i] < 97) ||
                                     (Input[i] > 122 && Input[i] < 127))
                            {
                                Color(12);
                                Console.WriteLine("\nSpecial Characters are not Allowed in Username...!\n");
                                Color(14);
                                Console.Write("\n\n [Press ENTER to Continue]");
                                Color(15);
                                Console.Read();
                                Valid = false;
                                break;
                            }
                        }
                        if (Valid)
                        {
                            InValid = false;
                        }
                    }
                }
                File.WriteLine("Username = " + Input);

                InValid = true;
                while (InValid)
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("______________________________");
                    Color(2);
                    Console.WriteLine("    Enter your Credentials   ");
                    Color(15);
                    Console.WriteLine("______________________________");
                    Color(10);
                    Console.Write("\n Enter Username = ENTERED SUCCESSFULLY");
                    Color(15);
                    Console.Write("\n Enter Password = ");
                    Input = Console.ReadLine() ?? "";
                    bool Valid = true;
                    if (Input.Length > 8)
                    {
                        Color(12);
                        Console.WriteLine("\nPassword must not exceed 8 Characters...!\n");
                        Color(14);
                        Console.Write("\n\n [Press ENTER to Continue]");
                        Color(15);
                        Console.Read();
                        Valid = false;
                    }
                    else if (Input == "")
                    {
                        Color(12);
                        Console.WriteLine("\nBlank Field in Password is not Allowed...!\n");
                        Color(14);
                        Console.Write("\n\n [Press ENTER to Continue]");
                        Color(15);
                        Console.Read();
                        Valid = false;
                    }


                    if (Valid)
                    {
                        InValid = false;
                    }
                }

                File.WriteLine("Password = " + Input);
                File.WriteLine("-----------------------------------------------");
            }

        }
        static void Sign_In()
        {
            string Path = "Accounts.txt";
            bool Run = true;

            while (Run)
            {
                string Username = "", Password = "";
                Console.Clear();
                Console.WriteLine("");
                Console.WriteLine("______________________________");
                Color(2);
                Console.WriteLine("    Enter your Credentials   ");
                Color(15);
                Console.WriteLine("______________________________");
                Color(14);
                Console.WriteLine("(Type \"+\" in Username and hit [ENTER] to Exit this Menu)");
                Color(15);
                Console.Write("\n Enter Username = ");
                Username = Console.ReadLine() ?? "";

                if (Username == "+")
                    break;

                Console.Write("\n Enter Password = ");
                Password = Console.ReadLine() ?? "";

                bool found = false;

                using (StreamReader File = new StreamReader(Path))
                {
                    string? Line;

                    while ((Line = File.ReadLine()) != null)
                    {
                        string A = Line;
                        string? B = File.ReadLine();
                        File.ReadLine();

                        A = Ignore_Title(A);
                        B = Ignore_Title(B);

                        if (Username == A && Password == B)
                        {
                            Color(10);
                            Console.Write("\n You Successfully Logged In...!");
                            Color(15);
                            Run = false;
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    Color(12);
                    Console.Write("\n Invalid Credentials...");
                    Color(10);
                    Console.Write("\n\n [Press ENTER to Continue]");
                    Color(15);
                    Console.Read();
                }
            }
        }

        static string Ignore_Title(string x)
        {
            string y = "";
            for (int i = 11; i < x.Length; i++)
            {

                y = y + x[i];
            }
            return y;
        }
    }
}