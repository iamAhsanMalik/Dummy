namespace STM.AIU.Application.Contracts.Identity;

public interface IAuthService
{
    Task<IdentityResult?> AddExternalLoginInfoAsync(ApplicationUser user, string? returnUrl = null);
    Task<IdentityResult?> EmailConfirmationAsync(string userId, string code);
    Task<string> EmailConfirmationTokenGeneratorAsync(ApplicationUser user);
    Task<SignInResult> ExternalLoginAsync(ExternalLoginInfo info, bool isPersistence = false);
    Task<string> GenerateTwoFactorAuthenticationTokenAsync(ApplicationUser user, string? tokenProvider);
    Task<ExternalLoginInfo> GetExternalLoginInfoAsync();
    AuthenticationProperties GetExternalLoginProperties(string provider, string? redirectUrl);
    Task<IList<string>> GetTwoFactorAuthenticationProvidersAsync(ApplicationUser user);
    Task<ApplicationUser> GetUserForCurrentTwoFactorAuthenticationAsync();
    Task LoginAsync(ApplicationUser user, bool isPersistence = false);
    Task LogoutAsync();
    Task<SignInResult?> PasswordLoginAsync(LoginDTO model, bool lockoutUserOnFailure = false);
    Task<IdentityResult?> PasswordResetAsync(ApplicationUser user, string code, string newPassword);
    Task<string> PasswordResetTokenGeneratorAsync(ApplicationUser user);
    Task<IdentityResult?> RegisterAsync(ApplicationUser user);
    Task<IdentityResult?> RegisterWithPasswordAsync(RegisterDTO model);
    Task<SignInResult> TwoFactorLoginAsync(VerifyCodeDTO model);
    Task<SignInResult> TwoFactorRecoveryCodeLoginAsync(UseRecoveryCodeDTO model);
    Task<IdentityResult?> UpdateExternalAuthTokensAsync(ExternalLoginInfo info);
}