using Sabim.Domain.DTOs.HelperDtos;
using System.Linq.Expressions;

namespace Sabim.Services.Contracts
{
    public interface IGenericService<T> where T : class
    {
        IQueryable<T> TFindAll(bool trackChanges);
        Task<T?> TFindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<ICollection<T>> TFindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> TGetByIdAsync(int id,bool trackChanges);
        Task<string> TAddAsync(T entity);
        Task<string> TUpdateAsync(T entity);
        Task<string> TDeleteAsync(short id);
        Task<string> TDeleteRangeByExpressionAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> TFindAllAsync(bool trackChanges);
        Task<List<T>> TFindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens);
        bool TIsAny(Expression<Func<T, bool>> predicate, short? excludeId = null);
        Task<AuditTrailDto?> TGetAuditTrailWithDetailsAsync<TKey>(TKey id);
        Task<object?> TAddAndGetIdAsync(T entity);
    }
}
