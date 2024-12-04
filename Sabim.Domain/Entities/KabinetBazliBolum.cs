namespace Sabim.Domain.Entities
{
    public class KabinetBazliBolum :BaseEntity
    {
        public short KabinetBazliBolumID { get; set; }
        public string KabinetBazliBolumAdi { get; set; }
        //Navigation property
        public ICollection<Kisim> Kisims { get; set; } = new List<Kisim>();
    }
}
