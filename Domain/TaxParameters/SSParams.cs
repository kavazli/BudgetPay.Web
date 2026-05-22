using System;

namespace Domain.TaxParameters;

public class SSParams
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal ActiveEmployeeSSRate { get; set; }
    public decimal ActiveEmployeeUIRate { get; set; }
    public decimal ActiveEmployerSSRate { get; set; }
    public decimal ActiveEmployerUIRate { get; set; }
    public decimal RetiredEmployeeSSRate { get; set; }
    public decimal RetiredEmployerSSRate { get; set; }
}
