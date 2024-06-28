using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Clients")]
public class Client
{
    [Key]
    public int IdClient { get; set; }

    public Firm? Firm { get; set; }

    public Individual? Individual { get; set; }
    
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;

    public ICollection<Contract> Contracts { get; set; } = new HashSet<Contract>();
}