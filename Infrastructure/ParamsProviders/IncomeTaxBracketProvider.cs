using System;
using Business.Interfaces.Params;
using Domain.TaxParameters;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ParamsProviders;

public class IncomeTaxBracketProvider : IIncomeTaxProvider<IncomeTaxBracket>
{
    private readonly AppDbContext _context;

    public IncomeTaxBracketProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IncomeTaxBracket> AddAsync(IncomeTaxBracket entity)
    {
        var result = await _context.IncomeTaxBrackets.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IncomeTaxBracket?> DeleteAsync(Guid id)
    {
        var entity = await _context.IncomeTaxBrackets.FindAsync(id);
        if (entity == null) return null;
        _context.IncomeTaxBrackets.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IncomeTaxBracket?> GetByYearAsync(int year)
    {
        var result = await _context.IncomeTaxBrackets.FirstOrDefaultAsync(e => e.Year == year);
        return result;
    }

    public async Task<List<IncomeTaxBracket>> GetByListYearAsync(int year)
    {
        var result = await _context.IncomeTaxBrackets.Where(e => e.Year == year).ToListAsync();
        return result;
    }


    public async Task<List<IncomeTaxBracket>> GetAllAsync()
    {
        var result = await _context.IncomeTaxBrackets.ToListAsync();
        return result;
    }


    public async Task<IncomeTaxBracket> UpdateAsync(IncomeTaxBracket entity)
    {
        var result = _context.IncomeTaxBrackets.Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IncomeTaxBracket?> GetByYearAndBracketAsync(int year, int bracket)
    {
        var result = await _context.IncomeTaxBrackets.FirstOrDefaultAsync(e => e.Year == year && e.Bracket == bracket);
        return result;
    }

}
