namespace Sabim.Domain.DTOs.MalzemeDtos
{
    public record CreateMalzemeDto : MalzemeBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
