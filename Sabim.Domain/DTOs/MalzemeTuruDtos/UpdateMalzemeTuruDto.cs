namespace Sabim.Domain.DTOs.MalzemeTuruDtos
{
    public record UpdateMalzemeTuruDto : MalzemeTuruBaseDto
    {
        public byte MalzemeTuruID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
