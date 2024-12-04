using AutoMapper;
using Sabim.Domain.DTOs.KisimDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class KisimProfile : Profile
    {
        public KisimProfile()
        {
            CreateMap<Kisim, ResultKisimWithPersonelCountDto>()
                .ForMember(dest => dest.BirimId, opt => opt.MapFrom(src => src.BirimId))
                .ForMember(dest => dest.BirimAdi, opt => opt.MapFrom(src => src.Birim.BirimAdi))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => src.PersonelGorevlendirilmes.Count))
                .ReverseMap();
        }
    }
}
