using System;
using System.Collections.Generic;
using System.Linq;

class Movie
{
    public string title;
    public int duration;
    public bool[] seats;

    public Movie(string t, int d, int numSeats)
    {
        title = t;
        duration = d;
        seats = new bool[numSeats];
    }

    public void ShowSeats()
    {
        Console.WriteLine("\nseats for " + title);
        for (int i = 0; i < seats.Length; i++)
        {
            if (seats[i])
                Console.WriteLine("seat" + (i + 1) + ": booked");
            else
                Console.WriteLine("seat" + (i + 1) + ": available");
        }
    }

    public void BookSeat(int seatNum)  ////تم الاستعانة بالذكاء الاصطناعي في هذا الجزء
    {
        if (seatNum < 1 || seatNum > seats.Length)
        {
            Console.WriteLine("Seat number not valid.");
            return;
        }


        if (!seats[seatNum - 1])
        {
            seats[seatNum - 1] = true;
            Console.WriteLine("Seat " + seatNum + " booked.");
        }
        else
        {
            Console.WriteLine("Seat " + seatNum + " already booked.");
        }
    }
}

public class Person
{
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }


    public string Role { get; set; }


}

class Program
{
    static List<Person> users = new List<Person>();
    static List<Movie> movies = new List<Movie>();

    static void CreateAcc()
    {
        Console.Clear();
        Console.Write("Enter name: ");
        string name = Console.ReadLine().Trim();
        if (name == null || name.Length < 3)
        {
            Console.WriteLine("Please Enter Valid Name");
            Console.ReadKey();
            name = null;
            CreateAcc();
        }
    username:
        Console.Clear();
        Console.WriteLine("Enter name: " + name);
        Console.Write("Enter username: ");
        string username = Console.ReadLine().Trim();
        if (username == null || username.Length < 3 || username.Length > 15)
        {
            Console.WriteLine("Please Enter Valid username");
            Console.ReadKey();
            username = null;
            goto username;
        }

        foreach (var user in users)
        {
            if (user.Username == username)
            {
                Console.WriteLine("username already exists please try again");
                Console.ReadLine();
                return;
            }
        }

        Console.Write("enter password: ");
        string password = Console.ReadLine();
        if (password == null)
            Console.WriteLine("Password can't be null");
        else if (password.Length < 5)
            Console.WriteLine("Password ");
        string role;
        while (true)
        {
            Console.Write("Enter Role admin or customer :- ");
            role = Console.ReadLine();

            if (role.ToLower() == "admin" || role.ToLower() == "customer")
                break;

            Console.WriteLine("Invalid role!!! please type admin or customer.");
        }

        users.Add(new Person
        {
            Name = name,
            Username = username,
            Password = password,
            Role = role
        });

        Console.WriteLine("Account creation success! Press enter please.");
        Console.ReadLine();
    }
    static string CenterText(string text)
    {
        int padding = (Console.WindowWidth - text.Length) / 2;
        return new string(' ', Math.Max(padding, 0)) + text;
    }
    static void Login()
    {
        Person foundUser = null;
        if (users.Count == 0)
        {
            Console.WriteLine(CenterText("Please Create acc "));
            Console.ReadKey();
            Main();
        }
        int count = 0;
        while (true)
        {
            Console.Clear();
            Console.Write("Enter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();


            foreach (var user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    foundUser = user;
                    break;
                }
            }
            if (count == 3)
            {
                Console.WriteLine("you don't have any tries");
                Console.ReadKey();
                Main();
            }
            if (foundUser != null)
            {
                Console.WriteLine($"You are logged in as {foundUser.Role}.");
                if (foundUser.Role.ToLower() == "admin")
                {
                    AdminMenu(foundUser);
                }
                else
                {
                    CustomerMenu(foundUser);
                }
                return;
            }
            else
            {
                Console.WriteLine("Invalid username or password Press enter to try again");
                Console.WriteLine(" you have " + (3 - count) + " Tries ");
                count++;
                Console.ReadKey();
            }
        }
    }

