using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_s24787.Models;

[Table("Users")]
public class User
{
    [Key]
    public int IdUser { get; set; }
    
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    
    [MaxLength(200)]
    public string RefreshToken { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string Salt { get; set; } = string.Empty;

    [MaxLength(10)]
    public string Role { get; set; } = string.Empty;
}