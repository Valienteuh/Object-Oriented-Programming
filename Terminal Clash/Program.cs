using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace GameProject
{
    struct Enemy
    {
        public int X;
        public int Y;
        public int Direction;
        public bool IsAlive;
    }

    struct Coin
    {
        public int X;
        public int Y;
        public bool IsActive;
    }

    struct Bullet
    {
        public int X;
        public int Y;
        public bool IsActive;
        public int Direction;
    }

    class Program
    {
        static void color(int colorCode)
        {
            switch (colorCode)
            {
                case 2:
                case 10:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 4:
                case 12:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 6:
                case 14:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 7:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case 9:
                case 11:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 13:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }

        static void gotoxy(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        static void cursorVisible(bool visible)
        {
            Console.CursorVisible = visible;
        }

        static char getCharXY(int x, int y)
        {
            try
            {
                if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight)
                {
                    int currentX = Console.CursorLeft;
                    int currentY = Console.CursorTop;

                    gotoxy(x, y);

                    char result = ' ';

                    gotoxy(currentX, currentY);

                    return result;
                }
            }
            catch
            {
            }
            return ' ';
        }

        static void SetConsoleSize(int width, int height)
        {
            try
            {
                Console.SetWindowSize(width, height);
                Console.SetBufferSize(width, height);
            }
            catch
            {
            }
        }

        static Enemy[] enemies = new Enemy[3];
        static Coin[] coins = new Coin[5];
        static Bullet[] bullets = new Bullet[2];

        static int Px = 3, Py = 2;

        static int Score = 0;
        static int Coins = 0;
        static int Lives = 3;
        static bool GameActive = true;
        static int CurrentLevel = 1;

        static char[,] wallMap = new char[40, 120];
        static int mapWidth = 0;
        static int mapHeight = 0;

        static void LoadMap(int level)
        {
            string filename;

            if (level == 1)
                filename = "map1.txt";
            else if (level == 2)
                filename = "map2.txt";
            else if (level == 3)
                filename = "map3.txt";
            else
                filename = "map1.txt";

            try
            {
                string[] lines = File.ReadAllLines(filename);
                mapHeight = lines.Length;

                for (int y = 0; y < lines.Length; y++)
                {
                    string line = lines[y];
                    mapWidth = Math.Max(mapWidth, line.Length);

                    gotoxy(0, y);
                    color(11);

                    for (int x = 0; x < line.Length; x++)
                    {
                        Console.Write(line[x]);
                        wallMap[y, x] = line[x];
                    }
                    Console.WriteLine();
                }
            }
            catch (FileNotFoundException)
            {
                gotoxy(0, 0);
                color(12);
                Console.WriteLine($"ERROR: Could not load map file: {filename}");
                Console.WriteLine("Make sure map1.txt, map2.txt, map3.txt are in the same folder!");
                color(7);
                Thread.Sleep(3000);
            }
        }

        static void ResetGameForLevel(int level)
        {
            Px = 3;
            Py = 2;
            Lives = 3;
            GameActive = true;

            for (int i = 0; i < 3; i++)
                enemies[i].IsAlive = true;

            for (int i = 0; i < 2; i++)
                bullets[i].IsActive = false;

            if (level == 1)
            {
                enemies[0].X = 15; enemies[0].Y = 6; enemies[0].Direction = 1;
                enemies[1].X = 50; enemies[1].Y = 10; enemies[1].Direction = 1;
                enemies[2].X = 35; enemies[2].Y = 14; enemies[2].Direction = 1;

                coins[0].X = 20; coins[0].Y = 5;
                coins[1].X = 40; coins[1].Y = 5;
                coins[2].X = 60; coins[2].Y = 9;
                coins[3].X = 30; coins[3].Y = 17;
                coins[4].X = 50; coins[4].Y = 18;
            }
            else if (level == 2)
            {
                enemies[0].X = 20; enemies[0].Y = 8; enemies[0].Direction = 1;
                enemies[1].X = 45; enemies[1].Y = 12; enemies[1].Direction = 1;
                enemies[2].X = 60; enemies[2].Y = 16; enemies[2].Direction = 1;

                coins[0].X = 25; coins[0].Y = 3;
                coins[1].X = 45; coins[1].Y = 10;
                coins[2].X = 65; coins[2].Y = 14;
                coins[3].X = 4; coins[3].Y = 16;
                coins[4].X = 55; coins[4].Y = 19;
            }
            else if (level == 3)
            {
                enemies[0].X = 25; enemies[0].Y = 10; enemies[0].Direction = 1;
                enemies[1].X = 40; enemies[1].Y = 14; enemies[1].Direction = 1;
                enemies[2].X = 55; enemies[2].Y = 18; enemies[2].Direction = 1;

                coins[0].X = 30; coins[0].Y = 6;
                coins[1].X = 50; coins[1].Y = 10;
                coins[2].X = 70; coins[2].Y = 14;
                coins[3].X = 40; coins[3].Y = 18;
                coins[4].X = 60; coins[4].Y = 5;
            }

            for (int i = 0; i < 5; i++)
                coins[i].IsActive = true;
        }

        static void DrawEnemy(int x, int y)
        {
            gotoxy(x, y);
            color(4);
            Console.Write("(E)");
            gotoxy(x, y + 1);
            Console.Write("/ \\");
        }

        static void ClearEnemy(int x, int y)
        {
            gotoxy(x - 1, y);
            Console.Write("     ");
            gotoxy(x - 1, y + 1);
            Console.Write("     ");
        }

        static void Enemy1()
        {
            if (enemies[0].IsAlive)
            {
                DrawEnemy(enemies[0].X, enemies[0].Y);
                if (enemies[0].X >= 65)
                    enemies[0].Direction = -1;
                if (enemies[0].X <= 12)
                    enemies[0].Direction = 1;
                enemies[0].X = enemies[0].X + enemies[0].Direction;
            }
        }

        static void Enemy2()
        {
            if (enemies[1].IsAlive)
            {
                DrawEnemy(enemies[1].X, enemies[1].Y);
                if (enemies[1].X >= 67)
                    enemies[1].Direction = -1;
                if (enemies[1].X <= 12)
                    enemies[1].Direction = 1;
                enemies[1].X = enemies[1].X + enemies[1].Direction;
            }
        }

        static void Enemy3()
        {
            if (enemies[2].IsAlive)
            {
                DrawEnemy(enemies[2].X, enemies[2].Y);
                if (enemies[2].X >= 62)
                    enemies[2].Direction = -1;
                if (enemies[2].X <= 12)
                    enemies[2].Direction = 1;
                enemies[2].X = enemies[2].X + enemies[2].Direction;
            }
        }

        static void CheckEnemyHit()
        {
            for (int i = 0; i < 2; i++)
            {
                if (bullets[i].IsActive)
                {
                    if (enemies[0].IsAlive && bullets[i].X >= enemies[0].X && bullets[i].X <= enemies[0].X + 2 && bullets[i].Y == enemies[0].Y)
                    {
                        bullets[i].IsActive = false;
                        gotoxy(bullets[i].X, bullets[i].Y);
                        Console.Write(" ");

                        ClearEnemy(enemies[0].X, enemies[0].Y);
                        enemies[0].IsAlive = false;
                        Score += 100;

                        UpdateScoreBoard();
                    }

                    if (enemies[1].IsAlive && bullets[i].X >= enemies[1].X && bullets[i].X <= enemies[1].X + 2 && bullets[i].Y == enemies[1].Y)
                    {
                        bullets[i].IsActive = false;
                        gotoxy(bullets[i].X, bullets[i].Y);
                        Console.Write(" ");

                        ClearEnemy(enemies[1].X, enemies[1].Y);
                        enemies[1].IsAlive = false;
                        Score += 100;

                        UpdateScoreBoard();
                    }

                    if (enemies[2].IsAlive && bullets[i].X >= enemies[2].X && bullets[i].X <= enemies[2].X + 2 && bullets[i].Y == enemies[2].Y)
                    {
                        bullets[i].IsActive = false;
                        gotoxy(bullets[i].X, bullets[i].Y);
                        Console.Write(" ");

                        ClearEnemy(enemies[2].X, enemies[2].Y);
                        enemies[2].IsAlive = false;
                        Score += 100;

                        UpdateScoreBoard();
                    }
                }
            }
        }

        static void DisplayHeader()
        {
            gotoxy(42, 2);
            color(3);
            Console.WriteLine(" _____  _____ ______  ___  ___ _____ _   _   ___   _     ");
            gotoxy(42, 3);
            Console.WriteLine("|_   _||  ___|| ___ \\ |  \\/  | |_   _| \\ | | / _ \\ | |    ");
            gotoxy(42, 4);
            Console.WriteLine("  | |  | |__  | |_/ / | .  . |   | | |  \\| |/ /_\\ \\| |    ");
            gotoxy(42, 5);
            Console.WriteLine("  | |  |  __| |    /  | |\\/| |   | | | . ` ||  _  || |    ");
            gotoxy(42, 6);
            Console.WriteLine("  | |  | |___ | |\\ \\  | |  | |  _| |_| |\\  || | | || |____");
            gotoxy(42, 7);
            Console.WriteLine("  \\_/  \\____/ \\_| \\_| \\_|  |_/  \\___/\\_| \\_/\\_| |_/\\_____/");
            gotoxy(42, 8);
            Console.WriteLine();
            gotoxy(42, 9);
            color(10);
            Console.WriteLine("             _____  _      ___   _____  _   _              ");
            gotoxy(42, 10);
            Console.WriteLine("            /  __ \\| |    / _ \\ /  ___|| | | |             ");
            gotoxy(42, 11);
            Console.WriteLine("            | /  \\/| |   / /_\\ \\\\ `--. | |_| |             ");
            gotoxy(42, 12);
            Console.WriteLine("            | |    | |   |  _  | `--. \\|  _  |             ");
            gotoxy(42, 13);
            Console.WriteLine("            | \\__/\\| |___| | | |/\\__/ /| | | |             ");
            gotoxy(42, 14);
            Console.WriteLine("             \\____/\\_____/\\_| |_/\\____/ \\_| |_/             ");
        }

        static void UpdateScoreBoard()
        {
            gotoxy(93, 2);
            Console.Write("    ");
            gotoxy(93, 2);
            Console.Write(CurrentLevel);

            gotoxy(93, 4);
            Console.Write("      ");
            gotoxy(93, 4);
            Console.Write(Score);

            gotoxy(93, 6);
            Console.Write("    ");
            gotoxy(93, 6);
            Console.Write(Coins);

            gotoxy(93, 8);
            Console.Write("    ");
            gotoxy(93, 8);
            Console.Write(Lives);

            gotoxy(95, 10);
            Console.Write("    ");
            gotoxy(95, 10);
            int enemiesLeft = 0;
            if (enemies[0].IsAlive)
                enemiesLeft++;
            if (enemies[1].IsAlive)
                enemiesLeft++;
            if (enemies[2].IsAlive)
                enemiesLeft++;
            Console.Write(enemiesLeft);
        }

        static void DrawCoins()
        {
            for (int i = 0; i < 5; i++)
            {
                if (coins[i].IsActive)
                {
                    gotoxy(coins[i].X, coins[i].Y);
                    color(14);
                    Console.Write("(C)");
                    color(7);
                }
            }
        }

        static void CheckCoinCollection()
        {
            for (int i = 0; i < 5; i++)
            {
                if (coins[i].IsActive)
                {
                    if (Px == coins[i].X && Py == coins[i].Y)
                    {
                        coins[i].IsActive = false;
                        Coins++;
                        Score += 10;

                        gotoxy(coins[i].X, coins[i].Y);
                        Console.Write("   ");

                        UpdateScoreBoard();
                    }
                }
            }
        }

        static void Player()
        {
            color(10);
            gotoxy(Px, Py);
            Console.WriteLine(" (P)");
            gotoxy(Px, Py + 1);
            Console.WriteLine("</ \\>");
        }

        static void ClearPlayer()
        {
            gotoxy(Px, Py);
            Console.WriteLine("    ");
            gotoxy(Px, Py + 1);
            Console.WriteLine("     ");
        }

        static void CheckPlayerHit()
        {
            if (enemies[0].IsAlive && Px >= enemies[0].X && Px <= enemies[0].X + 2 && Py == enemies[0].Y)
            {
                Lives--;
                UpdateScoreBoard();

                ClearPlayer();
                Px = 3;
                Py = 2;
                Player();

                if (Lives <= 0)
                {
                    GameActive = false;
                }
            }

            if (enemies[1].IsAlive && Px >= enemies[1].X && Px <= enemies[1].X + 2 && Py == enemies[1].Y)
            {
                Lives--;
                UpdateScoreBoard();

                ClearPlayer();
                Px = 3;
                Py = 2;
                Player();

                if (Lives <= 0)
                {
                    GameActive = false;
                }
            }

            if (enemies[2].IsAlive && Px >= enemies[2].X && Px <= enemies[2].X + 2 && Py == enemies[2].Y)
            {
                Lives--;
                UpdateScoreBoard();

                ClearPlayer();
                Px = 3;
                Py = 2;
                Player();

                if (Lives <= 0)
                {
                    GameActive = false;
                }
            }
        }

        static void Shoot()
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Spacebar)
                {
                    bullets[0].X = Px + 4;
                    bullets[0].Y = Py + 1;
                    bullets[0].IsActive = true;
                    bullets[0].Direction = 1;

                    bullets[1].X = Px;
                    bullets[1].Y = Py + 1;
                    bullets[1].IsActive = true;
                    bullets[1].Direction = -1;
                }
            }
        }

        static void UpdateBullets()
        {
            for (int i = 0; i < 2; i++)
            {
                if (bullets[i].IsActive)
                {
                    gotoxy(bullets[i].X, bullets[i].Y);
                    Console.Write(" ");

                    bullets[i].X += bullets[i].Direction;

                    bool hitWall = false;

                    if (bullets[i].Y >= 0 && bullets[i].Y < mapHeight && bullets[i].X >= 0 && bullets[i].X < mapWidth)
                    {
                        char ch = wallMap[bullets[i].Y, bullets[i].X];
                        if (ch == '=' || ch == '|')
                        {
                            hitWall = true;
                        }
                    }

                    if (bullets[i].X < 2 || bullets[i].X > 78 || hitWall)
                    {
                        bullets[i].IsActive = false;
                    }
                    else
                    {
                        gotoxy(bullets[i].X, bullets[i].Y);
                        color(3);
                        Console.Write("-");
                        color(7);
                    }
                }
            }
        }

        static bool Menu()
        {
            int Select = 0;
            int PrevSelect = -1;
            int SelectX = 14, SelectY = 4;

            while (true)
            {
                if (PrevSelect != Select)
                {
                    Console.Clear();
                    DisplayHeader();
                    color(7);
                    string[] Option = { "Start Game", "Level Select", "Quit", "Ahmad Hassan On TOP!!" };
                    int TempYforLoop = 7;
                    for (int i = 0; i < 4; i++)
                    {
                        gotoxy(15, TempYforLoop);
                        Console.Write(Option[i]);
                        TempYforLoop = TempYforLoop + 3;
                    }

                    gotoxy(10, 20);
                    color(7);
                    Console.Write("Use ");
                    color(2);
                    Console.Write("[Arrow Keys] ");
                    color(7);
                    Console.Write("or ");
                    color(2);
                    Console.Write("[W] [A]");
                    color(7);
                    Console.Write(" to Move ");
                    color(2);
                    Console.Write("[Up]");
                    color(7);
                    Console.Write(" & ");
                    color(2);
                    Console.Write("[Down] ");
                    color(7);
                    Console.Write("Respectively, then hit ");
                    color(4);
                    Console.Write("[ENTER]");
                    color(7);
                    Console.Write("!");

                    SelectY = 6 + Select * 3;
                    gotoxy(SelectX, SelectY);
                    for (int i = 0; i < 3; i++)
                    {
                        color(2);
                        if (i == 0 || i == 2)
                            for (int j = 0; j < Option[Select].Length + 2; j++)
                            {
                                Console.Write("#");
                            }
                        if (i == 1)
                        {
                            gotoxy(SelectX - 1, SelectY + 1);
                            Console.Write("| ");
                            color(4);
                            Console.Write(Option[Select]);
                            color(2);
                            Console.Write(" |");
                        }
                        gotoxy(SelectX, SelectY + 2);
                    }
                    PrevSelect = Select;
                    color(7);
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.S)
                    {
                        Select++;
                        if (Select > 3)
                            Select = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.W)
                    {
                        Select--;
                        if (Select < 0)
                            Select = 3;
                    }

                    if (key.Key == ConsoleKey.Enter)
                    {
                        if (Select == 0)
                        {
                            Console.Clear();
                            gotoxy(42, 12);
                            color(2);
                            Console.Write("Starting Level 1...");
                            color(7);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Game(1);
                            PrevSelect = -1;
                            continue;
                        }
                        else if (Select == 1)
                        {
                            int levelSelect = 1;
                            int prevLevelSelect = -1;
                            bool selecting = true;

                            while (selecting)
                            {
                                if (prevLevelSelect != levelSelect)
                                {
                                    Console.Clear();
                                    gotoxy(35, 5);
                                    color(14);
                                    Console.WriteLine("=== SELECT LEVEL ===");
                                    color(7);

                                    for (int i = 1; i <= 3; i++)
                                    {
                                        gotoxy(40, 8 + i * 2);
                                        if (i == levelSelect)
                                        {
                                            color(10);
                                            Console.Write($"> LEVEL {i} <");
                                        }
                                        else
                                        {
                                            color(7);
                                            Console.Write($"  LEVEL {i}  ");
                                        }
                                    }

                                    gotoxy(27, 18);
                                    color(11);
                                    Console.Write("Use [W]/[S] OR [Up]/[Down] to navigate");
                                    gotoxy(27, 20);
                                    color(10);
                                    Console.Write("        [ENTER] to select");
                                    gotoxy(27, 22);
                                    color(4);
                                    Console.Write("         [ESC] to go back");

                                    prevLevelSelect = levelSelect;
                                }

                                if (Console.KeyAvailable)
                                {
                                    var levelKey = Console.ReadKey(true);

                                    if (levelKey.Key == ConsoleKey.W || levelKey.Key == ConsoleKey.UpArrow)
                                    {
                                        levelSelect--;
                                        if (levelSelect < 1)
                                            levelSelect = 3;
                                    }
                                    if (levelKey.Key == ConsoleKey.S || levelKey.Key == ConsoleKey.DownArrow)
                                    {
                                        levelSelect++;
                                        if (levelSelect > 3)
                                            levelSelect = 1;
                                    }

                                    if (levelKey.Key == ConsoleKey.Enter)
                                    {
                                        Console.Clear();
                                        gotoxy(40, 12);
                                        color(2);
                                        Console.Write($"Starting Level {levelSelect}...");
                                        color(7);
                                        Thread.Sleep(1000);
                                        Console.Clear();
                                        Game(levelSelect);
                                        selecting = false;
                                        Console.Clear();
                                        gotoxy(40, 12);
                                        color(4);
                                        Console.Write("Game Over! ");
                                        Thread.Sleep(2000);
                                    }

                                    if (levelKey.Key == ConsoleKey.Escape)
                                    {
                                        selecting = false;
                                    }
                                }

                                Thread.Sleep(50);
                            }
                            PrevSelect = -1;
                        }
                        else if (Select == 2)
                        {
                            return true;
                        }
                    }
                }

                Thread.Sleep(100);
            }
        }

        static bool Game(int level)
        {
            try
            {
                SetConsoleSize(120, 40);
            }
            catch
            {
            }

            cursorVisible(false);
            CurrentLevel = level;

            ResetGameForLevel(level);

            gotoxy(0, 0);
            LoadMap(level);
            ScoreBoard();
            Instructions();

            DrawCoins();
            Player();

            while (GameActive)
            {
                if (enemies[0].IsAlive)
                {
                    ClearEnemy(enemies[0].X - enemies[0].Direction, enemies[0].Y);
                    Enemy1();
                }
                if (enemies[1].IsAlive)
                {
                    ClearEnemy(enemies[1].X - enemies[1].Direction, enemies[1].Y);
                    Enemy2();
                }
                if (enemies[2].IsAlive)
                {
                    ClearEnemy(enemies[2].X - enemies[2].Direction, enemies[2].Y);
                    Enemy3();
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);

                    if ((key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow) && CanMoveTo("Up") != "NoUp")
                    {
                        ClearPlayer();
                        Py--;
                        Player();
                    }
                    if ((key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) && CanMoveTo("Left") != "NoLeft")
                    {
                        ClearPlayer();
                        Px--;
                        Player();
                    }
                    if ((key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) && CanMoveTo("Down") != "NoDown")
                    {
                        ClearPlayer();
                        Py++;
                        Player();
                    }
                    if ((key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) && CanMoveTo("Right") != "NoRight")
                    {
                        ClearPlayer();
                        Px++;
                        Player();
                    }

                    if (key.Key == ConsoleKey.Spacebar)
                    {
                        bullets[0].X = Px + 4;
                        bullets[0].Y = Py + 1;
                        bullets[0].IsActive = true;
                        bullets[0].Direction = 1;

                        bullets[1].X = Px;
                        bullets[1].Y = Py + 1;
                        bullets[1].IsActive = true;
                        bullets[1].Direction = -1;
                    }

                    if (key.Key == ConsoleKey.Escape)
                        GameActive = false;
                }

                UpdateBullets();
                CheckEnemyHit();
                CheckCoinCollection();
                CheckPlayerHit();

                Thread.Sleep(30);

                if (!enemies[0].IsAlive && !enemies[1].IsAlive && !enemies[2].IsAlive)
                {
                    GameActive = false;

                    if (level < 3)
                    {
                        gotoxy(33, 25);
                        color(10);
                        Console.Write($"LEVEL {level} COMPLETE! Loading next level...");
                        color(7);
                        Thread.Sleep(2000);

                        Console.Clear();
                        Game(level + 1);
                        return true;
                    }
                    else
                    {
                        gotoxy(33, 25);
                        color(10);
                        Console.Write($"CONGRATULATIONS! You beat all levels! Final Score: {Score}");
                        color(7);
                        Thread.Sleep(3000);
                    }
                }

                Thread.Sleep(20);
            }

            if (Lives <= 0)
            {
                gotoxy(33, 25);
                color(12);
                Console.Write($"GAME OVER! Level {level} Failed! Score: {Score}");
                color(7);
                Thread.Sleep(2500);
            }

            return true;
        }

        static void ScoreBoard()
        {
            gotoxy(85, 0);
            color(11);
            Console.Write("[Statistics]");
            gotoxy(85, 2);
            color(14);
            Console.Write("Level : ");
            color(7);
            Console.Write(CurrentLevel);
            gotoxy(85, 4);
            color(13);
            Console.Write("Score : ");
            color(7);
            Console.Write(Score);
            gotoxy(85, 6);
            color(6);
            Console.Write("Coins : ");
            color(7);
            Console.Write(Coins);
            color(2);
            gotoxy(85, 8);
            Console.Write("Lives : ");
            color(7);
            Console.Write(Lives);

            gotoxy(85, 10);
            color(4);
            Console.Write("Enemies : ");
            color(7);
            int enemiesLeft = 0;
            if (enemies[0].IsAlive) enemiesLeft++;
            if (enemies[1].IsAlive) enemiesLeft++;
            if (enemies[2].IsAlive) enemiesLeft++;
            Console.Write($"{enemiesLeft}/3");
        }

        static void Instructions()
        {
            gotoxy(85, 13);
            color(11);
            Console.Write("[Instructions]");
            gotoxy(85, 15);
            color(6);
            Console.Write("Coins = ");
            color(7);
            Console.Write("10 Score");
            gotoxy(85, 16);
            color(6);
            Console.Write("Kill = ");
            color(7);
            Console.Write("100 Score");

            gotoxy(85, 18);
            color(11);
            Console.Write("[Controls]");
            gotoxy(85, 20);
            color(7);
            Console.Write("Movement = ");
            color(9);
            Console.Write("[W] [A] [S] [D]");

            gotoxy(85, 22);
            color(7);
            Console.Write("Shoot = ");
            color(10);
            Console.Write("[Space]");

            gotoxy(85, 24);
            color(7);
            Console.Write("Exit = ");
            color(12);
            Console.Write("[ESC]");
        }

        static string CanMoveTo(string Direction)
        {
            if (Direction == "Left")
            {
                if (Py >= 0 && Py < mapHeight && Px - 1 >= 0 && Px - 1 < mapWidth)
                {
                    if (wallMap[Py, Px - 1] == '|' || wallMap[Py, Px - 1] == '=')
                        return "NoLeft";
                }
                if (Py + 1 >= 0 && Py + 1 < mapHeight && Px - 1 >= 0 && Px - 1 < mapWidth)
                {
                    if (wallMap[Py + 1, Px - 1] == '|' || wallMap[Py + 1, Px - 1] == '=')
                        return "NoLeft";
                }
            }
            if (Direction == "Right")
            {
                if (Py >= 0 && Py < mapHeight && Px + 5 >= 0 && Px + 5 < mapWidth)
                {
                    if (wallMap[Py, Px + 5] == '|' || wallMap[Py, Px + 5] == '=')
                        return "NoRight";
                }
                if (Py + 1 >= 0 && Py + 1 < mapHeight && Px + 5 >= 0 && Px + 5 < mapWidth)
                {
                    if (wallMap[Py + 1, Px + 5] == '|' || wallMap[Py + 1, Px + 5] == '=')
                        return "NoRight";
                }
            }
            if (Direction == "Up")
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Py - 1 >= 0 && Py - 1 < mapHeight && Px + i >= 0 && Px + i < mapWidth)
                    {
                        if (wallMap[Py - 1, Px + i] == '=')
                            return "NoUp";
                    }
                }
            }
            if (Direction == "Down")
            {
                for (int i = 0; i < 6; i++)
                {
                    if (Py + 2 >= 0 && Py + 2 < mapHeight && Px + i >= 0 && Px + i < mapWidth)
                    {
                        if (wallMap[Py + 2, Px + i] == '=')
                            return "NoDown";
                    }
                }
            }

            return "NoProblem";
        }

        static void Main(string[] args)
        {
            Console.Clear();
            cursorVisible(false);

            if (Menu())
                return;
        }
    }
}