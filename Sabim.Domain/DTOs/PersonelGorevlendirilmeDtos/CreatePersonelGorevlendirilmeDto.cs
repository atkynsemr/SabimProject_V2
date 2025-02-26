namespace Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos
{
    public record CreatePersonelGorevlendirilmeDto : PersonelGorevlendirilmeBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
