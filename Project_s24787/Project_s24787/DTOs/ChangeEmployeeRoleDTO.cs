using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class ChangeEmployeeRoleDTO
{
    [Required]
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(10)]
    public string Role { get; set; } = string.Empty;
}