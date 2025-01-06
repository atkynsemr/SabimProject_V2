namespace Sabim.Domain.DTOs.SehirDtos
{
    public record UpdateSehirDto:SehirBaseDto
    {
        public short SehirID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
