using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class AddFirmClientDTO
{
    [Required]
    [MaxLength(100)]
    public string FirmName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Address { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(14)]
    public string KRSNumber { get; set; } = string.Empty;
}