namespace Sabim.Domain.DTOs.PersonelAyrilisDtos
{
    public abstract record PersonelAyrilisNedenleriBaseDto
    {
        public short PersonelId { get; init; }
        public byte PersonelAyrilisNedenleriId { get; init; }
        public string Aciklama { get; init; }   
        public DateTime? BaslangicTarihi { get; init; }
        public DateTime? BitisTarihi { get; init; }
        public short? PersonelAyrilisYeriId { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
