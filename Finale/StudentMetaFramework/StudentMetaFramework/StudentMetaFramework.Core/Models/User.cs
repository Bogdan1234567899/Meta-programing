using StudentMetaFramework.Core.Mapping;
using StudentMetaFramework.Core.Validation;

namespace StudentMetaFramework.Core.Models;

public class User
{
    [Column("Username")]
    [Required]
    [StringLength(3, 20)]
    public string? Username { get; set; }

    [Column("Email")]
    [Required]
    [StringLength(5, 60)]
    public string? Email { get; set; }

    [Column("Age")]
    [Range(0, 120)]
    public int Age { get; set; }

    [Ignore]
    public bool IsAdult => Age >= 18;

    public override string ToString()
        => $"{Username} | {Email} | Age={Age} | IsAdult={IsAdult}";
}
