namespace Sabim.Domain.DTOs.KisimDtos
{
    public record UpdateKisimDto :KisimBaseDto
    {
        public short KisimID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
