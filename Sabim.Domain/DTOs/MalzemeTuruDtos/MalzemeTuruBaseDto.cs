namespace Sabim.Domain.DTOs.MalzemeTuruDtos
{
    public abstract record MalzemeTuruBaseDto
    {
        public string TurAdi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
