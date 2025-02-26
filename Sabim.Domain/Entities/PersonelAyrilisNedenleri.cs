namespace Sabim.Domain.Entities
{
    public class PersonelAyrilisNedenleri : BaseEntity
    {
        public byte PersonelAyrilisNedenleriID { get; set; }
        public string Aciklama { get; set; } = string.Empty;
        public bool KaliciAyrilisMi { get; set; }
        public bool DonanimUyarisi { get; set; }
        public List<PersonelAyrilis> PersonelAyriliss { get; set; } = new List<PersonelAyrilis>();
    }
}
