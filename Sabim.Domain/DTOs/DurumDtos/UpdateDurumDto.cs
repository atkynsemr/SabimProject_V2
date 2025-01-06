namespace Sabim.Domain.DTOs.DurumDtos
{
    public record UpdateDurumDto : DurumBaseDto
    {
        public short DurumID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
