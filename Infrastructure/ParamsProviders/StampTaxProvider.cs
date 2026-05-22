using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ParamsProviders;

public class StampTaxProvider : IParamsProvider<StampTax>
{   

    private readonly AppDbContext _context;

    public StampTaxProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StampTax> AddAsync(StampTax entity)
    {
        var result = await _context.StampTaxes.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<StampTax?> DeleteAsync(Guid id)
    {
        var entity = await _context.StampTaxes.FindAsync(id);
        if (entity == null) return null;
        _context.StampTaxes.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<List<StampTax>> GetByListYearAsync(int year)
    {
        var result = _context.StampTaxes.Where(d => d.Year == year).ToListAsync();
        return result;
    }

    public async Task<StampTax?> GetByYearAsync(int year)
    {
        var result = await _context.StampTaxes.FirstOrDefaultAsync(d => d.Year == year);
        return result;
    }

    public async Task<List<StampTax>> GetAllAsync()
    {
        var result = await _context.StampTaxes.ToListAsync();
        return result;
    }

    public async Task<StampTax> UpdateAsync(StampTax entity)
    {
        var result = _context.StampTaxes.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }
}
