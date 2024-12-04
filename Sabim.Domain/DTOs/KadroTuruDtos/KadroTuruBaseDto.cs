namespace Sabim.Domain.DTOs.KadroTuruDtos
{
    public abstract record KadroTuruBaseDto
    {
        public string? KadroTuruAdi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
