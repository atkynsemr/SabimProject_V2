namespace Sabim.Domain.Entities
{
    public class PersonelGorevlendirilme : BaseEntity
    {
        public short PersonelGorevlendirilmeID { get; set; }
        public short PersonelId { get; set; }
        public short KisimId { get; set; }
        public bool AsilGorevlendirilmeYeriMi { get; set; }
        public short GorevlendirilmeTipiId { get; set; }
        public bool GorevlendirilmeAktifMi { get; set; }
        public DateTime GorevlendirilmeBaslangicTarihi { get; set; }
        public DateTime? GorevlendirilmeBitisTarihi { get; set; }
        // Navigation Properties
        public Personel Personel { get; set; }
        public Kisim Kisim { get; set; }
        public GorevlendirilmeTipi GorevlendirilmeTipi { get; set; }
    }
}
