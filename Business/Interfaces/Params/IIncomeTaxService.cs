using System;

namespace Business.Interfaces.Params;

public interface IIncomeTaxService<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T> GetByYearAsync(int year);
    Task<T> GetByYearAndBracketAsync(int year, int bracket);
    Task<List<T>> GetByListYearAsync(int year);
    Task<List<T>> GetAllAsync();
    Task<T> UpdateAsync(Guid id, T entity);
    Task<T> DeleteAsync(Guid id);
}
