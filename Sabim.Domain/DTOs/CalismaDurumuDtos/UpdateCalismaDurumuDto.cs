namespace Sabim.Domain.DTOs.CalismaDurumuDtos
{
    public record UpdateCalismaDurumuDto :CalismaDurumuBaseDto
    {
        public short CalismaDurumuID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
