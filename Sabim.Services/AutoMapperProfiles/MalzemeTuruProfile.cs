using AutoMapper;
using Sabim.Domain.DTOs.MalzemeTuruDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class MalzemeTuruProfile : Profile
    {
        public MalzemeTuruProfile()
        {
            CreateMap<MalzemeTuru, ResultMalzemeTuruDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
            CreateMap<CreateMalzemeTuruDto, MalzemeTuru>();
            CreateMap<UpdateMalzemeTuruDto, MalzemeTuru>();
        }
    }
}
