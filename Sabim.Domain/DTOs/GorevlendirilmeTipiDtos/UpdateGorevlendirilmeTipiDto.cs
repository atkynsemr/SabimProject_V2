namespace Sabim.Domain.DTOs.GorevlendirilmeTipiDtos
{
    public record UpdateGorevlendirilmeTipiDto : GorevlendirilmeTipiBaseDto
    {
        public short GorevlendirilmeTipiID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
