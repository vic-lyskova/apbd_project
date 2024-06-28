using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class EditIndividualClientDTO
{
    [MaxLength(100)]
    public string? Name { get; set; } = null;

    [MaxLength(100)]
    public string? Surname { get; set; } = null;

    [MaxLength(100)]
    public string? Address { get; set; } = null;

    [MaxLength(100)]
    public string? Email { get; set; } = null;

    [MaxLength(50)]
    public string? PhoneNumber { get; set; } = null;
}