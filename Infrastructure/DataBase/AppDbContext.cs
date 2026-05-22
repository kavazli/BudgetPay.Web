using System;
using Domain.Scenario;
using Domain.TaxParameters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<DisabilityDegree> DisabilityDegrees { get; set;}
    public DbSet<IncomeTaxBracket> IncomeTaxBrackets { get; set;}
    public DbSet<MinimumWage> MinimumWages { get; set;}
    public DbSet<SSParams> SSParams { get; set;}
    public DbSet<StampTax> StampTaxes { get; set;}
    public DbSet<EconomicIndicators> EconomicIndicators { get; set;}


    public DbSet<ScenarioModel> Scenarios { get; set;}

}
