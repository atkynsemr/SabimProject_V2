using AutoMapper;
using Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelGorevlendirilmeProfile : Profile
    {
        public PersonelGorevlendirilmeProfile()
        {
            CreateMap<PersonelGorevlendirilme, CreatePersonelGorevlendirilmeDto>().ReverseMap();
            CreateMap<PersonelGorevlendirilme, UpdatePersonelGorevlendirilmeDto>().ReverseMap();
            CreateMap<PersonelGorevlendirilme, ResultPersonelGorevlendirilmeDto>()
            .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi))
            .ReverseMap();
        }
    }
}
