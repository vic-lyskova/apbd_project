using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Payments")]
public class Payment
{
    [Key]
    public int IdPayment { get; set; }

    public int IdClient { get; set; }

    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; } = null!;
    
    public int IdContract { get; set; }

    public double Amount { get; set; }

    public DateTime Date { get; set; }
}