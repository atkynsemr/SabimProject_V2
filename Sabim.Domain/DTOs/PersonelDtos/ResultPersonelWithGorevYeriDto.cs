namespace Sabim.Domain.DTOs.PersonelDtos
{
    public record ResultPersonelWithGorevYeriDto
    {
        public short PersonelID { get; init; }
        public string? Ad { get; init; }
        public string? Soyad { get; init; }
        public string AdSoyad => $"{Ad} {Soyad}".Trim();
        public int SicilNumarasi { get; init; }
        public string? BolumAdi { get; init; }
        public string? BirimAdi { get; init; }
        public string? KisimAdi { get; init; }
        public string? CalismaDurumAdi { get; init; }
        public string? UnvanAdi { get; init; }
        public string? KurumAdi { get; init; }
    }
}
