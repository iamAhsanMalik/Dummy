using Microsoft.Extensions.DependencyInjection;
using STM.AIU.Application.Contracts.Helpers;
using STM.AIU.Application.Helpers;

namespace STM.AIU.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IAiuHelpers, AiuHelpers>();
        services.AddSingleton<IDateTimeHelpers, DateTimeHelpers>();
        services.AddSingleton<IEscaperHelpers, EscaperHelpers>();
        services.AddSingleton<IGeneralHelpers, GeneralHelpers>();
        services.AddSingleton<IGeneratorHelpers, GeneratorHelpers>();
        services.AddSingleton<ISecurityHelpers, SecurityHelpers>();
        services.AddSingleton<IValidatorHelpers, ValidatorHelpers>();
        return services;
    }
}
