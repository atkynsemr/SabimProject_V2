using AutoMapper;
using Sabim.Domain.DTOs.GorevlendirilmeTuruDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class GorevlendirilmeTuruProfile : Profile
    {
        public GorevlendirilmeTuruProfile()
        {
            CreateMap<GorevlendirilmeTuru, ResultGorevlendirilmeTuruDto>().ReverseMap();
            CreateMap<GorevlendirilmeTuru, ResultGorevlendirilmeTuruWithPersonelCountDto>()
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
