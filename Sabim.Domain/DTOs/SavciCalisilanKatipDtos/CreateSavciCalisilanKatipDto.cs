namespace Sabim.Domain.DTOs.SavciCalisilanKatipDtos
{
    public record CreateSavciCalisilanKatipDto :SavciCalisilanKatipBaseDto
    {
        public short? OlusturanPersonelId { get; init; }
        public DateTime? OlusturulmaTarihi { get; init; }
    }
}
