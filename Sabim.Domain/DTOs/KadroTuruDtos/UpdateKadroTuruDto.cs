namespace Sabim.Domain.DTOs.KadroTuruDtos
{
    public record UpdateKadroTuruDto : KadroTuruBaseDto
    {
        public short KadroTuruID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
