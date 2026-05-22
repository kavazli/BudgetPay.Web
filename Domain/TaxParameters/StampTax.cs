using System;

namespace Domain.TaxParameters;

public class StampTax
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal Rate { get; set; }
    
}
