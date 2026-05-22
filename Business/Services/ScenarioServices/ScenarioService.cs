using Business.Interfaces.Scenario;
using Domain.Scenario;

namespace Business.Services.ScenarioServices;

public class ScenarioService : IScenarioService<ScenarioModel>
{   

    private readonly IScenarioProvider<ScenarioModel> _provider;


    public ScenarioService(IScenarioProvider<ScenarioModel> provider)
    {
        _provider = provider;
    }

    public async Task<ScenarioModel> AddAsync(ScenarioModel entity)
    {
        var result = await _provider.AddAsync(entity);
        return result;
    }

    public async Task<ScenarioModel> DeleteAsync(Guid id)
    {   
        var result = await _provider.DeleteAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"{id} adına ait kayıt bulunamadı.");
        }
        return result;
    }   
       
    public async Task<List<ScenarioModel>> GetAllAsync()
    {
        var result = await _provider.GetAllAsync();
        return result ?? new List<ScenarioModel>();
    }

    public async Task<ScenarioModel?> GetByNameAsync(string name)
    {
        var result = await _provider.GetByNameAsync(name);
        return result;
    }

    public async Task<ScenarioModel> UpdateAsync(ScenarioModel entity)
    {   

        var tempEntity = await _provider.GetByNameAsync(entity.ScenarioName);
        if (tempEntity == null)
        {
            throw new InvalidOperationException($"{entity.ScenarioName} adına ait kayıt bulunamadı.");
        }

        tempEntity.Company = entity.Company;
        tempEntity.Year = entity.Year;
        tempEntity.ScenarioName = entity.ScenarioName;
        tempEntity.EconomicIndicator = entity.EconomicIndicator;
        tempEntity.WelfarShare = entity.WelfarShare;
        tempEntity.RafeOfIncrase = entity.RafeOfIncrase;
        tempEntity.TotalRate = entity.TotalRate;
        tempEntity.Overtime_50 = entity.Overtime_50;
        tempEntity.Overtime_100 = entity.Overtime_100;
        tempEntity.Bonus = entity.Bonus;
        tempEntity.BonusMonth = entity.BonusMonth;
        tempEntity.ShoppingVoucher = entity.ShoppingVoucher;
        tempEntity.ShoppingVoucherMonth = entity.ShoppingVoucherMonth;


        var result = await _provider.UpdateAsync(tempEntity);
        if (result == null)        
        {
            throw new InvalidOperationException($"{entity.ScenarioName} adına ait kayıt bulunamadı.");     
        }
        return result;
    }
    
    public async Task<ScenarioModel> DeleteAllAsync()
    {
        var result = await _provider.DeleteAllAsync();
        return result;
    }
}
