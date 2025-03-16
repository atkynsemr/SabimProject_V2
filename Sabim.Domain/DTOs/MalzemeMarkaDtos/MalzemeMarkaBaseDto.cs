namespace Sabim.Domain.DTOs.MalzemeMarkaDtos
{
    public abstract record MalzemeMarkaBaseDto
    {
        public string MarkaAdi { get; init; }
        public byte MalzemeCinsiId { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
