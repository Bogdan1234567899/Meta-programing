using System.Globalization;
using System.Text;


Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 2)
{
    Console.WriteLine("Використання:");
    Console.WriteLine("  dotnet run -- write <path>");
    Console.WriteLine("  dotnet run -- read  <path>");
    return;
}

string cmd = args[0].ToLower();
string path = args[1];

if (cmd == "write")
{
    WriteFile(path);
}
else if (cmd == "read")
{
    ReadFile(path);
}
else
{
    Console.WriteLine("Команда має бути write або read");
}

void WriteFile(string p)
{
    Directory.CreateDirectory(Path.GetDirectoryName(p) ?? ".");

    Product[] arr = new Product[3];
    arr[0] = new Product(1, 19.99, "Notebook");
    arr[1] = new Product(2, 5.49, "Pen");
    arr[2] = new Product(3, 149.00, "Backpack");

    using (FileStream fs = new FileStream(p, FileMode.Create, FileAccess.Write, FileShare.None))
    using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8))
    {
        bw.Write(Encoding.ASCII.GetBytes("MAGC"));
        bw.Write(1);

        for (int i = 0; i < arr.Length; i++)
        {
            bw.Write(arr[i].Id);
            bw.Write(arr[i].Price);
            bw.Write(arr[i].Name);
        }
    }

    Console.WriteLine("OK. Записано: " + p);
}

void ReadFile(string p)
{
    if (!File.Exists(p))
    {
        Console.WriteLine("Файл не знайдено: " + p);
        return;
    }

    using (FileStream fs = new FileStream(p, FileMode.Open, FileAccess.Read, FileShare.Read))
    using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8))
    {
        string magic = Encoding.ASCII.GetString(br.ReadBytes(4));
        if (magic != "MAGC")
        {
            Console.WriteLine("Невірний формат (нема MAGC).");
            return;
        }

        int version = br.ReadInt32();
        if (version != 1)
        {
            Console.WriteLine("Підтримується тільки v1, у файлі v" + version);
            return;
        }

        Console.WriteLine("Файл: " + p);
        Console.WriteLine("Версія: v" + version);

        while (fs.Position < fs.Length)
        {
            try
            {
                int id = br.ReadInt32();
                double price = br.ReadDouble();
                string name = br.ReadString();

                Console.WriteLine("#" + id + " " + name + " " + price.ToString("0.00", CultureInfo.InvariantCulture));
            }
            catch
            {
                break;
            }
        }
    }
}

class Product
{
    public int Id;
    public double Price;
    public string Name;

    public Product(int id, double price, string name)
    {
        Id = id;
        Price = price;
        Name = name;
    }
}
