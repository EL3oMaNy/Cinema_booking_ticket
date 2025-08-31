using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class Program
    {
        public static List<Person> users = new List<Person>();
        public static string filePath = "Movie.txt";

        public static void Main(string[] args)
        {
            // مستخدمين ثابتين
            users.Add(new Person("Admin User", "admin", "1234", "Admin"));
            users.Add(new Person("Customer User", "user", "0000", "Customer"));

            bool isValid = true;
            Person loggedUser = null;
            string role = null;

            while (isValid)
            {
                Console.Clear();
                Console.WriteLine("=== Cinema Ticket Booking System ===");

                // تسجيل الدخول
                Console.Write("Enter username: ");
                string user = Console.ReadLine();

                Console.Write("Enter password: ");
                string pass = Console.ReadLine();

                role = null;
                loggedUser = null;

                foreach (var p in users)
                {
                    string r = p.Login(user, pass);
                    if (r != null)
                    {
                        role = r;
                        loggedUser = p;
                    }
                }

                if (role == "Admin")
                {
                    AdminMenu();
                }
                else if (role == "Customer")
                {
                    CustomerMenu();
                }
                else
                {
                    Console.WriteLine("Invalid login.");
                    justEnter();
                    continue;
                }
            }
        }

        // قائمة الأدمن
        public static void AdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Admin Menu ---");
                Console.WriteLine("1. Add Movie");
                Console.WriteLine("2. Delete Movie");
                Console.WriteLine("3. View All Movies & Seats");
                Console.WriteLine("4. Logout");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddMovie();
                        break;
                    case "2":
                        DeleteMovie();
                        break;
                    case "3":
                        ViewMovies();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        justEnter();
                        break;
                }
            }
        }

        // قائمة المستخدم
        public static void CustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Customer Menu ---");
                Console.WriteLine("1. View Movies");
                Console.WriteLine("2. Book Seat");
                Console.WriteLine("3. Logout");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewMovies();
                        break;
                    case "2":
                        Movie.BookMovieSeat();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        justEnter();
                        break;
                }
            }
        }

        // إضافة فيلم جديد
        public static void AddMovie()
        {
            string title = null;
            int duration = 0;
            int seatCount = 0;
            Console.Clear();
            Console.Write("Enter movie title: ");
            title = Console.ReadLine();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Enter movie title: {title}");

                Console.Write("Enter duration (minutes): ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out duration) || duration <= 0)
                {
                    Console.WriteLine("Please enter a positive number");
                    justEnter();
                    continue;
                }
                break;

            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Enter movie title: {title}");
                Console.WriteLine($"Enter duration (minutes): {duration}");
                Console.Write("Enter number of seats: ");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out seatCount) || seatCount <= 0)
                {
                    Console.WriteLine("Please enter a positive number");
                    justEnter();
                    continue;
                }
                break;
            }

            string seatsData = string.Join(",", new string[seatCount].Select(_ => "0"));
            File.AppendAllText(filePath, $"{title}|{duration}|{seatsData}\n");

            Console.WriteLine("Movie added successfully!");
            justEnter();
        }

        // عرض كل الأفلام والمقاعد المحجوزة
        public static void ViewMovies()
        {
            Console.Clear();
            Console.WriteLine("--- Movies List ---");

            if (!File.Exists(filePath))
            {
                Console.WriteLine(" No movies found!");
                justEnter();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                string title = parts[0];
                int duration = int.Parse(parts[1]);
                string[] seatsData = parts[2].Split(',');

                Console.WriteLine($"{i + 1}. {title} ({duration} mins)");

                for (int s = 0; s < seatsData.Length; s++)
                {
                    bool booked = seatsData[s] == "1";
                    Console.ForegroundColor = booked ? ConsoleColor.DarkRed : ConsoleColor.Green;
                    Console.Write($"Seat {s + 1}: {(booked ? "Booked" : "Available")} | ");
                    if ((s + 1) % 5 == 0) Console.WriteLine();
                }
                Console.ResetColor();
                Console.WriteLine("\n");
            }
            justEnter();
        }

        // حجز مقعد 
        
        public static void justEnter()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Please Enter any key to continue...");
            Console.ResetColor();
            Console.ReadKey();
        }
        // مسح فيلم 
        public static void DeleteMovie()
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(" No movies found!");
                justEnter();
                return;
            }

            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                Console.WriteLine(" No movies available to delete!");
                justEnter();
                return;
            }

            Console.Clear();
            Console.WriteLine("--- Delete Movie ---");

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split('|');
                Console.WriteLine($"{i + 1}. {parts[0]} ({parts[1]} mins)");
            }

            int choice = 0;
            while (true)
            {
                Console.Write("Enter movie number to delete: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out choice) || choice < 1 || choice > lines.Length)
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }
                break;
            }

            // تأكيد
            Console.Write($"Are you sure you want to delete \"{lines[choice - 1].Split('|')[0]}\"? (y/n): ");
            string confirm = Console.ReadLine()?.ToLower();
            if (confirm != "y")
            {
                Console.WriteLine(" Deletion cancelled.");
                justEnter();
                return;
            }

            List<string> updated = lines.ToList();
            updated.RemoveAt(choice - 1);
            File.WriteAllLines(filePath, updated);

            Console.WriteLine(" Movie deleted successfully!");
            justEnter();
        }
    }
}