namespace Sabim.Domain.DTOs.MalzemeCinsiDtos
{
    public record UpdateMalzemeCinsiDto : MalzemeCinsiBaseDto
    {
        public byte MalzemeCinsiID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
