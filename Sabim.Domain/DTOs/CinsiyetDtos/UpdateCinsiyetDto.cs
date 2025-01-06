namespace Sabim.Domain.DTOs.CinsiyetDtos
{
    public record UpdateCinsiyetDto:CinsiyetBaseDto
    {
        public short CinsiyetID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
