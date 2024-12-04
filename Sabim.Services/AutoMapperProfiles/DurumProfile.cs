using AutoMapper;
using Sabim.Domain.DTOs.DurumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class DurumProfile : Profile
    {
        public DurumProfile()
        {
            CreateMap<Durum,ResultDurumDto>().ReverseMap();
        }
    }
}
