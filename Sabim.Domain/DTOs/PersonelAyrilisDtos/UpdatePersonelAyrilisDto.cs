namespace Sabim.Domain.DTOs.PersonelAyrilisDtos
{
    public record UpdatePersonelAyrilisDto : PersonelAyrilisNedenleriBaseDto
    {
        public short PersonelAyrilisID { get; init; }
        public bool KaliciAyrilisMi { get; init; }
        public bool DonanimUyarisi { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
