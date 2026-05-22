using System;

namespace Domain.TaxParameters;

public class EconomicIndicators
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal MinimumWageIncreaseRate { get; set; }
    public decimal InflationRate { get; set; }
    public decimal RevaluationRate { get; set; }
}
