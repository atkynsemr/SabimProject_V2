namespace Sabim.Domain.DTOs.BolumDtos
{
    public record CreateBolumDto :BolumBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
