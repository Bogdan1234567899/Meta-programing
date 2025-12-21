using System.Text;


Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 2)
{
    Console.WriteLine("Використання: dotnet run -- <inputFile> <outputFile>");
    return;
}

string inputFile = args[0];
string outputFile = args[1];

if (!File.Exists(inputFile))
{
    Console.WriteLine("Файл не знайдено: " + inputFile);
    return;
}

long removed = 0;
int lines = 0;

StringBuilder sb = new StringBuilder();

using (StreamReader sr = new StreamReader(inputFile, detectEncodingFromByteOrderMarks: true))
{
    string line;
    while ((line = sr.ReadLine()) != null)
    {
        lines++;

        int k = line.Length - 1;
        while (k >= 0 && line[k] == ' ')
        {
            removed++;
            k--;
        }

        if (k >= 0)
            sb.Append(line.Substring(0, k + 1));

        sb.Append('\n');
    }
}

Directory.CreateDirectory(Path.GetDirectoryName(outputFile) ?? ".");

File.WriteAllText(outputFile, sb.ToString(), new UTF8Encoding(false));

Console.WriteLine("OK");
Console.WriteLine("Рядків: " + lines);
Console.WriteLine("Прибрано пробілів: " + removed);
