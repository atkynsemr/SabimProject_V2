using AutoMapper;
using Sabim.Domain.DTOs.CalismaDurumuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    class CalismaDurumuProfile : Profile
    {
        public CalismaDurumuProfile()
        {
            CreateMap<CalismaDurumu, ResultCalismaDurumuDto>().ReverseMap();
            CreateMap<CalismaDurumu, ResultCalismaDurumuWithPersonelCountDto>()
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src => (ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
