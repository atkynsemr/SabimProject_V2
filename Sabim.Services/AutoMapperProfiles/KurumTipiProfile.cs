using AutoMapper;
using Sabim.Domain.DTOs.KurumTipiDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class KurumTipiProfile:Profile
    {
        public KurumTipiProfile()
        {
            CreateMap<KurumTipi, CreateKurumTipiDto>().ReverseMap();
            CreateMap<KurumTipi, UpdateKurumTipiDto>().ReverseMap();
            CreateMap<KurumTipi, ResultKurumTipiDto>().ReverseMap();
            CreateMap<KurumTipi, ResultKurumTipiWithKurumCountDto>()
                    .ForMember(dest => dest.KurumSayisi, opt => opt.MapFrom(src => (ushort)src.Kurums.Count()))
                    .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                    .ReverseMap();
        }
    }
}
