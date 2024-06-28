using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class LoginRequestDTO
{
    [Required]
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
}