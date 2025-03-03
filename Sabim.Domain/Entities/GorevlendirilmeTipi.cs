namespace Sabim.Domain.Entities
{
    public class GorevlendirilmeTipi : BaseEntity
    {
        public short GorevlendirilmeTipiID { get; set; }
        public string GorevlendirilmeTipiAciklama { get; set; } // Geçici Görevlendirme, Kalıcı Görevlendirme, Rotasyonel Görevlendirme, Yardımcı Görevlendirme, Eğitim Görevlendirmesi vb.
        // Navigation Property
        public ICollection<PersonelGorevlendirilme> PersonelGorevlendirilmes { get; set; } = new List<PersonelGorevlendirilme>();
        public ICollection<PersonelGeciciGorevlendirilme> PersonelGeciciGorevlendirilmes { get; set; } = new List<PersonelGeciciGorevlendirilme>();
    }
}
