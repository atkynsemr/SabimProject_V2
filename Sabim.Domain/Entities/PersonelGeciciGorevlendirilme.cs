namespace Sabim.Domain.Entities
{
    public class PersonelGeciciGorevlendirilme : BaseEntity
    {
        public short PersonelGeciciGorevlendirilmeID { get; set; }
        public short PersonelId { get; set; }
        public short GorevlendirilmeTipiId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public bool GorevlendirilmeAktifMi { get; set; }
        public short PersonelAyrilisYeriId { get; set; }
        // Navigation Properties
        public Personel Personel { get; set; }
        public Kurum PersonelAyrilisYeri { get; set; }
        public GorevlendirilmeTipi GorevlendirilmeTipi { get; set; }
    }
}
