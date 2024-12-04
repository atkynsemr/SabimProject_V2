namespace Sabim.Domain.Entities
{
    public class Personel:BaseEntity
    {
        public short PersonelID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int SicilNumarasi { get; set; }
        public string? CepTelefonu { get; set; } = String.Empty;
        public short CinsiyetId {  get; set; } 
        public short KanGrubuId { get; set; } 
        public DateTime? DogumTarihi { get; set; }
        public string? DogumYeri { get; set; } = String.Empty;
        public byte? Derece {  get; set; }
        public byte? Kademe { get; set; }
        public DateTime? MeslegeGirisTarihi { get; set; }
        public DateTime? BuradaGoreveBaslamaTarihi { get; set; }
        public string? KimlikNo { get; set; } = String.Empty;
        public DateTime? BuradanAyrilmaTarihi { get; set; }
        public string? AracPlakasi { get; set; } = String.Empty;
        public short UnvanId { get; set; }
        public short GorevlendirilmeTuruId { get; set; }
        public short KurumId { get; set; } // Personelin Kadrosunun Bulunduğu Kurum
        public short KadroTuruId { get; set; }
        public short CalismaDurumuId { get; set; }
        // Navigation properties
        public Cinsiyet Cinsiyet {  get; set; }
        public KanGrubu KanGrubu { get; set; }
        public Unvan Unvan {  get; set; }
        public GorevlendirilmeTuru GorevlendirilmeTuru {  get; set; }
        public Kurum Kurum {  get; set; }
        public KadroTuru KadroTuru {  get; set; }
        public CalismaDurumu CalismaDurumu {  get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<PersonelGorevlendirilme> PersonelGorevlendirilmes { get; set; } = new List<PersonelGorevlendirilme>();
    }
}
