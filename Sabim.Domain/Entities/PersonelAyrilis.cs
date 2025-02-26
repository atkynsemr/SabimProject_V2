namespace Sabim.Domain.Entities
{
    public class PersonelAyrilis : BaseEntity
    {
        public short PersonelAyrilisID { get; set; }
        public short PersonelId { get; set; }
        public byte PersonelAyrilisNedenleriId { get; set; }
        public DateTime? BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public short? PersonelAyrilisYeriId { get; set; }
        // Navigation Properties
        public Personel Personel { get; set; } 
        public PersonelAyrilisNedenleri PersonelAyrilisNedenleri { get; set; } 
        public Kurum? PersonelAyrilisYeri { get; set; } 
    }
}
