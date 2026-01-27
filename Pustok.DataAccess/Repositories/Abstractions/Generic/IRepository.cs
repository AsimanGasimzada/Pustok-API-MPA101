using Pustok.Core.Entities.Common;
using System.Linq.Expressions;

namespace Pustok.DataAccess.Repositories.Abstractions.Generic;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(Guid id);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<int> SaveChangesAsync();
}
