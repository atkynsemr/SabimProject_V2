namespace Sabim.Domain.DTOs.DurumDtos
{
    public record CreateDurumDto : DurumBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
