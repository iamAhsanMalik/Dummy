using System.ComponentModel.DataAnnotations;

namespace STM.AIU.Application.DTOs.AccountDTOs;

public class ForgotPasswordDTO
{
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
}
