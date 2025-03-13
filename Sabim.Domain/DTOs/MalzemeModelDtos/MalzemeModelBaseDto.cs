namespace Sabim.Domain.DTOs.MalzemeModelDtos
{
    public abstract record MalzemeModelBaseDto
    {
        public string ModelAdi { get; init; }
        public byte MalzemeMarkaId { get; init; }
        public byte MalzemeCinsiId { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
