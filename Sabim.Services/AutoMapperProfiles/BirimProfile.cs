using AutoMapper;
using Sabim.Domain.DTOs.BirimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class BirimProfile : Profile
    {
        public BirimProfile()
        {
            CreateMap<Birim, CreateBirimDto>().ReverseMap();
            CreateMap<Birim, UpdateBirimDto>().ReverseMap();
            CreateMap<Birim, ResultBirimDto>().ReverseMap();
            CreateMap<Birim, ResultBirimWithKisimCountDto>()
                .ForMember(dest => dest.BolumAdi, opt => opt.MapFrom(src => src.Bolum.BolumAdi))
                .ForMember(dest => dest.DurumAdi, opt=> opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.KisimSayisi, opt => opt.MapFrom(src => (ushort)src.Kisims.Count))
                .ReverseMap();
        }
    }
}
