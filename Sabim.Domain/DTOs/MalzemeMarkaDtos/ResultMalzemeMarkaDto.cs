namespace Sabim.Domain.DTOs.MalzemeMarkaDtos
{
    public record ResultMalzemeMarkaDto : MalzemeMarkaBaseDto
    {
        public byte MalzemeMarkaID { get; init; }
        public string MalzemeCinsiAdi { get; init; }
        public bool Selected { get; init; }
    }
}
