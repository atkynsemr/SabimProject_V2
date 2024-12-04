namespace Sabim.Domain.Entities
{
    public class SidebarMenu : BaseEntity
    {
        public short SidebarMenuID { get; set; }
        public string SidebarMenuAdi { get; set; }
        //Navigation property
        public virtual ICollection<Ekran> Ekrans { get; set; } = new List<Ekran>();
    }
}
