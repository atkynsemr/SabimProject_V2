using AutoMapper;
using Sabim.Domain.DTOs.UnvanDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class UnvanProfile : Profile
    {
        public UnvanProfile()
        {
            CreateMap<Unvan, ResultUnvanDto>().ReverseMap();
            CreateMap<Unvan, ResultUnvanWithPersonelCountDto>()
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
