using AutoMapper;
using Sabim.Domain.DTOs.KurumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class KurumProfile : Profile
    {
        public KurumProfile()
        {
            CreateMap<Kurum, CreateKurumDto>().ReverseMap();
            CreateMap<Kurum, UpdateKurumDto>().ReverseMap();
            CreateMap<Kurum, ResultKurumDto>().ReverseMap();
            CreateMap<Kurum, ResultKurumWithPersonelCountDto>()
                .ForMember(dest => dest.SehirAdi, opt => opt.MapFrom(src => src.Sehir.SehirAdi))
                .ForMember(dest => dest.KurumTipiAdi, opt => opt.MapFrom(src => src.KurumTipi.KurumTipiAdi))
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
