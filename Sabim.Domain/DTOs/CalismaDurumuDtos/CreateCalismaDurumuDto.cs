namespace Sabim.Domain.DTOs.CalismaDurumuDtos
{
    public record CreateCalismaDurumuDto: CalismaDurumuBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
