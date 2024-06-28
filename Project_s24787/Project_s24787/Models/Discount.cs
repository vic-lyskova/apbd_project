using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Discounts")]
public class Discount
{
    [Key]
    public int IdDiscount { get; set; }

    [MaxLength(50)]
    public string DiscountName { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Offer { get; set; } = string.Empty;

    [MaxLength(4)]
    public string Value { get; set; } = string.Empty;

    public DateTime ActiveFrom { get; set; }
    
    public DateTime? ActiveTo { get; set; }

    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
}