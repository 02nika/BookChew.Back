using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;

namespace Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryBase(DbContext dbContext)
    {
        DbSet = dbContext.Set<T>();
    }

    private DbSet<T> DbSet { get; }

    public IQueryable<T> FindAll() => DbSet;
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => DbSet.Where(expression);

    public async Task Create(T entity) => await DbSet.AddAsync(entity);

    public void Update(T entity) => DbSet.Update(entity);

    public void Delete(T entity) => DbSet.Remove(entity);
}