using System.Globalization;
using System.Text;


Console.OutputEncoding = Encoding.UTF8;

string inbox = "inbox";
string processed = "processed";

if (args.Length >= 1) inbox = args[0];
if (args.Length >= 2) processed = args[1];

Directory.CreateDirectory(inbox);
Directory.CreateDirectory(processed);

FileSystemWatcher w = new FileSystemWatcher(inbox, "*.csv");
w.IncludeSubdirectories = false;
w.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;

w.Created += (s, e) => Handle(e.FullPath, processed);
w.Changed += (s, e) => Handle(e.FullPath, processed);

w.EnableRaisingEvents = true;

Console.WriteLine("Watching: " + Path.GetFullPath(inbox));
Console.WriteLine("Processed: " + Path.GetFullPath(processed));
Console.WriteLine("Enter щоб зупинити...");

Console.ReadLine();

void Handle(string fullPath, string processedDir)
{
    Thread t = new Thread(() =>
    {
        Thread.Sleep(500);

        try
        {
            if (!File.Exists(fullPath)) return;

            string name = Path.GetFileNameWithoutExtension(fullPath);
            if (string.IsNullOrWhiteSpace(name)) name = "report";

            string ext = Path.GetExtension(fullPath);
            string date = DateTime.UtcNow.ToString("yyyyMMdd", CultureInfo.InvariantCulture);

            string dest = Path.Combine(processedDir, name + "-" + date + ext);

            int i = 1;
            while (File.Exists(dest))
            {
                dest = Path.Combine(processedDir, name + "-" + date + "-" + i + ext);
                i++;
            }

            File.Move(fullPath, dest);

            Console.WriteLine("OK: " + Path.GetFileName(fullPath) + " -> " + Path.GetFileName(dest));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    });

    t.IsBackground = true;
    t.Start();
}
