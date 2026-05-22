using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Interfaces.Params;
using Domain.TaxParameters;

namespace Business.Services.ParamsServices;

public class MinimumWageService : IParamsService<MinimumWage>
{   

    private readonly IParamsProvider<MinimumWage> _provider;


    public MinimumWageService(IParamsProvider<MinimumWage> provider)
    {
        _provider = provider;
    }


    public async Task<MinimumWage> AddAsync(MinimumWage entity)
    {   

        var existingRecord = await _provider.GetByYearAsync(entity.Year);
        if (existingRecord != null)
        {            
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt zaten mevcut.");
        }

        var result = await _provider.AddAsync(entity);
        return result;
    }

    public async Task<MinimumWage> DeleteAsync(Guid id)
    {   
        var result = await _provider.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }

        return result;
    }

    public async Task<List<MinimumWage>> GetByListYearAsync(int year)
    {   
        var result = await _provider.GetByListYearAsync(year);
        if (result == null || !result.Any())
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<List<MinimumWage>> GetAllAsync()
    {
        var result = await _provider.GetAllAsync();
        return result ?? new List<MinimumWage>();
    }

    public async Task<MinimumWage> GetByYearAsync(int year)
    {
        var result = await _provider.GetByYearAsync(year);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<MinimumWage> UpdateAsync(Guid id, MinimumWage entity)
    {   

        var result = await _provider.GetByYearAsync(entity.Year);
        if (result == null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt bulunamadı.");
        }

        result.Year = entity.Year;
        result.GrossSalary = entity.GrossSalary;
        result.NetSalary = entity.NetSalary;
        result.RetiredNetSalary = entity.RetiredNetSalary;
        result.Ceiling = entity.Ceiling;

        var updatedResult = await _provider.UpdateAsync(result);
        
        if(updatedResult == null )
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return updatedResult;
    }
    
}
