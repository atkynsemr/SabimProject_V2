namespace Sabim.Domain.DTOs.KabinetBazliBolumDtos
{
    public record CreateKabinetBazliBolumDto : KabinetBazliBolumBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
