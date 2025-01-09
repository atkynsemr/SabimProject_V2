using AutoMapper;
using Sabim.Domain.DTOs.KabinetBazliBolumDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class KabinetBazliBolumProfile : Profile
    {
        public KabinetBazliBolumProfile()
        {
            CreateMap<KabinetBazliBolum , CreateKabinetBazliBolumDto>().ReverseMap();
            CreateMap<KabinetBazliBolum, UpdateKabinetBazliBolumDto>().ReverseMap();
            CreateMap<KabinetBazliBolum, ResultKabinetBazliBolumDto>().ReverseMap();
            CreateMap<KabinetBazliBolum, ResultKabinetBazliBolumWithKisimCountDto>()
              .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
              .ForMember(dest => dest.KisimSayisi, opt => opt.MapFrom(src =>(ushort) src.Kisims.Count))
              .ReverseMap();
        }
    }
}
