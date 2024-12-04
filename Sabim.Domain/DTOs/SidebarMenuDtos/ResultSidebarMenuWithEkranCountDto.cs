namespace Sabim.Domain.DTOs.SidebarMenuDtos
{
    public record ResultSidebarMenuWithEkranCountDto :SidebarMenuBaseDto
    {
        public short SidebarMenuID { get; init; }
        public ushort EkranSayisi { get; init; }
    }
}
