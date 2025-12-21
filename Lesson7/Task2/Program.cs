using System.Text;


Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 1)
{
    Console.WriteLine("Використання: dotnet run -- <outputPath> [KEY=VALUE ...]");
    return;
}

string path = args[0];
List<string> lines = new List<string>();

for (int i = 1; i < args.Length; i++)
{
    string s = args[i];
    if (string.IsNullOrWhiteSpace(s)) continue;

    if (!s.Contains("="))
    {
        Console.WriteLine("Невірний формат, треба KEY=VALUE: " + s);
        return;
    }
    lines.Add(s);
}

while (lines.Count < 3)
{
    Console.Write("Введіть KEY=VALUE (потрібно мін 3): ");
    string s = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(s)) continue;

    if (!s.Contains("="))
    {
        Console.WriteLine("Невірний формат, спробуйте ще раз.");
        continue;
    }
    lines.Add(s);
}

Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");

using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
using (StreamWriter sw = new StreamWriter(fs, new UTF8Encoding(false)))
{
    sw.NewLine = "\n";

    for (int i = 0; i < lines.Count; i++)
        sw.WriteLine(lines[i]);

    sw.Flush();
    fs.Flush(true);
}

Console.WriteLine("OK. Записано рядків: " + lines.Count);
Console.WriteLine("Файл: " + path);
