namespace Sabim.Domain.DTOs.PersonelDtos
{
    public abstract record PersonelBaseDto
    {
        public string? Ad { get; init; }
        public string? Soyad { get; init; }
        public string AdSoyad => $"{Ad} {Soyad}".Trim();
        public int SicilNumarasi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