    static void AdminMenu(Person admin)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("###########Admin menu##############");
            Console.WriteLine("1. Add Movie");


            Console.WriteLine("2. View All Bookings");
            Console.WriteLine("3. CustomerMenu");
            Console.WriteLine("4. Logout");
            
            Console.Write("Choose: ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    AddMovie();
                    break;
                case "2":
                    ViewAllBookings(); 
                    break;
                case "3":
                    CustomerMenu(admin); 
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void AddMovie()
    {
        Console.Write("Enter movie title:- ");
        string title = Console.ReadLine();

        Console.Write("Enter duration (minutes): ");  ///تم استخدام الذكاء الاصطناعي في هذا الجزء 
        if (int.TryParse(Console.ReadLine(), out int duration))
        {
            Console.Write("Enter number of seats: ");
            if (int.TryParse(Console.ReadLine(), out int seatCount))
            {
                Movie m = new Movie(title, duration, seatCount);
                movies.Add(m);
                Console.WriteLine("movie added successfully!");
            }
            else Console.WriteLine("Invalid seat number!!!!");
        }
        else Console.WriteLine("Invalid duration!!!!!!!");
        Console.ReadLine();
    }

    static void ViewAllBookings()
    {
        Console.WriteLine("All bookings:-");
        foreach (var m in movies)
        {
            m.ShowSeats();
        }
        Console.WriteLine("Press enter to continue");
        Console.ReadLine();
    }

    static void CustomerMenu(Person customer)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("#####customer  menu#######3#");
            Console.WriteLine("1 choose movie");
            Console.WriteLine("2 logout");
            Console.Write(" choose: ");
            string choice = Console.ReadLine();

            if (choice == "1") ChooseMovie();

            else if (choice == "2")

                return;
            else Console.WriteLine("Invalid choice!!!!");
        }
    }

    static void ChooseMovie()
    {
        if (movies.Count == 0)
        {
            Console.WriteLine("No movies available!!!");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("available Movies:- ");
        for (int i = 0; i < movies.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {movies[i].title} ({movies[i].duration} mins)");
        }

        Console.Write("choose movie number: ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= movies.Count) //تم الاستعانة بالذكاء الاصطناعي في هذا الجزء
        {
            Movie selected = movies[choice - 1];
            selected.ShowSeats();

            Console.Write("Enter seat number to book: ");
            if (int.TryParse(Console.ReadLine(), out int seatNum))
            {
                selected.BookSeat(seatNum);
            }
        }
        else Console.WriteLine("Invalid choice!");
        Console.ReadLine();
    }
    static void shwoScreen()
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(CenterText("\n\n\n\n\t\t\t\t    ░█████╗░██╗███╗░░██╗███████╗███╗░░░███╗░█████╗░"));
        Console.WriteLine(CenterText("██╔══██╗██║████╗░██║██╔════╝████╗░████║██╔══██╗"));
        Console.WriteLine(CenterText("██║░░╚═╝██║██╔██╗██║█████╗░░██╔████╔██║███████║"));
        Console.WriteLine(CenterText("██║░░██╗██║██║╚████║██╔══╝░░██║╚██╔╝██║██╔══██║"));
        Console.WriteLine(CenterText("╚█████╔╝██║██║░╚███║███████╗██║░╚═╝░██║██║░░██║"));
        Console.WriteLine(CenterText("░╚════╝░╚═╝╚═╝░░╚══╝╚══════╝╚═╝░░░░░╚═╝╚═╝░░╚═╝"));
        Thread.Sleep(2500);
        Console.ResetColor();
    }
    static void Main()
    {
        shwoScreen();
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.WriteLine(CenterText("#####Cinema ticket booking#########"));
            Console.ResetColor();
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAcc();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    Console.WriteLine("Logged out successfully!");
                    return;
                default:
                    Console.WriteLine("Invalid choice! please try again");
                    break;
            }
        }
    }
}