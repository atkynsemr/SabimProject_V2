namespace Sabim.Domain.DTOs.BirimDtos
{
    public abstract record BirimBaseDto
    {
        public string? BirimAdi { get; init; }
        public short BolumId { get; init; }
        public string? BolumAdi { get; set; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
