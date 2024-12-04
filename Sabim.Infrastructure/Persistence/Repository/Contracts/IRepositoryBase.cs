using Sabim.Domain.Entities;
using System.Linq.Expressions;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
            IQueryable<T> FindAll(bool trackChanges);
            Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
            Task<ICollection<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
            Task<T> GetByIdAsync(int id);
            Task AddAsync(T entity);
            Task UpdateAsync(T entity);
            Task DeleteAsync(int id);
            Task<List<T>> FindAllAsync(bool trackChanges);
            Task<List<T>> FindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens);
    }
}
