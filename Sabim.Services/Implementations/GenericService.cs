using NLog.Filters;
using Sabim.Domain.DTOs.HelperDtos;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;
using System.Linq.Expressions;

namespace Sabim.Services.Implementations
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly IRepositoryBase<T> _repository;
 
        public GenericService(IRepositoryBase<T> repository)
        {
            _repository = repository;
        }

        public Task<string> TAddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        public bool TIsAny(Expression<Func<T, bool>> predicate, short? excludeId = null)
        {
            return _repository.IsAny(predicate,excludeId);
        }

        public Task<string> TDeleteAsync(short id)
        {
            return _repository.DeleteAsync(id);
        }

        public IQueryable<T> TFindAll(bool trackChanges)
        {

            return _repository.FindAll(trackChanges);
        }

        public Task<List<T>> TFindAllAsync(bool trackChanges)
        {
            return _repository.FindAllAsync(trackChanges);
        }

        public Task<List<T>> TFindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens)
        {
            return _repository.FindAllAsyncWithEntities(trackChanges,childrens);
        }

        public Task<ICollection<T>> TFindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return _repository.FindAllByConditionAsync(expression, trackChanges);
        }

        public Task<T?> TFindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
           return _repository.FindByConditionAsync(expression, trackChanges);
        }

        public Task<T> TGetByIdAsync(int id, bool trackChanges)
        {
             return _repository.GetByIdAsync(id,trackChanges);
        }

        public Task<string> TUpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task<AuditTrailDto?> TGetAuditTrailWithDetailsAsync<TKey>(TKey id)
        {
            return _repository.GetAuditTrailWithDetailsAsync(id);
        }

        public Task<object?> TAddAndGetIdAsync(T entity)
        {
            return _repository.AddAndGetIdAsync(entity);
        }

        public async Task<string> TDeleteRangeByExpressionAsync(Expression<Func<T, bool>> predicate)
        {
            return await _repository.DeleteRangeByExpressionAsync(predicate);
        }

        public async Task<List<T>> TFindByIdAsyncWithEntities(bool trackChanges, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] childrens)
        {
            return await _repository.FindByIdAsyncWithEntities(trackChanges,filter, childrens);
        }

        public async Task<T> TFindByIdWithIncludesAsync<TKey>(TKey id, bool trackChanges,params Expression<Func<T, object>>[] childrens)
        {
            return await _repository.FindByIdWithIncludesAsync(id,trackChanges, childrens);
        }
    }
}
