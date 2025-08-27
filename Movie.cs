using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Movie
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

        public void displaySeats()
        {
            Console.WriteLine("\nseats for " + title);
            for (int i = 0; i < seats.Length; i++)
            {
                if (seats[i])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("seat" + (i + 1) + ": booked");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("seat" + (i + 1) + ": available");
                    Console.ResetColor();
                }
            }
        }

        public void bookSeat(int seatNumber)
        {
            if (seatNumber < 1 || seatNumber > seats.Length)
            {
                Console.WriteLine("Seat number not valid.");
                return;
            }

            if (seats[seatNumber - 1])
            {
                Console.WriteLine("Seat " + seatNumber + " already booked.");
            }
            else
            {
                seats[seatNumber - 1] = true;
                Console.WriteLine("Seat " + seatNumber + " booked.");
            }
        }
    }
}
