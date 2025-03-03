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
                .ForMember(dest => dest.KisimAdi, opt => opt.MapFrom(src => src.Kisim.KisimAdi))
                .ForMember(dest => dest.BirimAdi, opt => opt.MapFrom(src => src.Kisim.Birim.BirimAdi))
                .ForMember(dest => dest.BolumAdi, opt => opt.MapFrom(src => src.Kisim.Birim.Bolum.BolumAdi))
                .ForMember(dest => dest.BirimId, opt => opt.MapFrom(src => src.Kisim.BirimId))
                .ForMember(dest => dest.BolumId, opt => opt.MapFrom(src => src.Kisim.Birim.BolumId))
                .ReverseMap();
        }
    }
}
