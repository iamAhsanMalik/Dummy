namespace STM.AIU.Application.Contracts.Infrastructure;

public interface ISmsService
{
  Task SendSmsAsync(string number, string message);
}
