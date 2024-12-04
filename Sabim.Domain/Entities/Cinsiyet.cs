namespace Sabim.Domain.Entities
{
    public class Cinsiyet:BaseEntity
    {
        public short CinsiyetID { get; set; }
        public string CinsiyetAdi { get; set; } // Erkek, Kadın, Belirtilmemiş
        //Navigation property
        public virtual ICollection<Personel> Personels { get; set; }
    }
}
