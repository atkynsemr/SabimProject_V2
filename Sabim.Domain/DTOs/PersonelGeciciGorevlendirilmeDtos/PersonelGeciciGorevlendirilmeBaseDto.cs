namespace Sabim.Domain.DTOs.PersonelGeciciGorevlendirilmeDtos
{
    public abstract record PersonelGeciciGorevlendirilmeBaseDto
    {
        //  public short PersonelGeciciGorevlendirilmeID { get; init; }
        public short PersonelId { get; init; }
        public short GorevlendirilmeTipiId { get; init; }
        public bool GorevlendirilmeAktifMi { get; set; }
        public DateTime BaslangicTarihi { get; init; } = DateTime.Now;
        public DateTime? BitisTarihi { get; init; }
        public short PersonelAyrilisYeriId { get; set; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
