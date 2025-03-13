using AutoMapper;
using Sabim.Domain.DTOs.MalzemeCinsiDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class MalzemeCinsiProfile : Profile
    {
        public MalzemeCinsiProfile()
        {
            CreateMap<MalzemeCinsi, ResultMalzemeCinsiDto>()
               .ForMember(dest => dest.TurAdi, opt => opt.MapFrom(src => src.MalzemeTuru.TurAdi))
               .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
            CreateMap<CreateMalzemeCinsiDto, MalzemeCinsi>();
            CreateMap<UpdateMalzemeCinsiDto, MalzemeCinsi>();
        }
    }
}
