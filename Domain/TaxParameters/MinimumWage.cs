using System;

namespace Domain.TaxParameters;

public class MinimumWage
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal NetSalary { get; set; }
    public decimal RetiredNetSalary { get; set; }
    public decimal Ceiling { get; set; }

}

