namespace Sabim.Domain.DTOs.KabinetBazliBolumDtos
{
    public record UpdateKabinetBazliBolumDto : KabinetBazliBolumBaseDto
    {
        public short KabinetBazliBolumID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
