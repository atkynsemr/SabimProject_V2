namespace Sabim.Domain.DTOs.PersonelDtos
{
    public record ResultPersonelDto :PersonelBaseDto
    {
        public short PersonelID { get; init; }
        public string? CepTelefonu { get; init; } 
        public short CinsiyetId { get; init; }
        public string? CinsiyetAdi { get; init; }
        public short KanGrubuId { get; init; }
        public string? KanGrubuAdi { get; init; }
        public DateTime? DogumTarihi { get; init; }
        public string? DogumYeri { get; init; }
        public string DogumYeriYili
        {
            get
            {
                // DogumYeri ve DogumTarihi ikisi de boşsa, boş string döndür
                if (string.IsNullOrEmpty(DogumYeri) && !DogumTarihi.HasValue)
                {
                    return string.Empty;
                }
                // DogumYeri boş ve DogumTarihi varsa sadece yılı döndür
                else if (string.IsNullOrEmpty(DogumYeri))
                {
                    return DogumTarihi.HasValue ? DogumTarihi.Value.Year.ToString() : string.Empty;
                }
                // DogumTarihi boş ve DogumYeri varsa sadece DogumYeri döndür
                else if (!DogumTarihi.HasValue)
                {
                    return DogumYeri;
                }
                // Hem DogumYeri hem DogumTarihi varsa "DogumYeri - Yıl" döndür
                return $"{DogumYeri} - {DogumTarihi.Value.Year}";
            }
        }
        
        public byte? Derece { get; init; }
        public byte? Kademe { get; init; }
        public string? DereceKademe => Derece.HasValue ? $"{Derece.Value}/{Kademe}" : "";
        public DateTime? MeslegeGirisTarihi { get; init; }
        public DateTime? BuradaGoreveBaslamaTarihi { get; init; }
        public DateTime? BirinciSinifaAyrilmaTarihi { get; init; }
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
        public short CalismaDurumuId { get; init; }
        public string? CalismaDurumAdi { get; init; }
        public short? SehirId { get; init; }
        public List<string>? CalisilanKatipler {  get; init; }
        public List<short>? CalisilanKatiplerIds { get; init; }
    }
}
