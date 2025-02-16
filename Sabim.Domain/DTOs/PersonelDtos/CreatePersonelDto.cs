namespace Sabim.Domain.DTOs.PersonelDtos
{
    public record CreatePersonelDto : PersonelBaseDto
    {
        public string? CepTelefonu { get; init; }
        public short CinsiyetId { get; init; }
        public short KanGrubuId { get; init; }
        public DateTime? DogumTarihi { get; init; }
        public string? DogumYeri { get; init; }
        public byte? Derece { get; init; }
        public byte? Kademe { get; init; }
        public DateTime? MeslegeGirisTarihi { get; init; }
        public DateTime? BuradaGoreveBaslamaTarihi { get; init; }
        public string? KimlikNo { get; init; }
        public DateTime? BuradanAyrilmaTarihi { get; init; }
        public DateTime? BirinciSinifaAyrilmaTarihi { get; init; }        
        public string? AracPlakasi { get; init; }
        public short UnvanId { get; init; }
        public short GorevlendirilmeTuruId { get; init; }
        public short KurumId { get; init; }
        public short KadroTuruId { get; init; }
        public short CalismaDurumuId { get; init; }
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public List<int>? SelectedPersonelIds { get; init; }
    }
}
