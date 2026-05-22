
using Domain.TaxParameters;
using Microsoft.Extensions.DependencyInjection;
using Business.Interfaces.Params;
using Business.Services.ParamsServices;
using Business.Interfaces.Scenario;
using Domain.Scenario;
using Business.Services.ScenarioServices;

namespace Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IDisabilityDegreService<DisabilityDegree>, DisabilityDegreeService>();
        services.AddScoped<IIncomeTaxService<IncomeTaxBracket>, IncomeTaxBracketService>();
        services.AddScoped<IParamsService<MinimumWage>, MinimumWageService>();
        services.AddScoped<IParamsService<SSParams>, SSParamsService>();
        services.AddScoped<IParamsService<StampTax>, StampTaxService>();
        services.AddScoped<IParamsService<EconomicIndicators>, EconomicIndicatorsService>();

        services.AddScoped<IScenarioService<ScenarioModel>, ScenarioService>();

        return services;
    }
}
