namespace Sabim.Domain.Entities
{
    public class Bolum :BaseEntity
    {
        public short BolumID { get; set; }
        public string BolumAdi { get; set; }
        //Navigation property
        public virtual ICollection<Birim> Birims { get; set; } = new List<Birim>();
    }
}
