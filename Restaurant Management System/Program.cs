using System;
using System.IO;
using System.Collections.Generic;

namespace RestaurantManagementSystem
{
    struct User
    {
        public string Username;
        public string Password;
        public string Name;
        public string Phone;
        public string Cnic;
    }

    struct MenuItem
    {
        public int Code;
        public string Name;
        public double Price;
        public double Rating;
        public int RatingCount;
        public bool IsOnSale;
        public double SalePrice;
    }

    struct Complaint
    {
        public int Number;
        public string CustomerName;
        public string Item;
        public string Details;
    }

    class Program
    {
        static void Main(string[] args)
        {
            User[] users = new User[100];
            MenuItem[] menu = new MenuItem[50];
            Complaint[] complaints = new Complaint[100];

            int UserCount = 0;
            int MenuCount = 0;
            int ComplaintCount = 0;

            double TodaySales = 0;
            int BillCount = 0;

            string CurrentUser = "";
            string CurrentUserName = "";
            bool IsAdmin = false;

            if (File.Exists("users.txt"))
            {
                using (StreamReader UserFile = new StreamReader("users.txt"))
                {
                    string Line;
                    while (UserCount < 100 && (Line = UserFile.ReadLine()) != null)
                    {
                        if (Line == "---")
                            continue;
                        users[UserCount].Username = Line;
                        if ((Line = UserFile.ReadLine()) == null) break;
                        users[UserCount].Password = Line;
                        if ((Line = UserFile.ReadLine()) == null) break;
                        users[UserCount].Name = Line;
                        if ((Line = UserFile.ReadLine()) == null) break;
                        users[UserCount].Phone = Line;
                        if ((Line = UserFile.ReadLine()) == null) break;
                        users[UserCount].Cnic = Line;
                        if ((Line = UserFile.ReadLine()) == null) break;
                        if ((Line = UserFile.ReadLine()) == null) break;
                        UserCount++;
                    }
                }
            }

            if (File.Exists("menu.txt"))
            {
                using (StreamReader MenuFile = new StreamReader("menu.txt"))
                {
                    string Line;
                    while (MenuCount < 50 && (Line = MenuFile.ReadLine()) != null)
                    {
                        string[] parts = Line.Split(' ');
                        if (parts.Length >= 6)
                        {
                            menu[MenuCount].Code = int.Parse(parts[0]);
                            menu[MenuCount].Price = double.Parse(parts[1]);
                            menu[MenuCount].Rating = double.Parse(parts[2]);
                            menu[MenuCount].RatingCount = int.Parse(parts[3]);
                            menu[MenuCount].IsOnSale = bool.Parse(parts[4]);
                            menu[MenuCount].SalePrice = double.Parse(parts[5]);
                            menu[MenuCount].Name = MenuFile.ReadLine();
                            MenuCount++;
                        }
                    }
                }
            }

            if (File.Exists("complaints.txt"))
            {
                using (StreamReader CompFile = new StreamReader("complaints.txt"))
                {
                    string Line;
                    while (ComplaintCount < 100 && (Line = CompFile.ReadLine()) != null)
                    {
                        if (Line == "---")
                            continue;
                        complaints[ComplaintCount].Number = StringToInteger(Line);
                        if ((Line = CompFile.ReadLine()) != null)
                            complaints[ComplaintCount].CustomerName = Line;
                        if ((Line = CompFile.ReadLine()) != null)
                            complaints[ComplaintCount].Item = Line;
                        if ((Line = CompFile.ReadLine()) != null)
                            complaints[ComplaintCount].Details = Line;
                        ComplaintCount++;
                        CompFile.ReadLine();
                    }
                }
            }

            bool Running = true;
            while (Running)
            {
                Console.Clear();

                SetColor(12);
                Console.Write("\t\tRESTAURANT ");
                SetColor(6);
                Console.Write("MANAGEMENT ");
                SetColor(14);
                Console.WriteLine("SYSTEM");
                SetColor(2);
                Console.WriteLine(" =========================================================");

                SetColor(10);
                Console.Write("|");
                SetColor(14);
                Console.Write("  1. ");
                SetColor(7);
                Console.Write("View Menu as Guest                                  ");
                SetColor(10);
                Console.WriteLine("|");
                SetColor(10);
                Console.Write("|");
                SetColor(14);
                Console.Write("  2. ");
                SetColor(7);
                Console.Write("Login as Customer                                   ");
                SetColor(10);
                Console.WriteLine("|");
                SetColor(10);
                Console.Write("|");
                SetColor(14);
                Console.Write("  3. ");
                SetColor(7);
                Console.Write("Sign Up as Customer                                 ");
                SetColor(10);
                Console.WriteLine("|");
                SetColor(10);
                Console.Write("|");
                SetColor(14);
                Console.Write("  4. ");
                SetColor(7);
                Console.Write("Login as Administrator                              ");
                SetColor(10);
                Console.WriteLine("|");
                SetColor(10);
                Console.Write("|");
                SetColor(14);
                Console.Write("  5. ");
                SetColor(7);
                Console.Write("Close Application                                   ");
                SetColor(10);
                Console.WriteLine("|");

                SetColor(2);
                Console.WriteLine(" =========================================================");

                int MainChoice;
                string Input;
                while (true)
                {
                    SetColor(11);
                    Console.Write("  Your Choice = ");
                    SetColor(7);
                    Input = Console.ReadLine();

                    if (string.IsNullOrEmpty(Input))
                    {
                        SetColor(12);
                        Console.WriteLine("  Please enter something!");
                        SetColor(7);
                        continue;
                    }

                    if (Input.Length != 1)
                    {
                        SetColor(12);
                        Console.WriteLine("  Please enter exactly one digit!");
                        SetColor(7);
                        continue;
                    }

                    char Ch = Input[0];
                    if (!char.IsDigit(Ch))
                    {
                        SetColor(12);
                        Console.WriteLine("  Please enter a number!");
                        SetColor(7);
                        continue;
                    }

                    MainChoice = Ch - '0';
                    if (MainChoice < 1 || MainChoice > 5)
                    {
                        SetColor(12);
                        Console.WriteLine("  Please enter 1-5!");
                        SetColor(7);
                        continue;
                    }
                    break;
                }

                if (MainChoice == 1)
                {
                    bool GuestRunning = true;
                    while (GuestRunning)
                    {
                        Console.Clear();
                        SetColor(9);
                        Console.WriteLine("\t\t    GUEST VIEW");
                        SetColor(2);
                        Console.WriteLine(" =========================================================");

                        SetColor(10);
                        Console.Write("|");
                        SetColor(14);
                        Console.Write("  1. ");
                        SetColor(7);
                        Console.Write("View Menu                                           ");
                        SetColor(10);
                        Console.WriteLine("|");
                        SetColor(10);
                        Console.Write("|");
                        SetColor(14);
                        Console.Write("  2. ");
                        SetColor(7);
                        Console.Write("Sort Menu Items                                     ");
                        SetColor(10);
                        Console.WriteLine("|");
                        SetColor(10);
                        Console.Write("|");
                        SetColor(14);
                        Console.Write("  3. ");
                        SetColor(7);
                        Console.Write("Go Back to Main Menu                                ");
                        SetColor(10);
                        Console.WriteLine("|");

                        SetColor(2);
                        Console.WriteLine(" =========================================================");

                        int GuestChoice;
                        string GuestInput;
                        while (true)
                        {
                            SetColor(11);
                            Console.Write("  Your Choice = ");
                            SetColor(7);
                            GuestInput = Console.ReadLine();

                            if (string.IsNullOrEmpty(GuestInput) || GuestInput.Length != 1 || !char.IsDigit(GuestInput[0]))
                            {
                                SetColor(12);
                                Console.WriteLine("  Please enter 1-3!");
                                SetColor(7);
                                continue;
                            }
                            GuestChoice = GuestInput[0] - '0';
                            if (GuestChoice < 1 || GuestChoice > 3)
                            {
                                SetColor(12);
                                Console.WriteLine("  Please enter 1-3!");
                                SetColor(7);
                                continue;
                            }
                            break;
                        }

                        if (GuestChoice == 1)
                        {
                            Console.Clear();
                            DisplayMenuItems(menu, MenuCount);
                            SetColor(4);
                            Console.WriteLine("\n  Note: You must login to order food items.");
                            SetColor(7);
                            Console.WriteLine("\n  Press Enter to continue...");
                            Console.ReadLine();
                        }
                        else if (GuestChoice == 2)
                        {
                            ShowSortingOptions(menu, ref MenuCount);
                        }
                        else if (GuestChoice == 3)
                        {
                            GuestRunning = false;
                        }
                    }
                }
                else if (MainChoice == 2)
                {
                    Console.Clear();
                    SetColor(9);
                    Console.WriteLine("\n\t\t    CUSTOMER LOGIN");
                    SetColor(2);
                    Console.WriteLine("===========================================================");
                    SetColor(7);

                    string Username, Password;
                    SetColor(11);
                    Console.Write("  Enter username: ");
                    SetColor(7);
                    Username = Console.ReadLine();

                    SetColor(11);
                    Console.Write("  Enter password: ");
                    SetColor(7);
                    Password = Console.ReadLine();

                    bool LoginSuccess = false;
                    for (int i = 0; i < UserCount; i++)
                    {
                        if (users[i].Username == Username && users[i].Password == Password)
                        {
                            CurrentUser = Username;
                            CurrentUserName = users[i].Name;
                            IsAdmin = false;
                            LoginSuccess = true;
                            break;
                        }
                    }

                    if (LoginSuccess)
                    {
                        SetColor(10);
                        Console.WriteLine("\n  Login successful! Welcome " + CurrentUserName + "!");
                        SetColor(7);
                        Console.WriteLine("\n  Press Enter to continue...");
                        Console.ReadLine();

                        bool CustomerRunning = true;
                        while (CustomerRunning)
                        {
                            Console.Clear();
                            SetColor(9);
                            Console.WriteLine("\t\t    CUSTOMER AREA");
                            SetColor(2);
                            Console.WriteLine(" =========================================================");
                            SetColor(7);

                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  1. ");
                            SetColor(7);
                            Console.Write("Order Food Item                                     ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  2. ");
                            SetColor(7);
                            Console.Write("View Menu                                           ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  3. ");
                            SetColor(7);
                            Console.Write("Sort Menu Items                                     ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  4. ");
                            SetColor(7);
                            Console.Write("View Bills                                          ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  5. ");
                            SetColor(7);
                            Console.Write("Rate Food Item                                      ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  6. ");
                            SetColor(7);
                            Console.Write("Submit Complaint                                    ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  7. ");
                            SetColor(7);
                            Console.Write("Logout                                              ");
                            SetColor(10);
                            Console.WriteLine("|");

                            SetColor(2);
                            Console.WriteLine(" =========================================================");

                            int CustomerChoice;
                            string CustInput;
                            while (true)
                            {
                                SetColor(11);
                                Console.Write("  Your Choice = ");
                                SetColor(7);
                                CustInput = Console.ReadLine();

                                if (string.IsNullOrEmpty(CustInput) || CustInput.Length != 1 || !char.IsDigit(CustInput[0]))
                                {
                                    SetColor(12);
                                    Console.WriteLine("  Please enter 1-7!");
                                    SetColor(7);
                                    continue;
                                }
                                CustomerChoice = CustInput[0] - '0';
                                if (CustomerChoice < 1 || CustomerChoice > 7)
                                {
                                    SetColor(12);
                                    Console.WriteLine("  Please enter 1-7!");
                                    SetColor(7);
                                    continue;
                                }
                                break;
                            }

                            if (CustomerChoice == 1)
                            {
                                OrderFoodItem(CurrentUserName, menu, MenuCount, ref TodaySales, ref BillCount);
                            }
                            else if (CustomerChoice == 2)
                            {
                                Console.Clear();
                                DisplayMenuItems(menu, MenuCount);
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                            else if (CustomerChoice == 3)
                            {
                                ShowSortingOptions(menu, ref MenuCount);
                            }
                            else if (CustomerChoice == 4)
                            {
                                Console.Clear();
                                SetColor(10);
                                Console.WriteLine("\n\t\t     YOUR BILLS");
                                SetColor(2);
                                Console.WriteLine("===========================================================");
                                SetColor(7);

                                if (!File.Exists("bills.txt"))
                                {
                                    SetColor(12);
                                    Console.WriteLine("  No bills found!");
                                    SetColor(7);
                                }
                                else
                                {
                                    using (StreamReader BillFile = new StreamReader("bills.txt"))
                                    {
                                        string Line;
                                        while ((Line = BillFile.ReadLine()) != null)
                                        {
                                            Console.WriteLine("  " + Line);
                                        }
                                    }
                                }
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                            else if (CustomerChoice == 5)
                            {
                                RateFoodItem(menu, MenuCount);
                            }
                            else if (CustomerChoice == 6)
                            {
                                SubmitComplaint(CurrentUserName, complaints, ref ComplaintCount, menu, MenuCount);
                            }
                            else if (CustomerChoice == 7)
                            {
                                SetColor(10);
                                Console.WriteLine("\n  Logging out...");
                                SetColor(7);
                                CurrentUser = "";
                                CurrentUserName = "";
                                CustomerRunning = false;
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        SetColor(12);
                        Console.WriteLine("\n  Invalid username or password!");
                        SetColor(7);
                        Console.WriteLine("\n  Press Enter to continue...");
                        Console.ReadLine();
                    }
                }
                else if (MainChoice == 3)
                {
                    SignUpCustomer(users, ref UserCount);
                }
                else if (MainChoice == 4)
                {
                    Console.Clear();
                    SetColor(13);
                    Console.WriteLine("\n\t\t     ADMIN LOGIN");
                    SetColor(2);
                    Console.WriteLine("===========================================================");
                    SetColor(7);

                    string Username, Password;
                    SetColor(11);
                    Console.Write("  Enter Admin Username: ");
                    SetColor(7);
                    Username = Console.ReadLine();

                    SetColor(11);
                    Console.Write("  Enter Admin Password: ");
                    SetColor(7);
                    Password = Console.ReadLine();

                    if (Username == "admin" && Password == "123")
                    {
                        SetColor(10);
                        Console.WriteLine("\n  Admin login successful!");
                        SetColor(7);
                        Console.WriteLine("\n  Press Enter to continue...");
                        Console.ReadLine();

                        IsAdmin = true;
                        bool AdminRunning = true;
                        while (AdminRunning)
                        {
                            Console.Clear();
                            SetColor(13);
                            Console.WriteLine("\t\t   ADMIN PANEL");
                            SetColor(2);
                            Console.WriteLine(" =========================================================");
                            SetColor(7);

                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  1. ");
                            SetColor(7);
                            Console.Write("Add New Food Item                                   ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  2. ");
                            SetColor(7);
                            Console.Write("Remove Food Item                                    ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  3. ");
                            SetColor(7);
                            Console.Write("View All Complaints                                 ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  4. ");
                            SetColor(7);
                            Console.Write("View Sales Report                                   ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  5. ");
                            SetColor(7);
                            Console.Write("View All Users                                      ");
                            SetColor(10);
                            Console.WriteLine("|");
                            SetColor(10);
                            Console.Write("|");
                            SetColor(14);
                            Console.Write("  6. ");
                            SetColor(7);
                            Console.Write("Logout                                              ");
                            SetColor(10);
                            Console.WriteLine("|");

                            SetColor(2);
                            Console.WriteLine(" =========================================================");

                            int AdminChoice;
                            string AdminInput;
                            while (true)
                            {
                                SetColor(11);
                                Console.Write("  Your Choice = ");
                                SetColor(7);
                                AdminInput = Console.ReadLine();

                                if (string.IsNullOrEmpty(AdminInput) || AdminInput.Length != 1 || !char.IsDigit(AdminInput[0]))
                                {
                                    SetColor(12);
                                    Console.WriteLine("  Please enter 1-6!");
                                    SetColor(7);
                                    continue;
                                }
                                AdminChoice = AdminInput[0] - '0';
                                if (AdminChoice < 1 || AdminChoice > 6)
                                {
                                    SetColor(12);
                                    Console.WriteLine("  Please enter 1-6!");
                                    SetColor(7);
                                    continue;
                                }
                                break;
                            }

                            if (AdminChoice == 1)
                            {
                                AddNewFoodItem(menu, ref MenuCount);
                            }
                            else if (AdminChoice == 2)
                            {
                                RemoveFoodItem(menu, ref MenuCount);
                            }
                            else if (AdminChoice == 3)
                            {
                                Console.Clear();
                                SetColor(4);
                                Console.WriteLine("\n\t\t   CUSTOMER COMPLAINTS");
                                SetColor(2);
                                Console.WriteLine("================================================================");
                                SetColor(7);

                                if (!File.Exists("complaints.txt"))
                                {
                                    SetColor(12);
                                    Console.WriteLine("  No complaints found!");
                                    SetColor(7);
                                }
                                else
                                {
                                    using (StreamReader CompFileIn = new StreamReader("complaints.txt"))
                                    {
                                        string Line;
                                        int Count = 0;
                                        while ((Line = CompFileIn.ReadLine()) != null)
                                        {
                                            if (Line == "---")
                                            {
                                                SetColor(2);
                                                Console.WriteLine("---------------------------------------------------------------");
                                                SetColor(7);
                                                continue;
                                            }
                                            Console.WriteLine("  " + Line);
                                            Count++;
                                        }
                                    }

                                    if (File.ReadAllText("complaints.txt").Trim().Length == 0)
                                    {
                                        SetColor(10);
                                        Console.WriteLine("  No complaints yet.");
                                        SetColor(7);
                                    }
                                }
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                            else if (AdminChoice == 4)
                            {
                                Console.Clear();
                                SetColor(10);
                                Console.WriteLine("\n\t\t   SALES REPORT");
                                SetColor(2);
                                Console.WriteLine("===========================================================");
                                SetColor(7);

                                Console.WriteLine($"{"Today's Sales:",-25}${TodaySales:F2}");
                                Console.WriteLine($"{"Total Bills:",-25}{BillCount}");
                                Console.WriteLine($"{"Average Bill:",-25}${(BillCount > 0 ? TodaySales / BillCount : 0):F2}");

                                SetColor(2);
                                Console.WriteLine("===========================================================");

                                if (TodaySales > 100)
                                {
                                    SetColor(10);
                                    Console.WriteLine("  Excellent sales today!");
                                }
                                else if (TodaySales > 50)
                                {
                                    SetColor(6);
                                    Console.WriteLine("  Good sales today!");
                                }
                                else
                                {
                                    SetColor(12);
                                    Console.WriteLine("  Sales need improvement.");
                                }
                                SetColor(7);
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                            else if (AdminChoice == 5)
                            {
                                Console.Clear();
                                SetColor(9);
                                Console.WriteLine("\n\t\t   ALL REGISTERED CUSTOMERS");
                                SetColor(2);
                                Console.WriteLine("================================================================");
                                SetColor(14);
                                Console.WriteLine($"{"Username",-15}{"Name",-20}{"Phone",-15}{"CNIC",-15}");
                                SetColor(2);
                                Console.WriteLine("================================================================");
                                SetColor(7);

                                if (UserCount == 0)
                                {
                                    SetColor(12);
                                    Console.WriteLine("  No users found!");
                                    SetColor(7);
                                }
                                else
                                {
                                    for (int i = 0; i < UserCount; i++)
                                    {
                                        Console.WriteLine($"{users[i].Username,-15}{users[i].Name,-20}{users[i].Phone,-15}{users[i].Cnic,-15}");
                                    }
                                }
                                SetColor(2);
                                Console.WriteLine("================================================================");
                                SetColor(7);
                                Console.WriteLine($"\n  Total Users: {UserCount}");
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                            else if (AdminChoice == 6)
                            {
                                SetColor(10);
                                Console.WriteLine("\n  Logging out...");
                                SetColor(7);
                                IsAdmin = false;
                                AdminRunning = false;
                                Console.WriteLine("\n  Press Enter to continue...");
                                Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        SetColor(12);
                        Console.WriteLine("\n  Invalid admin credentials!");
                        SetColor(7);
                        Console.WriteLine("\n  Press Enter to continue...");
                        Console.ReadLine();
                    }
                }
                else if (MainChoice == 5)
                {
                    Console.Clear();
                    SetColor(10);
                    Console.WriteLine("\n\t\tThank you for using Restaurant Management System!");
                    Console.WriteLine("\t\tGoodbye!\n");
                    SetColor(7);
                    Running = false;
                }
            }
        }
        static void SetColor(int colorCode)
        {
            switch (colorCode)
            {
                case 1: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case 2: Console.ForegroundColor = ConsoleColor.Green; break;
                case 3: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case 4: Console.ForegroundColor = ConsoleColor.Red; break;
                case 5: Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 6: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 7: Console.ForegroundColor = ConsoleColor.White; break;
                case 8: Console.ForegroundColor = ConsoleColor.Gray; break;
                case 9: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 10: Console.ForegroundColor = ConsoleColor.Green; break;
                case 11: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 12: Console.ForegroundColor = ConsoleColor.Red; break;
                case 13: Console.ForegroundColor = ConsoleColor.Magenta; break;
                case 14: Console.ForegroundColor = ConsoleColor.Yellow; break;
                default: Console.ForegroundColor = ConsoleColor.White; break;
            }
        }

        static int StringToInteger(string Str)
        {
            int Result = 0;
            for (int i = 0; i < Str.Length; i++)
            {
                char DigitChar = Str[i];
                int Digit = DigitChar - '0';
                Result = Result * 10 + Digit;
            }
            return Result;
        }

        static void DisplayMenuItems(MenuItem[] menu, int MenuCount)
        {
            SetColor(10);
            Console.WriteLine("\n\t\t\t   FOOD MENU");
            SetColor(2);
            Console.WriteLine("===============================================================");

            SetColor(14);
            Console.Write($"{"Code",-6}{"Item Name",-25}{"Price",-10}{"Rating",-10}\n");

            SetColor(2);
            Console.WriteLine("===============================================================");
            SetColor(7);

            for (int i = 0; i < MenuCount; i++)
            {
                Console.Write($" {menu[i].Code,-4}{menu[i].Name,-25}$");

                if (menu[i].IsOnSale)
                {
                    SetColor(12);
                    Console.Write($"{menu[i].SalePrice,-9:F2}");
                    SetColor(7);
                }
                else
                {
                    Console.Write($"{menu[i].Price,-9:F2}");
                }

                if (menu[i].RatingCount > 0)
                {
                    double AvgRating = menu[i].Rating / menu[i].RatingCount;
                    if (AvgRating >= 4.0)
                    {
                        SetColor(10);
                    }
                    else if (AvgRating >= 3.0)
                    {
                        SetColor(6);
                    }
                    else
                    {
                        SetColor(12);
                    }
                    Console.Write($"{AvgRating,2:F1}/5");
                    SetColor(7);
                }
                else
                {
                    Console.Write("No ratings");
                }
                Console.WriteLine();
            }
            SetColor(2);
            Console.WriteLine("===============================================================");
            SetColor(7);
        }

        static void SortPriceLowToHigh(MenuItem[] menu, int MenuCount)
        {
            for (int i = 0; i < MenuCount - 1; i++)
            {
                for (int j = 0; j < MenuCount - i - 1; j++)
                {
                    double Price1 = menu[j].IsOnSale ? menu[j].SalePrice : menu[j].Price;
                    double Price2 = menu[j + 1].IsOnSale ? menu[j + 1].SalePrice : menu[j + 1].Price;
                    if (Price1 > Price2)
                    {
                        MenuItem temp = menu[j];
                        menu[j] = menu[j + 1];
                        menu[j + 1] = temp;
                    }
                }
            }
        }

        static void SortPriceHighToLow(MenuItem[] menu, int MenuCount)
        {
            for (int i = 0; i < MenuCount - 1; i++)
            {
                for (int j = 0; j < MenuCount - i - 1; j++)
                {
                    double Price1 = menu[j].IsOnSale ? menu[j].SalePrice : menu[j].Price;
                    double Price2 = menu[j + 1].IsOnSale ? menu[j + 1].SalePrice : menu[j + 1].Price;
                    if (Price1 < Price2)
                    {
                        MenuItem temp = menu[j];
                        menu[j] = menu[j + 1];
                        menu[j + 1] = temp;
                    }
                }
            }
        }

        static void SortMostRated(MenuItem[] menu, int MenuCount)
        {
            for (int i = 0; i < MenuCount - 1; i++)
            {
                for (int j = 0; j < MenuCount - i - 1; j++)
                {
                    if (menu[j].RatingCount < menu[j + 1].RatingCount)
                    {
                        MenuItem temp = menu[j];
                        menu[j] = menu[j + 1];
                        menu[j + 1] = temp;
                    }
                }
            }
        }

        static void ShowSortingOptions(MenuItem[] menu, ref int MenuCount)
        {
            Console.Clear();
            SetColor(9);
            Console.WriteLine("\t\t    SORTING OPTIONS");
            SetColor(2);
            Console.WriteLine(" =========================================================");

            SetColor(10);
            Console.Write("|");
            SetColor(14);
            Console.Write("  1. ");
            SetColor(7);
            Console.Write("Price (Low to High)                                   ");
            SetColor(10);
            Console.WriteLine("|");
            SetColor(10);
            Console.Write("|");
            SetColor(14);
            Console.Write("  2. ");
            SetColor(7);
            Console.Write("Price (High to Low)                                   ");
            SetColor(10);
            Console.WriteLine("|");
            SetColor(10);
            Console.Write("|");
            SetColor(14);
            Console.Write("  3. ");
            SetColor(7);
            Console.Write("Most Rated Items                                      ");
            SetColor(10);
            Console.WriteLine("|");
            SetColor(10);
            Console.Write("|");
            SetColor(14);
            Console.Write("  4. ");
            SetColor(7);
            Console.Write("Back to Menu                                          ");
            SetColor(10);
            Console.WriteLine("|");

            SetColor(2);
            Console.WriteLine(" =========================================================");

            int SortChoice;
            string SortInput;
            while (true)
            {
                SetColor(11);
                Console.Write("  Your Choice = ");
                SetColor(7);
                SortInput = Console.ReadLine();

                if (string.IsNullOrEmpty(SortInput) || SortInput.Length != 1 || !char.IsDigit(SortInput[0]))
                {
                    SetColor(12);
                    Console.WriteLine("  Please enter 1-4!");
                    SetColor(7);
                    continue;
                }
                SortChoice = SortInput[0] - '0';
                if (SortChoice < 1 || SortChoice > 4)
                {
                    SetColor(12);
                    Console.WriteLine("  Please enter 1-4!");
                    SetColor(7);
                    continue;
                }
                break;
            }

            if (SortChoice == 1)
            {
                SortPriceLowToHigh(menu, MenuCount);
                SetColor(10);
                Console.WriteLine("\n  Sorted by Price (Low to High)!");
                SetColor(7);
            }
            else if (SortChoice == 2)
            {
                SortPriceHighToLow(menu, MenuCount);
                SetColor(10);
                Console.WriteLine("\n  Sorted by Price (High to Low)!");
                SetColor(7);
            }
            else if (SortChoice == 3)
            {
                SortMostRated(menu, MenuCount);
                SetColor(10);
                Console.WriteLine("\n  Sorted by Most Rated Items!");
                SetColor(7);
            }
            else if (SortChoice == 4)
            {
                return;
            }

            Console.Clear();
            DisplayMenuItems(menu, MenuCount);
            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }

        static void SignUpCustomer(User[] users, ref int UserCount)
        {
            Console.Clear();
            SetColor(10);
            Console.WriteLine("\n\t\t    CUSTOMER SIGN UP");
            SetColor(2);
            Console.WriteLine("===========================================================");
            SetColor(7);

            if (UserCount >= 100)
            {
                SetColor(12);
                Console.WriteLine("  Maximum users reached!");
                SetColor(7);
                Console.WriteLine("\n  Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            string Username, Password, Name, Phone, CnicNum;

            while (true)
            {
                SetColor(11);
                Console.Write("  Enter username: ");
                SetColor(7);
                Username = Console.ReadLine();

                if (string.IsNullOrEmpty(Username))
                {
                    SetColor(12);
                    Console.WriteLine("  Username cannot be empty!");
                    SetColor(7);
                    continue;
                }

                bool Exists = false;
                for (int i = 0; i < UserCount; i++)
                {
                    if (users[i].Username == Username)
                    {
                        Exists = true;
                        break;
                    }
                }

                if (Exists)
                {
                    SetColor(12);
                    Console.WriteLine("  Username already exists! Try another.");
                    SetColor(7);
                }
                else
                {
                    break;
                }
            }

            SetColor(11);
            Console.Write("  Enter password: ");
            SetColor(7);
            Password = Console.ReadLine();

            while (true)
            {
                SetColor(11);
                Console.Write("  Enter full name: ");
                SetColor(7);
                Name = Console.ReadLine();

                bool Valid = true;
                foreach (char c in Name)
                {
                    if (!char.IsLetter(c) && c != ' ')
                    {
                        Valid = false;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(Name))
                {
                    SetColor(12);
                    Console.WriteLine("  Name cannot be empty!");
                    SetColor(7);
                }
                else if (!Valid)
                {
                    SetColor(12);
                    Console.WriteLine("  Name must contain only letters!");
                    SetColor(7);
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                SetColor(11);
                Console.Write("  Enter phone number (11 digits): ");
                SetColor(7);
                Phone = Console.ReadLine();

                if (Phone.Length != 11)
                {
                    SetColor(12);
                    Console.WriteLine("  Phone number must be exactly 11 digits!");
                    SetColor(7);
                    continue;
                }

                bool Valid = true;
                foreach (char c in Phone)
                {
                    if (!char.IsDigit(c))
                    {
                        Valid = false;
                        break;
                    }
                }

                if (!Valid)
                {
                    SetColor(12);
                    Console.WriteLine("  Phone number must contain only digits!");
                    SetColor(7);
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                SetColor(11);
                Console.Write("  Enter CNIC (13 digits): ");
                SetColor(7);
                CnicNum = Console.ReadLine();

                if (CnicNum.Length != 13)
                {
                    SetColor(12);
                    Console.WriteLine("  CNIC must be exactly 13 digits!");
                    SetColor(7);
                    continue;
                }

                bool Valid = true;
                foreach (char c in CnicNum)
                {
                    if (!char.IsDigit(c))
                    {
                        Valid = false;
                        break;
                    }
                }

                if (!Valid)
                {
                    SetColor(12);
                    Console.WriteLine("  CNIC must contain only digits!");
                    SetColor(7);
                }
                else
                {
                    break;
                }
            }

            users[UserCount].Username = Username;
            users[UserCount].Password = Password;
            users[UserCount].Name = Name;
            users[UserCount].Phone = Phone;
            users[UserCount].Cnic = CnicNum;
            UserCount++;

            using (StreamWriter UserFileOut = new StreamWriter("users.txt", true))
            {
                UserFileOut.WriteLine(Username);
                UserFileOut.WriteLine(Password);
                UserFileOut.WriteLine(Name);
                UserFileOut.WriteLine(Phone);
                UserFileOut.WriteLine(CnicNum);
                UserFileOut.WriteLine("customer");
                UserFileOut.WriteLine("---");
            }

            SetColor(10);
            Console.WriteLine("\n  Account created successfully!");
            SetColor(7);
            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }

        static void OrderFoodItem(string CurrentUserName, MenuItem[] menu, int MenuCount, ref double TodaySales, ref int BillCount)
        {
            Console.Clear();
            DisplayMenuItems(menu, MenuCount);

            int ItemChoice, Quantity;
            SetColor(11);
            Console.Write("\n  Enter item code to buy: ");
            SetColor(7);
            int.TryParse(Console.ReadLine(), out ItemChoice);

            if (ItemChoice < 1 || ItemChoice > MenuCount)
            {
                SetColor(12);
                Console.WriteLine("  Invalid item code!");
                SetColor(7);
            }
            else
            {
                SetColor(11);
                Console.Write("  Enter quantity: ");
                SetColor(7);
                int.TryParse(Console.ReadLine(), out Quantity);

                if (Quantity <= 0)
                {
                    SetColor(12);
                    Console.WriteLine("  Invalid quantity!");
                    SetColor(7);
                }
                else
                {
                    double Price = menu[ItemChoice - 1].IsOnSale ? menu[ItemChoice - 1].SalePrice : menu[ItemChoice - 1].Price;
                    double Total = Price * Quantity;
                    double Tax = Total * 0.05;
                    double Delivery = 1.0;
                    double FinalTotal = Total + Tax + Delivery;

                    TodaySales += FinalTotal;
                    BillCount++;

                    Console.Clear();
                    SetColor(10);
                    Console.WriteLine("\n\t\t      ORDER CONFIRMED!");
                    SetColor(2);
                    Console.WriteLine("==============================================================");
                    SetColor(7);

                    Console.WriteLine($"{"Customer",-25}{CurrentUserName}");
                    Console.WriteLine($"{"Item",-25}{menu[ItemChoice - 1].Name}");
                    Console.WriteLine($"{"Quantity",-25}{Quantity}");

                    SetColor(2);
                    Console.WriteLine("--------------------------------------------------------------");
                    SetColor(7);

                    Console.WriteLine($"{"Item Price:",-25}${Price:F2}");
                    Console.WriteLine($"{"Subtotal:",-25}${Total:F2}");
                    Console.WriteLine($"{"Tax (5%):",-25}${Tax:F2}");
                    Console.WriteLine($"{"Delivery:",-25}${Delivery:F2}");

                    SetColor(2);
                    Console.WriteLine("--------------------------------------------------------------");
                    SetColor(10);
                    Console.WriteLine($"{"TOTAL:",-25}${FinalTotal:F2}");
                    SetColor(7);

                    SetColor(2);
                    Console.WriteLine("==============================================================");
                    SetColor(7);

                    using (StreamWriter BillFile = new StreamWriter("bills.txt", true))
                    {
                        BillFile.WriteLine($"Bill #{BillCount}");
                        BillFile.WriteLine($"Customer: {CurrentUserName}");
                        BillFile.WriteLine($"Item: {menu[ItemChoice - 1].Name}");
                        BillFile.WriteLine($"Quantity: {Quantity}");
                        BillFile.WriteLine($"Total: ${FinalTotal:F2}");
                        BillFile.WriteLine("------------------------");
                    }
                }
            }
            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }

        static void RateFoodItem(MenuItem[] menu, int MenuCount)
        {
            Console.Clear();
            SetColor(10);
            Console.WriteLine("\n\t\t\t   FOOD MENU");
            SetColor(2);
            Console.WriteLine("================================================================");

            SetColor(14);
            Console.WriteLine($"{"Code",-6}{"Item Name",-25}");
            SetColor(2);
            Console.WriteLine("================================================================");
            SetColor(7);

            for (int i = 0; i < MenuCount; i++)
            {
                Console.WriteLine($"{menu[i].Code,-6}{menu[i].Name,-25}");
            }
            SetColor(2);
            Console.WriteLine("================================================================");
            SetColor(7);

            int ItemCodeInput;
            double UserRating;

            SetColor(11);
            Console.Write("\n  Enter item code to rate: ");
            SetColor(7);
            int.TryParse(Console.ReadLine(), out ItemCodeInput);

            if (ItemCodeInput < 1 || ItemCodeInput > MenuCount)
            {
                SetColor(12);
                Console.WriteLine("  Invalid item code!");
                SetColor(7);
            }
            else
            {
                SetColor(11);
                Console.Write("  Enter rating (1-5): ");
                SetColor(7);
                double.TryParse(Console.ReadLine(), out UserRating);

                if (UserRating < 1 || UserRating > 5)
                {
                    SetColor(12);
                    Console.WriteLine("  Rating must be between 1 and 5!");
                    SetColor(7);
                }
                else
                {
                    int Index = ItemCodeInput - 1;
                    menu[Index].Rating += UserRating;
                    menu[Index].RatingCount++;

                    SetColor(10);
                    Console.WriteLine("\n  Thank you for your rating!");
                    SetColor(7);

                    using (StreamWriter MenuFileOut = new StreamWriter("menu.txt"))
                    {
                        for (int i = 0; i < MenuCount; i++)
                        {
                            MenuFileOut.WriteLine($"{menu[i].Code} {menu[i].Price} {menu[i].Rating} {menu[i].RatingCount} {menu[i].IsOnSale} {menu[i].SalePrice}");
                            MenuFileOut.WriteLine(menu[i].Name);
                        }
                    }
                }
            }
            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }

        static void SubmitComplaint(string CurrentUserName, Complaint[] complaints, ref int ComplaintCount, MenuItem[] menu, int MenuCount)
        {
            Console.Clear();
            SetColor(10);
            Console.WriteLine("\n\t\t\t   FOOD MENU");
            SetColor(2);
            Console.WriteLine("================================================================");

            SetColor(14);
            Console.WriteLine($"{"Code",-6}{"Item Name",-25}");
            SetColor(2);
            Console.WriteLine("================================================================");
            SetColor(7);

            for (int i = 0; i < MenuCount; i++)
            {
                Console.WriteLine($"{menu[i].Code,-6}{menu[i].Name,-25}");
            }
            SetColor(2);
            Console.WriteLine("================================================================");
            SetColor(7);

            int ItemCodeInput;
            string ComplaintText;

            SetColor(11);
            Console.Write("\n  Enter item code to complain about: ");
            SetColor(7);
            int.TryParse(Console.ReadLine(), out ItemCodeInput);

            if (ItemCodeInput < 1 || ItemCodeInput > MenuCount)
            {
                SetColor(12);
                Console.WriteLine("  Invalid item code!");
                SetColor(7);
            }
            else
            {
                SetColor(11);
                Console.Write("  Enter your complaint: ");
                SetColor(7);
                ComplaintText = Console.ReadLine();

                complaints[ComplaintCount].Number = ComplaintCount + 1;
                complaints[ComplaintCount].CustomerName = CurrentUserName;
                complaints[ComplaintCount].Item = menu[ItemCodeInput - 1].Name;
                complaints[ComplaintCount].Details = ComplaintText;
                ComplaintCount++;

                using (StreamWriter CompFileOut = new StreamWriter("complaints.txt", true))
                {
                    CompFileOut.WriteLine($"Complaint #{complaints[ComplaintCount - 1].Number}");
                    CompFileOut.WriteLine($"Name : {complaints[ComplaintCount - 1].CustomerName}");
                    CompFileOut.WriteLine($"Food : {complaints[ComplaintCount - 1].Item}");
                    CompFileOut.WriteLine($"Message : {complaints[ComplaintCount - 1].Details}");
                    CompFileOut.WriteLine("---");
                }

                SetColor(10);
                Console.WriteLine("\n  Complaint recorded successfully!");
                Console.WriteLine($"  Complaint ID: {ComplaintCount}");
                SetColor(7);
            }
            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }

        static void AddNewFoodItem(MenuItem[] menu, ref int MenuCount)
        {
            Console.Clear();
            SetColor(10);
            Console.WriteLine("\n\t\t   ADD NEW ITEM");
            SetColor(2);
            Console.WriteLine("===========================================================");
            SetColor(7);

            if (MenuCount >= 50)
            {
                SetColor(12);
                Console.WriteLine("  Menu is full!");
                SetColor(7);
                Console.WriteLine("\n  Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            string Name;
            double Price;

            while (true)
            {
                SetColor(11);
                Console.Write("  Enter item name: ");
                SetColor(7);
                Name = Console.ReadLine();

                bool OnlySpaces = true;
                bool IsEmpty = true;

                for (int i = 0; i < Name.Length; i++)
                {
                    if (Name[i] != ' ' && Name[i] != '\t')
                    {
                        OnlySpaces = false;
                        IsEmpty = false;
                        break;
                    }
                }

                if (IsEmpty || OnlySpaces)
                {
                    SetColor(12);
                    Console.WriteLine("  Item name cannot be empty or just spaces!");
                    SetColor(7);
                    continue;
                }

                bool Exists = false;
                for (int i = 0; i < MenuCount; i++)
                {
                    if (menu[i].Name == Name)
                    {
                        Exists = true;
                        break;
                    }
                }

                if (Exists)
                {
                    SetColor(12);
                    Console.WriteLine("  Item with this name already exists!");
                    SetColor(7);
                    continue;
                }

                break;
            }

            while (true)
            {
                SetColor(11);
                Console.Write("  Enter price: $");
                SetColor(7);

                if (!double.TryParse(Console.ReadLine(), out Price))
                {
                    SetColor(12);
                    Console.WriteLine("  Invalid price! Please enter a valid number.");
                    SetColor(7);
                    continue;
                }

                if (Price <= 0)
                {
                    SetColor(12);
                    Console.WriteLine("  Price must be greater than 0!");
                    SetColor(7);
                    continue;
                }

                if (Price > 1000)
                {
                    SetColor(12);
                    Console.WriteLine("  Price is too high! Maximum is $1000.");
                    SetColor(7);
                    continue;
                }

                break;
            }

            menu[MenuCount].Code = MenuCount + 1;
            menu[MenuCount].Name = Name;
            menu[MenuCount].Price = Price;
            menu[MenuCount].Rating = 0;
            menu[MenuCount].RatingCount = 0;
            menu[MenuCount].IsOnSale = false;
            menu[MenuCount].SalePrice = 0;
            MenuCount++;

            using (StreamWriter MenuFileOut = new StreamWriter("menu.txt"))
            {
                for (int i = 0; i < MenuCount; i++)
                {
                    MenuFileOut.WriteLine($"{menu[i].Code} {menu[i].Price} {menu[i].Rating} {menu[i].RatingCount} {menu[i].IsOnSale} {menu[i].SalePrice}");
                    MenuFileOut.WriteLine(menu[i].Name);
                }
            }

            SetColor(10);
            Console.WriteLine($"\n  Item '{Name}' added successfully!");
            SetColor(7);
            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }

        static void RemoveFoodItem(MenuItem[] menu, ref int MenuCount)
        {
            Console.Clear();
            SetColor(12);
            Console.WriteLine("\n\t\t   REMOVE FOOD ITEM");
            SetColor(2);
            Console.WriteLine("===========================================================");
            SetColor(7);

            if (MenuCount == 0)
            {
                SetColor(12);
                Console.WriteLine("  Menu is empty! No items to remove.");
                SetColor(7);
                Console.WriteLine("\n  Press Enter to continue...");
                Console.ReadLine();
                return;
            }

            SetColor(14);
            Console.WriteLine($"{"Code",-6}{"Item Name",-25}{"Price",-15}");
            SetColor(2);
            Console.WriteLine("-----------------------------------------------------------");
            SetColor(7);

            for (int i = 0; i < MenuCount; i++)
            {
                Console.Write($" {menu[i].Code,-4}{menu[i].Name,-25}$");
                Console.WriteLine($"{(menu[i].IsOnSale ? menu[i].SalePrice : menu[i].Price),-14:F2}");
            }
            SetColor(2);
            Console.WriteLine("===========================================================");
            SetColor(7);

            int ItemChoice;
            SetColor(11);
            Console.Write("\n  Enter item code to remove (0 to cancel): ");
            SetColor(7);
            int.TryParse(Console.ReadLine(), out ItemChoice);

            if (ItemChoice == 0)
            {
                return;
            }

            if (ItemChoice < 1 || ItemChoice > MenuCount)
            {
                SetColor(12);
                Console.WriteLine("  Invalid item code!");
                SetColor(7);
            }
            else
            {
                int Index = ItemChoice - 1;
                string ItemToRemove = menu[Index].Name;

                SetColor(11);
                Console.Write($"  Are you sure you want to remove '{ItemToRemove}'? (y/n): ");
                SetColor(7);
                string Confirm = Console.ReadLine();

                if (Confirm == "y" || Confirm == "Y")
                {
                    for (int i = Index; i < MenuCount - 1; i++)
                    {
                        menu[i] = menu[i + 1];
                    }

                    MenuCount--;

                    for (int i = 0; i < MenuCount; i++)
                    {
                        menu[i].Code = i + 1;
                    }

                    using (StreamWriter MenuFileOut = new StreamWriter("menu.txt"))
                    {
                        for (int i = 0; i < MenuCount; i++)
                        {
                            MenuFileOut.WriteLine($"{menu[i].Code} {menu[i].Price} {menu[i].Rating} {menu[i].RatingCount} {menu[i].IsOnSale} {menu[i].SalePrice}");
                            MenuFileOut.WriteLine(menu[i].Name);
                        }
                    }

                    SetColor(10);
                    Console.WriteLine($"\n  Item '{ItemToRemove}' removed successfully!");
                    SetColor(7);
                }
                else
                {
                    SetColor(14);
                    Console.WriteLine("\n  Removal cancelled.");
                    SetColor(7);
                }
            }

            Console.WriteLine("\n  Press Enter to continue...");
            Console.ReadLine();
        }


    }
}