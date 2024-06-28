using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Firms")]
public class Firm
{
    [MaxLength(100)]
    public string FirmName { get; set; } = string.Empty;

    public int IdClient { get; set; }

    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; }  = null!;
    
    [Key]
    [MaxLength(14)]
    public string KRSNumber { get; set; } = string.Empty;
}