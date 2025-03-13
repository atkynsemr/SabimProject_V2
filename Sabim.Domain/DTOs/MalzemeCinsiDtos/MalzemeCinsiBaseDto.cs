namespace Sabim.Domain.DTOs.MalzemeCinsiDtos
{
    public abstract record MalzemeCinsiBaseDto
    {
        public string MalzemeCinsiAdi { get; init; }
        public byte MalzemeTuruId { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
