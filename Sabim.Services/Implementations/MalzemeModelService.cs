using Sabim.Domain.Entities;
using Sabim.Infrastructure.Persistence.Repository.Contracts;
using Sabim.Services.Contracts;

namespace Sabim.Services.Implementations
{
    public class MalzemeModelService : GenericService<MalzemeModel>, IMalzemeModelService
    {
        public MalzemeModelService(IRepositoryBase<MalzemeModel> repository) : base(repository)
        {
        }
    }
}
