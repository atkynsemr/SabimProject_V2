namespace Sabim.Domain.DTOs.PersonelDtos
{
    public record UpdatePersonelDto : PersonelBaseDto
    {
        public short PersonelID { get; init; }
        public string? CepTelefonu { get; init; }
        public short CinsiyetId { get; init; }
        public string? CinsiyetAdi { get; init; }
        public short KanGrubuId { get; init; }
        public string? KanGrubuAdi { get; init; }
        public DateTime? DogumTarihi { get; init; }
        public string? DogumYeri { get; init; }
        public byte? Derece { get; init; }
        public byte? Kademe { get; init; }
        public DateTime? MeslegeGirisTarihi { get; init; }
        public DateTime? BuradaGoreveBaslamaTarihi { get; init; }
        public string? KimlikNo { get; init; }
        public DateTime? BuradanAyrilmaTarihi { get; init; }
        public string? AracPlakasi { get; init; }
        public short UnvanId { get; init; }
        public string? UnvanAdi { get; init; }
        public byte OncelikSirasi { get; init; }
        public short GorevlendirilmeTuruId { get; init; }
        public string? GorevlendirilmeTuruAdi { get; init; }
        public short KurumId { get; init; }
        public string? KurumAdi { get; init; }
        public short KadroTuruId { get; init; }
        public string? KadroTuruAdi { get; init; }
        public short CalismaDurumuID { get; init; }
        public string? CalismaDurumAdi { get; init; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
    }
}
