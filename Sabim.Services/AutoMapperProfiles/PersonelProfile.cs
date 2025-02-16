using AutoMapper;
using Sabim.Domain.DTOs.PersonelDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelProfile : Profile
    {
        public PersonelProfile()
        {
            CreateMap<Personel, CreatePersonelDto>().ReverseMap();
            CreateMap<Personel, UpdatePersonelDto>().ReverseMap();
            CreateMap<Personel, ListPersonelDto>().ReverseMap();
            CreateMap<Personel, ResultPersonelDto>()
                .ForMember(dest => dest.CinsiyetAdi, opt => opt.MapFrom(src => src.Cinsiyet.CinsiyetAdi))
                .ForMember(dest => dest.KanGrubuAdi, opt => opt.MapFrom(src => src.KanGrubu.KanGrubuAdi))
                .ForMember(dest => dest.UnvanAdi, opt => opt.MapFrom(src => src.Unvan.UnvanAdi))
                .ForMember(dest => dest.OncelikSirasi, opt => opt.MapFrom(src => src.Unvan.OncelikSirasi))
                .ForMember(dest => dest.GorevlendirilmeTuruAdi, opt => opt.MapFrom(src => src.GorevlendirilmeTuru.GorevlendirilmeTuruAdi))
                .ForMember(dest => dest.KurumAdi, opt => opt.MapFrom(src => src.Kurum.KurumAdi))
                .ForMember(dest => dest.KadroTuruAdi, opt => opt.MapFrom(src => src.KadroTuru.KadroTuruAdi))
                .ForMember(dest => dest.CalismaDurumAdi, opt => opt.MapFrom(src => src.CalismaDurumu.CalismaDurumAdi))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
                .ForMember(dest => dest.SehirId, opt => opt.MapFrom(src => (short?)(src.Kurum != null ? src.Kurum.SehirId : null)))
                .ForMember(dest => dest.CalisilanKatipler, opt => opt.MapFrom(
                    src => src.SavciOlarakCalisilanKatips != null
                        ? src.SavciOlarakCalisilanKatips
                            .Where(k => k.GorevlendirilmeAktifMi && k.Katip != null)
                            .Select(k => $"{k.Katip.Ad} {k.Katip.Soyad}")
                            .ToList()
                        : new List<string>()
                ))
                .ForMember(dest => dest.CalisilanKatiplerIds, opt => opt.MapFrom(
                    src => src.SavciOlarakCalisilanKatips != null
                    ? src.SavciOlarakCalisilanKatips
                        .Where(k => k.GorevlendirilmeAktifMi && k.Katip != null)
                        .Select(k => k.Katip.PersonelID) 
                        .ToList()
                    : new List<short>())); 
        }
    }
}
