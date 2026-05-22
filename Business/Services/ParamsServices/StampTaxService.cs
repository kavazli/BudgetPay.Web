
using Business.Interfaces.Params;
using Domain.TaxParameters;

namespace Business.Services.ParamsServices;

public class StampTaxService : IParamsService<StampTax>
{

    private readonly IParamsProvider<StampTax> _provider;


    public StampTaxService(IParamsProvider<StampTax> provider)
    {
        _provider = provider;
    }


    public async Task<StampTax> AddAsync(StampTax entity)
    {
        var existingRecord = await _provider.GetByYearAsync(entity.Year);
        if (existingRecord != null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt zaten mevcut.");
        }

        var result = await _provider.AddAsync(entity);
        return result;
    }


    public async Task<StampTax> DeleteAsync(Guid id)
    {
        var result = await _provider.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }

        return result;
    }

    public async Task<List<StampTax>> GetByListYearAsync(int year)
    {
        var result = await _provider.GetByListYearAsync(year);
        if (result == null || !result.Any())
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<StampTax> GetByYearAsync(int year)
    {
        var result = await _provider.GetByYearAsync(year);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<List<StampTax>> GetAllAsync()
    {
        var result = await _provider.GetAllAsync();
        return result ?? new List<StampTax>();
    }

    public async Task<StampTax> UpdateAsync(Guid id, StampTax entity)
    {
        var result = await _provider.GetByYearAsync(entity.Year);
        if (result == null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt bulunamadı.");
        }

        result.Year = entity.Year;
        result.Rate = entity.Rate;

        var updatedResult = await _provider.UpdateAsync(result);

        if (updatedResult == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return updatedResult;
    }
    
}
