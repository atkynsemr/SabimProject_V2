namespace Sabim.Domain.Entities
{
    public class PersonelUnvanGecmisi : BaseEntity
    {
        public short PersonelUnvanGecmisiID { get; set; }
        public short PersonelId { get; set; }
        public short UnvanId { get; set; }
        public DateTime UnvanaSahipOlduguTarih { get; set; }
        public DateTime? UnvanDegisimTarihi { get; set; }
        public Personel Personel { get; set; } // Personel objesini ekledik
        public Unvan Unvan { get; set; } // Unvan objesini ekledik
    }
}