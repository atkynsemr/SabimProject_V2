namespace Sabim.Domain.DTOs.PersonelAyrilisNedenleriDtos
{
    public abstract record PersonelAyrilisNedenleriBaseDto
    {
        public string Aciklama { get; init; }
        public bool KaliciAyrilisMi { get; set; }
        public bool DonanimUyarisi { get; set; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
