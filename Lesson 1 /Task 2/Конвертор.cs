namespace ConsoleApp9
{
    using System;

    class Program
    {
        static void Main()
        {
            // Курсы
            double USD_TO_UAH = 41.0;
            double EUR_TO_UAH = 44.0;

            Console.WriteLine("Конвертер валют (USD, EUR, UAH)");

            Console.Write("Введите сумму: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            Console.Write("Из какой валюты? (USD / EUR / UAH): ");
            string from = Console.ReadLine();

            Console.Write("В какую валюту? (USD / EUR / UAH): ");
            string to = Console.ReadLine();

            double inUAH = 0;

            // переводим сумму в гривны
            if (from == "UAH")
            {
                inUAH = amount;
            }
            if (from == "USD")
            {
                inUAH = amount * USD_TO_UAH;
            }
            if (from == "EUR")
            {
                inUAH = amount * EUR_TO_UAH;
            }

            double result = 0;

            // переводим гривны в целевую валюту
            if (to == "UAH")
            {
                result = inUAH;
            }
            if (to == "USD")
            {
                result = inUAH / USD_TO_UAH;
            }
            if (to == "EUR")
            {
                result = inUAH / EUR_TO_UAH;
            }

            Console.WriteLine(amount + " " + from + " = " + result + " " + to);

            Console.ReadLine();
        }
    }

}
