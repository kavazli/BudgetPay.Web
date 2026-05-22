using Business.Interfaces.Params;
using Business.Interfaces.Scenario;
using Business.Services.ParamsServices;
using Domain.Scenario;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Infrastructure.ParamsProviders;
using Infrastructure.ScenarioProviders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("SqlCon")));

        services.AddScoped<IDisabilityDegreeProvider<DisabilityDegree>, DisabilityDegreeProvider>();
        services.AddScoped<IIncomeTaxProvider<IncomeTaxBracket>, IncomeTaxBracketProvider>();
        services.AddScoped<IParamsProvider<MinimumWage>, MinimumWageProvider>();
        services.AddScoped<IParamsProvider<SSParams>, SSParamsProvider>();
        services.AddScoped<IParamsProvider<StampTax>, StampTaxProvider>();
        services.AddScoped<IParamsProvider<EconomicIndicators>, EconomicIndicatorsProvider>();


        services.AddScoped<IScenarioProvider<ScenarioModel>, ScenarioProvider>();
        

        return services;
    }
}
