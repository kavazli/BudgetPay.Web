using Business.Interfaces.Scenario;
using Domain.Scenario;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.ScenarioProviders;

public class ScenarioProvider : IScenarioProvider<ScenarioModel>
{   

    private readonly AppDbContext _context;

    public ScenarioProvider(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ScenarioModel> AddAsync(ScenarioModel entity)
    {
        var result = await _context.Scenarios.AddAsync(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<ScenarioModel?> DeleteAsync(Guid id)
    {
        var entity = await _context.Scenarios.FirstOrDefaultAsync(s => s.Id == id);
        if (entity == null) return null;
        _context.Scenarios.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<ScenarioModel>> GetAllAsync()
    {
        var result = await _context.Scenarios.ToListAsync();
        return result;
    }

    public async Task<ScenarioModel?> GetByNameAsync(string name)
    {
        var result = await _context.Scenarios.FirstOrDefaultAsync(d => d.ScenarioName == name);
        return result;
    }

    public async Task<ScenarioModel> UpdateAsync(ScenarioModel entity)
    {   
        _context.Scenarios.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task<ScenarioModel> DeleteAllAsync()
    {
        var entities = await _context.Scenarios.ToListAsync();
        _context.Scenarios.RemoveRange(entities);
        await _context.SaveChangesAsync();
        return new ScenarioModel();
    }
}
