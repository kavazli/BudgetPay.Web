using System;
using Business.Interfaces.Params;
using Domain.Enums;
using Domain.TaxParameters;

namespace Business.Services.ParamsServices;

public class DisabilityDegreeService : IDisabilityDegreService<DisabilityDegree>
{   

    private readonly IDisabilityDegreeProvider<DisabilityDegree> _provider;


    public DisabilityDegreeService(IDisabilityDegreeProvider<DisabilityDegree> provider)
    {
        _provider = provider;
    }


    public async Task<DisabilityDegree> AddAsync(DisabilityDegree entity)
    {
        var existingRecord = await _provider.GetByYearAndDegreeAsync(entity.Year, entity.Degree);
        if (existingRecord != null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ve {entity.Degree} derecesine ait kayıt zaten mevcut.");
        }

        var result = await _provider.AddAsync(entity);
        return result;
    }


    public async Task<DisabilityDegree> DeleteAsync(Guid id)
    {
        var result = await _provider.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }

        return result;
    }

    public async Task<List<DisabilityDegree>> GetByListYearAsync(int year)
    {
        var result = await _provider.GetByListYearAsync(year);
        if (result == null || !result.Any())
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<DisabilityDegree> GetByYearAsync(int year)
    {
        var result = await _provider.GetByYearAsync(year);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        } 
        return result;
    }

    public async Task<List<DisabilityDegree>> GetAllAsync()
    {
        var result = await _provider.GetAllAsync();
        return result ?? new List<DisabilityDegree>();
    }

    public async Task<DisabilityDegree> UpdateAsync(Guid id, DisabilityDegree entity)
    {
        var result = await _provider.GetByYearAsync(entity.Year);
        if (result == null)
        {
            throw new InvalidOperationException($"{entity.Year} yılına ait kayıt bulunamadı.");
        }

        result.Year = entity.Year;
        result.Degree = entity.Degree;
        result.Amount = entity.Amount;

        var updatedResult = await _provider.UpdateAsync(result);

        if (updatedResult == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return updatedResult;
    }

    public async Task<DisabilityDegree> GetByYearAndDegreeAsync(int year, Degree degree)
    {
        var result = await _provider.GetByYearAndDegreeAsync(year, degree);
        if (result == null)        
        {
            throw new InvalidOperationException($"{year} yılına ve {degree} derecesine ait kayıt bulunamadı.");
        }
        return result;
    }
}
