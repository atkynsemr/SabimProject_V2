namespace Sabim.Domain.Entities
{
    public class GorevlendirilmeTuru :BaseEntity
    {
        public short GorevlendirilmeTuruID { get; set; }
        public string GorevlendirilmeTuruAdi { get; set; }  // Görevlendirilmemiş, Dış Kurumdan Görevlendirilme, Dış Kuruma Görevlendirilme vb.
        public bool KurumPersonelListesineDahilMi {  get; set; } // Adliye Personel Listesinde Görünürlük Durumu 
        //Navigation property
        public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}
