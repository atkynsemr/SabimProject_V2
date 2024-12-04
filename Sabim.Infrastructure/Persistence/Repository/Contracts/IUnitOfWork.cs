namespace Sabim.Infrastructure.Persistence.Repository.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryBase<T> GetRepository<T>() where T : class;
        Task<int> SaveChangesAsync();
        int SaveChanges();  
    }
}
