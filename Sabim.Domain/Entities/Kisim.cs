namespace Sabim.Domain.Entities
{
    public class Kisim : BaseEntity
    {
        public short KisimID { get; set; }
        public string KisimAdi { get; set; }
        public short BirimId { get; set; }
        public short KabinetBazliBolumId { get; set; }
        public string Aciklama { get; set; }  //Bulunduğu yer hakkında açıklama vb.
        public string DahiliTelefon { get; set; }
        //Navigation property
        public Birim Birim { get; set; }
        public KabinetBazliBolum KabinetBazliBolum { get; set; }
        public virtual ICollection<PersonelGorevlendirilme> PersonelGorevlendirilmes { get; set; }= new List<PersonelGorevlendirilme>();
    }

}
