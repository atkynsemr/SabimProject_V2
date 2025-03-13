namespace Sabim.Domain.DTOs.MalzemeModelDtos
{
    public record CreateMalzemeModelDto : MalzemeModelBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }

}
