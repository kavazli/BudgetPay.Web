using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;

namespace Business.Services.ParamsServices;

public class SSParamsService : IParamsService<SSParams>
{   

    private readonly IParamsProvider<SSParams> _provider;


    public SSParamsService(IParamsProvider<SSParams> provider)
    {
        _provider = provider;
    }


    public async Task<SSParams> AddAsync(SSParams entity)
    {
        var existingRecord = await _provider.GetByYearAsync(entity.Year);
        if (existingRecord != null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt zaten mevcut.");
        }

        var result = await _provider.AddAsync(entity);
        return result;
    }


    public async Task<SSParams> DeleteAsync(Guid id)
    {
        var result = await _provider.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }

        return result;
    }

    public async Task<List<SSParams>> GetByListYearAsync(int year)
    {   
        var result = await _provider.GetByListYearAsync(year);
        if (result == null || !result.Any())
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<SSParams> GetByYearAsync(int year)
    {   
        var result = await _provider.GetByYearAsync(year);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<List<SSParams>> GetAllAsync()
    {
        var result = await _provider.GetAllAsync();
        return result ?? new List<SSParams>();
    }

    public async Task<SSParams> UpdateAsync(Guid id, SSParams entity)
    {
        var result = await _provider.GetByYearAsync(entity.Year);
        if (result == null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt bulunamadı.");
        }

        result.Year = entity.Year;
        result.ActiveEmployeeSSRate = entity.ActiveEmployeeSSRate;
        result.ActiveEmployeeUIRate = entity.ActiveEmployeeUIRate;
        result.ActiveEmployerSSRate = entity.ActiveEmployerSSRate;
        result.ActiveEmployerUIRate = entity.ActiveEmployerUIRate;
        result.RetiredEmployeeSSRate = entity.RetiredEmployeeSSRate;
        result.RetiredEmployerSSRate = entity.RetiredEmployerSSRate;

        var updatedResult = await _provider.UpdateAsync(result);

        if (updatedResult == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return updatedResult;
    }
}
