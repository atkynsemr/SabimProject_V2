using Sabim.Domain.Entities;
using Sabim.Domain.DTOs.UnvanDtos;

namespace Sabim.Services.Contracts
{
    public interface IUnvanService : IGenericService<Unvan>
    {
        Task<List<ResultUnvanWithPersonelCountDto>> TGetAllUnvanWithPersonelCountAsync(bool trackChanges);
        Task<string> TChangeOncelikSirasi(byte oncelikSirasi, short? UnvanID);
    }
}
