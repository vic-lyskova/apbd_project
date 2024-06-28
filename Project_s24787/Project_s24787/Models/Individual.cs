using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Individuals")]
public class Individual
{
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Surname { get; set; } = string.Empty;
    
    public int IdClient { get; set; }

    [ForeignKey(nameof(IdClient))]
    public Client Client { get; set; } = null!;
    
    [Key]
    [MaxLength(11)]
    public string PESEL { get; set; } = string.Empty;
}