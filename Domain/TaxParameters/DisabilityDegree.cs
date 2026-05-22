using System;
using Domain.Enums;

namespace Domain.TaxParameters;

public class DisabilityDegree
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public Degree Degree { get; set; }
    public decimal Amount { get; set; }

}
