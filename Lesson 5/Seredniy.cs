using System;
using System.Collections.Generic;

namespace CollectionsHomework
{
    public class Task2_Book
    {
        public static void Run()
        {
            Console.WriteLine("=== Завдання 2: СЕРЕДНІЙ - Book з IComparable<Book> ===\n");

            List<Book> books = new List<Book>
            {
                new Book("Кобзар", "Тарас Шевченко", 1840),
                new Book("Тигролови", "Іван Багряний", 1944),
                new Book("Собор", "Олесь Гончар", 1968),
                new Book("Кобзар", "Тарас Шевченко", 1860),
                new Book("Фата Моргана", "Михайло Коцюбинський", 1910),
                new Book("Тигролови", "Іван Багряний", 1946)
            };

            Console.WriteLine("Початковий список:");
            PrintBooks(books);

            // КОНТРАКТ List<T>.Sort() використовує IComparable<T>.CompareTo для впорядкування
            Console.WriteLine("\nСортування за природним порядком (Author -> Title -> Year):");
            books.Sort();
            PrintBooks(books);

            // КОНТРАКТ List<T>.BinarySearch() використовує IComparable<T>.CompareTo для пошуку
            Console.WriteLine("\nBinarySearch для наявного елемента:");
            Book searchExisting = new Book("Собор", "Олесь Гончар", 1968);
            int index = books.BinarySearch(searchExisting);
            if (index >= 0)
            {
                Console.WriteLine("Знайдено на позиції " + index + ": " + books[index]);
            }
            else
            {
                Console.WriteLine("Не знайдено (індекс: " + index + ")");
            }

            Console.WriteLine("\nBinarySearch для відсутнього елемента:");
            Book searchMissing = new Book("Тіні забутих предків", "Михайло Коцюбинський", 1911);
            index = books.BinarySearch(searchMissing);
            if (index >= 0)
            {
                Console.WriteLine("Знайдено на позиції " + index);
            }
            else
            {
                Console.WriteLine("Не знайдено. Індекс: " + index + ", місце для вставки: " + (~index));
            }

            Console.WriteLine("\n=== Демонстрація Equals/GetHashCode ===");
            Book book1 = new Book("Кобзар", "Тарас Шевченко", 1840);
            Book book2 = new Book("Кобзар", "Тарас Шевченко", 1860);
            Book book3 = new Book("Кобзар", "Тарас Шевченко", 1840);

            Console.WriteLine("book1: " + book1);
            Console.WriteLine("book2: " + book2 + " (інший рік)");
            Console.WriteLine("book3: " + book3);
            
            Console.WriteLine("\nbook1.Equals(book2): " + book1.Equals(book2) + " (різні роки, але рівні за Title+Author)");
            Console.WriteLine("book1.Equals(book3): " + book1.Equals(book3));
            Console.WriteLine("\nbook1.GetHashCode(): " + book1.GetHashCode());
            Console.WriteLine("book2.GetHashCode(): " + book2.GetHashCode() + " (однаковий з book1)");
            Console.WriteLine("book3.GetHashCode(): " + book3.GetHashCode());

            // КОНТРАКТ HashSet<T> використовує Equals/GetHashCode для визначення унікальності
            Console.WriteLine("\n=== Використання в HashSet ===");
            HashSet<Book> bookSet = new HashSet<Book>();
            bookSet.Add(book1);
            bookSet.Add(book2);
            bookSet.Add(book3);
            Console.WriteLine("Елементів у HashSet: " + bookSet.Count + " (має бути 1, бо всі рівні за Equals)");

            Console.WriteLine("\n=== Звіт ===");
            Console.WriteLine("Рішення: IComparable<Book> визначає природний порядок: Author -> Title -> Year (Ordinal).");
            Console.WriteLine("         Equals/GetHashCode визначають логічну рівність за Title+Author (без Year).");
            Console.WriteLine("Інваріанти: CompareTo повертає <0, 0, >0. Якщо a.Equals(b), то a.GetHashCode() == b.GetHashCode().");
            Console.WriteLine("Винятки: ArgumentNullException якщо CompareTo отримує null.");
            Console.WriteLine("Примітка: Equals не узгоджено з CompareTo - це допустимо, але потребує обережності.");
        }

        static void PrintBooks(List<Book> books)
        {
            for (int i = 0; i < books.Count; i++)
            {
                Console.WriteLine("  [" + i + "] " + books[i]);
            }
        }

        // КОНТРАКТ IComparable<T>: CompareTo(T other) повертає int (<0, 0, >0)
        // Увімкнює поведінку: Sort(), BinarySearch(), SortedSet, SortedDictionary
        public class Book : IComparable<Book>
        {
            public string Title { get; private set; }
            public string Author { get; private set; }
            public int Year { get; private set; }

            public Book(string title, string author, int year)
            {
                if (string.IsNullOrEmpty(title))
                    throw new ArgumentException("Title cannot be null or empty");
                if (string.IsNullOrEmpty(author))
                    throw new ArgumentException("Author cannot be null or empty");

                Title = title;
                Author = author;
                Year = year;
            }

            // КОНТРАКТ IComparable<Book>.CompareTo: природний порядок для типу
            // Повертає: <0 якщо this < other, 0 якщо рівні, >0 якщо this > other
            public int CompareTo(Book other)
            {
                if (other == null)
                {
                    return 1;
                }

                int authorComparison = string.Compare(Author, other.Author, StringComparison.Ordinal);
                if (authorComparison != 0)
                {
                    return authorComparison;
                }

                int titleComparison = string.Compare(Title, other.Title, StringComparison.Ordinal);
                if (titleComparison != 0)
                {
                    return titleComparison;
                }

                return Year.CompareTo(other.Year);
            }

            // КОНТРАКТ Object.Equals: визначає логічну рівність
            // Інваріант: якщо Equals повертає true, GetHashCode має повертати однакові значення
            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                Book other = obj as Book;
                if (other == null)
                {
                    return false;
                }

                return string.Equals(Title, other.Title, StringComparison.Ordinal) &&
                       string.Equals(Author, other.Author, StringComparison.Ordinal);
            }

            // КОНТРАКТ Object.GetHashCode: має бути узгоджено з Equals
            // Увімкнює поведінку: HashSet, Dictionary, Hashtable
            public override int GetHashCode()
            {
                int hash = 17;
                hash = hash * 31 + StringComparer.Ordinal.GetHashCode(Title);
                hash = hash * 31 + StringComparer.Ordinal.GetHashCode(Author);
                return hash;
            }

            public override string ToString()
            {
                return "\"" + Title + "\" by " + Author + " (" + Year + ")";
            }
        }
    }
}
