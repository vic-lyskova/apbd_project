using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class EditFirmClientDTO
{
    [MaxLength(100)]
    public string? FirmName { get; set; } = null;

    [MaxLength(100)]
    public string? Address { get; set; } = null;

    [MaxLength(100)]
    public string? Email { get; set; } = null;

    [MaxLength(50)]
    public string? PhoneNumber { get; set; } = null;
}