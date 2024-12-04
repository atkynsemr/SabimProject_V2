using AutoMapper;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelProfile : Profile
    {
        public PersonelProfile()
        {
            CreateMap<Personel, ResultPersonelDto>()
            .ForMember(dest => dest.CinsiyetAdi, opt => opt.MapFrom(src => src.Cinsiyet.CinsiyetAdi))
            .ForMember(dest => dest.KanGrubuAdi, opt => opt.MapFrom(src => src.KanGrubu.KanGrubuAdi))
            .ForMember(dest => dest.UnvanAdi, opt => opt.MapFrom(src => src.Unvan.UnvanAdi))
            .ForMember(dest => dest.OncelikSirasi, opt => opt.MapFrom(src => src.Unvan.OncelikSirasi))
            .ForMember(dest => dest.GorevlendirilmeTuruAdi, opt => opt.MapFrom(src => src.GorevlendirilmeTuru.GorevlendirilmeTuruAdi))
            .ForMember(dest => dest.KurumAdi, opt => opt.MapFrom(src => src.Kurum.KurumAdi))
            .ForMember(dest => dest.KadroTuruAdi, opt => opt.MapFrom(src => src.KadroTuru.KadroTuruAdi))
            .ForMember(dest => dest.CalismaDurumAdi, opt => opt.MapFrom(src => src.CalismaDurumu.CalismaDurumAdi))
            .ReverseMap();
        }
    }
}
