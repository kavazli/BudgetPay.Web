using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;

namespace Business.Services.ParamsServices;

public class EconomicIndicatorsService : IParamsService<EconomicIndicators>
{   
    private readonly IParamsProvider<EconomicIndicators>  _paramsService;

    public EconomicIndicatorsService(IParamsProvider<EconomicIndicators> paramsProvider)
    {
        _paramsService = paramsProvider;
    }


    public async Task<EconomicIndicators> AddAsync(EconomicIndicators entity)
    {
        var existingRecord = await _paramsService.GetByYearAsync(entity.Year);
        if (existingRecord != null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt zaten mevcut.");
        }
        var result = await _paramsService.AddAsync(entity);
        return result;
    }

    public async Task<EconomicIndicators> DeleteAsync(Guid id)
    {
        var result = await _paramsService.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<List<EconomicIndicators>> GetAllAsync()
    {
        var result = await _paramsService.GetAllAsync();
        return result ?? new List<EconomicIndicators>();
    }

    public async Task<List<EconomicIndicators>> GetByListYearAsync(int year)
    {
        var result = await _paramsService.GetByListYearAsync(year);
        if (result == null || !result.Any())
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<EconomicIndicators> GetByYearAsync(int year)
    {
        var result = await _paramsService.GetByYearAsync(year);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<EconomicIndicators> UpdateAsync(Guid id, EconomicIndicators entity)
    {
        var result = await _paramsService.GetByYearAsync(entity.Year);
        if (result == null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt bulunamadı.");
        }

        result.Year = entity.Year;
        result.MinimumWageIncreaseRate = entity.MinimumWageIncreaseRate;
        result.InflationRate = entity.InflationRate;
        result.RevaluationRate = entity.RevaluationRate;

        var updatedResult = await _paramsService.UpdateAsync(result);

        if (updatedResult == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return updatedResult;
    }

}

