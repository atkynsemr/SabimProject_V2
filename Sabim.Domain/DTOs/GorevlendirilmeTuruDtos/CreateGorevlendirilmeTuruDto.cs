namespace Sabim.Domain.DTOs.GorevlendirilmeTuruDtos
{
    public record CreateGorevlendirilmeTuruDto : GorevlendirilmeTuruBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
