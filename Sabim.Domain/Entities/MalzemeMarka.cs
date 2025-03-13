namespace Sabim.Domain.Entities
{
    public class MalzemeMarka : BaseEntity
    {
        public byte MalzemeMarkaID { get; set; }
        public string MarkaAdi { get; set; }
        public ICollection<MalzemeModel> MalzemeModels{ get; set; }
    }
}
