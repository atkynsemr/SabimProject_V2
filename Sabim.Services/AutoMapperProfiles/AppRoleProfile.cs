using AutoMapper;
using Sabim.Domain.DTOs.AppRoleDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRole, ResultAppRoleWithPersonelCountDto>()
                //.ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.AppUsers.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
