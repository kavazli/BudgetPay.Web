

using Domain.Enums;

namespace Domain.Scenario;

public class ScenarioModel
{
    public Guid Id { get; set; }
    public Company Company { get; set; }
    public int Year { get; set; }
    public string ScenarioName { get; set; } = string.Empty;
    public decimal EconomicIndicator { get; set; }
    public decimal WelfarShare { get; set; }
    public decimal RafeOfIncrase { get; set; }
    public decimal TotalRate { get; set; }
    public decimal Overtime_50 { get; set; }
    public decimal Overtime_100 { get; set; }
    public decimal Bonus { get; set; }
    public decimal BonusMonth { get; set; }
    public decimal ShoppingVoucher { get; set; }
    public decimal ShoppingVoucherMonth { get; set; }

}