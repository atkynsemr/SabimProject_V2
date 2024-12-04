namespace Sabim.Domain.Entities
{
    public class KurumTipi :BaseEntity
    {
        public short KurumTipiID { get; set; }
        public string KurumTipiAdi { get; set; }  //Gorev Yapilan Yer, Teknik Hizmet Yeri, Donanım Alınan Yer
        //Navigation property
        public virtual ICollection<Kurum> Kurums { get; set; } = new List<Kurum>();
    }
}
