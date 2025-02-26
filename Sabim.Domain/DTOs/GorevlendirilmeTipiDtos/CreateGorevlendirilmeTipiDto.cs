namespace Sabim.Domain.DTOs.GorevlendirilmeTipiDtos
{
    public record CreateGorevlendirilmeTipiDto : GorevlendirilmeTipiBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
