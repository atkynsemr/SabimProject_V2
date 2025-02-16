namespace Sabim.Domain.Entities
{
    public class Unvan :BaseEntity
    {
        public short UnvanID { get; set; }
        public string UnvanAdi { get; set; }
        public byte OncelikSirasi { get; set; }
        //Navigation property
        public ICollection<Personel> Personels { get; set; } = new List<Personel>();
        public ICollection<PersonelUnvanGecmisi> PersonelUnvanGecmisis { get; set; } = new List<PersonelUnvanGecmisi>();
    }
}
