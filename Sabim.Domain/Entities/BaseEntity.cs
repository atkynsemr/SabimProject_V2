namespace Sabim.Domain.Entities
{
    public abstract class BaseEntity
    {
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
        public short? SilenPersonelId { get; set; }
        public DateTime? SilinmeTarihi { get; set; }
        public short DurumId  { get; set; }
        // Navigation property
        public Durum Durum { get; set; }
        public Personel OlusturanPersonel { get; set; }
        public Personel GuncelleyenPersonel { get; set; }
        public Personel SilenPersonel { get; set; }
    }
}
