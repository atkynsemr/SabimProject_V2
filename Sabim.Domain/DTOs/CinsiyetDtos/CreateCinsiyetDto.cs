namespace Sabim.Domain.DTOs.CinsiyetDtos
{
    public record CreateCinsiyetDto :CinsiyetBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
