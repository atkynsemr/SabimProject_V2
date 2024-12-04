using AutoMapper;
using Sabim.Domain.DTOs.CinsiyetDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    class CinsiyetProfile:Profile
    {
        public CinsiyetProfile()
        {
            CreateMap<Cinsiyet, ResultCinsiyetDto>().ReverseMap();
            CreateMap<Cinsiyet, ResultCinsiyetWithPersonelCountDto>()
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
