using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MicroLab.Invoicer.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace MicroLab.Invoicer.Shared.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public InvoicerDbContext _dataDbContext { get; set; }

        public BaseRepository(InvoicerDbContext dataDbContext)
        {
            _dataDbContext = dataDbContext;
        }
        public async Task Add(T entity)
        {
            await _dataDbContext.AddAsync(entity);
            _dataDbContext.Entry(entity).State = EntityState.Added;
            await _dataDbContext.SaveChangesAsync();
        }

        public async Task AddRange(List<T> entity)
        {
            await _dataDbContext.AddRangeAsync(entity);
            await _dataDbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
             _dataDbContext.Remove(entity);
            await _dataDbContext.SaveChangesAsync();
        }

        public async Task DeleteRange(List<T> entity)
        {
            _dataDbContext.RemoveRange(entity);
            await _dataDbContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
             _dataDbContext.Update(entity);
            await _dataDbContext.SaveChangesAsync();
        }

        public async Task UpdateRange(List<T> entity)
        {
            _dataDbContext.UpdateRange(entity);
            await _dataDbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> GetAll()
        {
            var entitySet = _dataDbContext.Set<T>();
            return entitySet.AsQueryable();
        }

        public Task<T> GetFirst(Expression<Func<T, bool>> predicate)
        {
            return GetAll().FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }
    }
}
