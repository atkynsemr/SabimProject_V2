namespace Sabim.Domain.DTOs.MalzemeCinsiDtos
{
    public record CreateMalzemeCinsiDto : MalzemeCinsiBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
