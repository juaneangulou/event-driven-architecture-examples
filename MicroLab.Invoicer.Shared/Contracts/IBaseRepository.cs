using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.Invoicer.Shared.Contracts
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);
        Task AddRange(List<T> entity);
        Task Delete(T entity);
        Task DeleteRange(List<T> entity);
        Task Update(T entity);
        Task UpdateRange(List<T> entity);
        IQueryable<T> GetAll();
        Task<T> GetFirst(Expression<Func<T, bool>> predicate);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
    }
}
