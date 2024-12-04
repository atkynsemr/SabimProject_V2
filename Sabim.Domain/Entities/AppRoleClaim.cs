using Microsoft.AspNetCore.Identity;

namespace Sabim.Domain.Entities
{
    public class AppRoleClaim : IdentityRoleClaim<int>
    {
        public short EkranId { get; set; }
        public short? OlusturanPersonelId { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public short? GuncelleyenPersonelId { get; set; }
        public DateTime? GuncellenmeTarihi { get; set; }
        public short? SilenPersonelId { get; set; }
        public DateTime? SilinmeTarihi { get; set; }
        public short DurumId { get; set; }
        // Navigation property
        public Ekran Ekran { get; set; }
        public Durum Durum { get; set; }
        public Personel OlusturanPersonel { get; set; }
        public Personel GuncelleyenPersonel { get; set; }
        public Personel SilenPersonel { get; set; }
    }
}
