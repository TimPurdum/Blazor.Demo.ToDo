using System;
using ICC2024_1.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ICC2024_1;


public class ServerRepository(IDbContextFactory<AppDbContext> dbContextFactory) : IRepository
{
    public async Task<int> AddAsync<T>(T entity) where T: class, IIdentity
    {
        await using var dbContext = dbContextFactory.CreateDbContext();
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync<T>(int id) where T: class, IIdentity
    {
        await using var dbContext = dbContextFactory.CreateDbContext();
        var entity = await dbContext.FindAsync<T>(id);
        dbContext.Remove(entity!);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<T>> GetAllAsync<T>() where T: class, IIdentity
    {
        await using var dbContext = dbContextFactory.CreateDbContext();
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetAsync<T>(int id) where T: class, IIdentity
    {
        await using var dbContext = dbContextFactory.CreateDbContext();
        return (await dbContext.FindAsync<T>(id))!;
    }

    public async Task UpdateAsync<T>(T entity) where T: class, IIdentity
    {
        await using var dbContext = dbContextFactory.CreateDbContext();
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
    }
}
