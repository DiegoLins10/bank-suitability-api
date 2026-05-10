using Microsoft.Extensions.DependencyInjection;
using Suitability.Application.Features.EvaluateSuitability;
using Suitability.Application.Features.ValidateProduct;

namespace Suitability.Application;
public interface ISuitabilityMarker;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EvaluateSuitabilityHandler>();
        services.AddScoped<ValidateProductHandler>();
        return services;
    }
}
