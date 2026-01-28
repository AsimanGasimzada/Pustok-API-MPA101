using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities.Common;
using Pustok.DataAccess.Contexts;
using Pustok.DataAccess.Repositories.Abstractions.Generic;
using System.Linq.Expressions;

namespace Pustok.DataAccess.Repositories.Implementations.Generic;

internal class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }


    //Func<int,string,bool> func=(id,name)=>{if(id==1)return true;}
    //Func<T,bool> func2=(category)=>category.Name=="Category1"
    public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        var result = await _context.Set<T>().AnyAsync(expression);

        return result;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAll(bool ignoreQueryFilter = false) //(SELECT * FROM Products ).Include(x=>x.Category).OrderBy(x=>x.Id).GroupBy(x=>x.Id)
    {
        var query = _context.Set<T>().AsQueryable();

        if (ignoreQueryFilter)
            query = query.IgnoreQueryFilters();


        return query;
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(expression);

        return entity;
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var result = await _context.Set<T>().FindAsync(id);

        return result;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
}
