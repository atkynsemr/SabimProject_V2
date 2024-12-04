namespace Sabim.Domain.Entities
{
    public class Birim :BaseEntity
    {
        public short BirimID { get; set; }
        public string BirimAdi { get; set; }
        public short BolumId { get; set; }
        //Navigation property
        public Bolum Bolum { get; set; }
        public virtual ICollection<Kisim> Kisims { get; set; }
    }
}
