namespace Sabim.Domain.Entities
{
    public class KadroTuru:BaseEntity
    {
        public short KadroTuruID { get; set; }
        public string KadroTuruAdi { get; set; } // Kadrolu, Sözleşmeli(4/B), Sürekli İşçi(4/D)-Taşerondan Geçen, Sürekli İşçi(4/D)-Açıktan Atama vb.
        //Navigation property
        public virtual ICollection<Personel> Personels { get; set; }= new List<Personel>();
    }
}
