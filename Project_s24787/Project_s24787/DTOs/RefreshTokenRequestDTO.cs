using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class RefreshTokenRequestDTO
{
    [Required]
    [MaxLength(200)]
    public string RefreshToken { get; set; } = string.Empty;
}