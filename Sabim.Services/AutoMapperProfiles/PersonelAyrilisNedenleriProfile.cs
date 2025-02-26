using AutoMapper;
using Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelAyrilisNedenleriProfile : Profile
    {
        public PersonelAyrilisNedenleriProfile()
        {
            CreateMap<PersonelAyrilisNedenleri, CreatePersonelAyrilisNedenleriDto>().ReverseMap();
            CreateMap<PersonelAyrilisNedenleri, UpdatePersonelAyrilisNedenleriDto>().ReverseMap();
            CreateMap<PersonelAyrilisNedenleri, ResultPersonelAyrilisNedenleriDto>()
            .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
            .ReverseMap();
        }
    }
}
