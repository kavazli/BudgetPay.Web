using System;
using System.Net.WebSockets;
using Business.Interfaces.Params;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.ParamsProviders;

public class MinimumWageProvider : IParamsProvider<MinimumWage>
{
    private readonly AppDbContext _context;

    public MinimumWageProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MinimumWage> AddAsync(MinimumWage entity)
    {
        var result = await _context.MinimumWages.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<MinimumWage?> DeleteAsync(Guid id)
    {
        var entity = await _context.MinimumWages.FindAsync(id);
        if (entity == null) return null;
        _context.MinimumWages.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<List<MinimumWage>> GetByListYearAsync(int year)
    {
        var result = _context.MinimumWages.Where(d => d.Year == year).ToListAsync();
        return result;
    }

    public async Task<MinimumWage?> GetByYearAsync(int year)
    {
        var result = await _context.MinimumWages.FirstOrDefaultAsync(d => d.Year == year);
        return result;
    }

    public async Task<List<MinimumWage>> GetAllAsync()
    {
        var result = await _context.MinimumWages.ToListAsync();
        return result;
    }


    public async Task<MinimumWage> UpdateAsync(MinimumWage entity)
    {
        var result = _context.MinimumWages.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}
