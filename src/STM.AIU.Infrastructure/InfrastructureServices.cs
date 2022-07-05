namespace STM.AIU.Infrastructure;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DB Context Setting
        services.AddDbContext<AIUDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AiuNetCoreConnection")));

        // Authentication Configurations Options
        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters = AIUConstants.AllowedUserNameCharacters;
            options.User.RequireUniqueEmail = false;
        });

        // Identity Services
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AIUDbContext>().AddDefaultTokenProviders();

        services.AddAuthentication()
           .AddFacebook(facebookOptions =>
           {
               IConfigurationSection facebookAuthNSection = configuration.GetSection("ExternalAuthenticators:Faceboook");
               facebookOptions.ClientId = facebookAuthNSection["AppId"];
               facebookOptions.ClientSecret = facebookAuthNSection["AppSecret"];
               facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
           })
           .AddGoogle(googleOptions =>
           {
               IConfigurationSection googleAuthNSection = configuration.GetSection("ExternalAuthenticators:Google");
               googleOptions.ClientId = googleAuthNSection["AppId"];
               googleOptions.ClientSecret = googleAuthNSection["AppSecret"];
           })
           .AddMicrosoftAccount(microsoftOptions =>
           {
               IConfigurationSection microsoftAuthNSection = configuration.GetSection("ExternalAuthenticators:Microsoft");
               microsoftOptions.ClientId = microsoftAuthNSection["AppSecret"];
               microsoftOptions.ClientSecret = microsoftAuthNSection["AppSecret"];
           });

        services.Configure<MailJetConfig>(configuration.GetSection("MailJet"));
        services.Configure<SMTPConfig>(configuration.GetSection("SMTPServers"));
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserInfo, UserInfo>();

        #region Infrastructure
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISmsService, SMSService>();
        services.AddScoped<IFileHelpers, FileHelpers>();
        #endregion
        return services;
    }
}
