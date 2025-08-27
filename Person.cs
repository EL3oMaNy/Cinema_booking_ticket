using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Person
    {
        public string? name { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? role { get; set; }

    public static void CreateAcc()
        {
            //############ Input Name + Validation ############
            bool isValid = true;
            Console.Clear();
            Console.Write("Enter Full Name: ");


            string name = Console.ReadLine()!.Trim();
            string[] nameParts = name.Split(' ');
            string Name = "";
            foreach (string part in nameParts)
            {
                if (part != "")
                {
                    Name += part + " ";
                }
            }
            nameParts = Name.Trim().Split(" ");
            if (string.IsNullOrEmpty(Name))
            {
                Console.WriteLine("Cannot be null");
                Thread.Sleep(1000);
                Person.CreateAcc();
            }
            else if (nameParts.Length < 2)
            {
                Console.WriteLine("Enter at least two names");
                Thread.Sleep(1000);
                name = " ";
                Person.CreateAcc();
            }
            //############ Input Username + Validation ############
            string username = " ";
            while (isValid)
            {
                Console.Clear();
                Console.WriteLine("Enter Full Name: " + name);
                Console.Write("Enter username: ");


                username = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(username))
                {
                    Console.WriteLine("Cannot be null");
                    Thread.Sleep(1000);
                }
                else if (username.Length < 5)
                {
                    Console.WriteLine("Must be greater than 5 characters");
                    Thread.Sleep(1000);
                    username = " ";
                }
                else if (username.Contains(' '))
                {
                    Console.WriteLine("Enter username without spaces");
                    Thread.Sleep(1000);
                    username = " ";
                }
                else
                {
                    isValid = false;
                }
                foreach (var user in Program.users)
                {
                    if (user.username == username)
                    {
                        Console.WriteLine("username already exists please try again");
                        Thread.Sleep(1000);
                        isValid = true;
                    }
                }
            }
            //############ Input Password + Validation ############
            string password = " ";
            isValid = true;
            while (isValid)
            {
                Console.Clear();
                Console.WriteLine("Enter Full Name: " + name);
                Console.WriteLine("Enter username: " + username);
                Console.Write("Enter password: ");


                password = Console.ReadLine()!.Trim();
                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Cannot be null");
                    Thread.Sleep(1000);
                }
                else if (password.Length < 8)
                {
                    Console.WriteLine("At Least 8 characters");
                    Thread.Sleep(1000);
                    password = " ";
                }
                else if (!Regex.IsMatch(password, @"[A-Z]"))
                {
                    Console.WriteLine("Password must contain at least one capital letter");
                    Thread.Sleep(1000);
                    password = " ";
                }
                else if (!Regex.IsMatch(password, @"[a-z]"))
                {
                    Console.WriteLine("Password must contain at least one lowercase letter");
                    Thread.Sleep(1000);
                    password = " ";
                }
                else if (!Regex.IsMatch(password, @"[0-9]"))
                {
                    Console.WriteLine("Password must contain at least oun number");
                    Thread.Sleep(1000);
                    password = " ";
                }
                else
                {
                    isValid = false;
                }
            }
            string role = " ";
            isValid = true;
            while (isValid)
            {
                Console.Clear();

                Console.Write("Enter Role admin or customer :- ");
                role = Console.ReadLine()!;

                if (role.ToLower() == "admin" || role.ToLower() == "customer")
                {
                    isValid = false;
                }
                else
                {
                    Console.WriteLine("Invalid role!!! please type admin or customer.");
                    Thread.Sleep(1000);
                }
            }

            Program.users.Add(new Person
            {
                name = name,
                username = username,
                password = password,
                role = role
            });

            Console.WriteLine("Account creation success! Press enter please.");
            Console.ReadKey();
            Program.Main();
        }
        public static void Login()
        {
            Person foundUser = null!;
            if (Program.users.Count == 0)
            {
                Console.WriteLine("Please Create acc ");
                Thread.Sleep(750);
                Program.Main();
            }
            int count = 3;
            while (true)
            {
                Console.Clear();
                Console.Write("Enter username: ");
                string username = Console.ReadLine()!;

                Console.Write("Enter password: ");
                string password = Console.ReadLine()!;


                foreach (var user in Program.users)
                {
                    if (user.username == username && user.password == password)
                    {
                        foundUser = user;
                        break;
                    }
                }


                if (foundUser != null)
                {
                    Console.WriteLine($"You are logged in as {foundUser.role}.");
                    if (foundUser.role!.ToLower() == "admin")
                    {
                        Program.AdminMenu(foundUser);
                    }
                    else
                    {
                        Program.CustomerMenu(foundUser);
                    }
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid username or password try again");
                    Console.WriteLine(" you have " + (count) + " Tries ");
                    count--;
                    Console.ReadKey();
                }


                if (count == 0)
                {
                    Console.WriteLine("you don't have any tries");
                    Thread.Sleep(1000);
                    Program.Main();
                }
            }
        }

    }
}
