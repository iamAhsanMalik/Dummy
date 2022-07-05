namespace STM.AIU.Infrastructure.Identity.Helpers;

internal class UserInfo : IUserInfo
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContext;

    public UserInfo(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext)
    {
        _userManager = userManager;
        _httpContext = httpContext;
    }
    public async Task<ApplicationUser> FindUserByEmailAsync(string userEmail) => await _userManager.FindByEmailAsync(userEmail);
    public async Task<ApplicationUser> FindUserByIdAsync(string userId) => await _userManager.FindByIdAsync(userId);
    public async Task<bool> IsUserEmailConfirmedAsync(ApplicationUser user) => await _userManager.IsEmailConfirmedAsync(user);
    public async Task<string> GetUserEmailAsync(ApplicationUser user) => await _userManager.GetEmailAsync(user);
    public async Task<string> GetUserPhoneNumberAsync(ApplicationUser user) => await _userManager.GetPhoneNumberAsync(user);
    public async Task<ApplicationUser?> GetCurrentLoginUserAsync() => await _userManager.GetUserAsync(_httpContext.HttpContext?.User);
    public async Task<string> GetCurrentLoginUserFullNameAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContext.HttpContext?.User);
        return $"{user.FirstName} {user.LastName}";
    }
    public async Task<string> GetCurrentLoginUserUserNameAsync()
    {
        var user = await _userManager.GetUserAsync(_httpContext.HttpContext?.User);
        return $"{user.NormalizedUserName}";
    }
}
