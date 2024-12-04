namespace Sabim.Domain.Entities
{
    public class Ekran :BaseEntity
    {
        public short EkranID { get; set; }
        public short SidebarMenuId { get; set; }
        public string EkranAdi { get; set; }
        public bool Varsayilan { get; set; } = false;
        //Navigation property
        public SidebarMenu SidebarMenu { get; set; }
        public virtual ICollection<AppRoleClaim> AppRoleClaims { get; set; }
    }
}
