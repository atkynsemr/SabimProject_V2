using AutoMapper;
using Sabim.Domain.DTOs.MalzemeMarkaDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class MalzemeMarkaProfile : Profile
    {
        public MalzemeMarkaProfile()
        {
            CreateMap<MalzemeMarka, ResultMalzemeMarkaDto>()
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
            CreateMap<CreateMalzemeMarkaDto, MalzemeMarka>();
            CreateMap<UpdateMalzemeMarkaDto, MalzemeMarka>();
        }
    }
}
