namespace Sabim.Domain.DTOs.PersonelAyrilisDtos
{
    public record CreatePersonelAyrilisDto : PersonelAyrilisNedenleriBaseDto
    {
        public bool KaliciAyrilisMi { get; init; }
        public bool DonanimUyarisi { get; init; }
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
