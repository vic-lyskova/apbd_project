using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class AddIndividualClientDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Surname { get; set; } = string.Empty;
    
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
    [MaxLength(11)]
    public string PESEL { get; set; } = string.Empty;
}