namespace Sabim.Domain.DTOs.BirimDtos
{
    public record CreateBirimDto : BirimBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
