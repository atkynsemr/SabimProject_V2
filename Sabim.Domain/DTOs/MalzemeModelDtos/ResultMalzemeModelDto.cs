namespace Sabim.Domain.DTOs.MalzemeModelDtos
{
    public record ResultMalzemeModelDto : MalzemeModelBaseDto
    {
        public byte MalzemeModelID { get; init; }
        public string MarkaAdi { get; init; }
        public string MalzemeCinsiAdi { get; init; }
        public bool Selected { get; init; }
    }
}
