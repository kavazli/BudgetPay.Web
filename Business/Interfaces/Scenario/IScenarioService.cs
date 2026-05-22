namespace Business.Interfaces.Scenario;

public interface IScenarioService<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T?> GetByNameAsync(string name);
    Task<List<T>> GetAllAsync();
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(Guid id);
    
    Task<T> DeleteAllAsync();
    

}
