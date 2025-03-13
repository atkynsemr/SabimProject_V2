namespace Sabim.Domain.Entities
{
    public class MalzemeModel : BaseEntity
    {
        public byte MalzemeModelID { get; set; }
        public string ModelAdi { get; set; }
        public byte MalzemeMarkaId { get; set; }
        public byte MalzemeCinsiId { get; set; }
        public MalzemeMarka MalzemeMarka { get; set; }
        public MalzemeCinsi MalzemeCinsi { get; set; }
        public ICollection<Malzeme> Malzemes { get; set; }
    }
}
