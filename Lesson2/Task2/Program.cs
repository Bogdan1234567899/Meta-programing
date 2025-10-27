namespace TodoItem
{
    internal class Program
    {
        public static void Main()
        {
            

            TodoItem[] tasks = new TodoItem[3];
            tasks[0] = new TodoItem("Винести сміття");
            tasks[1] = new TodoItem("Помити посуд", true);
            tasks[2] = new TodoItem("Зробити домашнє завдання з C#");

            Console.WriteLine("Початковий стан масиву:");
            ВивестиСписок(tasks);

            tasks[0].MarkDone();
            tasks[1].MarkUndone();
            tasks[2].MarkDone();

            Console.WriteLine("Стан після MarkDone / MarkUndone:");
            ВивестиСписок(tasks);

            bool r1 = tasks[0].TryRename("Винести сміття і купити хліб");
            bool r2 = tasks[1].TryRename("   ");
            bool r3 = tasks[2].TryRename("Зробити домашнє завдання з інформатики");

            Console.WriteLine("Результат перейменування елемента 0: " + r1);
            Console.WriteLine("Результат перейменування елемента 1: " + r2 + " (має бути false)");
            Console.WriteLine("Результат перейменування елемента 2: " + r3);

            Console.WriteLine("Поточний стан після перейменувань:");
            ВивестиСписок(tasks);

            Console.WriteLine("Перевірка створення задачі з порожньою назвою (має бути виняток):");
            try
            {
                TodoItem погана = new TodoItem("   ");
                Console.WriteLine("ПОМИЛКА: створено елемент із порожньою назвою");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Отримано виняток: " + ex.Message);
            }

            
        }

        private static void ВивестиСписок(TodoItem[] tasks)
        {
            for (int i = 0; i < tasks.Length; i++)
            {
                TodoItem t = tasks[i];
                Console.WriteLine("[" + i + "] Назва=\"" + t.Title + "\", Виконано=" + t.IsDone);
            }
        }
    }
}
