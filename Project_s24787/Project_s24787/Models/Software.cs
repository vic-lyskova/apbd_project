using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("SoftwareSystems")]
public class Software
{
    [Key]
    public int IdSoftware { get; set; }

    [MaxLength(100)]
    public string SoftwareName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string CurrentVersion { get; set; } = string.Empty;

    public int IdCategory { get; set; }

    [ForeignKey(nameof(IdCategory))]
    public Category Category { get; set; } = null!;
    
    public double LicencePrice { get; set; }
}