namespace Sabim.Domain.DTOs.BolumDtos
{
    public record UpdateBolumDto : BolumBaseDto
    {
        public short BolumID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
