namespace Sabim.Domain.Entities
{
    public class KanGrubu:BaseEntity
    {
        public short KanGrubuID { get; set; }
        public string KanGrubuAdi { get; set; }
        //Navigation property
        public ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}
