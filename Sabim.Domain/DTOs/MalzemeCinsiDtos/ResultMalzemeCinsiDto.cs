namespace Sabim.Domain.DTOs.MalzemeCinsiDtos
{
    public record ResultMalzemeCinsiDto : MalzemeCinsiBaseDto
    {
        public byte MalzemeCinsiID { get; init; }
        public string TurAdi { get; init; }
        public bool Selected { get; init; }
    }
}
