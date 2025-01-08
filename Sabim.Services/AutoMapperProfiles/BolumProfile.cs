using AutoMapper;
using Sabim.Domain.DTOs.BolumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class BolumProfile : Profile
    {
        public BolumProfile()
        {
            CreateMap<Bolum, CreateBolumDto>().ReverseMap();
            CreateMap<Bolum, UpdateBolumDto>().ReverseMap();
            CreateMap<Bolum, ResultBolumDto>().ReverseMap();
            CreateMap<Bolum, ResultBolumWithBirimCountDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.BirimSayisi, opt => opt.MapFrom(src => (ushort)src.Birims.Count))
                .ReverseMap();
        }
    }
}
