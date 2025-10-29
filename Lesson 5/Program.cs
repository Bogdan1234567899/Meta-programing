using System;

namespace CollectionsHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            

            
            Task1_ForEach.Run();
            Console.WriteLine("\n\n");
            Pause();

            
            Task2_Book.Run();
            Console.WriteLine("\n\n");
            

            
            
        }

        static void Pause()
        {
            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
