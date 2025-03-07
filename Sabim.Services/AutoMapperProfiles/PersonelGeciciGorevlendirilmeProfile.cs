using AutoMapper;
using Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos;
using Sabim.Domain.Entities;

namespace Sabim.Services.AutoMapperProfiles
{
    public class PersonelGeciciGorevlendirilmeProfile : Profile
    {
        public PersonelGeciciGorevlendirilmeProfile()
        {
            CreateMap<PersonelGeciciGorevlendirilme, CreatePersonelGeciciGorevlendirilmeDto>().ReverseMap();
            CreateMap<PersonelGeciciGorevlendirilme, UpdatePersonelGeciciGorevlendirilmeDto>().ReverseMap();
            CreateMap<PersonelGeciciGorevlendirilme, ResultPersonelGeciciGorevlendirilmeDto>()
                .ForMember(dest => dest.KurumAdi, opt => opt.MapFrom(src => src.PersonelAyrilisYeri.KurumAdi))
                .ForMember(dest => dest.GorevlendirilmeTipiAciklama, opt => opt.MapFrom(src => src.GorevlendirilmeTipi.GorevlendirilmeTipiAciklama))
                .ForMember(dest => dest.PersonelAyrilisYeriId, opt => opt.MapFrom(src => (short?)(src.PersonelAyrilisYeri != null ? src.PersonelAyrilisYeri.SehirId : null)))
                .ForMember(dest => dest.DurumAdi, opt => opt.MapFrom(src => src.Durum.DurumAdi)).ReverseMap();
        }
    }
}
