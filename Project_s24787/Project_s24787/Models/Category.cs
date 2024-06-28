using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Categories")]
public class Category
{
    [Key]
    public int IdCategory { get; set; }
    
    [MaxLength(50)]
    public string CategoryName { get; set; } = string.Empty;
}