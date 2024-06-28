using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Contracts")]
public class Contract
{
    [Key]
    public int IdContract { get; set; }
    
    public int IdClient { get; set; }

    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; } = null!;

    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public double Price { get; set; }

    public ICollection<Discount> Discounts { get; set; } = new HashSet<Discount>();

    public int Update { get; set; } = 1;

    public int? AdditionalUpdates { get; set; }

    public int IdSoftware { get; set; }

    [ForeignKey(nameof(IdSoftware))]
    public Software Software { get; set; } = null!;

    [MaxLength(100)]
    public string Version { get; set; } = string.Empty;
    
    public Payment? Payment { get; set; }
}