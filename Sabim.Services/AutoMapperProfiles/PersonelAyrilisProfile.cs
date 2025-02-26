using AutoMapper;
using Sabim.Domain.DTOs.PersonelAyrilisDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelAyrilisProfile : Profile
    {
        public PersonelAyrilisProfile()
        {
            CreateMap<PersonelAyrilis, CreatePersonelAyrilisDto>().ReverseMap();
            CreateMap<PersonelAyrilis, UpdatePersonelAyrilisDto>().ReverseMap();
            CreateMap<PersonelAyrilis, ResultPersonelAyrilisDto>()
            .ForMember(dest => dest.Aciklama, opt => opt.MapFrom(src => src.PersonelAyrilisNedenleri.Aciklama))
            .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
            .ReverseMap();
        }
    }
}
