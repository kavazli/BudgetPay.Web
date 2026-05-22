using System;
using Business.Interfaces.Params;
using Domain.Enums;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ParamsProviders;

public class DisabilityDegreeProvider : IDisabilityDegreeProvider<DisabilityDegree>
{   

    private readonly AppDbContext _context;

    public DisabilityDegreeProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DisabilityDegree> AddAsync(DisabilityDegree entity)
    {
        var result = await _context.DisabilityDegrees.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<DisabilityDegree?> DeleteAsync(Guid id)
    {
        var entity = await _context.DisabilityDegrees.FindAsync(id);
        if (entity == null) return null;
        _context.DisabilityDegrees.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<List<DisabilityDegree>> GetByListYearAsync(int year)
    {
        var result = _context.DisabilityDegrees.Where(d => d.Year == year).ToListAsync();
        return result;
    }

    public async Task<DisabilityDegree?> GetByYearAsync(int year)
    {
        var result = await _context.DisabilityDegrees.FirstOrDefaultAsync(d => d.Year == year);
        return result;
    }

    public async Task<List<DisabilityDegree>> GetAllAsync()
    {
        var result = await _context.DisabilityDegrees.ToListAsync();
        return result;
    }

    public async Task<DisabilityDegree> UpdateAsync(DisabilityDegree entity)
    {
        var result = _context.DisabilityDegrees.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<DisabilityDegree?> GetByYearAndDegreeAsync(int year, Degree degree)
    {
        var result = await _context.DisabilityDegrees.FirstOrDefaultAsync(d => d.Year == year && d.Degree == degree);
        return result;
    }
}
