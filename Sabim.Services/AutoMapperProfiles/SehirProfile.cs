using AutoMapper;
using Sabim.Domain.DTOs.SehirDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class SehirProfile : Profile
    {
        public SehirProfile()
        {
            CreateMap<Sehir, ResultSehirDto>().ReverseMap();
            CreateMap<Sehir, ResultSehirWithKurumCountDto>()
                .ForMember(dest => dest.KurumSayisi, opt => opt.MapFrom(src => (ushort)src.Kurums.Count()))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
