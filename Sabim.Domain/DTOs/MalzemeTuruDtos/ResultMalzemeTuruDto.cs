namespace Sabim.Domain.DTOs.MalzemeTuruDtos
{
    public record ResultMalzemeTuruDto : MalzemeTuruBaseDto
    {
        public byte MalzemeTuruID { get; init; }
        public bool Selected { get; init; }
    }

}
