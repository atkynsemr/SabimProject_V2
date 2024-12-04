namespace Sabim.Domain.DTOs.KurumDtos
{
    public abstract record KurumBaseDto
    {
        public string? KurumAdi { get; init; }
        public short SehirId { get; init; }
        public string? SehirAdi { get; init; }
        public short KurumTipiId { get; init; }
        public string? KurumTipiAdi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
