using System.Text;

Console.OutputEncoding = Encoding.UTF8;

record Student(int Id, string Name, string Group, int Avg, bool IsActive, string Email);

var students = new List<Student>
{
    new Student(1, "Ivan Petrenko", "A1", 91, true,  "ivan@uni.com"),
    new Student(2, "Olena Koval",   "A1", 78, true,  "olena@uni.com"),
    new Student(3, "Dmytro Bondar", "A1", 84, false, "dmytro@uni.com"),
    new Student(4, "Nazar Shevchuk","B2", 88, true,  "nazar@uni.com"),
    new Student(5, "Iryna Melnyk",  "B2", 80, true,  "iryna@uni.com"),
    new Student(6, "Sofiia Horbun", "B2", 67, true,  "sofiia@uni.com"),
    new Student(7, "Taras Lysenko", "C3", 95, true,  "taras@uni.com"),
    new Student(8, "Kateryna Rud",  "C3", 82, true,  "kateryna@uni.com"),
    new Student(9, "Andrii Sokol",  "C3", 59, false, "andrii@uni.com"),
    new Student(10,"Marta Step",    "A1", 86, true,  "marta@uni.com"),
};

var q = students
    .Where(s => s.IsActive && s.Avg >= 80)
    .OrderByDescending(s => s.Avg)
    .ThenBy(s => s.Name)
    .Select(s => new { s.Name, s.Avg });

foreach (var x in q)
{
    Console.WriteLine(x.Name + " — " + x.Avg);
}
