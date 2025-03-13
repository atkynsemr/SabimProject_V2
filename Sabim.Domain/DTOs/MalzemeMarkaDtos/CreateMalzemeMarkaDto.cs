namespace Sabim.Domain.DTOs.MalzemeMarkaDtos
{
    public record CreateMalzemeMarkaDto : MalzemeMarkaBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
