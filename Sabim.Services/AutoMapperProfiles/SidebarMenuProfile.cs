using AutoMapper;
using Sabim.Domain.DTOs.SidebarMenuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class SidebarMenuProfile : Profile
    {
        public SidebarMenuProfile()
        {
            CreateMap<SidebarMenu, ResultSidebarMenuWithEkranCountDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.EkranSayisi, opt => opt.MapFrom(src => src.Ekrans.Count))
                .ReverseMap();
        }
    }
}
