namespace Sabim.Domain.DTOs.KurumTipiDtos
{
    public record CreateKurumTipiDto:KurumTipiBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
