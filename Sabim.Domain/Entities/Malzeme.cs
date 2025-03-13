namespace Sabim.Domain.Entities
{
    public class Malzeme : BaseEntity
    {
        public short MalzemeID { get; set; }
        public string SeriNumarasi { get; set; }
        public byte MalzemeModelId { get; set; }
        public byte MalzemeDurumuId { get; set; }
        public MalzemeModel MalzemeModel { get; set; }
        public MalzemeDurumu MalzemeDurumu { get; set; }
    }
}
