namespace Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos
{
    public record UpdatePersonelAyrilisNedenleriDto : PersonelAyrilisNedenleriBaseDto
    {
        public byte PersonelAyrilisNedenleriID { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
