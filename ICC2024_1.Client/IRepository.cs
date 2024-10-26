using System;

namespace ICC2024_1.Client;

public interface IRepository
{
    Task<T> GetAsync<T>(int id) where T: class, IIdentity;
    Task<List<T>> GetAllAsync<T>() where T: class, IIdentity;
    Task<int> AddAsync<T>(T entity) where T: class, IIdentity;
    Task UpdateAsync<T>(T entity) where T: class, IIdentity;
    Task DeleteAsync<T>(int id) where T: class, IIdentity;
}