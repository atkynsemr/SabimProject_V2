namespace Sabim.Domain.DTOs.MalzemeDtos
{
    public abstract record MalzemeBaseDto
    {
        public string SeriNumarasi { get; init; }
        public byte MalzemeModelId { get; init; }
        public byte MalzemeDurumuId { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
