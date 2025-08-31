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
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }

        public Person(string name, string username, string password, string role)
        {
            Name = name;
            Username = username;
            Password = password;
            Role = role;
        }
        public string Login(string username, string password)
        {
            if (Username == username && Password == password)
            {
                return Role;
            }
            return null!;
        }

    }
}
