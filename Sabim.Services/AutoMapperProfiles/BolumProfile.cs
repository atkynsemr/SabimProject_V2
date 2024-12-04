using AutoMapper;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class BolumProfile : Profile
    {
        public BolumProfile()
        {
            CreateMap<Bolum, ResultBolumDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();

            CreateMap<Bolum, ResultBolumWithBirimCountDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.BirimSayisi, opt => opt.MapFrom(src => src.Birims.Count))
                .ReverseMap();
        }
    }
}
