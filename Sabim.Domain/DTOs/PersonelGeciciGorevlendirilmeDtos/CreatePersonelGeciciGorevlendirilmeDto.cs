namespace Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos
{
    public record CreatePersonelGeciciGorevlendirilmeDto : PersonelGeciciGorevlendirilmeBaseDto
    {
        public short KurumId { get; set; }
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
