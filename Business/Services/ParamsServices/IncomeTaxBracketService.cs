using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;

namespace Business.Services.ParamsServices;

public class IncomeTaxBracketService : IIncomeTaxService<IncomeTaxBracket>
{

    private readonly IIncomeTaxProvider<IncomeTaxBracket> _provider;


    public IncomeTaxBracketService(IIncomeTaxProvider<IncomeTaxBracket> provider)
    {
        _provider = provider;
    }


    public async Task<IncomeTaxBracket> AddAsync(IncomeTaxBracket entity)
    {
        var existingRecord = await _provider.GetByYearAndBracketAsync(entity.Year, entity.Bracket);
        if (existingRecord != null)
        {
            throw new InvalidOperationException($"{entity.Year} ve {entity.Bracket} dilimine ait kayıt zaten mevcut.");
        }

        var result = await _provider.AddAsync(entity);
        return result;
    }


    public async Task<IncomeTaxBracket> DeleteAsync(Guid id)
    {
        var result = await _provider.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }

        return result;
    }


    public async Task<IncomeTaxBracket> GetByYearAsync(int year)
    {
        var result = await _provider.GetByYearAsync(year);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<List<IncomeTaxBracket>> GetByListYearAsync(int year)
    {
        var result = await _provider.GetByListYearAsync(year);
        if (result == null || !result.Any())
        {
            throw new InvalidOperationException($"{year} yılına ait kayıt bulunamadı.");
        }
        return result;
    }

    public async Task<List<IncomeTaxBracket>> GetAllAsync()
    {
        var result = await _provider.GetAllAsync();
        return result ?? new List<IncomeTaxBracket>();
    }


    public async Task<IncomeTaxBracket> UpdateAsync(Guid id, IncomeTaxBracket entity)
    {
        var result = await _provider.GetByYearAndBracketAsync(entity.Year, entity.Bracket);
        if (result == null)
        {
            throw new InvalidOperationException($"{entity.Year} yılı ve {entity.Bracket} dilimine ait kayıt bulunamadı.");
        }

        result.Year = entity.Year;
        result.Bracket = entity.Bracket;
        result.MinAmount = entity.MinAmount;
        result.MaxAmount = entity.MaxAmount;
        result.Rate = entity.Rate;

        var updatedResult = await _provider.UpdateAsync(result);

        if (updatedResult == null)
        {
            throw new InvalidOperationException($"{id} ID'sine ait kayıt bulunamadı.");
        }
        return updatedResult;
    }

    public async Task<IncomeTaxBracket> GetByYearAndBracketAsync(int year, int bracket)
    {
        var result = await _provider.GetByYearAndBracketAsync(year, bracket);
        if (result == null)
        {
            throw new InvalidOperationException($"{year} yılı ve {bracket} dilimine ait kayıt bulunamadı.");
        }
        return result;
    }

}
