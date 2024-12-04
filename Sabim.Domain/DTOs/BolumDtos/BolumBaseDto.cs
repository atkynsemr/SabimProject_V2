namespace Sabim.Domain.DTOs.BolumDtos
{
    public abstract record BolumBaseDto
    {
        public string BolumAdi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
