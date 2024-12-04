using AutoMapper;
using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class BirimProfile : Profile
    {
        public BirimProfile()
        {
            CreateMap<Birim,ResultBirimWithKisimCount>()
                .ForMember(dest => dest.BolumId, opt=> opt.MapFrom(src => src.BolumId))
                .ForMember(dest => dest.BolumAdi, opt => opt.MapFrom(src => src.Bolum.BolumAdi))
                .ForMember(dest => dest.DurumAdi, opt=> opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.KisimSayisi, opt => opt.MapFrom(src => src.Kisims.Count))
                .ReverseMap();
        }
    }
}
