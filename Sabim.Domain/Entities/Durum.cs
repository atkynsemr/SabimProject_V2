namespace Sabim.Domain.Entities
{
    public class Durum
    {
        public short DurumID { get; set; }
        public string DurumAdi { get; set; }  // Aktif, Pasif, Askıya Alınmış, Silinmiş, Dondurulmuş, Kapatılmış vb.
        public bool AktifMi { get; set; } 
    }
}
