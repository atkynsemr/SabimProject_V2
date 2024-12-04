using Microsoft.EntityFrameworkCore;
using Sabim.Infrastructure.Persistence.Context;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using System.Linq.Expressions;

namespace Sabim.Infrastructure.Persistence.Repository.Implementations
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected SabimDbContext _context;
        public RepositoryBase(SabimDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Entity { get => _context.Set<T>(); }
        public async Task AddAsync(T entity) => await Entity.AddAsync(entity);
        public async Task DeleteAsync(int id)
        {
            var entity = await Entity.FindAsync(id); 
            if (entity == null)
            {
                throw new KeyNotFoundException($"ID {id} ile kayıt bulunamadı.");
            }
            Entity.Remove(entity);
        }
        public IQueryable<T> FindAll(bool trackChanges) => !trackChanges ? Entity.AsNoTracking() : Entity;

        public async Task<List<T>> FindAllAsync(bool trackChanges) 
        {
            return trackChanges ? await Entity.ToListAsync(): await Entity.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> FindAllAsyncWithEntities(bool trackChanges, params Expression<Func<T, object>>[] childrens)
        {
            IQueryable<T> query = Entity;

            // Dinamik olarak include işlemi yapıyoruz
            if (childrens.Any())
            {
                foreach (var includeExpression in childrens)
                {
                    query = query.Include(includeExpression); // Include edilen ilişkiler
                }
            }

            // Eğer tracking yapılacaksa
            if (trackChanges)
            {
                return await query.ToListAsync();
            }
            else
            {
                // Tracking yapılmayacaksa, AsNoTracking kullan
                return await query.AsNoTracking().ToListAsync();
            }
        }

        public async Task<ICollection<T>> FindAllByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) 
        {
            var query = Entity.Where(expression);
            if (!trackChanges)
            {
                query = query.AsNoTracking();  
            }
            return await query.ToListAsync();  
        }
        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            IQueryable<T> query = Entity;
            if (!trackChanges)
            {
                query = query.AsNoTracking(); 
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            var result = await Entity.FindAsync(id);
            if (result == null)
            {
                throw new KeyNotFoundException($"ID {id} ile kayıt bulunamadı.");
            }
            return result;
        }
        public async Task UpdateAsync(T entity)
        {
             Entity.Update(entity); 
        }
    }
}
