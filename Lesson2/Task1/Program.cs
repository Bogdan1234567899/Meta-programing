namespace Counter_homework
{
    public class Program
    {
        public static void Main()
        {
            

            Counter c = new Counter(3);
            Console.WriteLine("Початкове значення (має бути 3): " + c.Value);

            Console.WriteLine("Робимо Increment у циклі for");
            for (int i = 0; i < 5; i++)
            {
                c.Increment();
                Console.WriteLine("Після Increment номер " + (i + 1) + ": " + c.Value);
            }

            Console.WriteLine("Робимо TryDecrement у циклі while доки значення не дійде до 0");
            int крок = 0;
            bool успіх = true;
            while (успіх)
            {
                крок = крок + 1;
                успіх = c.TryDecrement();
                Console.WriteLine("Крок " + крок + ", успіх=" + успіх + ", значення=" + c.Value);
            }

            Console.WriteLine("Перевірка звичайного Decrement() коли значення вже 0 (має бути виняток)");
            try
            {
                c.Decrement();
                Console.WriteLine("ПОМИЛКА: виняток не кинуто, а мав бути");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Отримано виняток: " + ex.Message);
            }

            c.Increment();
            Console.WriteLine("Після одного Increment значення зараз: " + c.Value);

            c.Reset();
            Console.WriteLine("Після Reset значення зараз: " + c.Value);

            Console.WriteLine("Перевіряємо, що не можна створити лічильник з від'ємним стартом");
            try
            {
                Counter поганий = new Counter(-10);
                Console.WriteLine("ПОМИЛКА: вдалося створити Counter(-10)");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Отримано виняток (це правильно): " + ex.Message);
            }

           
        }
    }
}
