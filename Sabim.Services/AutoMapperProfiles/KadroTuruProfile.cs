using AutoMapper;
using Sabim.Domain.DTOs.KadroTuruDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class KadroTuruProfile :Profile
    {
        public KadroTuruProfile()
        {
            CreateMap<KadroTuru,CreateKadroTuruDto>().ReverseMap();
            CreateMap<KadroTuru,UpdateKadroTuruDto>().ReverseMap();
            CreateMap<KadroTuru,KadroTuruBaseDto>().ReverseMap();
            CreateMap<KadroTuru,ResultKadroTuruWithPersonelCountDto>()
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
