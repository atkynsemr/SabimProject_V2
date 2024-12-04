namespace Sabim.Domain.DTOs.CinsiyetDtos
{
    public abstract record CinsiyetBaseDto
    {
        public string? CinsiyetAdi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
