namespace Sabim.Domain.DTOs.KurumTipiDtos
{
    public abstract record KurumTipiBaseDto
    {
        public string? KurumTipiAdi { get; init; }
        public short DurumId  { get; init; }
        public string? DurumAdi { get; init; }
    }
}
