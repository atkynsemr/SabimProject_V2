namespace Sabim.Domain.Entities
{
    public class Sehir :BaseEntity
    {
        public short SehirID { get; set; }
        public string SehirAdi { get; set; }
        public byte SehirKodu { get; set; }
        //Navigation property
        public virtual ICollection<Kurum> Kurums { get; set; } = new List<Kurum>();
    }
}
