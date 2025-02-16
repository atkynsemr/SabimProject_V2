namespace Sabim.Domain.DTOs.SavciCalisilanKatipDtos
{
    public record UpdateSavciCalisilanKatipDto : SavciCalisilanKatipBaseDto
    {
        public short? OlusturanPersonelId { get; init; }
        public DateTime? OlusturulmaTarihi { get; init; }
        public short? GuncelleyenPersonelId { get; init; }
        public DateTime? GuncellenmeTarihi { get; init; }
        public List<short>? SelectedPersonelIds { get; init; }
    }
}
