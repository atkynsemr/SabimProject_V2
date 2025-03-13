namespace Sabim.Domain.DTOs.MalzemeDurumuDtos
{
    public record CreateMalzemeDurumuDto : MalzemeDurumuBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
