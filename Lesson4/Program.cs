// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        public static void Main()
        {
            TimeInterval t1 = new TimeInterval(9, 30, 11, 15);
            TimeInterval t2 = new TimeInterval("09:30-11:15");
            TimeInterval t3 = new TimeInterval("11:20-11:30");

            Console.WriteLine($"t1: {t1}");
            Console.WriteLine($"t2: {t2}");

            Console.WriteLine("------------------------------------------------");

            Console.WriteLine($"t1 = t2:(Equals) {t1.Equals(t2)}");
            Console.WriteLine($"t1 = t2:(==) {t1 == t2}");

            Console.WriteLine($"t1 = t3:(Equals) {t1.Equals(t3)}");
            Console.WriteLine($"t1 = t3:(==) {t1 == t3}");

            Console.WriteLine("------------------------------------------------");

            Console.WriteLine("Press any Key.....");
            Console.ReadKey();
        }
    }
}
