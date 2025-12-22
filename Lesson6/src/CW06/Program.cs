using CW06;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("CW06 — мінімальний рівень (демо)");

// -------------------- Task 1 --------------------
Console.WriteLine("\n[Task 1] ParseSetting");
try
{
    var (key, value) = ConfigParser.ParseSetting("user=admin");
    Console.WriteLine($"OK: {key} = {value}");

    // Поганий рядок (без '='), щоб показати FormatException
    ConfigParser.ParseSetting("bad_line_without_equals");
}
catch (Exception ex)
{
    Console.WriteLine($"Caught: {ex.GetType().Name}: {ex.Message}");
}

// -------------------- Task 2 --------------------
Console.WriteLine("\n[Task 2] checked/unchecked");
Console.WriteLine($"AddWrapped(int.MaxValue, 1) = {OverflowMath.AddWrapped(int.MaxValue, 1)}");
try
{
    Console.WriteLine($"AddChecked(int.MaxValue, 1) = {OverflowMath.AddChecked(int.MaxValue, 1)}");
}
catch (OverflowException)
{
    Console.WriteLine("AddChecked(int.MaxValue, 1) -> OverflowException (expected)");
}

// -------------------- Task 3 --------------------
Console.WriteLine("\n[Task 3] TempFileWriter (IDisposable)");
TempFileWriter writer;
using (writer = new TempFileWriter())
{
    writer.WriteLine("Hello");
    writer.WriteLine("World");
    Console.WriteLine($"Wrote to temp file: {writer.FilePath}");
}
try
{
    writer.WriteLine("After dispose");
}
catch (ObjectDisposedException)
{
    Console.WriteLine("Write after Dispose() -> ObjectDisposedException (expected)");
}

// -------------------- Task 4 --------------------
Console.WriteLine("\n[Task 4] Delegates as strategies");
var input = "  Phone: +38(050)123-45-67  ";
Console.WriteLine($"Input: {input}");
Console.WriteLine($"TrimToUpper: {TextTransforms.Transform(input, TextTransforms.TrimToUpper)}");
Console.WriteLine($"MaskDigits:  {TextTransforms.Transform(input, TextTransforms.MaskDigits)}");

// -------------------- Task 5 --------------------
Console.WriteLine("\n[Task 5] Events on custom classes");
var counter = new Counter();

// Підписка №1: показати значення
counter.Changed += (_, v) => Console.WriteLine($"Value = {v}");

// Підписка №2: короткий лог
counter.Changed += (_, v) => Console.WriteLine($"[log] incremented to {v}");

counter.Increment();
counter.Increment();
counter.Increment();

Console.WriteLine("\nDone.");
