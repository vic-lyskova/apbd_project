using System.ComponentModel.DataAnnotations;

namespace Project_s24787.DTOs;

public class AddPaymentDTO
{
    [Required]
    public int IdContract { get; set; }
    
    [Required]
    public double Amount { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
}