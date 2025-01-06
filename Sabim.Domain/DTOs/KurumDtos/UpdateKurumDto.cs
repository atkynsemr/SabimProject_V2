namespace Sabim.Domain.DTOs.KurumDtos
{
    public record UpdateKurumDto : KurumBaseDto
    {
        public short KurumID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
