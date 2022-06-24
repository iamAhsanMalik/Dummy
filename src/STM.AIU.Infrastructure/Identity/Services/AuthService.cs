#region Packages Registration
#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using STM.AIU.Application.Contracts.Identity;
using STM.AIU.Application.DTOs.AccountDTOs;
using STM.AIU.Application.Models;
using System.Text;

namespace STM.AIU.Infrastructure.Identity.Services;
internal class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Login
    public async Task LoginAsync(ApplicationUser user, bool isPersistence = false) => await _signInManager.SignInAsync(user, isPersistent: isPersistence);
    public async Task<SignInResult?> PasswordLoginAsync(LoginDTO model, bool lockoutUserOnFailure = false) =>
        (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password)) ? await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: lockoutUserOnFailure) : default;

    public async Task<SignInResult> ExternalLoginAsync(ExternalLoginInfo info, bool isPersistence = false) =>
        // Sign in the user with this external login provider if the user already has a account.
        await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: isPersistence);
    public async Task<SignInResult> TwoFactorLoginAsync(VerifyCodeDTO model) =>
        // Sign in the user after two factor code validation.
        await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, model.RememberMe, model.RememberBrowser);
    public async Task<SignInResult> TwoFactorRecoveryCodeLoginAsync(UseRecoveryCodeDTO model) => await _signInManager.TwoFactorRecoveryCodeSignInAsync(model.Code);
    #endregion

    #region Register
    public async Task<IdentityResult?> RegisterAsync(ApplicationUser user) => await _userManager.CreateAsync(user);
    public async Task<IdentityResult?> RegisterWithPasswordAsync(RegisterDTO model) => await _userManager.CreateAsync(new ApplicationUser { UserName = model.Email, Email = model.Email }, model.Password);

    #endregion

    #region Logout
    public async Task LogoutAsync() => await _signInManager.SignOutAsync();
    #endregion

    public async Task<string> EmailConfirmationTokenGeneratorAsync(ApplicationUser user) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(await _userManager.GenerateEmailConfirmationTokenAsync(user)));
    public async Task<string> PasswordResetTokenGeneratorAsync(ApplicationUser user) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(await _userManager.GeneratePasswordResetTokenAsync(user)));

    #region External Login Information
    public AuthenticationProperties GetExternalLoginProperties(string provider, string? redirectUrl) => _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
    public async Task<ExternalLoginInfo> GetExternalLoginInfoAsync() => await _signInManager.GetExternalLoginInfoAsync();
    public async Task<IdentityResult?> PasswordResetAsync(ApplicationUser user, string code, string newPassword) => (user != null && !string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(newPassword)) ? await _userManager.ResetPasswordAsync(user, code, newPassword) : default;
    public async Task<ApplicationUser> GetUserForCurrentTwoFactorAuthenticationAsync() => await _signInManager.GetTwoFactorAuthenticationUserAsync();
    public async Task<IList<string>> GetTwoFactorAuthenticationProvidersAsync(ApplicationUser user) => await _userManager.GetValidTwoFactorProvidersAsync(user);
    public async Task<string> GenerateTwoFactorAuthenticationTokenAsync(ApplicationUser user, string? tokenProvider) => await _userManager.GenerateTwoFactorTokenAsync(user, tokenProvider);

    public async Task<IdentityResult?> AddExternalLoginInfoAsync(ApplicationUser user, string? returnUrl = null)
    {
        // Get the information about the user from the external login provider
        var info = await _signInManager.GetExternalLoginInfoAsync();
        var result = await RegisterAsync(user);
        return (result?.Succeeded == true) ? await _userManager.AddLoginAsync(user, info) : default;
    }
    public async Task<IdentityResult?> UpdateExternalAuthTokensAsync(ExternalLoginInfo info) =>
        // Update any authentication tokens as well
        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

    public async Task<IdentityResult?> EmailConfirmationAsync(string userId, string code) => (!string.IsNullOrEmpty(userId) || !string.IsNullOrEmpty(code)) ? await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(userId), code) : default;
    #endregion
}