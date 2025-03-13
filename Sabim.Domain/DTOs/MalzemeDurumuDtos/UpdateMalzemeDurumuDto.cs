namespace Sabim.Domain.DTOs.MalzemeDurumuDtos
{
    public record UpdateMalzemeDurumuDto : MalzemeDurumuBaseDto
    {
        public byte MalzemeDurumuID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
