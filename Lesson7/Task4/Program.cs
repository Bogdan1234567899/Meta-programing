using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;


Console.OutputEncoding = Encoding.UTF8;

if (args.Length < 2)
{
    Console.WriteLine("Використання:");
    Console.WriteLine("  dotnet run -- save <path> <id> <fullName> <email>");
    Console.WriteLine("  dotnet run -- load <path>");
    return;
}

string cmd = args[0].ToLower();

if (cmd == "save")
{
    if (args.Length < 5)
    {
        Console.WriteLine("Недостатньо аргументів для save.");
        return;
    }

    string path = args[1];
    int id;
    if (!int.TryParse(args[2], out id))
    {
        Console.WriteLine("id має бути число.");
        return;
    }

    string fullName = args[3];
    string email = args[4];

    UserProfile p = new UserProfile();
    p.Id = id;
    p.FullName = fullName;
    p.Email = email;
    p.RegisteredUtc = DateTimeOffset.UtcNow;
    p.IsInternal = false;

    Directory.CreateDirectory(Path.GetDirectoryName(path) ?? ".");

    string json = JsonSerializer.Serialize(p, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(path, json, new UTF8Encoding(false));

    Console.WriteLine("OK. Збережено: " + path);
}
else if (cmd == "load")
{
    string path = args[1];

    if (!File.Exists(path))
    {
        Console.WriteLine("Файл не знайдено: " + path);
        return;
    }

    string json = File.ReadAllText(path, Encoding.UTF8);
    UserProfile p = JsonSerializer.Deserialize<UserProfile>(json);

    if (p == null)
    {
        Console.WriteLine("Не вдалося прочитати JSON.");
        return;
    }

    Console.WriteLine("Id: " + p.Id);
    Console.WriteLine("FullName: " + p.FullName);
    Console.WriteLine("Email: " + p.Email);
    Console.WriteLine("RegisteredUtc: " + p.RegisteredUtc.ToUniversalTime().ToString("O"));
    Console.WriteLine("IsInternal: " + p.IsInternal + " (не зберігається в JSON)");
}
else
{
    Console.WriteLine("Команда має бути save або load");
}

class UserProfile
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("full_name")]
    public string FullName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("registered_utc")]
    public DateTimeOffset RegisteredUtc { get; set; }

    [JsonIgnore]
    public bool IsInternal { get; set; }
}
