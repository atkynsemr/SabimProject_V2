using AutoMapper;
using Sabim.Domain.DTOs.AppUserDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, CreateUserDto>().ReverseMap();
            CreateMap<AppUser, ResultAppUserDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.PersonelAdSoyad, opt => opt.MapFrom(src => $"{src.Personel.Ad} {src.Personel.Soyad}"))
                //.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppRole.Name))
                .ReverseMap();
        }
    }
}
