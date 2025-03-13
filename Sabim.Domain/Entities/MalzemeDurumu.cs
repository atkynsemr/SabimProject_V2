namespace Sabim.Domain.Entities
{
    public class MalzemeDurumu : BaseEntity
    {
        public byte MalzemeDurumuID { get; set; }
        public string MalzemeDurumuAdi { get; set; } // Kullanımda, Hurda, Arızalı, Parça Bekliyor, Çalışır Durumda, Bakımda
        public ICollection<Malzeme> Malzemes { get; set; }
    }
}
