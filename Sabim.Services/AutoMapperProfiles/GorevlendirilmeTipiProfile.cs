using AutoMapper;
using Sabim.Domain.DTOs.GorevlendirilmeTipiDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class GorevlendirilmeTipiProfile : Profile
    {
        public GorevlendirilmeTipiProfile()
        {
            CreateMap<GorevlendirilmeTipi, CreateGorevlendirilmeTipiDto>().ReverseMap();
            CreateMap<GorevlendirilmeTipi, UpdateGorevlendirilmeTipiDto>().ReverseMap();
            CreateMap<GorevlendirilmeTipi, ResultGorevlendirilmeTipiDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
        }
    }
}
