namespace Sabim.Domain.DTOs.KadroTuruDtos
{
    public record CreateKadroTuruDto : KadroTuruBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
