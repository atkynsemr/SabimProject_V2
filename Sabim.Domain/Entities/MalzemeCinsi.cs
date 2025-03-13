namespace Sabim.Domain.Entities
{
    public class MalzemeCinsi :BaseEntity
    {
        public byte MalzemeCinsiID { get; set; }
        public string MalzemeCinsiAdi { get; set; } // Yazıcı, Tarayıcı, PC Monitörü, PC Kasası, Laptop, Yazıcı-Tarayıcı vb.
        public byte MalzemeTuruId { get; set; }
        public MalzemeTuru MalzemeTuru { get; set; }
        public ICollection<MalzemeModel> MalzemeModels { get; set; }
    }
}
