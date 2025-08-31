using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Movie
    {
        public string Title { get; set; }
        public int Duration { get; set; }
        public bool[] Seats { get; set; }

        public static void BookMovieSeat()
        {
            if (!File.Exists(Program.filePath))
            {
                Console.WriteLine(" No movies found!");
                Program.justEnter();
                return;
            }

            string[] lines = File.ReadAllLines(Program.filePath);
            int choice = 0;
            while (true)
            {
                Console.Clear();
                Program.ViewMovies();
                Console.Write("Select movie number: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out choice) || choice < 1 || choice > lines.Length)
                {
                    Console.WriteLine("Please enter a valid input");
                    Program.justEnter();
                    continue;
                }
                break;
            }

            string[] parts = lines[choice - 1].Split('|');
            string title = parts[0];
            int duration = int.Parse(parts[1]);
            string[] seatsData = parts[2].Split(',');
            int seatNum = 0;
            while (true)
            {
                Console.Clear();
                Program.ViewMovies();
                Console.WriteLine($"Select movie number: {choice}");
                Console.Write("Enter seat number to book: ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out seatNum) || seatNum < 1 || seatNum > seatsData.Length)
                {
                    Console.WriteLine("Invalid seat number.");
                    Program.justEnter();
                    continue;
                }

                if (seatsData[seatNum - 1] == "1")
                {
                    Console.WriteLine("Seat already booked.");
                    Program.justEnter();
                    continue;
                }
                break;
            }
            seatsData[seatNum - 1] = "1";
            lines[choice - 1] = $"{title}|{duration}|{string.Join(",", seatsData)}";
            File.WriteAllLines(Program.filePath, lines);

            Console.WriteLine($"Seat {seatNum} booked successfully!");
            Program.justEnter();
        }
    }
}
