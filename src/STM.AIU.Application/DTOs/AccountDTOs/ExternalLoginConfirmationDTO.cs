using System.ComponentModel.DataAnnotations;

namespace STM.AIU.Application.DTOs.AccountDTOs;

public class ExternalLoginConfirmationDTO
{
  [Required]
  [EmailAddress]
  public string? Email { get; set; }
}
