namespace Sabim.Domain.DTOs.PersonelUnvanGecmisiDtos
{
    public record CreatePersonelUnvanGecmisiDto : PersonelUnvanGecmisiBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
