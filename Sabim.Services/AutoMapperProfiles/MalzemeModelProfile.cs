using AutoMapper;
using Sabim.Domain.DTOs.MalzemeModelDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class MalzemeModelProfile : Profile
    {
        public MalzemeModelProfile()
        {
            CreateMap<MalzemeModel, ResultMalzemeModelDto>()
                  .ForMember(dest => dest.MarkaAdi, opt => opt.MapFrom(src => src.MalzemeMarka.MarkaAdi))
                  .ForMember(dest => dest.MalzemeCinsiAdi, opt => opt.MapFrom(src => src.MalzemeMarka.MalzemeCinsi.MalzemeCinsiAdi))
                  .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
            CreateMap<CreateMalzemeModelDto, MalzemeModel>();
            CreateMap<UpdateMalzemeModelDto, MalzemeModel>();
        }
    }
}
