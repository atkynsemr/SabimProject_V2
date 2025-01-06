namespace Sabim.Domain.DTOs.KurumDtos
{
    public record CreateKurumDto :KurumBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
