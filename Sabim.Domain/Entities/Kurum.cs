namespace Sabim.Domain.Entities
{
    public class Kurum :BaseEntity
    {
        public short KurumID { get; set; }
        public string KurumAdi { get; set; }
        public short SehirId { get; set; }
        public short KurumTipiId { get; set; }
        public KurumTipi KurumTipi { get; set; }
        public Sehir Sehir { get; set; }
        //Navigation property
        public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}
