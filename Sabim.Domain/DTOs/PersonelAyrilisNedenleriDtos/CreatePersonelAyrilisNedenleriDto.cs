namespace Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos
{
    public record CreatePersonelAyrilisNedenleriDto : PersonelAyrilisNedenleriBaseDto
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
    }
}
