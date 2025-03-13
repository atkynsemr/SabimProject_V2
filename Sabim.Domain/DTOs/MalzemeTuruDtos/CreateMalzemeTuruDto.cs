namespace Sabim.Domain.DTOs.MalzemeTuruDtos
{
    public record CreateMalzemeTuruDto : MalzemeTuruBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
