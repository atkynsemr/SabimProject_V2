using AutoMapper;
using Sabim.Domain.DTOs.MalzemeDurumuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class MalzemeDurumuProfile : Profile
    {
        public MalzemeDurumuProfile()
        {
            CreateMap<MalzemeDurumu, ResultMalzemeDurumuDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
            CreateMap<CreateMalzemeDurumuDto, MalzemeDurumu>();
            CreateMap<UpdateMalzemeDurumuDto, MalzemeDurumu>();
        }
    }
}
