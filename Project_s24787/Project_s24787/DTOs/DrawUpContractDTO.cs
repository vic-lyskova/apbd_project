using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class DrawUpContractDTO
{
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    public ICollection<int>? Discounts { get; set; } = new HashSet<int>();

    public int? Updates { get; set; }

    [Required]
    public int Software { get; set; }

    [Required]
    [MaxLength(100)]
    public string Version { get; set; } = string.Empty;
}