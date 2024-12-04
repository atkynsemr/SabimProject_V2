namespace Sabim.Entities.Models
{
    public class RecordStatus
    {
        public int RecordStatusID { get; set; }
        public string RecordStatusName { get; set; }  // Aktif, Pasif, Askıya Alınmış, Silinmiş, Dondurulmuş, Kapatılmış vb.
        public bool IsActive { get; set; } = true;
    }
}
