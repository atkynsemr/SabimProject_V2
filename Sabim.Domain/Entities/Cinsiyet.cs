namespace Sabim.Domain.Entities
{
    public class Cinsiyet:BaseEntity
    {
        public short CinsiyetID { get; set; }
        public string CinsiyetAdi { get; set; } // Erkek, Kadın, Belirtilmemiş
        //Navigation property
        public ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}
