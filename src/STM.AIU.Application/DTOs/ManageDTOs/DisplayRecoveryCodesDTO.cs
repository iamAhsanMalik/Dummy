namespace STM.AIU.Application.DTOs.ManageDTOs;

public class DisplayRecoveryCodesDTO
{
  [Required]
  public IEnumerable<string>? Codes { get; set; }

}
