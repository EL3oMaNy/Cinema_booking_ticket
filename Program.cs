using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    public class Program
    {
        public static string filePath = "Movie.txt";
        public static List<Person> users = new List<Person>();
        public static List<Movie> movies = new List<Movie>();

        public static void AdminMenu(Person admin)
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
                string choice = Console.ReadLine()!;
                switch (choice)
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

        public static void AddMovie()
        {
            Console.Write("Enter movie title:- ");
            string title = Console.ReadLine()!;

            Console.Write("Enter duration (minutes): ");
            if (int.TryParse(Console.ReadLine(), out int duration))
            {
                Console.Write("Enter number of seats: ");
                if (int.TryParse(Console.ReadLine(), out int seatCount))
                {
                    Movie m = new Movie(title, duration, seatCount);
                    File.AppendAllText(filePath,$"Name:{title}   |duration:{duration} minutes   |numOfSeats:{seatCount}" + Environment.NewLine);

                    Console.WriteLine("movie added successfully!");
                }
                else Console.WriteLine("Invalid seat number!!!!");
            }
            else Console.WriteLine("Invalid duration!!!!!!");
            Console.ReadLine();
        }

        public static void ViewAllBookings()
        {
            Console.WriteLine("All bookings:-");
            foreach (var m in movies)
            {
                m.displaySeats();
            }
            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }

        public static void CustomerMenu(Person customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("#####customer  menu#######3#");
                Console.WriteLine("1 choose movie");
                Console.WriteLine("2 logout");
                Console.Write(" choose: ");
                string choice = Console.ReadLine()!;

                if (choice == "1") ChooseMovie();

                else if (choice == "2")

                    return;
                else Console.WriteLine("Invalid choice!!!!");
            }
        }

        public static void ChooseMovie()
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
                selected.displaySeats();

                Console.Write("Enter seat number to book: ");
                if (int.TryParse(Console.ReadLine(), out int seatNum))
                {
                    selected.bookSeat(seatNum);
                }
            }
            else Console.WriteLine("Invalid choice!");
            Console.ReadLine();
        }
        public static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.Clear();
                Console.WriteLine("#####Cinema ticket booking#########");
                Console.ResetColor();
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose: ");
                string choice = Console.ReadLine()!;

                switch (choice)
                {
                    case "1":
                        Person.CreateAcc();
                        break;
                    case "2":
                        Person.Login();
                        break;
                    case "3":
                        Console.WriteLine("Logged out successfully!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice! please try again");
                        Thread.Sleep(750);
                        break;
                }
            }
        }
    }
}