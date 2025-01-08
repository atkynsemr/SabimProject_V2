namespace Sabim.Domain.DTOs.BirimDtos
{
    public record UpdateBirimDto : BirimBaseDto
    {
        public short BirimID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
