using System;

namespace Business.Interfaces.Params;

public interface IParamsProvider<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T?> GetByYearAsync(int year);
    Task<List<T>> GetByListYearAsync(int year);
    Task<List<T>> GetAllAsync();
    Task<T> UpdateAsync(T entity);
    Task<T?> DeleteAsync(Guid id);
}
