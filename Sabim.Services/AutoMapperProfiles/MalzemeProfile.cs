using AutoMapper;
using Sabim.Domain.DTOs.MalzemeDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class MalzemeProfile : Profile
    {
        public MalzemeProfile()
        {
            CreateMap<Malzeme, ResultMalzemeDto>()
                .ForMember(dest => dest.TurAdi, opt => opt.MapFrom(src => src.MalzemeModel.MalzemeCinsi.MalzemeTuru.TurAdi))
                .ForMember(dest => dest.MalzemeCinsiAdi, opt => opt.MapFrom(src => src.MalzemeModel.MalzemeCinsi.MalzemeCinsiAdi))
                .ForMember(dest => dest.MarkaAdi, opt => opt.MapFrom(src => src.MalzemeModel.MalzemeMarka.MarkaAdi))
                .ForMember(dest => dest.ModelAdi, opt => opt.MapFrom(src => src.MalzemeModel.ModelAdi))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
            CreateMap<CreateMalzemeDto, Malzeme>();
            CreateMap<UpdateMalzemeDto, Malzeme>();
        }
    }
}
