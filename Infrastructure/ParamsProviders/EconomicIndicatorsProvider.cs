using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ParamsProviders;



public class EconomicIndicatorsProvider : IParamsProvider<EconomicIndicators>
{   

    private readonly AppDbContext _appContext;
    

    public EconomicIndicatorsProvider(AppDbContext appContext)
    {
        _appContext = appContext;
    }



    public async Task<EconomicIndicators> AddAsync(EconomicIndicators entity)
    {
        var result = await _appContext.EconomicIndicators.AddAsync(entity);
        await _appContext.SaveChangesAsync();
        return result.Entity;
    }


    public async Task<EconomicIndicators?> DeleteAsync(Guid id)
    {
        var result = await _appContext.EconomicIndicators.FindAsync(id);
        if (result == null) return null;
        _appContext.EconomicIndicators.Remove(result);
        await _appContext.SaveChangesAsync();
        return result;
    }


    public async Task<List<EconomicIndicators>> GetAllAsync()
    {
        var result = await _appContext.EconomicIndicators.ToListAsync();
        return result;
    }

    public async Task<List<EconomicIndicators>> GetByListYearAsync(int year)
    {
        var result = await _appContext.EconomicIndicators.Where(d => d.Year == year).ToListAsync();
        return result;
    }

    public async Task<EconomicIndicators?> GetByYearAsync(int year)
    {
        var result = await _appContext.EconomicIndicators.FirstOrDefaultAsync(d => d.Year == year);
        return result;
    }

    public async Task<EconomicIndicators> UpdateAsync(EconomicIndicators entity)
    {
        var result = _appContext.EconomicIndicators.Update(entity);
        await _appContext.SaveChangesAsync();
        return result.Entity;
    }
}
