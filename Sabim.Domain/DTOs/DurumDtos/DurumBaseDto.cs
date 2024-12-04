namespace Sabim.Domain.DTOs.DurumDtos
{
    public abstract record DurumBaseDto
    {
        public string? DurumAdi { get; init; }
        public bool AktifMi { get; init; }
    }
}
