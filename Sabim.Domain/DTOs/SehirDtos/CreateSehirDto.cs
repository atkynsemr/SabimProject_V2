namespace Sabim.Domain.DTOs.SehirDtos
{
    public record CreateSehirDto:SehirBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
