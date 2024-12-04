using System.Linq.Expressions;

namespace Sabim.Services.Contracts
{
    public interface IGenericService<T> where T : class
    {
        IQueryable<T> TFindAll(bool trackChanges);
        Task<T?> TFindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<ICollection<T>> TFindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> TGetByIdAsync(int id);
        Task TAddAsync(T entity);
        Task TUpdateAsync(T entity);
        Task TDeleteAsync(int id);
        Task<List<T>> TFindAllAsync(bool trackChanges);
        Task<List<T>> TFindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens);
    }
}
