using AutoMapper;
using Sabim.Domain.DTOs.EkranDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class EkranProfile : Profile
    {
        public EkranProfile()
        {
            CreateMap<Ekran, ResultEkranDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.SidebarMenuAdi, opt => opt.MapFrom(src => src.SidebarMenu.SidebarMenuAdi))
                .ReverseMap();
        }
    }
}
