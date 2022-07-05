namespace STM.AIU.Application.Contracts.Identity;

public interface IUserInfo
{
    Task<ApplicationUser> FindUserByEmailAsync(string userEmail);
    Task<ApplicationUser> FindUserByIdAsync(string userId);
    Task<ApplicationUser?> GetCurrentLoginUserAsync();
    Task<string> GetCurrentLoginUserFullNameAsync();
    Task<string> GetCurrentLoginUserUserNameAsync();
    Task<string> GetUserEmailAsync(ApplicationUser user);
    Task<string> GetUserPhoneNumberAsync(ApplicationUser user);
    Task<bool> IsUserEmailConfirmedAsync(ApplicationUser user);
}