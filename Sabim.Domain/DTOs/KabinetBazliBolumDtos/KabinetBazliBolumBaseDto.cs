namespace Sabim.Domain.DTOs.KabinetBazliBolumDtos
{
    public abstract record KabinetBazliBolumBaseDto
    {
        public string? KabinetBazliBolumAdi { get; set; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
