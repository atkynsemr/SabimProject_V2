using Sabim.Domain.DTOs.HelperDtos;
using System.Linq.Expressions;

namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> FindAll(bool trackChanges);
        Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<ICollection<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> GetByIdAsync<TKey>(TKey id, bool trackChanges);
        Task<string> AddAsync(T entity);
        Task<string> UpdateAsync(T entity);
        Task<string> DeleteAsync<TKey>(TKey id);
        Task<string> DeleteRangeByExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> FindAllAsync(bool trackChanges);
        Task<List<T>> FindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens);
        bool IsAny(Expression<Func<T, bool>> predicate, short? excludeId = null);
        Task<AuditTrailDto?> GetAuditTrailWithDetailsAsync<TKey>(TKey id);
        Task<object?> AddAndGetIdAsync(T entity);

    }
}
