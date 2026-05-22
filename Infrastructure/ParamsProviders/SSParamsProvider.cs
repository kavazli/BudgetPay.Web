using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ParamsProviders;

public class SSParamsProvider : IParamsProvider<SSParams>
{
    private readonly AppDbContext _appContext;

    public SSParamsProvider(AppDbContext appContext)
    {
        _appContext = appContext;
    }

    public async Task<SSParams> AddAsync(SSParams entity)
    {
        var result = await _appContext.SSParams.AddAsync(entity);
        await _appContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<SSParams?> DeleteAsync(Guid id)
    {
        var entity = await _appContext.SSParams.FindAsync(id);
        if (entity == null) return null;
        _appContext.SSParams.Remove(entity);
        await _appContext.SaveChangesAsync();
        return entity;
    }

    public Task<List<SSParams>> GetByListYearAsync(int year)
    {
        var result = _appContext.SSParams.Where(d => d.Year == year).ToListAsync();
        return result;
    }

    public async Task<SSParams?> GetByYearAsync(int year)
    {
        var result = await _appContext.SSParams.FirstOrDefaultAsync(d => d.Year == year);
        return result;
    }

    public async Task<List<SSParams>> GetAllAsync()
    {
        var result = await _appContext.SSParams.ToListAsync();
        return result;
    }


    public async Task<SSParams> UpdateAsync(SSParams entity)
    {
        var result = _appContext.SSParams.Update(entity);
        await _appContext.SaveChangesAsync();
        return result.Entity;
    }
}
