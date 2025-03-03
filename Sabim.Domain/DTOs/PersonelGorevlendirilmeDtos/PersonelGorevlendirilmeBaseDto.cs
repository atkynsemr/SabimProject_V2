namespace Sabim.Domain.DTOs.PersonelGorevlendirilmeDtos
{
    public abstract record PersonelGorevlendirilmeBaseDto
    {
        public short PersonelId { get; init; }
        public short KisimId { get; init; }
        public bool AsilGorevlendirilmeYeriMi { get; init; }
        public short GorevlendirilmeTipiId { get; init; }
        public bool GorevlendirilmeAktifMi { get; set; }
        public DateTime GorevlendirilmeBaslangicTarihi { get; init; } = DateTime.Now;
        public DateTime? GorevlendirilmeBitisTarihi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}






