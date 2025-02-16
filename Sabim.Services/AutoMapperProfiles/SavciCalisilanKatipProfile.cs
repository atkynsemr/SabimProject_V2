using AutoMapper;
using Sabim.Domain.DTOs.SavciCalisilanKatipDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class SavciCalisilanKatipProfile : Profile
    {
        public SavciCalisilanKatipProfile()
        {
            CreateMap<SavciCalisilanKatip, CreateSavciCalisilanKatipDto>().ReverseMap();
        }
    }
}
