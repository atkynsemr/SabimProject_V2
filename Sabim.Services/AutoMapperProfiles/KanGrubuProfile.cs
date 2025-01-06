using AutoMapper;
using Sabim.Domain.DTOs.KanGrubuDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class KanGrubuProfile :Profile
    {
        public KanGrubuProfile() {
            CreateMap<KanGrubu, CreateKanGrubuDto>().ReverseMap();
            CreateMap<KanGrubu, UpdateKanGrubuDto>().ReverseMap();
            CreateMap<KanGrubu, ResultKanGrubuDto>().ReverseMap();
            CreateMap<KanGrubu, ResultKanGrubuWithPersonelCountDto>()
                .ForMember(dest => dest.PersonelSayisi, opt => opt.MapFrom(src =>(ushort)src.Personels.Count))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ReverseMap();
        }
    }
}
