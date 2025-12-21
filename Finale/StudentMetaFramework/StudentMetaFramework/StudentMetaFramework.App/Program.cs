using StudentMetaFramework.Core.Mapping;
using StudentMetaFramework.Core.Models;
using StudentMetaFramework.Core.Validation;

namespace StudentMetaFramework.App;

public static class Program
{
    public static void Main()
    {
        var file = "users.csv";

        if (!File.Exists(file))
        {
            Console.WriteLine("users.csv not found. Creating demo file...");
            CreateDemoCsv(file);
        }

        var importer = new CsvImporter();
        CsvImportResult<User> importResult;

        try
        {
            importResult = importer.Import<User>(file);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Import failed: {ex.Message}");
            return;
        }

        Console.WriteLine($"Imported objects: {importResult.Items.Count}");
        if (importResult.Errors.Count > 0)
        {
            Console.WriteLine($"Import row errors: {importResult.Errors.Count}");
            foreach (var e in importResult.Errors)
                Console.WriteLine($"  Line {e.LineNumber}: {e.Message} | {e.RawLine}");
        }

        var validator = new ModelValidator();
        int ok = 0, bad = 0;

        Console.WriteLine();
        Console.WriteLine("Validation results:");
        foreach (var u in importResult.Items)
        {
            var errors = validator.Validate(u);
            if (errors.Count == 0)
            {
                ok++;
                Console.WriteLine($"[OK] {u}");
            }
            else
            {
                bad++;
                Console.WriteLine($"[BAD] {u}");
                foreach (var err in errors)
                    Console.WriteLine($"   - {err}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"Summary: valid={ok}, invalid={bad} (rows with import errors are not included).");
    }

    private static void CreateDemoCsv(string path)
    {
        // Header must match [Column("...")] names in User model
        var lines = new List<string>
        {
            "Username,Email,Age",
            "alex,alex@mail.com,21",
            "bo,,20",              // Email missing => validation error
            "toooolongusernameeeeeeeee,aa@bb.com,25", // username length error
            "kate,kate@mail.com,notnumber", // import convert error
            "max,max@mail.com,200" // age out of range
        };

        File.WriteAllLines(path, lines);
    }
}
