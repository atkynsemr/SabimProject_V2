namespace Sabim.Domain.DTOs.SehirDtos
{
    public abstract  record SehirBaseDto
    {
        public string? SehirAdi { get; init; }
        public byte SehirKodu { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
