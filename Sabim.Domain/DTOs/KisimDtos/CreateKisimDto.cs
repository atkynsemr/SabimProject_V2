namespace Sabim.Domain.DTOs.KisimDtos
{
    public record CreateKisimDto : KisimBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
