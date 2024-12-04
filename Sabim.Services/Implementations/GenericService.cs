using AutoMapper;
using Sabim.Domain.Entities;
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

        public Task TAddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task TDeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<T> TGetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task TUpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
