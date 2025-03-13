namespace Sabim.Domain.Entities
{
    public class MalzemeTuru :BaseEntity
    {
        public byte MalzemeTuruID { get; set; }
        public string TurAdi { get; set; } // Sarf Malzeme, Donanım Malzemesi vb.
        public ICollection<MalzemeCinsi> MalzemeCinsis { get; set; }
    }
}
