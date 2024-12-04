namespace Sabim.Domain.Entities
{
    public class CalismaDurumu :BaseEntity
    {
        public short CalismaDurumuID { get; set; }
        public  string CalismaDurumAdi { get; set; } //Görevde, Geçici Ayrılış (Ücretsiz İzin, Askerlik,Doğum İzni vb.), Kalıcı Ayrılış (Emeklilik, Nakil,İstifa,Görevlendirme Sonlandırılması vb.) vb.
        public bool KurumPersonelListesineDahilMi { get; set; } // Adliye Personel Listesinde Görünürlük Durumu 
        // Navigation property
        public virtual ICollection<Personel> Personels { get; set; } = new List<Personel>();
    }
}
