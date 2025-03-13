namespace Sabim.Domain.DTOs.MalzemeDurumuDtos
{
    public abstract record MalzemeDurumuBaseDto
    {
        public string MalzemeDurumuAdi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
