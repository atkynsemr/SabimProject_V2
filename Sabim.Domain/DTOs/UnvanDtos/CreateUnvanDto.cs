namespace Sabim.Domain.DTOs.UnvanDtos
{
    public record CreateUnvanDto:UnvanBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public byte OncelikDurumu { get; set; }
    }
}
