using AutoMapper;
using Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelUnvanGecmisiProfile : Profile
    {
        public PersonelUnvanGecmisiProfile()
        {
            CreateMap<PersonelUnvanGecmisi, CreatePersonelUnvanGecmisiDto>().ReverseMap();
            CreateMap<PersonelUnvanGecmisi, ResultPersonelUnvanGecmisiDto>()
            .ForMember(dest => dest.UnvanAdi, opt => opt.MapFrom(src => src.Unvan.UnvanAdi))
            .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
            .ReverseMap();
        }
    }
}
