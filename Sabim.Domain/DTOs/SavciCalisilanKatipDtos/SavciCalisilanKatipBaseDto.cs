namespace Sabim.Domain.DTOs.SavciCalisilanKatipDtos
{
    public abstract record class SavciCalisilanKatipBaseDto
    {
        public short SavciId { get; init; }
        public short KatipId { get; init; }
        public DateTime GorevlendirilmeBaslamaTarihi { get; init; }
        public DateTime? GorevlendirilmeBitisTarihi { get; init; }
        public bool GorevlendirilmeAktifMi { get; init; }
        public short DurumId { get; init; }
        public string? DurumAdi { get; init; }
    }
}
