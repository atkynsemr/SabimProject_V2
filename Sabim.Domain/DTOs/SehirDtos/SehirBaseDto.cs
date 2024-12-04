namespace Sabim.Domain.DTOs.SehirDtos
{
    public abstract  record SehirBaseDto
    {
        public string? SehirAdi { get; set; }
        public byte SehirKodu { get; set; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
