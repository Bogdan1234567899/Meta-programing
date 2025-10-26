namespace ConsoleApp9
{
    using System;

    class Program
    {
        static void Main()
        {

            
            Console.Write("Введіть перше число: ");
            double a = Convert.ToDouble(Console.ReadLine());

            
            Console.Write("Введіть друге число: ");
            double b = Convert.ToDouble(Console.ReadLine());

            
            Console.Write("Оберіть операцію (+, -, *, /): ");
            string op = Console.ReadLine();

            double result;

            
            switch (op)
            {
                case "+":
                    result = a + b;
                    Console.WriteLine($"Результат: {a} + {b} = {result}");
                    break;

                case "-":
                    result = a - b;
                    Console.WriteLine($"Результат: {a} - {b} = {result}");
                    break;

                case "*":
                    result = a * b;
                    Console.WriteLine($"Результат: {a} * {b} = {result}");
                    break;

                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("Помилка: ділення на нуль неможливе.");
                    }
                    else
                    {
                        result = a / b;
                        Console.WriteLine($"Результат: {a} / {b} = {result}");
                    }
                    break;

                default:
                    Console.WriteLine("Невідома операція. Використовуйте тільки +, -, *, /.");
                    break;
            }

            Console.WriteLine("Натисніть Enter, щоб вийти...");
            Console.ReadLine();
        }
    }

}
